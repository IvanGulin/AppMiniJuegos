using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trivial
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            List<Pregunta> preguntas = new List<Pregunta>();

            // Pregunta 1
            Pregunta pregunta1 = new Pregunta("Capital de Francia", new List<string> { "Paris", "Atenas", "Madrid", "Berlin" }, 0);
            preguntas.Add(pregunta1);

            // Pregunta 2
            Pregunta pregunta2 = new Pregunta("Año del descubrimiento de America", new List<string> { "1958", "1948", "1498", "1492" }, 3);
            preguntas.Add(pregunta2);

            // Pregunta 3
            Pregunta pregunta3 = new Pregunta("Artista con el album de musica mas vendido de la historia", new List<string> { "Queen", "ABBA", "Michael Jackson", "AC/DC" }, 2);
            preguntas.Add(pregunta3);


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(preguntas));
        }
    }
}
