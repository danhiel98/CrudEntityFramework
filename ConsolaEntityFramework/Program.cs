using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolaEntityFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, int> telefonosPersona = new Dictionary<int, int>();
            int opcion = 0, cantidad = 0;
            string nombre = "", apellido = "", direccion = "", numeroTelefono = "";
            short id = 0;
            Funciones fnc = new Funciones();
            
            fnc.MostrarMenu();

            if (int.TryParse(Console.ReadLine(), out opcion))
            {
                Console.Clear();
                switch (opcion)
                {
                    case 1:
                        Console.WriteLine("1. Agregar Registro");
                        fnc.IngresarInformacion(ref nombre, ref apellido, ref direccion);
                        id = (short)fnc.AgregarPersona(new Persona() { Nombre = nombre, Apellido = apellido, Direccion = direccion });
                        Console.WriteLine($"¡Registro con id {id} agregado correctamente!");
                        Console.WriteLine("¿Cuántos números de teléfono desea agregar?");
                        cantidad = int.Parse(Console.ReadLine());
                        if (cantidad > 0)
                        {
                            for (int i = 0; i < cantidad; i++)
                            {
                                Console.WriteLine("Número:");
                                numeroTelefono = Console.ReadLine();
                                fnc.AgregarTelefono(new Telefono() { IdPersona = id, Numero = numeroTelefono });
                            }
                            Console.WriteLine("¡Números de teléfono agregados!");
                        }
                        Console.WriteLine("¡Terminado!");
                        break;
                    case 2:
                        Console.WriteLine("2. Editar Registro");
                        cantidad = fnc.ObtenerPersonas().Count;
                        Console.WriteLine($"Hay {cantidad} personas, introduzca el id de una para editar sus datos");
                        id = Int16.Parse(Console.ReadLine());
                        var persona = fnc.ObtenerPersonas().Single(p => p.Id == id);
                        if (persona != null)
                        {
                            var telefonos = fnc.ObtenerTelefonos(persona.Id);
                            Console.WriteLine("Registro a Editar:");
                            fnc.MostrarDetallePersona(persona.Id);
                            Console.WriteLine("===========================");
                            opcion = 1;
                            if (telefonos.Count > 0)
                            {
                                Console.WriteLine("¿Qué desea hacer?");
                                Console.WriteLine("1. Editar la información general");
                                Console.WriteLine("2. Agregar número de teléfono");
                                Console.WriteLine("3. Editar/Eliminar un número de teléfono");
                                opcion = int.Parse(Console.ReadLine());
                            }

                            switch (opcion)
                            {
                                case 1:
                                    fnc.IngresarInformacion(ref nombre, ref apellido, ref direccion);
                                    fnc.EditarPersona(new Persona() { Id = id, Nombre = nombre, Apellido = apellido, Direccion = direccion });
                                    break;
                                case 2:
                                    Console.WriteLine("Ingrese el número de teléfono:");
                                    numeroTelefono = Console.ReadLine();
                                    fnc.AgregarTelefono(new Telefono() { IdPersona = id, Numero = numeroTelefono });
                                    break;
                                case 3:
                                    Console.WriteLine("Seleccione el teléfono a editar:");
                                    int i = 0;
                                    foreach (var t in telefonos)
                                    {
                                        telefonosPersona.Add(++i, t.Id);
                                        Console.WriteLine($"{i} - {t.Numero}");
                                    }
                                    opcion = int.Parse(Console.ReadLine());
                                    if (telefonosPersona.Where(t => t.Key == opcion) != null)
                                    {
                                        Console.WriteLine("Introduzca el nuevo número (dejar en banco para eliminar):");
                                        numeroTelefono = Console.ReadLine();
                                        if (numeroTelefono.Length > 0)
                                            fnc.EditarTelefono(new Telefono() { Id = telefonosPersona[opcion], IdPersona = id, Numero = numeroTelefono });
                                        else
                                            fnc.EliminarTelefono(telefonosPersona[opcion]);
                                    }
                                    break;
                                default:
                                    break;
                            }
                            Console.WriteLine("¡Registro Actualizado!");
                        }
                        break;
                    case 3:
                        fnc.MostrarRegistros();
                        break;
                    case 4:
                        Console.WriteLine("4. Eliminar Registro");
                        cantidad = fnc.ObtenerPersonas().Count;
                        Console.WriteLine($"Hay {cantidad} personas, introduzca el id de una para editar sus datos");
                        id = Int16.Parse(Console.ReadLine());
                        var person = fnc.ObtenerPersonas().SingleOrDefault(p => p.Id == id);
                        if (person != null)
                        {
                            Console.WriteLine("¿Seguro que desea eliminar el siguiente registro? S/N");
                            fnc.MostrarDetallePersona(person.Id);
                            var opc = Console.ReadLine();
                            switch (opc)
                            {
                                case "S":
                                    fnc.EliminarPersona(id);
                                    Console.WriteLine("¡Registro Eliminado!");
                                    break;
                                case "N":
                                    Console.WriteLine("No se eliminó el registro");
                                    break;
                                default:
                                    Console.WriteLine("Opción inválida");
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("No se encontró el registro");
                        }
                        break;
                    default:
                        Console.WriteLine("Opción Inválida");
                        break;
                }
                Console.ReadLine();
                Main(null);
            }
            Console.ReadLine();
        }        

    }
}
