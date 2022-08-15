﻿using MathNet.Numerics.Random;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TP1.Mvvm;


namespace TP1.Views
{
    /// <summary>
    /// Interaction logic for PuntoB.xaml
    /// </summary>
    public partial class PuntoB : Page
    {
       // decimal rand_num;
        Random rd = new Random();
        int muestra;
        int subintervalos;
        //private decimal xi_1;
        //private decimal cIndependiente;
        //private decimal cMultiplicadora;
        //private decimal modulo;
        private List<decimal> numeros_aleatorios = new List<decimal>();
        private Gestor gestor;

        public PuntoB()
        {
            InitializeComponent();
            gestor = new Gestor();
            
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }


        private void BtnGenerar_B_Click(object sender, RoutedEventArgs e)
        {
           
            muestra = Int32.Parse(TxtMuestra.Text);
            subintervalos = Int32.Parse(TxtSubintervalos.Text);


            numeros_aleatorios.Clear(); // deja el vector estado vacio

           
            double[] samples = SystemRandomSource.Doubles(muestra, 37);  // genera numeros random [0; 1), primer argumento es la cantidad a generar
            numeros_aleatorios = samples.Select(a => (decimal)a).ToList();


            mostrarVectorEstado(numeros_aleatorios);
           
        }

        private void BtnTest_Click(object sender, RoutedEventArgs e)
        {
           gestor.testChiCuadrado(numeros_aleatorios, this.muestra, this.subintervalos);
        }

       
        private void mostrarVectorEstado(List<decimal> vectorEstado)
        {
            

            foreach (var item in vectorEstado)
            {
                Console.WriteLine(item); // solo para checkear desp borro

            }

            Console.WriteLine(vectorEstado.ToArray().Count()); // solo para checkear desp borro
            

        }

        
    }
}
