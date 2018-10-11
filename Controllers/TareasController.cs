using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Intento.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Intento.Controllers
{
    public class TareasController : Controller
    {
        // GET: Tareas
        ITareasRepository repo;

        public TareasController()
        {
            repo = new SqliteTareasRepository();
        }
        public ActionResult Index()
        {
            var model = repo.LeerTareas();
            return View(model);
        }

        // GET: Tareas/Details/5
        public ActionResult Details(int id)
        {
            var model = repo.LeerTareasPorID(id);
            return View(model);
        }

        // GET: Tareas/Create
        public ActionResult Create()
        {
            var model = new Tarea();
            return View(model);
        }

        // POST: Tareas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tarea model)
        {
            try
            {
                // TODO: Add insert logic here
                var resultado = repo.CrearTarea(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Tareas/Edit/5
        public ActionResult Edit(int id)
        {
             var model = repo.LeerTareasPorID(id);
             if(model==null)
			    return NotFound();
             return View(model);
        }

        // POST: Tareas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Tarea model)
        {
            var tarea = repo.LeerTareasPorID(model.ID);
            if(model==null)
	            return NotFound();
            try
            {
                // TODO: Add update logic here
                tarea.ID = model.ID;
                tarea.Descripcion = model.Descripcion;
                tarea.Prioridad = model.Prioridad;
                tarea.Completado = model.Completado;
                var resultado = repo.ActualizarTarea(tarea);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Tareas/Delete/5
        public ActionResult Delete(int id)
        {
            var model = repo.LeerTareasPorID(id);
            if(model==null)
                return NotFound();
            return View(model);
        }

        // POST: Tareas/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            
            try
            {
                // TODO: Add delete logic here
                var resultado = repo.BorrarTarea(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}