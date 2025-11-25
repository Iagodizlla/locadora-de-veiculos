using LocadoraDeVeiculos.Dominio.Compartilhado;

namespace LocadoraDeVeiculos.Dominio.ModuloConfig;

public class Config : EntidadeBase
{
    public double Gasolina {  get; set; }
    public double Gas {  get; set; }
    public double Diessel {  get; set; }
    public double Alcool { get; set; }

    public Config() { }
    public Config(double gasolina, double gas, double diessel, double alcool) : this()
    {
        Gasolina = gasolina;
        Gas = gas;
        Diessel = diessel;
        Alcool = alcool;
    }
}
