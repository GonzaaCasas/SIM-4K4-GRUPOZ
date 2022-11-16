
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Controls;
using TP6.Models;
using TP6.Mvvm;

namespace TP6.Views {
	/// <summary>
	/// Interaction logic for HojaGrilla.xaml
	/// </summary>
	public partial class HojaGrilla : Page {
		public static DataTable grilla;

		public static bool tablaCreada = false;

		public HojaGrilla() {
			InitializeComponent();
			CargarTodo(Gestor.puntoA);

		}

		private void CargarTodo(bool flag) {
			if (flag) {
				DgvGrilla.DataContext = grilla;

			}
		}

		private static DataTable CrearTabla() {
			tablaCreada = true;
			DataTable tabla = new DataTable();
			tabla.Columns.Add("nEvento");

			//evento y reloj
			tabla.Columns.Add("reloj");
			tabla.Columns.Add("evento");
			tabla.Columns.Add("material");

			//llegadas
			tabla.Columns.Add("tEntreLlegadas");
			tabla.Columns.Add("proxLlegada");

			//servidor 1
			tabla.Columns.Add("estado1");
			tabla.Columns.Add("material1");
			tabla.Columns.Add("tAtencion1");
			tabla.Columns.Add("proxFinAtencion1");
			tabla.Columns.Add("cola1");

			//servidor 2
			tabla.Columns.Add("estado2");
			tabla.Columns.Add("material2");
			tabla.Columns.Add("tAtencion2");
			tabla.Columns.Add("proxFinAtencion2");
			tabla.Columns.Add("cola2");

			//servidor 3
			tabla.Columns.Add("estado3");
			tabla.Columns.Add("material3");
			tabla.Columns.Add("tAtencion3");
			tabla.Columns.Add("proxFinAtencion3");
			tabla.Columns.Add("cola3");

			//servidor 4
			tabla.Columns.Add("estado4");
			tabla.Columns.Add("material4");
			tabla.Columns.Add("tAtencion4");
			tabla.Columns.Add("proxFinAtencion4");
			tabla.Columns.Add("cola4");


			//servidor 5
			tabla.Columns.Add("estado5");
			tabla.Columns.Add("material5");
			tabla.Columns.Add("tAtencion5");
			tabla.Columns.Add("proxFinAtencion5");
			tabla.Columns.Add("cola5desde2");
			tabla.Columns.Add("cola5desde4");

			//tp6
			tabla.Columns.Add("proxFinEncastre");
			tabla.Columns.Add("valorT");
			tabla.Columns.Add("valorPico");

			tabla.Columns.Add("colaEncastre_ProductoDesdeS5");
			tabla.Columns.Add("colaEncastre_ProductoDesdeS3");


			//calculo
			tabla.Columns.Add("acumSolicitadas");
			tabla.Columns.Add("acumEnsamblados");
			tabla.Columns.Add("propRealizadosSolicitados");
			tabla.Columns.Add("promedioDuracionEnsamble");
			tabla.Columns.Add("promedioEnsamblesPorHora");


			tabla.Columns.Add("cantMaxCola1");
			tabla.Columns.Add("cantMaxCola2");
			tabla.Columns.Add("cantMaxCola3");
			tabla.Columns.Add("cantMaxCola4");
			tabla.Columns.Add("cantMaxCola5desde2");
			tabla.Columns.Add("cantMaxCola5desde4");


			tabla.Columns.Add("cantMaxColaEncastre");

			tabla.Columns.Add("tiempoAcumuladoEnEsperaSeccion1");
			tabla.Columns.Add("tiempoAcumuladoEnEsperaSeccion2");
			tabla.Columns.Add("tiempoAcumuladoEnEsperaSeccion3");
			tabla.Columns.Add("tiempoAcumuladoEnEsperaSeccion4");
			tabla.Columns.Add("tiempoAcumuladoEnEsperaSeccion5DesdeS2");
			tabla.Columns.Add("tiempoAcumuladoEnEsperaSeccion5DesdeS4");

			tabla.Columns.Add("tiempoAcumuladoOcupadoSeccion1");
			tabla.Columns.Add("tiempoAcumuladoOcupadoSeccion2");
			tabla.Columns.Add("tiempoAcumuladoOcupadoSeccion3");
			tabla.Columns.Add("tiempoAcumuladoOcupadoSeccion4");
			tabla.Columns.Add("tiempoAcumuladoOcupadoSeccion5");

			tabla.Columns.Add("promedioPermanenciaColaSeccion1");
			tabla.Columns.Add("promedioPermanenciaColaSeccion2");
			tabla.Columns.Add("promedioPermanenciaColaSeccion3");
			tabla.Columns.Add("promedioPermanenciaColaSeccion4");
			tabla.Columns.Add("promedioPermanenciaColaSeccion5DesdeS2");
			tabla.Columns.Add("promedioPermanenciaColaSeccion5DesdeS4");

			tabla.Columns.Add("porcentajeOcupacioSeccion1");
			tabla.Columns.Add("porcentajeOcupacioSeccion2");
			tabla.Columns.Add("porcentajeOcupacioSeccion3");
			tabla.Columns.Add("porcentajeOcupacioSeccion4");
			tabla.Columns.Add("porcentajeOcupacioSeccion5");

			tabla.Columns.Add("acumProductosEnCola");
			tabla.Columns.Add("promedioProductosEnCola");

			tabla.Columns.Add("acumProductosEnSistema");
			tabla.Columns.Add("promedioProductosEnSistema");

			tabla.Columns.Add("proporcionTiempoBloqueoA3");
			tabla.Columns.Add("proporcionTiempoBloqueoA5");



			return tabla;

		}

