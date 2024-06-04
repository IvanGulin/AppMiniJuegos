using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Trivial
{
    public partial class Form2 : Form
    {
        private List<Palabra> palabras; // Lista que almacena todas las palabras disponibles del juego.
        private List<char> acertadas; // Lista de letras acertadas en el juego.
        private char letraSeleccionada; // Letra elegida en cada jugada.
        private string palabraActual; // Palabra elegida aleatoriamente que se almacena en esta variable.
        private string pistaActual = "Pista: "; // Pequeñas pistas que se mostrarán encima de la zona de adivinar la palabra.
        private string letrasUsadas; // Una cadena que almacena todas las letras usadas.
        private byte cantFallos = 0; // Almacena la cantidad de veces que se ha fallado en la palabra.

        public Form2(List<Palabra> palabras)
        {
            InitializeComponent();

            // Mensaje de como jugar.
            MessageBox.Show("COMO JUGAR: \n" +
                "Es un ahorcado de toda la vida. \n" +
                "Tendrás una pista sobre la palabra y tendrás que ir introduciendo letras.\n" +
                "Si dicha letra está en la palabra entonces se mostrará en su posición/es.\n" +
                "En caso de no haber acertado alguna letra se ira descubriendo más parte del ahorcado\n" +
                "Si cometes 6 fallos perderás.");

            // Inicialización de las listas.
            this.palabras = new List<Palabra>();
            this.palabras = palabras;
            acertadas = new List<char>();

            // Llamadas a los métodos principales que se deben llamar al iniciar el juego.
            ElegirPalabra();
            FillAcertadas();
            FillCasillas();

            // Asigno la pista de la palabra actual al label de la pista.
            Pista.Text = pistaActual;
        }


        // Método para llenar la lista "acertadas" de "_" como campos vacíos para utilizarla más adelante 
        // con la cantidad de caracteres que la palabra a adivinar.
        private void FillAcertadas()
        {
            for (int i = 0; i < palabraActual.Length; i++)
            {
                acertadas.Add('_');
            }
        }

        // Método que inicia las casillas con la cantidad de caracteres que tenga la palabra a adivinar.
        private void FillCasillas()
        {
            for (int i = 0; i < palabraActual.Length; i++)
            {
                if (palabraActual[i].Equals(" "))
                {
                    casillas_BarraBajas.Text += " ";

                }
                else
                {
                    casillas_BarraBajas.Text += "_ ";
                }
            }
        }

        // Método que aleatoriamente elige una palabra de las posibles y la almacena junto a su pista.
        private void ElegirPalabra()
        {
            Random rnd = new Random();
            byte palabraRnd = (byte)rnd.Next(0, palabras.Count);
            palabraActual = palabras[palabraRnd].palabra;
            pistaActual += palabras[palabraRnd].pista;
        }


        // Método que comprueba si cada letra jugada esta dentro de la palabra o no.
        // En caso de que la letra este dentro de la palabra a adivinar devolverá un true indicando el acierto.
        // En caso de que la letra no esté en la palabra devolverá un false indicando que ha fallado.
        private bool MostrarCasillas()
        {
            bool correcta = false; // variable booleana que sirve para devolverla como acierto o fallo de la letra.

            // Bucle que recorre cada uno de los caracteres de la palabra a adivinar y comprueba si cada uno de los caracteres
            // es el mismo que la letra jugada, en caso de que esta letra se encuentre en algun sitio de la palabra se almacenará true.
            for (int i = 0; i < palabraActual.Length; i++)
            {
                if (letraSeleccionada == palabraActual[i])
                {
                    acertadas[i] = letraSeleccionada;
                    correcta = true;
                }
            }

            // Almaceno los aciertos separado por espacios en una cadena que almacenaré en el label de las casillas.
            string casillasConEspacios = listaToString(acertadas);
            casillas_BarraBajas.Text = casillasConEspacios;

            return correcta; // Retorno del valor booleano.
        }

        // Método para añadir espacios entre las letras de la palabra y se vean los "_" separados.
        private String listaToString(List<char> lista)
        {
            String palabra = ""; // Inicio una variable cadena vacía.

            // Recorro toda la lista de caracteres enviada por parámetro y
            // almaceno en la palabra la letra que contenga cada posición más un espacio.
            foreach (var letra in lista)
            {
                palabra += letra.ToString() + " ";
            }

            return palabra; // Retorno la palabra.
        }

        // Método que se llamará cuando se llegue a 6 fallos y mostrará mensaje de que se ha perdido y cerrará la ventana.
        private void Perder()
        {
            MessageBox.Show("Has fallado las seis veces posibles, has Perdido. \n" +
                "La palabra era: " + palabraActual);

            Close();
        }

        // Método que hará una "animación" del ahorcado cada vez que se fallo hasta un maximo de 6.
        private void MostrarCambioAhorcado()
        {
            // Dependiendo del numero de fallos se quitará uno u otro panel que tapa el dibujo.
            switch (cantFallos)
            {
                case 1:
                    // ACTIVAR PALO 1
                    Palo1.Visible = false;
                    break;
                case 2:
                    // ACTIVAR PALO 2
                    Palo2.Visible = false;
                    break;
                case 3:
                    Palo3.Visible = false;
                    // ACTIVAR PALO 3
                    break;
                case 4:
                    // ACTIVAR PALO 4
                    Palo4.Visible = false;
                    break;
                case 5:
                    // ACTIVAR PALO 5
                    Palo5.Visible = false;
                    break;
                case 6:
                    // ACTIVAR PALO 6
                    Palo6.Visible = false;
                    break;
            }
        }


        // Método que comprueba si se han acertado todas las letras de la palabra y por lo tanto se habrá ganado.
        private void ComprobarVictoria()
        {
            byte cantidadAciertos = 0; // Inicio una variable númerica en 0.

            // Recorro todas las letras de la palabra a adivinar.
            // Sí la letra de la posición "i" esta almacenada en "acertadas" sumará uno a la cantidadAciertos, así hasta el final de la palabra.
            for (int i = 0; i < palabraActual.Length; i++)
            {
                if (palabraActual[i].Equals(acertadas[i]))
                {
                    cantidadAciertos++;
                }
            }

            // Si al final del bucle la cantidad de aciertos es exactamente igual a la cantidad de letras de la palabra
            // significará que se ha ganado, entonces se llama al método Victoria().
            if (cantidadAciertos == palabraActual.Length) Victoria();
        }


        // Método que muestra un mensaje de que se ha ganado el juego y cierra la ventana.
        private void Victoria()
        {
            MessageBox.Show("Has ganado, la palabra era: " + palabraActual);
            Close();
        }


        #region Letras del teclado

        // Método que hace de cada botón posible de teclas y hace todo lo que tenga que hacer cada una de las teclas.
        private void LetrasClick(Button boton)
        {
            // Una vez se haya hecho click en una tecla, esta tecla se almacenará en una variable para ser usada en las comprobaciones.
            letraSeleccionada = Convert.ToChar(boton.Text); 

            // Desactivo el botón para que no se pueda volver a hacer click en él.
            boton.Enabled = false;

            // Almaceno la letra en la cadena de "letrasUsadas" para tener el historial de las teclas utilizadas.
            letrasUsadas += " " + letraSeleccionada + " ";

            // Almaceno esas letras utilizadas en el label de LetrasUsadas para que se muestre en ese label.
            LetrasUsadas.Text = letrasUsadas;


            // Comprobación de que la letra introducida sea correcta o no.
            if (MostrarCasillas())
            {
                // En caso de haber acertado la letra se cambiara el color del boton a verde (acierto).
                boton.ForeColor = Color.Green;

                // Comprobación de que la lista de letras acertadas sea mayor a 2 para optimizar un poco.
                // En caso de ser mayor de 2 llama a la funcion de ComprobarVictoria().
                if (acertadas.Count > 2) ComprobarVictoria();
            }
            // En caso de no haber acertado la letra se cambiara el color del botón a un rojo oscuro,
            // se sumará uno a la cantidad de fallos y se llamará al método de mostrar los cambios del dibujo del ahorcado.
            else
            {
                boton.ForeColor = Color.DarkRed;
                cantFallos++;
                MostrarCambioAhorcado();
            }

            if (cantFallos == 6) Perder(); // Si la cantidad de fallos es 6 entonces llama al método Perder().
        }


        // Todos los botones de las teclas.
        private void LetraQ_Click(object sender, EventArgs e)
        {
            LetrasClick(LetraQ);
        }

        private void LetraW_Click(object sender, EventArgs e)
        {
            LetrasClick(LetraW);
        }

        private void LetraE_Click(object sender, EventArgs e)
        {
            LetrasClick(LetraE);
        }

        private void LetraR_Click(object sender, EventArgs e)
        {
            LetrasClick(LetraR);
        }

        private void LetraT_Click(object sender, EventArgs e)
        {
            LetrasClick(LetraT);
        }

        private void LetraY_Click(object sender, EventArgs e)
        {
            LetrasClick(LetraY);
        }

        private void LetraU_Click(object sender, EventArgs e)
        {
            LetrasClick(LetraU);
        }

        private void LetraI_Click(object sender, EventArgs e)
        {
            LetrasClick(LetraI);
        }

        private void LetraO_Click(object sender, EventArgs e)
        {
            LetrasClick(LetraO);
        }

        private void LetraP_Click(object sender, EventArgs e)
        {
            LetrasClick(LetraP);
        }

        private void LetraA_Click(object sender, EventArgs e)
        {
            LetrasClick(LetraA);
        }

        private void LetraS_Click(object sender, EventArgs e)
        {
            LetrasClick(LetraS);
        }

        private void LetraD_Click(object sender, EventArgs e)
        {
            LetrasClick(LetraD);
        }

        private void LetraF_Click(object sender, EventArgs e)
        {
            LetrasClick(LetraF);
        }

        private void LetraG_Click(object sender, EventArgs e)
        {
            LetrasClick(LetraG);
        }

        private void LetraH_Click(object sender, EventArgs e)
        {
            LetrasClick(LetraH);
        }

        private void LetraJ_Click(object sender, EventArgs e)
        {
            LetrasClick(LetraJ);
        }

        private void LetraK_Click(object sender, EventArgs e)
        {
            LetrasClick(LetraK);
        }

        private void LetraL_Click(object sender, EventArgs e)
        {
            LetrasClick(LetraL);
        }

        private void LetraÑ_Click(object sender, EventArgs e)
        {
            LetrasClick(LetraÑ);
        }

        private void LetraZ_Click(object sender, EventArgs e)
        {
            LetrasClick(LetraZ);
        }

        private void LetraX_Click(object sender, EventArgs e)
        {
            LetrasClick(LetraX);
        }

        private void LetraC_Click(object sender, EventArgs e)
        {
            LetrasClick(LetraC);
        }

        private void LetraV_Click(object sender, EventArgs e)
        {
            LetrasClick(LetraV);
        }

        private void LetraB_Click(object sender, EventArgs e)
        {
            LetrasClick(LetraB);
        }

        private void LetraN_Click(object sender, EventArgs e)
        {
            LetrasClick(LetraN);
        }

        private void LetraM_Click(object sender, EventArgs e)
        {
            LetrasClick(LetraM);
        }
        #endregion
    }
}