using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trivial
{
    public class Pregunta
    {
        public string Titulo { get; set; }
        public List<string> Opciones { get; set; }
        public int Correcta { get; set; }

        public Pregunta(string titulo, List<string> opciones, int correcta)
        {
            Titulo = titulo;
            Opciones = opciones;
            Correcta = correcta;
        }
    }


    public class Palabra
    {
        public string palabra { get; set; }
        public string pista { get; set; }

        public Palabra(string palabra, string pista)
        {
            this.palabra = palabra;
            this.pista = pista;
        }
    }
}
