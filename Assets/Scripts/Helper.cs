using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ficheros para trabajos cómodos
namespace Helper
{

   // manejo de cuestiones numéricas
   // el System.Random de mono no sirve, hay que hacerlo con UnityEngine
   public static class N
   {
      /// <summary>
      /// Devuelve un número aletario en el intervalo dado.
      /// </summary>
      /// <param name="min">del menos</param>
      /// <param name="max">al mayor</param>
      /// <returns>un float aleatorio en el intervalo dado</returns>
      public static float Random(float min, float max) =>
         UnityEngine.Random.Range(min, max);

      /// <summary>
      /// Devuelve un float dado entre un número y su opuesto
      /// </summary>
      /// <param name="valor"></param>
      /// <returns></returns>
      public static float Random(float valor) =>
         valor > 0 ?
            Random(valor * -1, valor):
            Random(valor, valor * -1);
            

      /// <summary>
      /// Devuelve un número aletario en el intervalo dado.
      /// </summary>
      /// <param name="min">del menos</param>
      /// <param name="max">al mayor</param>
      /// <returns>un float aleatorio en el intervalo dado</returns>
      public static int Random(int min , int max) =>
         UnityEngine.Random.Range(min, max);

   } // class


   // la clase que maneja las gemas
   public static class Gem
   {
      // mándala para un punto aleatorio del mapa
      // la deja caer de aire
      public static void RandomizePosition(GameObject gema) =>
         gema.transform.position =
            new Vector3(N.Random(4.5f), 9f, N.Random(4.5f));

      // cambia su forma aleatoriamente
      public static void RandomizeShape(GameObject gema) =>
         gema.transform.localScale =
            new Vector3(N.Random(0.1f, 0.3f), N.Random(0.1f, 0.3f), N.Random(0.1f, 0.3f));

      // FIXME fusionar con los métodos anteriores
      // tira una gema completamente aleatorizada
      public static void LanzarGema(GameObject gema)
      {
         RandomizePosition(gema);
         RandomizeShape(gema);
      }


   } // Gem

} // Helper