using FluentValidation.TestHelper;
using LocadoraDeVeiculos.Dominio.ModuloCliente;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomovel;

namespace LocadoraDeVeiculos.Testes.Unidade.ModuloCliente.Dominio;

[TestClass]
[TestCategory("Testes de Unidade")]
public class ClienteTests
{
    private ValidadorCliente _validador;

    [TestInitialize]
    public void Inicializar()
    {
        _validador = new ValidadorCliente();
    }

    [TestMethod]
    public void Deve_Passar_Quando_Todas_Propriedades_Forem_Validas()
    {
        var cliente = new Cliente("Iago", new Endereco("Jose de Alencar", 40, "Coral", "lages", "SC"),
            "49999015857", ETipoCliente.PessoaFisica, "12312312312", "");

        _validador.TestValidate(cliente).ShouldNotHaveAnyValidationErrors();
    }

    [TestMethod]
    public void Deve_Falhar_Quando_Nome_Estiver_Vazio()
    {
        var cliente = new Cliente("", new Endereco("Jose de Alencar", 40, "Coral", "lages", "SC"),
            "49999015857", ETipoCliente.PessoaFisica, "12312312312", "");

        var result = _validador.TestValidate(cliente);

        result.ShouldHaveValidationErrorFor(p => p.Nome)
            .WithErrorMessage("O campo Nome é obrigatório");
    }

    [TestMethod]
    public void Deve_Falhar_Quando_Nome_For_Menor_Que_Tres_Caracteres()
    {
        var cliente = new Cliente("Ia", new Endereco("Jose de Alencar", 40, "Coral", "lages", "SC"),
            "49999015857", ETipoCliente.PessoaFisica, "12312312312", "");

        var result = _validador.TestValidate(cliente);

        result.ShouldHaveValidationErrorFor(p => p.Nome)
            .WithErrorMessage("O campo Nome deve conter no mínimo 3 caracteres");
    }

    [TestMethod]
    public void Deve_Falhar_Quando_Endereco_Estiver_Vazio()
    {
        var cliente = new Cliente("Iago", null,
            "49999015857", ETipoCliente.PessoaFisica, "12312312312", "");

        var result = _validador.TestValidate(cliente);

        result.ShouldHaveValidationErrorFor(p => p.Endereco)
            .WithErrorMessage("O campo Endereco é obrigatório");
    }

    [TestMethod]
    public void Deve_Falhar_Quando_Telefone_Estiver_Vazio()
    {
        var cliente = new Cliente("Iago", new Endereco("Jose de Alencar", 40, "Coral", "lages", "SC"),
            "", ETipoCliente.PessoaFisica, "12312312312", "");

        var result = _validador.TestValidate(cliente);

        result.ShouldHaveValidationErrorFor(p => p.Telefone)
            .WithErrorMessage("O campo Telefone é obrigatório");
    }

    [TestMethod]
    public void Deve_Falhar_Quando_Telefone_For_Menor_Que_Oito_Caracteres()
    {
        var cliente = new Cliente("Iago", new Endereco("Jose de Alencar", 40, "Coral", "lages", "SC"),
            "4999901", ETipoCliente.PessoaFisica, "12312312312", "");

        var result = _validador.TestValidate(cliente);

        result.ShouldHaveValidationErrorFor(p => p.Telefone)
            .WithErrorMessage("O campo Telefone deve conter no mínimo 8 caracteres");
    }
}