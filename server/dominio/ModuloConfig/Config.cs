using LocadoraDeVeiculos.Dominio.Compartilhado;

namespace LocadoraDeVeiculos.Dominio.ModuloConfig;

public class Config : EntidadeBase
{
    public decimal Gasolina { get; set; }
    public decimal Gas { get; set; }
    public decimal Diesel { get; set; }
    public decimal Alcool { get; set; }

    public Config() { }
    public Config(decimal gasolina, decimal gas, decimal diesel, decimal alcool) : this()
    {
        Gasolina = gasolina;
        Gas = gas;
        Diesel = diesel;
        Alcool = alcool;
    }
}
