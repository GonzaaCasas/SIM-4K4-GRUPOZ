﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Text.RegularExpressions;
using System.Collections;

namespace TP1.Views
{
    /// <summary>
    /// Interaction logic for PuntoA.xaml
    /// </summary>
    /// 

    public partial class PuntoA : Page
    {
        private decimal semilla;
        private decimal cIndependiente;
        private decimal cMultiplicadora;
        private decimal modulo;
        private decimal muestra;
        private ArrayList numeros_aleatorios = new ArrayList();
        private decimal xi;
        

        public PuntoA()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void BtnGenerar_Click(object sender, RoutedEventArgs e)
        {
            semilla = Int32.Parse(TxtSemilla.Text); // semilla
            cIndependiente = Int32.Parse(TxtConstanteIndependiente.Text);
            cMultiplicadora = Int32.Parse(TxtConstanteMultiplicadora.Text);
            modulo = Int32.Parse(TxtModulo.Text);

            muestra = 20;

            generar(semilla, cIndependiente, cMultiplicadora, modulo);

            // despues checkear lo de cambiar int por double o decimal


            mostrarVectorEstado(numeros_aleatorios);

        }

        private void generar(decimal xi_1,decimal cIndependiente, decimal cMultiplicadora,decimal modulo)
        {

            numeros_aleatorios.Clear(); // deja el vector estado vacio


            decimal ri = (xi_1/ modulo);

            numeros_aleatorios.Add(ri); // agregar el primer random que corresponde a la semilla

           

            for (int i = 0; i < muestra - 1; i++)
            {

                xi = generadorCongruenteMultiplicativo(xi_1, cMultiplicadora, modulo);
                numeros_aleatorios.Add(xi / modulo); // agrega los random
                xi_1 = xi;

             

            }
        }

        private decimal generadorCongruenteMultiplicativo(decimal xi_1, decimal a, decimal m)
        {
            return (a * xi_1 ) % m;
        }


        private void mostrarVectorEstado(ArrayList vectorEstado)
        {
            foreach (var item in vectorEstado)
            {
                Console.WriteLine(item); // esto es para chequear nomas en la consola hay que ver desp como mostrar en la ventana
            }
            
        }

        //private int generadorCongruenteMixto(int xi_1, int a, int c, int m)
        //{

        //    return (a * xi_1 + c) % m;
        //}

    }
}
