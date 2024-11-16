using System.Runtime.CompilerServices;

namespace Polinomios
{
    public class Polinomio
    {
        private Monomio Cabeza;


        public Polinomio()
        {
            Cabeza = null;
        }

        public Monomio GetCabeza()
        {
            return Cabeza;
        }

        public void Agregar(Monomio monomio)
        {
            if (monomio != null)
            {
                if (Cabeza == null)
                {
                    Cabeza = monomio;
                }
                else
                {
                    Monomio apuntador = Cabeza;
                    Monomio predecesor = null;
                    int encontrado = 0;
                    while (apuntador != null && encontrado == 0)
                    {
                        if (monomio.Exponente == apuntador.Exponente)
                        {
                            encontrado = 1;
                        }
                        else if (monomio.Exponente < apuntador.Exponente)
                        {
                            encontrado = 2;
                        }
                        else
                        {
                            predecesor = apuntador;
                            apuntador = apuntador.Siguiente;
                        }
                    }
                    if (encontrado == 1)
                    {
                        double coeficiente = monomio.Coeficiente + apuntador.Coeficiente;
                        if (coeficiente == 0)
                        {
                            if (predecesor == null)
                            {
                                Cabeza = apuntador.Siguiente;
                            }
                            else
                            {
                                predecesor.Siguiente = apuntador.Siguiente;
                            }
                        }
                        else
                        {
                            apuntador.Coeficiente = coeficiente;
                        }
                    }
                    else
                    {
                        Insertar(monomio, predecesor);
                    }
                }
            }
        }

        public void Insertar(Monomio monomio, Monomio predecesor)
        {
            if (monomio != null)
            {
                if (predecesor == null)
                {
                    monomio.Siguiente = Cabeza;
                    Cabeza = monomio;
                }
                else
                {
                    monomio.Siguiente = predecesor.Siguiente;
                    predecesor.Siguiente = monomio;
                }
            }
        }


        private String[] ObtenerTextos()
        {
            String[] textos = new String[2];
            textos[0] = "";
            textos[1] = "";

            Monomio apuntador = Cabeza;
            while (apuntador != null)
            {
                string texto = apuntador.Coeficiente.ToString()+ " ";
                if (apuntador.Exponente != 0)
                {
                    texto += "X";
                }
                if (apuntador.Coeficiente >= 0)
                {
                    texto = "+" + texto;
                }
                textos[0] += new string(' ', texto.Length);
                textos[1] += texto;

                if (apuntador.Exponente != 0 && apuntador.Exponente != 1)
                {
                    texto = apuntador.Exponente.ToString();
                    textos[0] += texto;
                    textos[1] += new string(' ', texto.Length);
                }
                apuntador = apuntador.Siguiente;
            }


            return textos;
        }

        public void Mostrar(Label lbl)
        {
            String[] textos = ObtenerTextos();
            lbl.Font = new System.Drawing.Font("Courier New", 12);
            lbl.Text = textos[0] + "\n" + textos[1];
        }

        //********** Métodos estaticos **********

        public static Polinomio Sumar(Polinomio p1, Polinomio p2)
        {
            Polinomio pR = new Polinomio();
            Monomio apuntador1 = p1.GetCabeza();
            Monomio apuntador2 = p2.GetCabeza();

            Monomio monomio;
            while (apuntador1 != null || apuntador2 != null)
            {
                monomio = null;
                if (apuntador1 != null && apuntador2 != null &&
                    apuntador1.Exponente == apuntador2.Exponente)
                {
                    if (apuntador1.Coeficiente + apuntador2.Coeficiente != 0)
                        monomio = new Monomio(apuntador1.Coeficiente + apuntador2.Coeficiente, apuntador1.Exponente);
                    apuntador1 = apuntador1.Siguiente;
                    apuntador2 = apuntador2.Siguiente;
                }
                else if(apuntador1 != null && 
                    (apuntador2==null || apuntador1.Exponente<apuntador2.Exponente))
                {
                    monomio = new Monomio(apuntador1.Coeficiente, apuntador1.Exponente);
                    apuntador1 = apuntador1.Siguiente;
                }
                else
                {
                    monomio = new Monomio(apuntador2.Coeficiente, apuntador2.Exponente);
                    apuntador2 = apuntador2.Siguiente;                }

                if(monomio != null)
                {
                    pR.Agregar(monomio);
                }
            }

            return pR;
        }

