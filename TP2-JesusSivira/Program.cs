using System;
using System.Collections.Generic;
using System.IO;

namespace TP2_JesusSivira
{
    class Program
    {
        static void Main(string[] args)
        {
            // Creamos un diccionario para pedir los test por pantalla, recomendado usar el enunciado del ejercicio
            Dictionary<String, decimal> tests = new Dictionary<string, decimal>();

            string ultimosDigitosTest = "11111";
            decimal saldoTest = 0;

            while (ultimosDigitosTest != "0000")
            {
                Console.WriteLine("Ingrese los ultimos 4 digitos de la tarjeta a registrar: \n");
                Console.WriteLine("\n Para dejar de cargar ingrese 0000. ");
                ultimosDigitosTest = Console.ReadLine();

                if (ultimosDigitosTest != "0000")
                {
                    Console.WriteLine("Ingrese el salgo de la tarjeta: \n");
                    saldoTest = Convert.ToDecimal(Console.ReadLine());
                }
                
                tests.Add(ultimosDigitosTest, saldoTest);

            }

            // Creamos un diccionario que tenga como key el numero de tarjeta y valor el monto

            Dictionary<String, decimal> tarjetas = new Dictionary<string, decimal>();
            using (StreamReader lector = new StreamReader(@"C:\Textos\tarjetas.txt"))
            {
                string registro = lector.ReadLine();

                // Recorremos la primera linea del archivo solamente 
                int posicion = registro.IndexOf(';');
                string columna1 = registro.Substring(0, posicion);
                registro = registro.Substring(posicion + 1);

                posicion = registro.IndexOf(';');
                string columna2 = registro.Substring(0, posicion);
                registro = registro.Substring(posicion + 1);

                string columna3 = registro;

                registro = lector.ReadLine();

                // Recorremos cada linea del archivo y la agregamos a su diccionario

                while (registro != null)
                {
                    posicion = registro.IndexOf(';');
                    string ultimosDigitos = registro.Substring(0, posicion);
                    registro = registro.Substring(posicion + 1);

                    posicion = registro.IndexOf(';');
                    string comercio = registro.Substring(0, posicion);
                    registro = registro.Substring(posicion + 1);

                    decimal saldo = Convert.ToDecimal(registro);

                    tarjetas.Add(ultimosDigitos, saldo);
                    registro = lector.ReadLine();
                }
                lector.Close();

                //Escribimos el archivo consolidado.txt

                using (StreamWriter escribir = new StreamWriter(@"C:\Textos\Consolidado.txt"))
                {

                    // Escribimos el encabezado
                    string encabezado = "NroTarjeta;SaldoConsolidado;Diferencia";

                    escribir.WriteLine(encabezado);

                    //Recorremos los diccionarios para encontrar las tarjetas iguales

                    foreach (KeyValuePair<string, decimal> tarjeta in tarjetas)
                    {
                        foreach (KeyValuePair<string, decimal> test in tests)
                        {
                            if (tarjeta.Key == test.Key)
                            {
                                if (tarjeta.Value == test.Value)
                                {
                                    escribir.WriteLine(tarjeta.Key + ";" + "SI" + ";" + (tarjeta.Value - test.Value));
                                }
                                else
                                {
                                    escribir.WriteLine(tarjeta.Key + ";" + "NO" + ";" + (tarjeta.Value - test.Value));
                                }
                            }
                        }
                    }

                }

                Console.ReadKey();
            }
                
        }
    }
}
