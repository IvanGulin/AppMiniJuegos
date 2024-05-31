using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Trivial
{
    public partial class Form1 : Form
    {
        private int aciertos = 0;
        private List<Pregunta> preguntas;
        private List<Pregunta> preguntasOriginales;
        private List<int> pAcertadas = new List<int>();
        private List<Button> botones;
        private int seleccion;
        private int correcta;
        private int preguntaActual;
        private bool comodin50 = true;

        public Form1(List<Pregunta> preguntas)
        {
            InitializeComponent();
            ResizeImageAndAssignToButton(button5, Properties.Resources._5266185, new Size(30, 30));
            ResizeImageAndAssignToButton(button6, Properties.Resources._5266185, new Size(30, 30));
            ResizeImageAndAssignToButton(button7, Properties.Resources._5266185, new Size(30, 30));
            this.preguntas = new List<Pregunta>(preguntas);
            this.preguntasOriginales = new List<Pregunta>(preguntas);
            Pregunta();
        }
        private void ResizeImageAndAssignToButton(Button button, Image image, Size newSize)
        {
            // Redimensionar la imagen
            Image resizedImage = new Bitmap(image, newSize);

            // Asignar la imagen redimensionada al botón
            button.Image = resizedImage;

            // Ajustar el estilo del botón para que la imagen se vea bien
            button.ImageAlign = ContentAlignment.MiddleCenter;
            button.TextImageRelation = TextImageRelation.ImageAboveText; // Ajusta esto según tus necesidades
        }

        private void Pregunta()
        {
            acertadas.Text = Convert.ToString(this.aciertos);

            if (preguntas.Count == 0)
            {
                MessageBox.Show($"¡Has respondido todas las preguntas! Has acertado: {aciertos}/{preguntasOriginales.Count} preguntas.");
                this.Close();
                return;
            }

            Random rnd = new Random();
            int i = rnd.Next(0, preguntas.Count);

            this.preguntaActual = i;
            preguntaTexto.Text = preguntas[i].Titulo;

            button1.Text = preguntas[i].Opciones[0];
            button2.Text = preguntas[i].Opciones[1];
            button3.Text = preguntas[i].Opciones[2];
            button4.Text = preguntas[i].Opciones[3];

            this.correcta = preguntas[i].Correcta;
        }

        private void ResponderPregunta()
        {
            if (seleccion != correcta)
            {
                string respuestaCorrecta = preguntas[preguntaActual].Opciones[correcta];
                MessageBox.Show("Has fallado, la respuesta correcta era: " + respuestaCorrecta);
            }
            else
            {
                pAcertadas.Add(preguntaActual);
                MessageBox.Show("¡Correcto! Vamos a la siguiente pregunta" + "\n Sumando uno a la cantidad de aciertos...");
                aciertos++;
            }

            // Borrar la pregunta respondida de la lista
            preguntas.RemoveAt(preguntaActual);

            // Llamar a la función Pregunta para generar una nueva pregunta.
            Pregunta();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.seleccion = 0;
            ResponderPregunta();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.seleccion = 1;
            ResponderPregunta();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.seleccion = 2;
            ResponderPregunta();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.seleccion = 3;
            ResponderPregunta();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Comodín del 50%
            if (comodin50)
            {
                listaBotones();

                Random rnd = new Random();
                int cont = 0;
                int posi = 0;
                List<int> usadas = new List<int>();

                while (cont != 2)
                {
                    posi = rnd.Next(0, 3);

                    if (posi != correcta && !usadas.Contains(posi))
                    {
                        botones[posi].Text = "//";
                        usadas.Add(posi);
                        cont++;
                    }
                }
                comodin50 = false;
            }
            else
            {
                MessageBox.Show("Ya has utilizado este comodín, no puedes volver a usarlo.");
            }
        }

        private void listaBotones()
        {
            botones = new List<Button>();
            botones.Add(button1);
            botones.Add(button2);
            botones.Add(button3);
            botones.Add(button4);
        }
    }

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
}