        public static Polinomio Restar(Polinomio p1, Polinomio p2)
        {
            Polinomio pR = new Polinomio();
            Monomio apuntador1 = p1.GetCabeza();
            Monomio apuntador2 = p2.GetCabeza();

            Monomio monomio;
            while (apuntador1 != null || apuntador2 != null)
            {
                monomio = null;
                if (apuntador1 != null && apuntador2 != null &&
                    apuntador1.Exponente == apuntador2.Exponente)
                {
                    if (apuntador1.Coeficiente - apuntador2.Coeficiente != 0)
                        monomio = new Monomio(apuntador1.Coeficiente - apuntador2.Coeficiente, apuntador1.Exponente);
                    apuntador1 = apuntador1.Siguiente;
                    apuntador2 = apuntador2.Siguiente;
                }
                else if (apuntador1 != null &&
                    (apuntador2 == null || apuntador1.Exponente < apuntador2.Exponente))
                {
                    monomio = new Monomio(apuntador1.Coeficiente, apuntador1.Exponente);
                    apuntador1 = apuntador1.Siguiente;
                }
                else
                {
                    monomio = new Monomio(-apuntador2.Coeficiente, apuntador2.Exponente);
                    apuntador2 = apuntador2.Siguiente;
                }

                if (monomio != null)
                {
                    pR.Agregar(monomio);
                }
            }

            return pR;
        }

        public static Polinomio Multiplicar1(Polinomio p1, Polinomio p2)
        {
            Polinomio pR = new Polinomio(); 

            Monomio apuntador1 = p1.GetCabeza(); 

            while (apuntador1 != null)
            {
                Monomio apuntador2 = p2.GetCabeza(); 
                
                while (apuntador2 != null)
                {
                    
                    double coeficiente = apuntador1.Coeficiente * apuntador2.Coeficiente;
                    int exponente = apuntador1.Exponente + apuntador2.Exponente;

                    pR.Agregar(new Monomio(coeficiente, exponente));

                    apuntador2 = apuntador2.Siguiente;
                }
                apuntador1 = apuntador1.Siguiente;
            }

            return pR; 
        }

        public static Polinomio Multiplicar(Polinomio p1, Polinomio p2)
        {
            Polinomio pR = new Polinomio();

            Monomio m1 = p1.GetCabeza();
            while (m1 != null)
            {
                Monomio m2 = p2.GetCabeza();
                while (m2 != null)
                {
                    double nuevoCoef = m1.Coeficiente * m2.Coeficiente;
                    int nuevoExp = m1.Exponente + m2.Exponente;
                    pR.Agregar(new Monomio(nuevoCoef, nuevoExp));

                    m2 = m2.Siguiente;
                }
                m1 = m1.Siguiente;
            }

            return pR;
        }




        public static ResultadoDivision Dividir(Polinomio dividendo, Polinomio divisor)
        {
            //El programa divide polinomios simples o mas bien con divisores simples
            //no puede haber más de una x porque se petatea :c
            Polinomio cociente = new Polinomio();
            Polinomio residuo = new Polinomio();
            residuo = dividendo; 

            while (residuo.GetCabeza() != null && residuo.GetCabeza().Exponente >= divisor.GetCabeza().Exponente)
            {
                
                double coeficienteCociente = residuo.GetCabeza().Coeficiente / divisor.GetCabeza().Coeficiente;
                int exponenteCociente = residuo.GetCabeza().Exponente - divisor.GetCabeza().Exponente;

                
                Monomio monomioCociente = new Monomio(coeficienteCociente, exponenteCociente);
                cociente.Agregar(monomioCociente);

                Polinomio multiplicacion = new Polinomio();
                multiplicarPorMonomio(divisor, monomioCociente, ref multiplicacion);

                residuo = Restar(residuo, multiplicacion);
                
            }

            // Devuelve el cociente y el residuo
            return new ResultadoDivision { Cociente = cociente, Residuo = residuo };
        }
        public static void multiplicarPorMonomio(Polinomio polinomio, Monomio monomio, ref Polinomio resultado)
        {
            Monomio actual = polinomio.GetCabeza();
            while (actual != null)
            {
                double coeficienteProducto = actual.Coeficiente * monomio.Coeficiente;
                int exponenteProducto = actual.Exponente + monomio.Exponente;
                Monomio producto = new Monomio(coeficienteProducto, exponenteProducto);
                resultado.Agregar(producto);
                actual = actual.Siguiente;
            }
        }


        // Derivar solo agarra el primer polinomio
        public static Polinomio Derivar(Polinomio p1)
        {
            Polinomio derivada = new Polinomio();
            Monomio apuntador1 = p1.GetCabeza();
            while (apuntador1 != null)
            {
                if (apuntador1.Exponente != 0)
                {
                    double nuevoCoeficiente = apuntador1.Coeficiente * apuntador1.Exponente;
                    int nuevoExponente = apuntador1.Exponente - 1;
                    Monomio terminoDerivado = new Monomio(nuevoCoeficiente, nuevoExponente);
                    derivada.Agregar(terminoDerivado);
                }
                apuntador1 = apuntador1.Siguiente;
            }
            return derivada;
        }
    }
}
