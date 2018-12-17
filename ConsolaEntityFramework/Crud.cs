using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsolaEntityFramework
{
    public abstract class Crud
    {
        #region Persona
        public int AgregarPersona(Persona item)
        {
            using (AgendaEntities cnx = new AgendaEntities())
            {
                cnx.Persona.Add(item);
                cnx.SaveChanges();
                return item.Id;
            }
        }

        public void EditarPersona(Persona item)
        {
            using (AgendaEntities cnx = new AgendaEntities())
            {
                var original = cnx.Persona.Find(item.Id);
                original.Nombre = item.Nombre.Length > 0 ? item.Nombre : original.Nombre;
                original.Apellido = item.Apellido.Length > 0 ? item.Apellido : original.Apellido;
                original.Direccion = item.Direccion.Length > 0 ? item.Direccion : original.Direccion;
                cnx.Configuration.ValidateOnSaveEnabled = false; // Desactivar validación
                cnx.SaveChanges();
            }
        }

        public List<Persona> ObtenerPersonas()
        {
            using (AgendaEntities cnx = new AgendaEntities())
            {
                return cnx.Persona.ToList<Persona>();
            }
        }

        public void EliminarPersona(int id)
        {
            using (AgendaEntities cnx = new AgendaEntities())
            {
                var persona = cnx.Persona.Single(p => p.Id == id);
                if (persona.Telefono.Count > 0) // Si la persona tiene números de teléfono se deben eliminar
                    EliminarTelefonos(persona.Id);
                cnx.Persona.Remove(persona);
                cnx.SaveChanges();
            }
        }
        #endregion

        #region Teléfono
        public int AgregarTelefono(Telefono item)
        {
            using (AgendaEntities cnx = new AgendaEntities())
            {
                cnx.Telefono.Add(item);
                cnx.SaveChanges();
                return item.Id;
            }
        }

        public void EditarTelefono(Telefono item)
        {
            using (AgendaEntities cnx = new AgendaEntities())
            {
                var original = cnx.Telefono.Find(item.Id);
                original.Numero = item.Numero.Length > 0 ? item.Numero : original.Numero;
                cnx.Configuration.ValidateOnSaveEnabled = false; // Desactivar validación
                cnx.SaveChanges();
            }
        }

        public List<Telefono> ObtenerTelefonos(int IdPersona)
        {
            using (AgendaEntities cnx = new AgendaEntities())
            {
                return cnx.Telefono.Where(t => t.IdPersona == IdPersona).ToList<Telefono>();
            }
        }

        public void EliminarTelefono(int id)
        {
            using (AgendaEntities cnx = new AgendaEntities())
            {
                var telefono = cnx.Telefono.Single(p => p.Id == id);
                cnx.Telefono.Remove(telefono);
                cnx.SaveChanges();
            }
        }

        public void EliminarTelefonos(int idPersona)
        {
            using (AgendaEntities cnx = new AgendaEntities())
            {
                var tels = cnx.Telefono.Where(t => t.IdPersona == idPersona);
                foreach (var t in tels)
                {
                    cnx.Telefono.Remove(t);
                    cnx.SaveChanges();
                }
            }
        }
        #endregion

    }
}
