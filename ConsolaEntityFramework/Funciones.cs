using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolaEntityFramework
{
    class Funciones : Crud
    {
        public void MostrarRegistros()
        {
            var personas = ObtenerPersonas();
            Console.WriteLine("--REGISTROS--");
            foreach (var p in personas)
            {
                Console.WriteLine($"Id: {p.Id}");
                Console.WriteLine($"Nombres: {p.Nombre}");
                Console.WriteLine($"Apellidos: {p.Apellido}");
                Console.WriteLine($"Dirección: {p.Direccion}");
                Console.WriteLine($"Activo: {p.Activo}");
                Console.WriteLine("Teléfonos:");
                var telefonos = ObtenerTelefonos(p.Id);
                foreach (var t in telefonos)
                {
                    Console.WriteLine(t.Numero);
                }
                Console.WriteLine("");
            }
        }

        public void MostrarDetallePersona(int id)
        {
            var persona = ObtenerPersonas().Single(p => p.Id == id);
            if (persona != null)
            {
                Console.WriteLine($"Id: {persona.Id}");
                Console.WriteLine($"Nombres: {persona.Nombre}");
                Console.WriteLine($"Apellidos: {persona.Apellido}");
                Console.WriteLine($"Dirección: {persona.Direccion}");
                Console.WriteLine($"Activo: {persona.Activo}");
                var telefonos = ObtenerTelefonos(persona.Id);
                foreach (var t in telefonos)
                {
                    Console.WriteLine(t.Numero);
                }
            }
        }

        public void MostrarMenu()
        {
            Random rnd = new Random();
            Console.Clear();
            Console.ForegroundColor = (ConsoleColor)rnd.Next(1, 15);
            Console.WriteLine("Agenda de contactos");
            Console.WriteLine("__Menú_________________");
            Console.WriteLine("|1. Agregar Registro   |");
            Console.WriteLine("|2. Modificar Registro |");
            Console.WriteLine("|3. Mostrar Registros  |");
            Console.WriteLine("|4. Eliminar Registro  |");
            Console.WriteLine("------------------------");
        }

        public void IngresarInformacion(ref string nombre, ref string apellido, ref string direccion)
        {
            Console.WriteLine("Nombres:");
            nombre = Console.ReadLine();
            Console.WriteLine("Apellidos:");
            apellido = Console.ReadLine();
            Console.WriteLine("Dirección:");
            direccion = Console.ReadLine();
        }

    }
}
