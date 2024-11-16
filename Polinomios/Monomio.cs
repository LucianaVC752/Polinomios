namespace Polinomios
{
    public class Monomio
    {
        public double Coeficiente;
        public int Exponente;
        public Monomio Siguiente;


        public Monomio(double Coeficiente, int Exponente)
        {
            this.Exponente = Exponente;
            this.Coeficiente= Coeficiente;
        }
    }

    public class ResultadoDivision
    {
        public Polinomio Cociente;
        public Polinomio Residuo;

        public ResultadoDivision()
        {
            Cociente = new Polinomio();
            Residuo = new Polinomio();
        }
    }

}
