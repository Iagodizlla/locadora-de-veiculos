using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloAutomovel;
using LocadoraDeVeiculos.Dominio.ModuloCondutor;
using LocadoraDeVeiculos.Dominio.ModuloPlano;
using LocadoraDeVeiculos.Dominio.ModuloTaxa;

namespace LocadoraDeVeiculos.Dominio.ModuloAluguel;

public class Aluguel : EntidadeBase
{
    public Condutor Condutor { get; set; }
    public Automovel Automovel { get; set; }
    public Plano Plano { get; set; }
    public List<Taxa> Taxas { get; set; }
    public DateTimeOffset DataSaida { get; set; }
    public DateTimeOffset DataRetornoPrevista { get; set; }
    public DateTimeOffset? DataDevolucao { get; set; }
    public int QuilometragemInicial { get; set; }
    public int? QuilometragemFinal { get; set; }
    public int NivelCombustivelNaSaida { get; set; }
    public int? NivelCombustivelNaDevolucao { get; set; }
    public bool? SeguroCliente { get; set; }
    public bool? SeguroTerceiro { get; set; }
    public double? ValorSeguroPorDia { get; set; }
    public bool Status { get; set; }
    public Guid AutomovelId { get; set; }
    public Guid CondutorId { get; set; }
    public Guid PlanoId { get; set; }

    public Aluguel() { }
    public Aluguel(Condutor condutor, Automovel automovel, Plano plano, List<Taxa> taxas, DateTimeOffset dataSaida, DateTimeOffset dataRetornoPrevista, int quilometragemInicial, int nivelCombustivelNaSaida, bool status) : this()
    {
        Condutor = condutor;
        Automovel = automovel;
        Plano = plano;
        Taxas = taxas;
        DataSaida = dataSaida;
        DataRetornoPrevista = dataRetornoPrevista;
        QuilometragemInicial = quilometragemInicial;
        NivelCombustivelNaSaida = nivelCombustivelNaSaida;
        Status = status;
    }
}
