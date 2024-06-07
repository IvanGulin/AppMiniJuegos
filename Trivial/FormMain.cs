using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Trivial
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

            MaximizedBounds = Screen.FromHandle(Handle).WorkingArea;

            Guna2PictureBox[] gunaPictureBoxes = { guna2PictureBox1, guna2PictureBox2, guna2PictureBox3 };

            // Suscribir los eventos MouseEnter y MouseLeave para cada Guna2PictureBox
            foreach (var pictureBox in gunaPictureBoxes)
            {
                pictureBox.MouseEnter += new EventHandler(GunaPictureBox_MouseEnter);
                pictureBox.MouseLeave += new EventHandler(GunaPictureBox_MouseLeave);
            }
        }

        //  Juego Preguntas.
        private void button1_Click_1(object sender, EventArgs e)
        {
            Form1 formPreguntas = new Form1(Preguntas());
            formPreguntas.ShowDialog();
        }

        // Juego Wordle.
        private void button2_Click(object sender, EventArgs e)
        {
            Form3 wordle = new Form3(Wordle());
            wordle.ShowDialog();
        }

        // Juego Ahorcado.
        private void button3_Click(object sender, EventArgs e)
        {
            Form2 formAhorcado = new Form2(Palabras());
            formAhorcado.ShowDialog();
        }

        // Juego 4
        private void button4_Click(object sender, EventArgs e)
        {
            Form4 queSera = new Form4(QueSera());
            queSera.ShowDialog();
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

        private List<Palabra> Palabras()
        {
            List<Palabra> palabras = new List<Palabra>();

            string[] palabra = {
                "PERSONA",
                "AGUACATE",
                "TECLADO",
                "ESTUDIAR",
                "TRABAJAR",
                "CANCIONES",
                "JUGAR",
                "PROGRAMACION",
                "ALEMANIA"
            };

            string[] pista = {
                "Tu y yo lo somos",
                "Mitad agua",
                "Delante tuya",
                "Lo que no hace la mayoría",
                "Lo que le vendria bien a más de uno",
                "Música",
                "Lo que se hace por las noches",
                "Con lo que se ha hecho esta app",
                "País"
            };

            // Añadir palabras a la lista
            for (int i = 0; i < palabra.Length; i++)
            {
                Palabra palabraLista = new Palabra(palabra[i], pista[i]);
                palabras.Add(palabraLista);
            }

            return palabras;
        }

        private List<Palabra> Wordle()
        {
            List<Palabra> wordle = new List<Palabra>();

            string[] palabra = {
                "PERSONA",
                "AGUACATE",
                "TECLADO",
                "ESTUDIAR",
                "TRABAJAR",
                "CANCION",
                "JUGAR",
                "PROGRAMA",
                "ALEMANIA"
            };

            // Añadir palabras a la lista
            for (int i = 0; i < palabra.Length; i++)
            {
                Palabra p = new Palabra(palabra[i]);
                wordle.Add(p);
            }

            return wordle;
        }

        private Dictionary<String, List<String>> QueSera()
        {
            Dictionary<String, List<String>> palabrasPistas = new Dictionary<String, List<String>>();

            // Agregar palabras y sus pistas
            palabrasPistas.Add("PELOTA", new List<string> { "Se usa en deportes", "Es redonda", "Puede rebotar", "Usada en fútbol y baloncesto" });
            palabrasPistas.Add("BOTELLA", new List<string> { "Contiene líquidos", "Tiene una tapa", "Puede ser de vidrio o plástico", "Puede ser reciclable" });
            palabrasPistas.Add("TECLADO", new List<string> { "Se usa con computadoras", "Tiene letras y números", "Permite escribir", "Conectado por USB o inalámbrico" });
            palabrasPistas.Add("SOFÁ", new List<string> { "Mueble para sentarse", "Generalmente en la sala", "Puede ser de varios tamaños", "Puede ser reclinable" });
            palabrasPistas.Add("CAMA", new List<string> { "Mueble para dormir", "Tiene colchón", "Se encuentra en el dormitorio", "Puede ser individual o matrimonial" });

            return palabrasPistas;
        }

        

        #region Botones (Cerrar, maximizar, minimizar)
        private void guna2PictureBox1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal) WindowState = FormWindowState.Maximized;
            else WindowState = FormWindowState.Normal;
        }

        private void guna2PictureBox3_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        // Manejador del evento MouseEnter
        private void GunaPictureBox_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Guna2PictureBox pictureBox)
            {
                pictureBox.BackColor = Color.Purple; // Cambiar al color deseado
            }
        }

        // Manejador del evento MouseLeave
        private void GunaPictureBox_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Guna2PictureBox pictureBox)
            {
                pictureBox.BackColor = Color.Transparent; // Restaurar al color original o a otro color deseado
            }
        }
        #endregion
    }
}