		public static void CargarFila(List<object> fila, int num) {
			if (!tablaCreada) {
				grilla = CrearTabla();
			}

			DataRow dr = grilla.NewRow();
			dr[0] = num + 1;

			FilaVectorEstado filaTabla = (FilaVectorEstado)fila[0];

			dr[1] = filaTabla.reloj;
			dr[2] = filaTabla.evento;
			dr[3] = filaTabla.material;

			dr[4] = filaTabla.tiempoEntreLlegadas;
			dr[5] = filaTabla.proximaLlegada;

			//s1
			dr[6] = filaTabla.estadoS1;
			dr[7] = filaTabla.materialS1;
			dr[8] = filaTabla.tiempoAtencionS1;
			dr[9] = filaTabla.proximoFinAtencionS1;
			dr[10] = filaTabla.colaS1;

			//s2
			dr[11] = filaTabla.estadoS2;
			dr[12] = filaTabla.materialS2;
			dr[13] = filaTabla.tiempoAtencionS2;
			dr[14] = filaTabla.proximoFinAtencionS2;
			dr[15] = filaTabla.colaS2;

			//s3
			dr[16] = filaTabla.estadoS3;
			dr[17] = filaTabla.materialS3;
			dr[18] = filaTabla.tiempoAtencionS3;
			dr[19] = filaTabla.proximoFinAtencionS3;
			dr[20] = filaTabla.colaS3;

			//s4
			dr[21] = filaTabla.estadoS4;
			dr[22] = filaTabla.materialS4;
			dr[23] = filaTabla.tiempoAtencionS4;
			dr[24] = filaTabla.proximoFinAtencionS4;
			dr[25] = filaTabla.colaS4;

			//s5
			dr[26] = filaTabla.estadoS5;
			dr[27] = filaTabla.materialS5;
			dr[28] = filaTabla.tiempoAtencionS5;
			dr[29] = filaTabla.proximoFinAtencionS5;
			dr[30] = filaTabla.colaS5_ProductoDesdeS2;
			dr[31] = filaTabla.colaS5_ProductoDesdeS4;


			//tp6

			dr[32] = filaTabla.proximoFinEncastre;
			dr[33] = filaTabla.valort;
			dr[34] = Math.Round(filaTabla.picox, 4, MidpointRounding.AwayFromZero).ToString();

			dr[35] = filaTabla.colaEncastre_ProductoDesdeS5;
			dr[36] = filaTabla.colaEncastre_ProductoDesdeS3;

			//calculo
			Calculo calculoTabla = (Calculo)fila[1];

			dr[37] = calculoTabla.acumSolicitadas;
			dr[38] = calculoTabla.acumEnsamblados;
			dr[39] = Math.Round(calculoTabla.propRealizadosSolicitados, 2, MidpointRounding.AwayFromZero).ToString();
			dr[40] = Math.Round(calculoTabla.promedioDuracionEnsamble, 2, MidpointRounding.AwayFromZero).ToString();
			dr[41] = Math.Round(calculoTabla.promedioEnsamblesPorHora, 2, MidpointRounding.AwayFromZero).ToString();

			dr[42] = calculoTabla.cantMaxCola1;
			dr[43] = calculoTabla.cantMaxCola2;
			dr[44] = calculoTabla.cantMaxCola3;
			dr[45] = calculoTabla.cantMaxCola4;
			dr[46] = calculoTabla.cantMaxColaS5_ProductoDesdeS2;
			dr[47] = calculoTabla.cantMaxColaS5_ProductoDesdeS4;



			dr[48] = calculoTabla.cantMaxColaEncastre;

			dr[49] = calculoTabla.tiempoAcumuladoEnEsperaSeccion1;
			dr[50] = calculoTabla.tiempoAcumuladoEnEsperaSeccion2;
			dr[51] = calculoTabla.tiempoAcumuladoEnEsperaSeccion3;
			dr[52] = calculoTabla.tiempoAcumuladoEnEsperaSeccion4;
			dr[53] = calculoTabla.tiempoAcumuladoEnEsperaSeccion5DesdeS2;
			dr[54] = calculoTabla.tiempoAcumuladoEnEsperaSeccion5DesdeS4;

			dr[55] = calculoTabla.tiempoAcumuladoOcupadoSeccion1;
			dr[56] = calculoTabla.tiempoAcumuladoOcupadoSeccion2;
			dr[57] = calculoTabla.tiempoAcumuladoOcupadoSeccion3;
			dr[58] = calculoTabla.tiempoAcumuladoOcupadoSeccion4;
			dr[59] = calculoTabla.tiempoAcumuladoOcupadoSeccion5;

			dr[60] = Math.Round(calculoTabla.promedioPermanenciaColaSeccion1, 2, MidpointRounding.AwayFromZero).ToString();
			dr[61] = Math.Round(calculoTabla.promedioPermanenciaColaSeccion2, 2, MidpointRounding.AwayFromZero).ToString();
			dr[62] = Math.Round(calculoTabla.promedioPermanenciaColaSeccion3, 2, MidpointRounding.AwayFromZero).ToString();
			dr[63] = Math.Round(calculoTabla.promedioPermanenciaColaSeccion4, 2, MidpointRounding.AwayFromZero).ToString();
			dr[64] = Math.Round(calculoTabla.promedioPermanenciaColaSeccion5DesdeS2, 2, MidpointRounding.AwayFromZero).ToString();
			dr[65] = Math.Round(calculoTabla.promedioPermanenciaColaSeccion5DesdeS4, 2, MidpointRounding.AwayFromZero).ToString();



			//dr[57] = Math.Round(calculoTabla.promedioPermanenciaColaSeccion5, 2, MidpointRounding.AwayFromZero).ToString(); 

			dr[66] = Math.Round(calculoTabla.porcentajeOcupacioSeccion1, 2, MidpointRounding.AwayFromZero).ToString();
			dr[67] = Math.Round(calculoTabla.porcentajeOcupacioSeccion2, 2, MidpointRounding.AwayFromZero).ToString();
			dr[68] = Math.Round(calculoTabla.porcentajeOcupacioSeccion3, 2, MidpointRounding.AwayFromZero).ToString();
			dr[69] = Math.Round(calculoTabla.porcentajeOcupacioSeccion4, 2, MidpointRounding.AwayFromZero).ToString();
			dr[70] = Math.Round(calculoTabla.porcentajeOcupacioSeccion5, 2, MidpointRounding.AwayFromZero).ToString();

			dr[71] = calculoTabla.acumProductosEnCola;
			dr[72] = Math.Round(calculoTabla.promedioProductosEnCola, 2, MidpointRounding.AwayFromZero).ToString();

			dr[73] = calculoTabla.acumProductosEnSistema;
			dr[74] = Math.Round(calculoTabla.promedioProductosEnSistema, 2, MidpointRounding.AwayFromZero).ToString();

			dr[75] = Math.Round(calculoTabla.proporcionTiempoBloqueoA3, 2, MidpointRounding.AwayFromZero).ToString();
			dr[76] = Math.Round(calculoTabla.proporcionTiempoBloqueoA5, 2, MidpointRounding.AwayFromZero).ToString();



			//for (int i = 0; i <= 6; i++)
			//{
			//    Actividad actividad = (Actividad)fila[i];

			//    dr[(i * 5) + 1] = Math.Round(actividad.d, 4, MidpointRounding.AwayFromZero).ToString();
			//    dr[(i * 5) + 2] = Math.Round(actividad.mi, 4, MidpointRounding.AwayFromZero).ToString();
			//    dr[(i * 5) + 3] = Math.Round(actividad.mf, 4, MidpointRounding.AwayFromZero).ToString();
			//    dr[(i * 5) + 4] = Math.Round(actividad.mf_tarde, 4, MidpointRounding.AwayFromZero).ToString();
			//    dr[(i * 5) + 5] = Math.Round(actividad.mi_tarde, 4, MidpointRounding.AwayFromZero).ToString();
			//}

			//Calculo calculo = (Calculo)fila[7];
			//dr[36] = Math.Round(calculo.mediaDuracion, 4, MidpointRounding.AwayFromZero).ToString();
			//dr[37] = Math.Round(calculo.std, 4, MidpointRounding.AwayFromZero).ToString();
			//dr[38] = Math.Round(calculo.min, 4, MidpointRounding.AwayFromZero).ToString();
			//dr[39] = Math.Round(calculo.max, 4, MidpointRounding.AwayFromZero).ToString();
			//dr[40] = Math.Round(calculo.probDias, 4, MidpointRounding.AwayFromZero).ToString();
			//dr[41] = Math.Round(calculo.fechaFijar, 4, MidpointRounding.AwayFromZero).ToString();
			//dr[42] = calculo.caminoCritico;

			grilla.Rows.Add(dr);
		}


		//    for (int i = 0; i< 3; i++)
		//    {
		//        DataRow _row = tabla.NewRow();
		//_row[0] = "1";
		//        _row[1] = "20";
		//        _row[2] = "15";
		//        _row[3] = "10";
		//        _row[4] = "5";
		//        _row[5] = "0";

		//        _row[6] = "1";
		//        _row[7] = "20";
		//        _row[8] = "15";
		//        _row[9] = "10";
		//        _row[10] = "5";
		//        _row[11] = "0";

		//        _row[12] = "1";
		//        _row[13] = "20";
		//        _row[14] = "15";
		//        _row[15] = "10";
		//        _row[16] = "5";
		//        _row[17] = "0";

		//        tabla.Rows.Add(_row);
		//    }

	}
}
