using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Trivial
{
    public partial class Form1 : Form
    {
        private List<Pregunta> preguntas; // Lista de preguntas.
        private List<byte> preguntasRespondidas = new List<byte>(); // Lista de preguntas respondidas.
        private List<Button> botones; // Lista de botones.
        private List<byte> historial_50_50 = new List<byte>(); // Lista de las preguntas en las que se haya usado el comodín 50/50.
        private byte seleccion, correcta, preguntaActual, tiempoRestante;
        private byte aciertos = 0, listaNum = 0;
        private bool comodin50 = true, comodinSiguiente = true, comodinRecuperar = true, comodinInternet = true;

        public Form1(List<Pregunta> preguntas)
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

            // Configuración de los tamaños de las imagenes.
            ResizeImageAndAssignToButton(button5, Properties.Resources._50_50, new Size(30, 30));
            ResizeImageAndAssignToButton(button6, Properties.Resources.flecha_siguiente, new Size(30, 30));
            ResizeImageAndAssignToButton(button7, Properties.Resources.google, new Size(30, 30));

            // Mensaje de como jugar al inciar el programa.
            MessageBox.Show("COMO JUGAR:" +
                "\n -Es un juego de preguntas de toda la vida. Deberás responder bien todas las preguntas que puedas." +
                "\n -No pierdes al fallar una pregunta, solamente esa pregunta no te contará como acertada." +
                "\n -Tendrás que responder 15 preguntas para terminar el juego y se te mostrará el resultado al final." +
                "\n\n COMODINES:" +
                "\n -El 50/50 de toda la vida, se te quitarán dos preguntas incorrectas." +
                "\n -Pasar de pregunta. Este comodín te pondrá otra pregunta aleatoria y no tendrás que responder a la anterior." +
                "\n -Busqueda por internet. Tendrás un contador de 30 segundos, si en ese tiempo no has respondido automáticamente" +
                "saldrá que has fallado y pasará a la siguiente pregunta. Obviamente puedes utilizar internet para buscar la respuesta." +
                "\n -Recuperar, este comodín te devolverá una sola vez alguno de los comodínes que ya hayas utilizado.");

            // Asigno los checkboxes de los comodines a true de inicio.
            checkBox1.Checked = true;
            checkBox2.Checked = true;
            checkBox3.Checked = true;

            // Inicializo la lista con las preguntas
            this.preguntas = new List<Pregunta>(preguntas);

            ListaPreguntas(); // Llamo al método que llena la lista
            ListaBotones(); // Llamo al método de crear la lista de los botones.
            Pregunta(); // Llamo al método que inicia la pregunta.
        }

        // Método para cambiar el tamaño de las imagenes usadas en los comodines. Gracias CHATGPT
        private void ResizeImageAndAssignToButton(Button button, Image image, Size newSize)
        {
            // Redimensionar la imagen
            Image resizedImage = new Bitmap(image, newSize);

            // Asignar la imagen redimensionada al botón
            button.Image = resizedImage;

            // Ajustar el estilo del botón para que la imagen se vea bien
            button.ImageAlign = ContentAlignment.MiddleCenter;
            button.TextImageRelation = TextImageRelation.ImageAboveText;
        }


        #region Metodos principales de las preguntas.
        // Este método sera el principal, cada pregunta respondida llamará a este método que comprobará si se han respondido las 15 preguntas.
        // Si se han respondido las 15 preguntas entonces muestra un mensaje con el número de aciertos frente al número de preguntas respondidas.
        // Si el número de preguntas respondidas no es 15 entonces llama a la siguiente pregunta.
        private void Pregunta()
        {
            seleccion = 9; // Asigno de inicio la selección en 9 para que no provoque errores con el comodín 50/50.
            acertadas.Text = Convert.ToString(aciertos); // Asigna el valor de las preguntas acertadas a un label llamado acertadas.

            if (preguntasRespondidas.Count == 15)
            {
                MessageBox.Show($"¡Has respondido todas las preguntas! Has acertado: {aciertos}/{preguntasRespondidas.Count} preguntas.");
                Close();
                return;
            }

            listBox1.SelectionMode = SelectionMode.One;
            listBox1.SelectedIndex = listaNum;
            listaNum++;

            SiguientePregunta(); // Llamada al método que genera la siguiente pregunta.
        }

        // Método que se encarga de generar la siguiente pregunta 
        private void SiguientePregunta()
        {
            // Genero un número aleatorio entre 0 y el número de preguntas disponibles dentro de la lista "preguntas".
            Random rnd = new Random();
            byte i = (byte)rnd.Next(0, preguntas.Count);

            // Almaceno el número aleatorio en una variable de la clase llamada preguntaActual .
            preguntaActual = i;

            // Le asigno al label del enunciado de la pregunta el valor de "titulo" en la pregunta en la posición de la preguntaActual.
            preguntaTexto.Text = preguntas[i].Titulo;

            // Hago un bucle por todos los botones en la lista de botones y les asigno el valor de la opción en cada uno de ellos. 
            // Además los habilito para que se pueda hacer click en todos ellos ya que en el comodín 50/50 deshabilitare dos de ellos.
            for (int j = 0; j < botones.Count; j++)
            {
                botones[j].Text = preguntas[i].Opciones[j];
                botones[j].Enabled = true;
            }

            correcta = (byte)preguntas[i].Correcta; // Almaceno la posición de la respuesta correcta en una variable global.
        }

        // Método que comprueba si la respuesta a la pregunta ha sido correcta o incorrecta.
        private void ResponderPregunta()
        {
            // En caso de ser incorrecta muestra un mensaje por pantalla diciendo que se ha fallado y con la solución.
            if (seleccion != correcta)
            {
                string respuestaCorrecta = preguntas[preguntaActual].Opciones[correcta];
                MessageBox.Show("Has fallado, la respuesta correcta era: " + respuestaCorrecta);
            }
            // En caso de haber acertado se muestra un mensaje de que se ha acertado y se suma uno al valor de los aciertos.
            else
            {
                MessageBox.Show("¡Correcto! Vamos a la siguiente pregunta" + "\n Sumando uno a la cantidad de aciertos...");
                aciertos++;
            }

            // Agrego la preguntaActual a la lista de preguntasRespondidas.
            preguntasRespondidas.Add(preguntaActual);

            // Borrar la pregunta respondida de la lista para que así no pueda repetirse.
            preguntas.RemoveAt(preguntaActual);

            // Llamar a la función Pregunta para generar una nueva pregunta.
            Pregunta();
        }

        private void ListaPreguntas()
        {
            for (int i = 0; i < 15; i++)
            {
                listBox1.Items.Add("Pregunta " + (i+1));
            }
        }
        #endregion


        #region Botones
        // Método que crea una lista de los cuatro botones del programa.
        private void ListaBotones()
        {
            botones = new List<Button>
            {
                button1,
                button2,
                button3,
                button4
            };
        }

        // Los cuatro botones de las opciones de respuesta.
        private void button1_Click_1(object sender, EventArgs e)
        {
            seleccion = 0;
            ResponderPregunta();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            seleccion = 1;
            ResponderPregunta();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            seleccion = 2;
            ResponderPregunta();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            seleccion = 3;
            ResponderPregunta();
        }
        #endregion



        #region Comodines
        // Comodín del 50/50. Nos quitará dos opciones posibles de las 4 opciones a elegir, evitando que nos quite la opcion correcta.
        private void button5_Click_1(object sender, EventArgs e)
        {
            // Comprobación de que se pueda usar el comodin y que no se esté utilizando el comodín en una pregunta que ya se haya usado.
            if (comodin50 && !historial_50_50.Contains(preguntaActual))
            {
                Random rnd = new Random();
                byte cont = 0;
                List<byte> modificados = new List<byte>(); // Lista que almacena los botones ya modificados.

                // Bucle que modifica un total de dos botones.
                while (cont < 2)
                {
                    byte posiPregunta = (byte)rnd.Next(0, 4); // Random de una entre cuatro posiciones posibles.

                    // Comprobar que dicha posición no es la respuesta correcta y que ademas no este en la lista anterior que significaría que ya se modificó.
                    if (posiPregunta != correcta && !modificados.Contains(posiPregunta))
                    {
                        botones[posiPregunta].Text = "//"; // Cambio el valor del texto a "//" para identificar que es una respuesta incorrecta.
                        botones[posiPregunta].Enabled = false; // Cambio el valor de Enabled para que no se pueda seleccionar dicha opción.
                        modificados.Add(posiPregunta); // Añado esa posición a la lista anterior.
                        cont++;
                    }
                }
                historial_50_50.Add(preguntaActual); // Añado la pregunta actual a otra lista que almacena el historial de preguntas en las que se haya usado el comodín.
                comodin50 = false; // Deshabilito el comodín.
                checkBox1.Checked = false; // Desmarco la casilla del comodín para que se vea que no está disponible dicho comodín.
            }
            // Si no se puede usar el comodín o se esta usando en la misma pregunta dos veces entonces muestra el siguiente mensaje.
            else
            {
                MessageBox.Show("Ya has utilizado este comodín, no puedes volver a usarlo.");
            }
        }

        // Comodín de pasar de pregunta, este método cambiará a otra pregunta aleatoria.
        private void button6_Click_1(object sender, EventArgs e)
        {
            // Comprobación de que el comodín esté habilitado.
            if (comodinSiguiente)
            {
                // Cambio el valor del comodín a deshabilitado y también deshabilito su checkbox para indicar que no se puede usar.
                comodinSiguiente = false;
                checkBox2.Checked = false;

                // Borrar la pregunta respondida de la lista
                preguntas.RemoveAt(preguntaActual);

                // Llamar a la función Pregunta para generar una nueva pregunta.
                SiguientePregunta();
            }
            // Si el comodín esta deshabilitado muestra el siguiente mensaje.
            else
            {
                MessageBox.Show("Ya has utilizado este comodín, no puedes volver a usarlo.");
            }
        }

        // Comodín Internet. Inicia un timer de 30 segundos, al cabo de esos 30 segundos si no se ha respondido dará la respuesta por fallada.
        private void button7_Click_1(object sender, EventArgs e)
        {
            // Comprueba que esté habilitado el comodín.
            if (comodinInternet)
            {
                // Si está habilitado muestra el label del tiempo restante, empieza en 30 segundos y cambia el valor del label a "Te quedan 30 segundos".
                labelTiempo.Visible = true;
                tiempoRestante = 30;
                labelTiempo.Text = "Te quedan " + tiempoRestante + " segundos";

                // Inicio los timers.
                IniciarTemporizadores();

                //Deshabilito el comodín y su checkbox para indicar que no se puede usar.
                comodinInternet = false;
                checkBox3.Checked = false;
            }
            else
            {
                MessageBox.Show("Ya has utilizado este comodín, no puedes volver a usarlo.");
            }
        }

        #endregion

        #region Comodín de recuperación de comodines.
        // Son tres botones que cada uno de ellos devuelve la posibilidad de usar cada comodín de los anteriores (solo una vez).
        // Cada uno de ellos comprueba si el comodín está habilitado, si lo está muestra un mensaje de que se ha recuperado dicho comodin
        // y habilita dicho comodín y su checkbox, también deshabilitado el comodín de Recuperación.
        // Si ya se ha usado este comodín muestra un mensaje de que no se puede volver a usar.
        private void button8_Click_1(object sender, EventArgs e)
        {
            if (comodinRecuperar)
            {
                MessageBox.Show("Has recuperado el comodín del 50/50.");
                comodin50 = true;
                checkBox1.Checked = true;
                comodinRecuperar = false;
            }
            else
            {
                MessageBox.Show("Ya has utilizado este comodín, no puedes volver a usarlo.");
            }
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            if (comodinRecuperar)
            {
                MessageBox.Show("Has recuperado el comodín de pasar de pregunta.");
                comodinSiguiente = true;
                checkBox2.Checked = true;
                comodinRecuperar = false;
            }
            else
            {
                MessageBox.Show("Ya has utilizado este comodín, no puedes volver a usarlo.");
            }
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            if (comodinRecuperar)
            {
                MessageBox.Show("Has recuperado el comodín de búsqueda en internet.");
                comodinInternet = true;
                checkBox3.Checked = true;
                comodinRecuperar = false;
            }
            else
            {
                MessageBox.Show("Ya has utilizado este comodín, no puedes volver a usarlo.");
            }
        }
        #endregion


        #region Timer
        // Evento del timer, este método sera llamado cada segundo y comprobará si la respuesta es correcta o no y también si queda tiempo.
        private void timer1_Tick(object sender, EventArgs e)
        {
            // Si aún queda tiempo, se actualiza el contador y se muestra en el label
            if (tiempoRestante > 0)
            {
                tiempoRestante--;
                labelTiempo.Text = "Te quedan " + tiempoRestante + " segundos";
            }
            // Si el tiempo se ha agotado, se detiene el temporizador, se muestra un mensaje y se pasa a la siguiente pregunta
            else
            {
                timer1.Stop();
                labelTiempo.Text = "Fin del tiempo.";
                MessageBox.Show("No has respondido dentro del tiempo permitido.");
                seleccion = 9; // Dejo la selección en 9 para evitar bugs.
                ResponderPregunta(); // Llama a la siguiente pregunta.
                labelTiempo.Visible = false; // Esconde el label del tiempo.
            }
        }

        // Comprobación de que el valor seleccionado sea la respuesta correcta o no.
        private bool ComprobarRespuesta()
        {
            if (seleccion == correcta)
                return true;
            else
                return false;
        }

        // Método para iniciar ambos temporizadores
        private void IniciarTemporizadores()
        {
            // Inicia el temporizador de cuenta atrás con intervalo de un segundo.
            timer1.Interval = 1000;
            timer1.Start();

            // Inicia el temporizador para comprobar la respuesta con un intervalo de 100 milisegundos.
            timer2.Interval = 100;
            timer2.Start();
        }

        private void timer2_Tick_1(object sender, EventArgs e)
        {
            // Si la respuesta es correcta, se detienen los temporizadores y se oculta el label del tiempo.
            if (ComprobarRespuesta())
            {
                timer1.Stop();
                timer2.Stop();
                labelTiempo.Visible = false;
            }
            // Si la respuesta es incorrecta y la selección es diferente a 9 (valor por defecto para evitar bugs) se paran los timers y se oculta el label.
            else if (!ComprobarRespuesta() && seleccion != 9) 
            {
                timer1.Stop();
                timer2.Stop();
                labelTiempo.Visible = false;
            }
        }
        #endregion


        #region Botones (Cerrar, maximizar, minimizar)
        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
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