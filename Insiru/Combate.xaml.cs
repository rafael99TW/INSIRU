﻿using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Insiru
{
    /// <summary>
    /// Lógica de interacción para Combate.xaml
    /// </summary>

    public partial class Combate : Window
    {

        private readonly Pokemon pokemon_aliado;
        private readonly Pokemon pokemon_enemigo;

        private readonly int shiny_aliado = 0;
        private readonly int shiny_enemigo = 0;
        private int pokemon_aliado_maxVida;
        private int pokemon_enemigo_maxVida;

        public Combate(Pokemon pokemon_aliado, Pokemon pokemon_enemigo, int shiny1, int shiny2)
        {
            InitializeComponent();

            this.pokemon_aliado = pokemon_aliado;
            this.pokemon_enemigo = pokemon_enemigo;

            shiny_aliado = shiny1;
            shiny_enemigo = shiny2;

        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Nombre_Aliado.Content = pokemon_aliado.Nombre;
            Nombre_Enemigo.Content = pokemon_enemigo.Nombre;

            if (shiny_aliado == 0) Pokemon_Aliado.Source = new BitmapImage(new Uri("/Images/Pokemons/Aliado/" + Conector.imagenes_Pokemon(pokemon_aliado.Nombre) + ".png", UriKind.Relative));
            else Pokemon_Aliado.Source = new BitmapImage(new Uri("/Images/Pokemons/Aliado Shiny/" + Conector.imagenes_Pokemon(pokemon_aliado.Nombre) + ".png", UriKind.Relative));

            if (shiny_enemigo == 0) Pokemon_Enemigo.Source = new BitmapImage(new Uri("/Images/Pokemons/Enemigo/" + Conector.imagenes_Pokemon(pokemon_enemigo.Nombre) + ".png", UriKind.Relative));
            else Pokemon_Enemigo.Source = new BitmapImage(new Uri("/Images/Pokemons/Enemigo Shiny/" + Conector.imagenes_Pokemon(pokemon_enemigo.Nombre) + ".png", UriKind.Relative));

            pokemon_aliado_maxVida = pokemon_aliado.Vida;
            pokemon_enemigo_maxVida = pokemon_enemigo.Vida;

            ObtenerAtaques();

        }

        private void ObtenerAtaques()
        {
            ArrayList nombres = Conector.obtener_Ataque();

            Ataque1.Content = nombres[0];
            Ataque2.Content = nombres[1];
            Ataque3.Content = nombres[2];
            Ataque4.Content = nombres[3];

            ObtenerColor(pokemon_aliado.Tipo, Ataque4);

        }

        private void ObtenerColor(string tipo, Button boton)
        {
            switch (tipo)
            {
                case "Agua":
                    boton.Background = new SolidColorBrush(Color.FromRgb(26, 53, 255));
                    break;

                case "Fuego":
                    boton.Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                    break;

                case "Planta":
                    boton.Background = new SolidColorBrush(Color.FromRgb(69, 255, 0));
                    break;
            }
        }

        //Placaje
        private void Ataque1_Click(object sender, RoutedEventArgs e)
        {

            double WidthBarraEnemiga = ((pokemon_enemigo.Vida - 5) * 164) / pokemon_enemigo_maxVida;

            if (WidthBarraEnemiga <= 0)
            {
                Vida_Enemigo.Width = 0;
                pokemon_enemigo.Vida = 0;

                //Enviar a la pantalla de victoria

            }
            else
            {
                Vida_Enemigo.Width = WidthBarraEnemiga;
                pokemon_enemigo.Vida -= 5;
            }
            
        }

        //Curar
        private void Ataque3_Click(object sender, RoutedEventArgs e)
        {

            double WidthBarraAliada = ((pokemon_aliado.Vida + 3) * 164) / pokemon_aliado_maxVida;

            if (WidthBarraAliada <= 0)
            {
                Vida_Aliado.Width = 0;
                pokemon_aliado.Vida = 0;

                //Enviar a la pantalla de derrota

            }
            else if(WidthBarraAliada >= 164)
            {

                //Mostrar mensaje - Ya tienes la vida al máximo
                MessageBox.Show("Ya tienes la vida al máximo");

            }else
            {
                Vida_Aliado.Width = WidthBarraAliada;
                pokemon_aliado.Vida += 3;
            }
            
        }
    }
}