using FluentValidation;
using FluentValidation.Results;
using LocadoraDeVeiculos.Aplicacao.Compartilhado;
using LocadoraDeVeiculos.Aplicacao.ModuloGrupoAutomovel.Commands.Editar;
using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomovel;
using Moq;

namespace LocadoraDeVeiculos.Testes.Unidade.ModuloGrupoAutomovel.Aplicacao;

[TestClass]
[TestCategory("Testes de Unidade")]
public class EditarGrupoAutomovelRequestHandlerTests
{
    private Mock<IContextoPersistencia> _contextoMock;
    private Mock<IRepositorioGrupoAutomovel> _repositorioGrupoAutomovelMock;
    private Mock<IValidator<GrupoAutomovel>> _validador;

    private EditarGrupoAutomovelRequestHandler _handler;

    [TestInitialize]
    public void Inicializar()
    {
        _contextoMock = new Mock<IContextoPersistencia>();
        _repositorioGrupoAutomovelMock = new Mock<IRepositorioGrupoAutomovel>();
        _validador = new Mock<IValidator<GrupoAutomovel>>();

        _handler = new EditarGrupoAutomovelRequestHandler(
            _repositorioGrupoAutomovelMock.Object,
            _contextoMock.Object,
            _validador.Object
        );
    }

    [TestMethod]
    public async Task Deve_Editar_GrupoAutomovel_Com_Sucesso()
    {
        // Arrange
        var request = new EditarGrupoAutomovelRequest(Guid.NewGuid(), "Moto"
        );

        _repositorioGrupoAutomovelMock
            .Setup(r => r.SelecionarPorIdAsync(request.Id))
            .ReturnsAsync(new GrupoAutomovel("Carro")
            { Id = request.Id }
            );

        _validador
            .Setup(v => v.ValidateAsync(It.IsAny<GrupoAutomovel>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _repositorioGrupoAutomovelMock
            .Setup(r => r.SelecionarTodosAsync())
            .ReturnsAsync(new List<GrupoAutomovel>());

        _repositorioGrupoAutomovelMock
            .Setup(r => r.EditarAsync(It.IsAny<GrupoAutomovel>()))
            .ReturnsAsync(true);

        _contextoMock
            .Setup(c => c.GravarAsync())
            .ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(request, It.IsAny<CancellationToken>());

        // Assert
        _repositorioGrupoAutomovelMock.Verify(r => r.EditarAsync(It.IsAny<GrupoAutomovel>()), Times.Once);
        _contextoMock.Verify(c => c.GravarAsync(), Times.Once);

        Assert.IsTrue(result.IsSuccess);
    }

    [TestMethod]
    public async Task Nao_Deve_Editar_GrupoAutomovel_Se_Nao_Encontrado()
    {
        // Arrange
        var request = new EditarGrupoAutomovelRequest(Guid.NewGuid(), "Moto"
        );

        _repositorioGrupoAutomovelMock
            .Setup(r => r.SelecionarPorIdAsync(request.Id))
            .ReturnsAsync((GrupoAutomovel)null);

        // Act
        var result = await _handler.Handle(request, It.IsAny<CancellationToken>());

        // Assert
        _repositorioGrupoAutomovelMock.Verify(r => r.EditarAsync(It.IsAny<GrupoAutomovel>()), Times.Never);
        _contextoMock.Verify(c => c.GravarAsync(), Times.Never);

        Assert.IsFalse(result.IsSuccess);

        var mensagemErroEsperada = ErrorResults.NotFoundError(request.Id).Message;
        Assert.AreEqual(mensagemErroEsperada, result.Errors.First().Message);
    }
}