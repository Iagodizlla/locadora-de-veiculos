using FluentValidation.TestHelper;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomovel;

namespace LocadoraDeVeiculos.Testes.Unidade.ModuloGrupoAutomovel.Dominio;

[TestClass]
[TestCategory("Testes de Unidade")]
public class GrupoAutomovelTests
{
    private ValidadorGrupoAutomovel _validador;

    [TestInitialize]
    public void Inicializar()
    {
        _validador = new ValidadorGrupoAutomovel();
    }

    [TestMethod]
    public void Deve_Passar_Quando_Todas_Propriedades_Forem_Validas()
    {
        var grupo = new GrupoAutomovel("Moto");

        _validador.TestValidate(grupo).ShouldNotHaveAnyValidationErrors();
    }

    [TestMethod]
    public void Deve_Falhar_Quando_Nome_Estiver_Vazio()
    {
        var grupo = new GrupoAutomovel("");

        var result = _validador.TestValidate(grupo);

        result.ShouldHaveValidationErrorFor(p => p.Nome)
            .WithErrorMessage("O campo Nome é obrigatório");
    }

    [TestMethod]
    public void Deve_Falhar_Quando_Nome_For_Menor_Que_Tres_Caracteres()
    {
        var grupo = new GrupoAutomovel("Mo");

        var result = _validador.TestValidate(grupo);

        result.ShouldHaveValidationErrorFor(p => p.Nome)
            .WithErrorMessage("O campo Nome deve conter no mínimo 3 caracteres");
    }
}