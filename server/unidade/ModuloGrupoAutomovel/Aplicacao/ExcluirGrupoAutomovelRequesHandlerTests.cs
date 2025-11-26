using LocadoraDeVeiculos.Aplicacao.Compartilhado;
using LocadoraDeVeiculos.Aplicacao.ModuloGrupoAutomovel.Commands.Excluir;
using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloAutomovel;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomovel;
using LocadoraDeVeiculos.Dominio.ModuloPlano;
using Moq;

namespace LocadoraDeVeiculos.Testes.Unidade.ModuloGrupoAutomovel.Aplicacao;

[TestClass]
[TestCategory("Testes de Unidade")]
public class ExcluirGrupoAutomovelRequesHandlerTests
{
    private Mock<IContextoPersistencia> _contextoMock;
    private Mock<IRepositorioGrupoAutomovel> _repositorioGrupoAutomovelMock;
    private Mock<IRepositorioPlano> _repositorioPlanoMock;
    private Mock<IRepositorioAutomovel> _repositorioAutomovelMock;

    private ExcluirGrupoAutomovelRequestHandler _handler;

    [TestInitialize]
    public void Inicializar()
    {
        _contextoMock = new Mock<IContextoPersistencia>();
        _repositorioGrupoAutomovelMock = new Mock<IRepositorioGrupoAutomovel>();
        _repositorioPlanoMock = new Mock<IRepositorioPlano>();
        _repositorioAutomovelMock = new Mock<IRepositorioAutomovel>();

        _handler = new ExcluirGrupoAutomovelRequestHandler(
            _repositorioPlanoMock.Object,
            _repositorioAutomovelMock.Object,
            _repositorioGrupoAutomovelMock.Object,
            _contextoMock.Object
        );
    }

    [TestMethod]
    public async Task Deve_Excluir_GrupoAutomovel_Com_Sucesso()
    {
        // Arrange
        var request = new ExcluirGrupoAutomovelRequest(Guid.NewGuid());
        var medicoSelecionado = new GrupoAutomovel("Moto")
        {
            Id = request.Id
        };

        _repositorioGrupoAutomovelMock
            .Setup(r => r.SelecionarPorIdAsync(request.Id))
            .ReturnsAsync(medicoSelecionado);

        _repositorioGrupoAutomovelMock
            .Setup(r => r.ExcluirAsync(It.IsAny<GrupoAutomovel>()))
            .ReturnsAsync(true);

        _contextoMock
            .Setup(c => c.GravarAsync())
            .ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(request, It.IsAny<CancellationToken>());

        // Assert
        _repositorioGrupoAutomovelMock.Verify(r => r.ExcluirAsync(It.IsAny<GrupoAutomovel>()), Times.Once);
        _contextoMock.Verify(c => c.GravarAsync(), Times.Once);

        Assert.IsTrue(result.IsSuccess);
    }

    [TestMethod]
    public async Task Nao_Deve_Excluir_GrupoAutomovel_Se_Nao_Encontrado()
    {
        // Arrange
        var request = new ExcluirGrupoAutomovelRequest(Guid.NewGuid());

        _repositorioGrupoAutomovelMock
            .Setup(r => r.SelecionarPorIdAsync(request.Id))
            .ReturnsAsync((GrupoAutomovel)null);

        // Act
        var result = await _handler.Handle(request, It.IsAny<CancellationToken>());

        // Assert
        _repositorioGrupoAutomovelMock.Verify(r => r.ExcluirAsync(It.IsAny<GrupoAutomovel>()), Times.Never);
        _contextoMock.Verify(c => c.GravarAsync(), Times.Never);

        Assert.IsFalse(result.IsSuccess);

        var mensagemErroEsperada = ErrorResults.NotFoundError(request.Id).Message;
        Assert.AreEqual(mensagemErroEsperada, result.Errors.First().Message);
    }

    [TestMethod]
    public async Task Deve_Retornar_Erro_Se_Ocorreu_Excecao_Ao_Excluir()
    {
        // Arrange
        var request = new ExcluirGrupoAutomovelRequest(Guid.NewGuid());

        _repositorioGrupoAutomovelMock
            .Setup(r => r.SelecionarPorIdAsync(request.Id))
            .ReturnsAsync(new GrupoAutomovel("Moto")
            { Id = request.Id }
            );

        _repositorioGrupoAutomovelMock
            .Setup(r => r.ExcluirAsync(It.IsAny<GrupoAutomovel>()))
            .ThrowsAsync(new Exception("Erro ao excluir médico"));

        _contextoMock
            .Setup(c => c.RollbackAsync())
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(request, It.IsAny<CancellationToken>());

        // Assert
        _repositorioGrupoAutomovelMock.Verify(r => r.ExcluirAsync(It.IsAny<GrupoAutomovel>()), Times.Once);
        _contextoMock.Verify(c => c.RollbackAsync(), Times.Once);

        Assert.IsFalse(result.IsSuccess);

        var mensagemErroEsperada = ErrorResults.InternalServerError(new Exception()).Message;
        Assert.AreEqual(mensagemErroEsperada, result.Errors.First().Message);
    }
}