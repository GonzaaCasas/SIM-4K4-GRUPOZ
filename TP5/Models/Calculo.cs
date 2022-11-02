using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP5.Models
{
    internal class Calculo
    {

        public int acumSolicitadas { get; set; }

        public int acumEnsamblados { get; set; }

        public decimal propRealizadosSolicitados { get; set; }

        public decimal promedioDuracionEnsamble { get; set; }

        public decimal reloj { get; set; }

        public List<decimal> ensamblesPorHora { get; set; }

        public decimal promedioEnsamblesPorHora { get; set; }

        public decimal cantMaxCola1 { get; set; }
        public decimal cantMaxCola2 { get; set; }
        public decimal cantMaxCola3 { get; set; }
        public decimal cantMaxCola4 { get; set; }
        public decimal cantMaxCola5 { get; set; }

        public decimal cantMaxColaEncastre { get; set; }

        public decimal tiempoAcumuladoEnEsperaSeccion1 { get; set; }
        public decimal tiempoAcumuladoEnEsperaSeccion2 { get; set; }
        public decimal tiempoAcumuladoEnEsperaSeccion3 { get; set; }
        public decimal tiempoAcumuladoEnEsperaSeccion4 { get; set; }
        public decimal tiempoAcumuladoEnEsperaSeccion5 { get; set; }

        public decimal tiempoAcumuladoOcupadoSeccion1 { get; set; }
        public decimal tiempoAcumuladoOcupadoSeccion2 { get; set; }
        public decimal tiempoAcumuladoOcupadoSeccion3 { get; set; }
        public decimal tiempoAcumuladoOcupadoSeccion4 { get; set; }
        public decimal tiempoAcumuladoOcupadoSeccion5 { get; set; }

        public decimal promedioPermanenciaColaSeccion1 { get; set; }
        public decimal promedioPermanenciaColaSeccion2 { get; set; }
        public decimal promedioPermanenciaColaSeccion3 { get; set; }
        public decimal promedioPermanenciaColaSeccion4 { get; set; }
        public decimal promedioPermanenciaColaSeccion5 { get; set; }

        public decimal porcentajeOcupacioSeccion1 { get; set; }
        public decimal porcentajeOcupacioSeccion2 { get; set; }
        public decimal porcentajeOcupacioSeccion3 { get; set; }
        public decimal porcentajeOcupacioSeccion4 { get; set; }
        public decimal porcentajeOcupacioSeccion5 { get; set; }

        public decimal acumProductosEnCola { get; set; }
        public decimal promedioProductosEnCola { get; set; }

        public decimal acumProductosEnSistema { get; set; }
        public decimal promedioProductosEnSistema { get; set; }



        public Calculo()
        {
            this.ensamblesPorHora = new List<decimal>(new decimal[24]);
        }

        public Calculo(Calculo calculoAnterior)
        {
            this.acumSolicitadas = calculoAnterior.acumSolicitadas ;

            this.acumEnsamblados = calculoAnterior.acumEnsamblados;

            this.tiempoAcumuladoEnEsperaSeccion1 = calculoAnterior.tiempoAcumuladoEnEsperaSeccion1;
            this.tiempoAcumuladoEnEsperaSeccion2 = calculoAnterior.tiempoAcumuladoEnEsperaSeccion2;
            this.tiempoAcumuladoEnEsperaSeccion3 = calculoAnterior.tiempoAcumuladoEnEsperaSeccion3;
            this.tiempoAcumuladoEnEsperaSeccion4 = calculoAnterior.tiempoAcumuladoEnEsperaSeccion4;
            this.tiempoAcumuladoEnEsperaSeccion5 = calculoAnterior.tiempoAcumuladoEnEsperaSeccion5;

            this.tiempoAcumuladoOcupadoSeccion1 = calculoAnterior.tiempoAcumuladoOcupadoSeccion1;
            this.tiempoAcumuladoOcupadoSeccion2 = calculoAnterior.tiempoAcumuladoOcupadoSeccion2;
            this.tiempoAcumuladoOcupadoSeccion3 = calculoAnterior.tiempoAcumuladoOcupadoSeccion3;
            this.tiempoAcumuladoOcupadoSeccion4 = calculoAnterior.tiempoAcumuladoOcupadoSeccion4;
            this.tiempoAcumuladoOcupadoSeccion5 = calculoAnterior.tiempoAcumuladoOcupadoSeccion5;

            this.acumProductosEnCola = calculoAnterior.acumProductosEnCola;
            this.acumProductosEnSistema = calculoAnterior.acumProductosEnSistema;

            this.reloj = calculoAnterior.reloj ;

            this.ensamblesPorHora = calculoAnterior.ensamblesPorHora;

    }


        public void calcularProporcionRealizadosSolicitados()
        {
            if (this.acumSolicitadas != 0)
            {
                this.propRealizadosSolicitados = (decimal)this.acumEnsamblados / (decimal)this.acumSolicitadas;

            }


        }

        public void calcularPromedioDuracionEnsamble()
        {
            if (this.acumEnsamblados != 0)
            {
                this.promedioDuracionEnsamble = this.reloj / this.acumEnsamblados;

            }

        }

        public void calcularPromedioEnsamblesPorHora()
        {
            if (ensamblesPorHora != null)
            {
                this.promedioEnsamblesPorHora = this.ensamblesPorHora.Sum() / 24;

            }
        }

        public void calcularPermanenciaColaSecciones(List<Servidor> servidores)
        {
            if (servidores[0].cantClientesPasaronPorCola != 0)
            {
                promedioPermanenciaColaSeccion1 = this.tiempoAcumuladoEnEsperaSeccion1 / servidores[0].cantClientesPasaronPorCola;

            }

            if (servidores[1].cantClientesPasaronPorCola != 0)
            {
              promedioPermanenciaColaSeccion2 = this.tiempoAcumuladoEnEsperaSeccion2 / servidores[1].cantClientesPasaronPorCola;


            }
            if (servidores[2].cantClientesPasaronPorCola != 0)
            {
                promedioPermanenciaColaSeccion3 = this.tiempoAcumuladoEnEsperaSeccion1 / servidores[2].cantClientesPasaronPorCola;

            }
            if (servidores[3].cantClientesPasaronPorCola != 0)
            {
                promedioPermanenciaColaSeccion4 = this.tiempoAcumuladoEnEsperaSeccion1 / servidores[3].cantClientesPasaronPorCola;

            }
            if (servidores[4].cantClientesPasaronPorCola != 0)
            {
                promedioPermanenciaColaSeccion5 = this.tiempoAcumuladoEnEsperaSeccion1 / servidores[4].cantClientesPasaronPorCola;

            }


        }

        public void calcularPorcentajeOcupacionSecciones()
        {

            this.porcentajeOcupacioSeccion1 = (this.tiempoAcumuladoOcupadoSeccion1 / this.reloj) * 100;
            this.porcentajeOcupacioSeccion2 = (this.tiempoAcumuladoOcupadoSeccion2 / this.reloj) * 100;
            this.porcentajeOcupacioSeccion3 = (this.tiempoAcumuladoOcupadoSeccion3 / this.reloj) * 100;
            this.porcentajeOcupacioSeccion4 = (this.tiempoAcumuladoOcupadoSeccion4 / this.reloj) * 100;
            this.porcentajeOcupacioSeccion5 = (this.tiempoAcumuladoOcupadoSeccion5 / this.reloj) * 100;

        }

        public  void determinarCantMaxColas(List<Servidor> servidores, Queue<Cliente> productosProvenientesSeccion3, Queue<Cliente> productosProvenientesSeccion5 )
        {
            this.cantMaxCola1 = servidores[0].maximoCola;
            this.cantMaxCola2 = servidores[1].maximoCola;
            this.cantMaxCola3 = servidores[2].maximoCola;
            this.cantMaxCola4 = servidores[3].maximoCola;
            this.cantMaxCola5 = servidores[4].maximoCola;

            this.cantMaxColaEncastre = productosProvenientesSeccion3.Count + productosProvenientesSeccion5.Count > this.cantMaxColaEncastre ? productosProvenientesSeccion3.Count + productosProvenientesSeccion5.Count : this.cantMaxColaEncastre;
        }

        public  void calcularProductosEnCola(decimal cantEventos)
        {
            this.promedioProductosEnCola = this.acumProductosEnCola / cantEventos;
        }

        public  void calcularProductosEnSistemas(decimal cantEventos)
        {
            this.promedioProductosEnSistema = this.acumProductosEnSistema / cantEventos;
        }

    }
}
