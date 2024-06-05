using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trivial
{
    public partial class Form3 : Form
    {
        private List<Palabra> palabras;
        private List<List<Button>> intentos;
        private string palabraActual, palabraIntroducida;
        public Form3(List<Palabra> palabras)
        {
            InitializeComponent();

            textBox1.KeyDown += new KeyEventHandler(textBox1_KeyDown); // Añadir el evento de pulsación de tecla.

            this.palabras = palabras;
            intentos = new List<List<Button>>();

            AddIntentos();
            Palabra();
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

        private void PintarCasillas()
        {
            byte cont = 0;
            foreach (var intento in intentos)
            {
                foreach (var boton in intento)
                {
                    if (cont == palabraActual.Length)
                    {
                        cont = 0;
                        break;
                    }

                    cont++;
                    boton.Text = "_ ";
                }
            }
        }



        private void ComprobarPalabra()
        {
            foreach (var letra in palabraIntroducida)
            {
                foreach (var letra2 in palabraActual)
                {
                    
                }
            }
        }



        // Método que comprueba si se ha introducido la tecla Enter (Se ha enviado la palabra).
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                palabraIntroducida = textBox1.Text;
                //ComprobarPalabra();
                textBox1.Text = "";
                PintarCasillas();
            }
        }


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
    }
}
