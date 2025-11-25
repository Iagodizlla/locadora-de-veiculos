using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloAutomovel;
using LocadoraDeVeiculos.Dominio.ModuloCliente;
using LocadoraDeVeiculos.Dominio.ModuloCondutor;
using LocadoraDeVeiculos.Dominio.ModuloPlano;
using LocadoraDeVeiculos.Dominio.ModuloTaxa;

namespace LocadoraDeVeiculos.Dominio.ModuloAluguel;

public class Aluguel : EntidadeBase
{
    #region 
    #region Entidades
    public Condutor Condutor { get; set; }
    public Automovel Automovel { get; set; }
    public Plano Plano { get; set; }
    public Cliente Cliente { get; set; }
    public List<Taxa> Taxas { get; set; }
    public Guid AutomovelId { get; set; }
    public Guid CondutorId { get; set; }
    public Guid PlanoId { get; set; }
    public Guid ClienteId { get; set; }
    #endregion
    #region Datas
    public DateTimeOffset DataSaida { get; set; }
    public DateTimeOffset DataRetornoPrevista { get; set; }
    public DateTimeOffset? DataDevolucao { get; set; }
    #endregion
    #region Km e Combustivel
    public int QuilometragemInicial { get; set; }
    public int? QuilometragemFinal { get; set; }
    public int NivelCombustivelNaSaida { get; set; }
    public int? NivelCombustivelNaDevolucao { get; set; }
    #endregion
    #region Seguros
    public bool SeguroCliente { get; set; }
    public bool SeguroTerceiro { get; set; }
    public double? ValorSeguroPorDia { get; set; }
    #endregion
    public bool Status { get; set; } = false;
    #endregion
    public Aluguel() { }
    public Aluguel(Cliente cliente, Condutor condutor, Automovel automovel, Plano plano, List<Taxa> taxas, DateTimeOffset dataSaida, DateTimeOffset dataRetornoPrevista,
        DateTimeOffset? dataDevolucao, int quilometragemInicial, int? quilometragemFinal, int nivelCombustivelNaSaida, int? nivelCombustivelNaDevolucao,
        bool seguroCliente, bool seguroTerceiro, double? valorSeguroPorDia, bool status) : this()
    {
        Cliente = cliente;
        Condutor = condutor;
        Automovel = automovel;
        Plano = plano;
        Taxas = taxas;
        DataSaida = dataSaida;
        DataRetornoPrevista = dataRetornoPrevista;
        DataDevolucao = dataDevolucao;
        QuilometragemInicial = quilometragemInicial;
        QuilometragemFinal = quilometragemFinal;
        NivelCombustivelNaSaida = nivelCombustivelNaSaida;
        NivelCombustivelNaDevolucao = nivelCombustivelNaDevolucao;
        SeguroCliente = seguroCliente;
        SeguroTerceiro = seguroTerceiro;
        ValorSeguroPorDia = valorSeguroPorDia;
        Status = status;
    }
}
