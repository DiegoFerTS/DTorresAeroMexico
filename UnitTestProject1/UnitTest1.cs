using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            List<ML.Usuario> listaUsuario = new List<ML.Usuario>();
            ML.Usuario usuario1 = new ML.Usuario();
            usuario1.UserName = "Hola";
            usuario1.Password = "123";

            ML.Usuario usuario2 = new ML.Usuario();
            usuario2.UserName = "hOla";
            usuario2.Password = "23";

            ML.Usuario usuario3 = new ML.Usuario();
            usuario3.UserName = "hoLa";
            usuario3.Password = "3";

            listaUsuario.Add(usuario1);
            listaUsuario.Add(usuario2);
            listaUsuario.Add(usuario3);

            List<ML.Pasajero> listaPasajero = new List<ML.Pasajero>();
            ML.Pasajero pasajero1 = new ML.Pasajero();
            pasajero1.Nombre = "Roberto";
            pasajero1.Apellidos = "Lopez";

            ML.Pasajero pasajero2 = new ML.Pasajero();
            pasajero2.Nombre = "Luis";
            pasajero2.Apellidos = "Perez";

            ML.Pasajero pasajero3 = new ML.Pasajero();
            pasajero3.Nombre = "Alexis";
            pasajero3.Apellidos = "Rodriguez";

            listaPasajero.Add(pasajero1);
            listaPasajero.Add(pasajero2);
            listaPasajero.Add(pasajero3);

            for (int i = 0; i < listaPasajero.Count; i++)
            {
                Console.WriteLine("Usuario:" + listaUsuario[i].UserName);
                Console.WriteLine("Password:" + listaUsuario[i].Password);
                Console.WriteLine("Nombre:" + listaPasajero[i].Nombre);
                Console.WriteLine("Apellidos:" + listaPasajero[i].Apellidos);
                Console.WriteLine("\n");
            }

            Assert.IsNotNull(listaPasajero);
        }
    }
}
