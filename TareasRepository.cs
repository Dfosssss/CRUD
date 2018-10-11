using System;
using System.Collections.Generic;
using System.Linq;

namespace Intento.Models
{
    public class TareasRepository : ITareasRepository
    {
        private static List<Tarea> db;
        static TareasRepository()
        {
            db = new List<Tarea>();
            /* db.Add(new Tarea()
            {
                ID=1,
                Descripcion ="Programar en asp",
                Prioridad = 1,
                Completado = false
            }
            );

            db.Add(new Tarea()
            {
                ID=2,
                Descripcion ="Debug en asp",
                Prioridad = 2,
                Completado = false
            }
            );*/
            db.Add(new Tarea(1,"Calando",1,false));
            db.Add(new Tarea(2,"Calando 2",2,false));
        }

        public List<Tarea> LeerTareas()
        {
            return db.ToList();
        }
        public Tarea LeerTareasPorID(int id)
        {
            return db.FirstOrDefault(t => t.ID==id);
        }

        public bool CrearTarea(Tarea model)
        {
            try{
	                if(db.Count==0){
	                    model.ID =1;
	                }else{
	                    model.ID = db.Max(p=> p.ID)+1;
	                }
	                db.Add(model);
	                return true;
	            }catch{
	                return false;
	            }  
        }

        public bool ActualizarTarea(Tarea tarea)
        {
            var t = db.FirstOrDefault(tar => tarea.ID == tar.ID);
            
            if(t==null)
                return false;

            t.Completado = tarea.Completado;
            t.Descripcion = tarea.Descripcion;
            t.Prioridad = tarea.Prioridad;
            return true;
        }

        public bool BorrarTarea(int id)
        {
            var t = db.FirstOrDefault(tar => id == tar.ID);
            
            if(t==null)
                return false;
            
            db.Remove(t);
            return true;
        }
    }

    public interface ITareasRepository
    {
        List<Tarea> LeerTareas();
        Tarea LeerTareasPorID(int id);
        bool CrearTarea(Tarea model);
        bool ActualizarTarea(Tarea tarea);
        bool BorrarTarea(int id);
    }
}