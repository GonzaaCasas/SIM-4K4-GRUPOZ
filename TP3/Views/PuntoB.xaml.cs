﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TP3.Mvvm;
using System.Threading;
using System.ComponentModel;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Ookii.Dialogs.Wpf;

namespace TP3.Views
{
    /// <summary>
    /// Interaction logic for PuntoB.xaml
    /// </summary>
    /// 


    public partial class PuntoB : Page
    {
        int muestra;
        int subintervalos;
        private List<decimal> numeros_aleatorios = new List<decimal>();
        private Gestor gestor;

        private static List<decimal> valores_variableAleatoriaExpNeg = new List<decimal>();
        private static List<decimal> valores_variableAleatoriaPoisson = new List<decimal>();
        private static List<decimal> valores_variableAleatoriaNormal = new List<decimal>();

        private static List<decimal> observadosUniforme = new List<decimal>();
        private static List<decimal> observadosExponencial = new List<decimal>();
        private static List<decimal> observadosPoisson = new List<decimal>();
        private static List<decimal> observadosNormal = new List<decimal>();
        private static List<decimal> esperadosUniforme = new List<decimal>();
        private static List<decimal> esperadosExponencial = new List<decimal>();
        private static List<decimal> esperadosPoisson = new List<decimal>();
        private static List<decimal> esperadosNormal = new List<decimal>();
        private static List<string> mediosExponencial = new List<string>();
        private static List<string> mediosPoisson = new List<string>();
        private static List<string> mediosNormal = new List<string>();

        private List<decimal> limites = new List<decimal>();
        private List<decimal> resultadosTest = new List<decimal>();
        bool cargado = false;
        DataTable tablaExcel;
        private Grafico grafico = null;

        public PuntoB()
        {
            InitializeComponent();
            gestor = new Gestor();
            cargado = true;

        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private bool validarCampos()
        {
            return true;
        }

        private void Txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (cargado)
            {
                //lo dejo por las dudas
            }
        }


        private void BtnExportar_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new VistaFolderBrowserDialog();
            dialog.Description = "Elija una carpeta para exportar la serie generada";
            dialog.UseDescriptionForTitle = true; // This applies to the Vista style dialog only, not the old dialog.

            if (!VistaFolderBrowserDialog.IsVistaFolderDialogSupported)
            {
                MessageBox.Show("Because you are not using Windows Vista or later, the regular folder browser dialog will be used. Please use Windows Vista to see the new dialog.", "Sample folder browser dialog");
            }

            if ((bool)dialog.ShowDialog())
            {
                //MessageBox.Show($"The selected folder was:{Environment.NewLine}{dialog.SelectedPath}", "Sample folder browser dialog");
                Gestor.ExportarExcel(dialog.SelectedPath, tablaExcel, "Serie generada punto B");
                MessageBox.Show("La serie de numeros generada se ha exportado en " + dialog.SelectedPath,
                    "Exportacion exitosa", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        #region botones para mostrar distribuciones

        private void BtnExp_OnClick(object sender, RoutedEventArgs e)
        {
            subintervalos = int.Parse(TxtSubintervalos.Text);
            if (validarCampos())
            {
                (observadosExponencial, esperadosExponencial, mediosExponencial) = Gestor.obtenerTodoExp(subintervalos);
                construirGrafico(observadosExponencial, esperadosExponencial, mediosExponencial.ToArray());
                ejecutarTestChi(2);
            }
        }

        private void BtnPoisson_OnClick(object sender, RoutedEventArgs e)
        {
            subintervalos = int.Parse(TxtSubintervalos.Text);
            if (validarCampos())
            {
                (observadosPoisson, esperadosPoisson, mediosPoisson) = Gestor.obtenerTodoPoisson(subintervalos);
                construirGrafico(observadosPoisson, esperadosPoisson, mediosPoisson.ToArray());
                ejecutarTestChi(1);
            }
        }

        private void BtnNormal_OnClick(object sender, RoutedEventArgs e)
        {
            subintervalos = int.Parse(TxtSubintervalos.Text);
            if (validarCampos())
            {
                (observadosNormal, esperadosNormal, mediosNormal) = Gestor.obtenerTodoNormal(subintervalos);
                construirGrafico(observadosNormal, esperadosNormal, mediosNormal.ToArray());
                ejecutarTestChi(0);
            }
        }

        #endregion

        private void ejecutarTestChi(int indice)
        {
            resultadosTest.Clear();
            resultadosTest = Gestor.test(this.subintervalos); // 0: normal | 1: poisson | 2: exp | 3: teorico


            lblJiObtenida.Content = "Chi Obtenido \n" + Math.Round(resultadosTest[indice], 4, MidpointRounding.AwayFromZero).ToString();
            lblJiTabulada.Content = "Chi Tabulado \n" + Math.Round(resultadosTest[3], 4, MidpointRounding.AwayFromZero).ToString();

            lblJiObtenida.Visibility = Visibility.Visible;
            lblJiTabulada.Visibility = Visibility.Visible;
            if (resultadosTest[indice] < resultadosTest[3])
            {
                lblAprobacion.Content = "H0 Aceptada";
                lblAprobacion.Foreground = Brushes.DarkGreen;
            }
            else
            {
                lblAprobacion.Content = "H0 Rechazada";
                lblAprobacion.Foreground = Brushes.Red;
            }
            lblAprobacion.Visibility = Visibility.Visible;
        }

        private void construirGrafico(List<decimal> observado, List<decimal> esperado, string[] intervalos)
        {
            grafico = new Grafico();
            dockPlot.Children.Clear();
            dockPlot.Children.Add(grafico);
            grafico.AgregarColeccion(esperado.ToArray(), "Esperado");
            grafico.AgregarColeccion(observado.ToArray(), "Observados");

            grafico.AgregarIntervalos(intervalos);
            grafico.Visible(true);
        }
    }


}
