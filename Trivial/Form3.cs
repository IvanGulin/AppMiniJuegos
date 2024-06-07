using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Trivial
{
    public partial class Form3 : Form
    {
        private List<Palabra> palabras; // Lista de palabras seleccionables para el juego.
        private List<List<Button>> intentos; // Lista de listas de los botones que contiene cada linea de la palabra en el GUI.
        private List<List<char>> letras; // Listas de listas de char de las letras de la palabra.
        private string palabraActual, palabraIntroducida; // Palabra que se ha seleccionada en el juego  /  Palabra que se ha introcido en cada intento.
        private byte numIntento = 0; //  Número del intento actual.

        public Form3(List<Palabra> palabras)
        {
            InitializeComponent();

            MaximizedBounds = Screen.FromHandle(Handle).WorkingArea;

            Guna2PictureBox[] gunaPictureBoxes = { guna2PictureBox1, guna2PictureBox2, guna2PictureBox3, guna2PictureBox4 };

            // Suscribir los eventos MouseEnter y MouseLeave para cada Guna2PictureBox
            foreach (var pictureBox in gunaPictureBoxes)
            {
                pictureBox.MouseEnter += new EventHandler(GunaPictureBox_MouseEnter);
                pictureBox.MouseLeave += new EventHandler(GunaPictureBox_MouseLeave);
            }

            MessageBox.Show("COMO JUGAR: \n" +
                "Es un simple Wordle de palabras.\n" +
                "Debes introducir palabras de la misma cantidad de letras que las casillas mostradas.\n" +
                "En cada intento te mostrara cada posición de las letras que has introducido y se mostrará si estan bien, mal, o en otra posición.\n" +
                "\nVerde = Letra correcta en la posición Correcta.\n" +
                "Amarillo = Letra correcta en posición Incorrecta.\n" +
                "Rojo = Letra y posición Incorrecta.");

            textBox1.KeyDown += new KeyEventHandler(textBox1_KeyDown); // Añadir el evento de pulsación de tecla.

            this.palabras = palabras;
            intentos = new List<List<Button>>();

            ActiveControl = textBox1;

            // Llamadas a los métodos principales.
            AddIntentos();
            Palabra();
            AddListaLetras();
            PintarCasillas();
        }

        // Método que elige una palabra aleatoria.
        private void Palabra()
        {
            Random rnd = new Random();
            byte num = (byte)rnd.Next(0, palabras.Count);

            palabraActual = palabras[num].palabra;
            textBox1.MaxLength = palabraActual.Length; // Tamaño maximo de caracteres que se pueden escribir en el textbox
        }


        // Método que pinta inicialmente todas las casillas con " _ ".
        private void PintarCasillas()
        {
            byte cont;
            byte contSinLetras = 0;

            foreach (var intento in intentos)
            {
                cont = 0;
                contSinLetras = 0;
                foreach (var boton in intento)
                {
                    if (cont >= palabraActual.Length)
                    {
                        boton.Visible = false;
                        contSinLetras++;
                    }
                    else
                    {
                        boton.Text = "_ ";
                    }
                    cont++;
                }
            }

            // Instrucción que calcula la posición centrada de la palabra teniendo en cuenta las letras que contiene.
            panel2.Location = new Point(panel2.Location.X + (contSinLetras * 25), panel2.Location.Y); 
        }


        // Método que comprueba si se han acertado todas las letras de la palabra a adivinar, entonces termina el programa con un mensaje de Victoria.
        private void ComprobarVictoria()
        {
            bool correctas = true;

            for (int i = 0; i < palabraIntroducida.Length; i++)
            {
                if (!palabraIntroducida[i].Equals(palabraActual[i]))
                {
                    correctas = false;
                }
            }

            if (correctas)
            {
                MessageBox.Show("Has acertado la palabra!");
                Close();
            }
        }


        // Método que comprueba cada intento de palabras comparandolo con la letra de la palabra a adivinar segun la posición de las letras.
        // Si la letra es igual en la misma posición que la palabra a adivinar entonces se muestra esa letra en esa posición y además se pinta de verde.
        // Si la letra esta dentro de la palabra a adivinar pero no en esa posición entonces la pinta de color amarillo.
        private void ComprobarPalabra()
        {
            ValidarVerder();

            for (int i = 0; i < palabraIntroducida.Length; i++)
            {
                for (int j = 0; j < palabraActual.Length; j++)
                {
                    if (palabraIntroducida[i].Equals(palabraActual[i]))
                    {
                        // Pinta el fondo de la letra del color que requiera (verde = correcto / amarillo = no esta en la posición / rojo = no contiene esa letra).
                        // Pone el "hover" del mismo color.
                        // Almacena dicha letra en la posición de las casillas en el GUI.
                        intentos[numIntento][i].BackColor = Color.Green; 
                        intentos[numIntento][i].FlatAppearance.MouseOverBackColor = Color.Green;
                        intentos[numIntento][i].Text = palabraIntroducida[i].ToString();
                        break;
                    }
                    else if (palabraIntroducida[i].Equals(palabraActual[j]))
                    {
                        intentos[numIntento][i].Text = palabraIntroducida[i].ToString();

                        if (ComprobarAmarillo(palabraIntroducida[i]))
                        {
                            intentos[numIntento][i].BackColor = Color.DarkGoldenrod;
                            intentos[numIntento][i].FlatAppearance.MouseOverBackColor = Color.DarkGoldenrod;
                        }
                        else
                        {
                            intentos[numIntento][i].BackColor = Color.Red;
                            intentos[numIntento][i].FlatAppearance.MouseOverBackColor = Color.Red;
                        }
                        break;
                    }
                    else
                    {
                        intentos[numIntento][i].BackColor = Color.Red;
                        intentos[numIntento][i].FlatAppearance.MouseOverBackColor = Color.Red;
                        intentos[numIntento][i].Text = palabraIntroducida[i].ToString();
                    }
                }
            }

            numIntento++; // Suma uno al número de intentos para controlar cuando se pierde.

            // Comprobación de cuando los intentos lleguen a 6, que entonces se habrá perdido.
            if (numIntento == 6)
            {
                MessageBox.Show("Has perdido!");
                Close();
            }

            ComprobarVictoria();
        }


        // Método que comprueba si la letra está dentro de la palabra .
        private bool ComprobarAmarillo(char c)
        {
            foreach (List<char> listaLetra in letras)
            {
                if (c.Equals(listaLetra[0]))
                {
                    return true;
                }
            }
            return false;
        }


        // Método que comprueba si se ha acertado la posición de alguna letra entonces se van borrando de una lista que almacena cuantas veces se repite esa letra
        // en la palabra, al llegar a 0 veces se borra la lista directamente.
        // de esta manera controlamos las veces que aparece una letra y cuando dejar de comprobar si aparece mas veces.
        private void ValidarVerder()
        {
            for (int i = 0; i < palabraIntroducida.Length; i++)
            {
                if (palabraIntroducida[i].Equals(palabraActual[i]))
                {
                    foreach (List<char> listaLetra in letras)
                    {
                        if (palabraIntroducida[i].Equals(listaLetra[0]))
                        {
                            listaLetra.RemoveAt(0);

                            if (listaLetra.Count == 0)
                            {
                                letras.Remove(listaLetra);
                            }
                            break;
                        }
                    }
                }
            }
        }

        // Método que comprueba si se ha introducido la tecla Enter (Se ha enviado la palabra).
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.Text.Length != palabraActual.Length)
                {
                    MessageBox.Show("Introduce la misma cantidad de caracteres :P");
                }
                else
                {
                    palabraIntroducida = textBox1.Text;
                    palabraIntroducida = palabraIntroducida.ToUpper();
                    ComprobarPalabra();
                    textBox1.Text = "";
                }
            }
        }


        // Método para crear listas de las veces que se repite cada letra de manera que solo creemos las listas de letras que contenga la palabra.
        private void AddListaLetras()
        {
            bool existe = true;
            letras = new List<List<char>>();
            byte cont = 1;

            foreach (char letra in palabraActual)
            {
                if (letras.Count == 0)
                {
                    letras.Add(new List<char>());
                    letras[0].Add(letra);
                }
                else
                {
                    foreach (List<char> listaLetras in letras)
                    {
                        
                        if (listaLetras[0].Equals(letra))
                        {
                            listaLetras.Add(letra);
                            existe = true;
                            break;
                        }
                        else
                        {
                            existe = false;
                        }
                    }
                    if (!existe)
                    {
                        letras.Add(new List<char>());
                        letras[cont].Add(letra);
                        cont++;
                    }
                }
            }
        }

        // Método para almacenar todos las posiciones posibles de letras en el GUI en listas de botones y almacenarlos en otra lista de "filas".
        private void AddIntentos()
        {
            List<Button> botonesIntento1 = new List<Button> { Letra1_1, Letra2_1, Letra3_1, Letra4_1, Letra5_1, Letra6_1, Letra7_1, Letra8_1 };
            List<Button> botonesIntento2 = new List<Button> { Letra1_2, Letra2_2, Letra3_2, Letra4_2, Letra5_2, Letra6_2, Letra7_2, Letra8_2 };
            List<Button> botonesIntento3 = new List<Button> { Letra1_3, Letra2_3, Letra3_3, Letra4_3, Letra5_3, Letra6_3, Letra7_3, Letra8_3 };
            List<Button> botonesIntento4 = new List<Button> { Letra1_4, Letra2_4, Letra3_4, Letra4_4, Letra5_4, Letra6_4, Letra7_4, Letra8_4 };
            List<Button> botonesIntento5 = new List<Button> { Letra1_5, Letra2_5, Letra3_5, Letra4_5, Letra5_5, Letra6_5, Letra7_5, Letra8_5 };
            List<Button> botonesIntento6 = new List<Button> { Letra1_6, Letra2_6, Letra3_6, Letra4_6, Letra5_6, Letra6_6, Letra7_6, Letra8_6 };

            intentos.Add(botonesIntento1);
            intentos.Add(botonesIntento2);
            intentos.Add(botonesIntento3);
            intentos.Add(botonesIntento4);
            intentos.Add(botonesIntento5);
            intentos.Add(botonesIntento6);
        }

        #region Botones (Cerrar, maximizar, minimizar)
        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            Close();
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
