using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Trivial
{
    public partial class Form4 : Form
    {
        private Dictionary<String, List<String>> palabrasPistas; // Diccionario de palabras y sus pistas.
        private List<String> pistas; // Lista de solo las pistas.
        private List<Label> labels; // Lista de todos los labels que se modificarán mientras se juega.
        private List<Guna2Panel> panels; // Lista de los paneles en los que estarán los labels.
        private string palabraAdivinar, palabraIntroducida; 
        private byte numIntento = 0, numPista;
        public Form4(Dictionary<String, List<String>> palabrasPistas)
        {
            InitializeComponent();

            MaximizedBounds = Screen.FromHandle(Handle).WorkingArea;

            MessageBox.Show("COMO JUGAR: \n" +
                "Tienes que adivinar una palabra seleccionada por el sistema.\n" +
                "Se dará una pista por cada intento.\n" +
                "Tendrás un total de 4 pistas y 4 intentos.\n" +
                "En el momento que la aciertes Ganarás a no ser que te quedes sin intentos.\n" +
                "En el caso de que en el último intento no aciertes la palabra, perderás.");

            Guna2PictureBox[] gunaPictureBoxes = { guna2PictureBox1, guna2PictureBox2, guna2PictureBox3 };

            // Suscribir los eventos MouseEnter y MouseLeave para cada Guna2PictureBox
            foreach (var pictureBox in gunaPictureBoxes)
            {
                pictureBox.MouseEnter += new EventHandler(GunaPictureBox_MouseEnter);
                pictureBox.MouseLeave += new EventHandler(GunaPictureBox_MouseLeave);
            }

            textBox1.KeyDown += new KeyEventHandler(textBox1_KeyDown); // Añadir el evento de pulsación de tecla.

            this.palabrasPistas = palabrasPistas;
            labels = new List<Label>();
            panels = new List<Guna2Panel>();
            pistas = new List<string>();

            // Llamada a métodos principales.
            AddLabelsPanels();
            LabelsVacios();
            SeleccionarPalabra();
            MostrarPista();
        }

        // Método para elegir una palabra aleatoria dentro del diccionario.
        private void SeleccionarPalabra()
        {
            Random rnd = new Random();
            byte posi = (byte)rnd.Next(palabrasPistas.Count);
            byte cont = 0;

            foreach (var palabra in palabrasPistas)
            {
                if (cont == posi)
                {
                    // Almaceno la palabra (key) en la variable de la palabra.
                    palabraAdivinar = palabra.Key;

                    // Almaceno en la lista pistas el valor de (value) que es una lista de Strings.
                    pistas = palabra.Value;

                    return;
                }
                cont++;
            }
        }
        
        // Método que cambia el color del panel correspondiente de cada pista dada y cambia su texto al de la pista actual.
        private void MostrarPista()
        {
            panels[numIntento].BackColor = Color.DarkGray;
            labels[numIntento].Text = pistas[numPista];

            numPista++;
            numIntento++;
        }

        // Método que comprueba si se han excedido los intentos o si bien se ha acertado la palabra.
        private bool ComprobarPalabra()
        {
            bool acierto = false;

            if (numPista == 4)
            {
                MessageBox.Show("Has perdido. La palabra era: " + palabraAdivinar);
                Close();
            }
            else
            {
                if (palabraAdivinar == palabraIntroducida)
                {
                    MessageBox.Show("Has acertado la palabra. Muy Bien!");
                    acierto = true;
                    Close();
                }
            }

            return acierto;
        }


        // Método que actúa cada vez que se presiona el Enter en el textbox.
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                palabraIntroducida = textBox1.Text;
                palabraIntroducida = palabraIntroducida.ToUpper();

                // Si la palabra no se ha acertado y los intentos aún no son 6 significa que se sigue el juego entonces hace las siguientes acciones.
                if (!ComprobarPalabra() && numIntento <= 6)
                {
                    labels[numIntento].Text = palabraIntroducida;
                    panels[numIntento].BackColor = Color.Gray;
                    numIntento++;
                    MostrarPista();
                }

                textBox1.Text = "";
            }
        }

        // Método para llenar las listas de panels y labels.
        private void AddLabelsPanels()
        {
            labels.Add(label3);
            labels.Add(label4);
            labels.Add(label5);
            labels.Add(label6);
            labels.Add(label7);
            labels.Add(label8);
            labels.Add(label9);

            panels.Add(guna2Panel1);
            panels.Add(guna2Panel2);
            panels.Add(guna2Panel3);
            panels.Add(guna2Panel4);
            panels.Add(guna2Panel5);
            panels.Add(guna2Panel6);
            panels.Add(guna2Panel7);
        }

        // Método para iniciar los labels vacíos.
        private void LabelsVacios()
        {
            foreach (var label in labels)
            {
                label.Text = "";
            }
        }

        #region Botones (Cerrar, maximizar, minimizar)
        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void guna2PictureBox2_Click_1(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal) WindowState = FormWindowState.Maximized;
            else WindowState = FormWindowState.Normal;
        }

        private void guna2PictureBox3_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
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
