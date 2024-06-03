using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Trivial
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1(Preguntas());
            form.ShowDialog();
        }

        private List<Pregunta> Preguntas()
        {
            List<Pregunta> preguntas = new List<Pregunta>();

            string[] titulos = {
            "Capital de Francia",
            "Año del descubrimiento de America",
            "Artista con el album de musica mas vendido de la historia",
            "Elemento quimico con el simbolo 'O'",
            "Pais más grande del mundo por area",
            "Planeta más cercano al Sol",
            "El autor de 'Cien años de soledad'",
            "El oceano más grande del mundo",
            "La ciudad con más habitantes en el mundo",
            "La montaña más alta del mundo",
            "Creador de Facebook",
            "Primera mujer en ganar un Premio Nobel",
            "El rio más largo del mundo",
            "El pintor de 'La ultima cena'",
            "La obra más famosa de Shakespeare",
            "El metal más abundante en la corteza terrestre",
            "El descubridor de la penicilina",
            "El país con más medallas olímpicas de todos los tiempos",
            "La capital de Australia",
            "La primera computadora electrónica",
            "El inventor del telefono",
            "La novela más vendida de todos los tiempos",
            "El país con la mayor producción de vino",
            "La ciudad sede de los Juegos Olímpicos 2008"
            };

            List<string>[] opciones = {
            new List<string> { "Paris", "Atenas", "Madrid", "Berlin" },
            new List<string> { "1958", "1948", "1498", "1492" },
            new List<string> { "Queen", "ABBA", "Michael Jackson", "AC/DC" },
            new List<string> { "Oxigeno", "Oro", "Osmio", "Oganesón" },
            new List<string> { "China", "Estados Unidos", "Rusia", "Canada" },
            new List<string> { "Mercurio", "Venus", "Tierra", "Marte" },
            new List<string> { "Gabriel Garcia Marquez", "Pablo Neruda", "Julio Cortazar", "Jorge Luis Borges" },
            new List<string> { "Atlantico", "Indico", "Artico", "Pacifico" },
            new List<string> { "Nueva York", "Tokio", "Shanghai", "Mexico D.F." },
            new List<string> { "K2", "Kangchenjunga", "Lhotse", "Everest" },
            new List<string> { "Steve Jobs", "Elon Musk", "Bill Gates", "Mark Zuckerberg" },
            new List<string> { "Rosalind Franklin", "Marie Curie", "Ada Lovelace", "Florence Nightingale" },
            new List<string> { "Amazonas", "Nilo", "Yangtze", "Mississippi" },
            new List<string> { "Vincent van Gogh", "Pablo Picasso", "Leonardo da Vinci", "Claude Monet" },
            new List<string> { "Macbeth", "Romeo y Julieta", "Hamlet", "Othello" },
            new List<string> { "Hierro", "Cobre", "Aluminio", "Calcio" },
            new List<string> { "Louis Pasteur", "Alexander Fleming", "Gregor Mendel", "Robert Koch" },
            new List<string> { "China", "Rusia", "Estados Unidos", "Alemania" },
            new List<string> { "Sydney", "Melbourne", "Canberra", "Brisbane" },
            new List<string> { "UNIVAC", "IBM 701", "ENIAC", "Apple I" },
            new List<string> { "Thomas Edison", "Nikola Tesla", "Alexander Graham Bell", "Guglielmo Marconi" },
            new List<string> { "Don Quijote de la Mancha", "Harry Potter y la piedra filosofal", "El Hobbit", "Matar a un ruiseñor" },
            new List<string> { "Francia", "Italia", "España", "Estados Unidos" },
            new List<string> { "Londres", "Beijing", "Atenas", "Sídney" }
            };

            int[] correctas = {
            0, 3, 2, 0, 2, 0, 0, 3, 1, 3, 3, 1, 1, 2, 1, 2, 1, 2, 2, 2, 2, 0, 1, 1
            };

            // Añadir preguntas a la lista
            for (int i = 0; i < titulos.Length; i++)
            {
                Pregunta pregunta = new Pregunta(titulos[i], opciones[i], correctas[i]);
                preguntas.Add(pregunta);
            }

            return preguntas;
        }
    }
}
