using System;

namespace Intento.Models
{
    public class Tarea
    {
        public int ID {get;set;}
        public string Descripcion {get;set;}
        public int Prioridad {get;set;}
        public bool Completado {get;set;}

        public Tarea()
        {

        }

        public Tarea(int ID, string Descripcion, int Prioridad, bool Completado)
        {
            this.ID = ID;
            this.Descripcion = Descripcion;
            this.Prioridad = Prioridad;
            this.Completado = Completado;
        }
        
    }
}