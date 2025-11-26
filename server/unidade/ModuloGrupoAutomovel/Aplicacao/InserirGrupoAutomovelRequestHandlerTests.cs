using FluentValidation;
using FluentValidation.Results;
using LocadoraDeVeiculos.Aplicacao.ModuloGrupoAutomovel.Commands.Inserir;
using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloAutenticacao;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomovel;
using Moq;

namespace LocadoraDeVeiculos.Testes.Unidade.ModuloGrupoAutomovel.Aplicacao;

[TestClass]
[TestCategory("Testes de Unidade")]
public class InserirGrupoAutomovelRequestHandlerTests
{
    private Mock<IContextoPersistencia> _contextoMock;
    private Mock<IRepositorioGrupoAutomovel> _repositorioGrupoAutomovelMock;
    private Mock<IValidator<GrupoAutomovel>> _validadorMock;
    private Mock<ITenantProvider> _tenantProviderMock;

    private InserirGrupoAutomovelRequestHandler _handler;

    [TestInitialize]
    public void Inicializar()
    {
        _contextoMock = new Mock<IContextoPersistencia>();
        _repositorioGrupoAutomovelMock = new Mock<IRepositorioGrupoAutomovel>();
        _validadorMock = new Mock<IValidator<GrupoAutomovel>>();
        _tenantProviderMock = new Mock<ITenantProvider>();

        _handler = new InserirGrupoAutomovelRequestHandler(
            _contextoMock.Object,
            _repositorioGrupoAutomovelMock.Object,
            _tenantProviderMock.Object,
            _validadorMock.Object
        );
    }

    [TestMethod]
    public async Task Deve_Inserir_GrupoAutomovel()
    {
        // Arrange
        var request = new InserirGrupoAutomovelRequest(
            "Moto"
        );

        _validadorMock.Setup(v => v.ValidateAsync(It.IsAny<GrupoAutomovel>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _repositorioGrupoAutomovelMock
            .Setup(r => r.SelecionarTodosAsync())
            .ReturnsAsync(new List<GrupoAutomovel>());

        _repositorioGrupoAutomovelMock
            .Setup(r => r.InserirAsync(It.IsAny<GrupoAutomovel>()))
            .ReturnsAsync(Guid.NewGuid());

        _contextoMock
            .Setup(c => c.GravarAsync())
            .ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(request, It.IsAny<CancellationToken>());

        // Assert
        _repositorioGrupoAutomovelMock.Verify(x => x.InserirAsync(It.IsAny<GrupoAutomovel>()), Times.Once);
        _contextoMock.Verify(x => x.GravarAsync(), Times.Once);

        Assert.IsTrue(result.IsSuccess);
    }
}