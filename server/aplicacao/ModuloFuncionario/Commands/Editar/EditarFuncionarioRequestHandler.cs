using FluentResults;
using FluentValidation;
using LocadoraDeVeiculos.Aplicacao.Compartilhado;
using LocadoraDeVeiculos.Aplicacao.ModuloFuncionario;
using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloFuncionario;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloFuncionario.Commands.Editar;

public class EditarFuncionarioRequestHandler(
    IRepositorioFuncionario repositorioFuncionario,
    IContextoPersistencia contexto,
    IValidator<Funcionario> validador
) : IRequestHandler<EditarFuncionarioRequest, Result<EditarFuncionarioResponse>>
{
    public async Task<Result<EditarFuncionarioResponse>> Handle(EditarFuncionarioRequest request, CancellationToken cancellationToken)
    {
        var funcionarioSelecionado = await repositorioFuncionario.SelecionarPorIdAsync(request.Id);

        if (funcionarioSelecionado == null)
            return Result.Fail(ErrorResults.NotFoundError(request.Id));

        funcionarioSelecionado.Usuario.UserName = request.Username;
        funcionarioSelecionado.Salario = request.Salario;
        funcionarioSelecionado.Admissao = request.Admissao;

        var resultadoValidacao = 
            await validador.ValidateAsync(funcionarioSelecionado, cancellationToken);
        
        if (!resultadoValidacao.IsValid)
        {
            var erros = resultadoValidacao.Errors
                .Select(failure => failure.ErrorMessage)
                .ToList();

            return Result.Fail(ErrorResults.BadRequestError(erros));
        }

        var funcionarios = await repositorioFuncionario.SelecionarTodosAsync();

        if (NomeDuplicado(funcionarioSelecionado, funcionarios))
            return Result.Fail(FuncionarioErrorResults.NomeDuplicadoError(funcionarioSelecionado.Usuario.UserName));
        
        try
        {
            await repositorioFuncionario.EditarAsync(funcionarioSelecionado);

            await contexto.GravarAsync();
        }
        catch (Exception ex)
        {
            await contexto.RollbackAsync();
            
            return Result.Fail(ErrorResults.InternalServerError(ex));
        }
 
        return Result.Ok(new EditarFuncionarioResponse(funcionarioSelecionado.Id));
    }
    
    private bool NomeDuplicado(Funcionario funcionario, IList<Funcionario> funcionarios)
    {
        return funcionarios
            .Where(r => r.Id != funcionario.Id)
            .Any(registro => string.Equals(
                registro.Usuario.UserName,
                funcionario.Usuario.UserName,
                StringComparison.CurrentCultureIgnoreCase)
            );
    }
}