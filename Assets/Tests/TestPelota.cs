using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestPelota
    {

        /// <summary>
        /// La pelotica de los cojones.
        /// </summary>
        GameObject Pelota {get; set;}

        /// <summary>
        /// El MonoBehaviour de la pelota
        /// </summary>
        PelotaController Controlador {get; set;}

        [SetUp]
        public void SetUpPelota()
        {
            // coge la pelota de los cojones
            Pelota = GameObject.FindGameObjectWithTag("jugador");

            // coge el script de la pelota
            Controlador = Pelota.GetComponent<PelotaController>();

        } // SetUpPelota

        [Test]
        public void AumentarUnPunto()
        {
            // cuantos puntos habían antes
            int PuntosAntes = Controlador.Puntos;

            // aumenta un punto
            Controlador.AumentarPuntos();

            // debe haber aumentado solo un punto
            Assert.True(Controlador.Puntos-1==PuntosAntes);
        }


        [Test]
        public void AumentarTresPuntos() 
        {
            int PuntosAntes = Controlador.Puntos;
            Controlador.AumentarPuntos(3);
            Assert.True(Controlador.Puntos-3==PuntosAntes);
        }


        [Test]
        public void ConsultarRecord()
        {
            Assert.True(PlayerPrefs.HasKey("record"));
        }

        [Test]
        public void PelotaHasRecordField()
        {
            Assert.Zero(Controlador.Record);
        }


    } // class
} // namespace
