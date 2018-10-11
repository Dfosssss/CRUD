

using System.Collections.Generic;
using Intento.Models;
using Microsoft.Data.Sqlite;

namespace Intento
{
    public class SqliteTareasRepository : ITareasRepository
    {
        private readonly string connection;

        public SqliteTareasRepository()
        {
            connection = "Filename=app.db";
        }

        public int ExecuteCMD(SqliteCommand cmd)
        {
            using(var con = new SqliteConnection(connection))
            {
                cmd.Connection = con;
                cmd.Connection.Open();
                var inserted = cmd.ExecuteNonQuery();
                return inserted;
            }
        }

        public bool ActualizarTarea(Tarea tarea)
        {
            var cmd = new SqliteCommand("UPDATE Tareas SET Descripcion = @Descripcion,"+
                                        " Prioridad = @Prioridad,"+
                                        " Completado = @Completado"+
                                        " WHERE ID = @ID");
            cmd.Parameters.AddWithValue("@ID", tarea.ID);
            cmd.Parameters.AddWithValue("@Descripcion", tarea.Descripcion);
            cmd.Parameters.AddWithValue("@Prioridad", tarea.Prioridad);
            cmd.Parameters.AddWithValue("@Completado", (tarea.Completado ? 1 : 0));

            ExecuteCMD(cmd);
            /*using(var con = new SqliteConnection(connection))
            {
                cmd.Connection = con;
                cmd.Connection.Open();
                var inserted = cmd.ExecuteNonQuery();
            }*/
            return true;
        }

        public bool BorrarTarea(int id)
        {
            var cmd = new SqliteCommand("DELETE FROM Tareas WHERE ID = @ID");
            cmd.Parameters.AddWithValue("@ID",id);

            ExecuteCMD(cmd);
            /*using(var con = new SqliteConnection(connection))
            {
                cmd.Connection = con;
                cmd.Connection.Open();
                var inserted = cmd.ExecuteNonQuery();
            }*/
            return true;
        }

        public bool CrearTarea(Tarea model)
        {
            var cmd = new SqliteCommand("INSERT INTO Tareas (Descripcion, Prioridad, Completado)"+
                                        " VALUES (@Descripcion, @Prioridad, @Completado)");
            cmd.Parameters.AddWithValue("@Descripcion", model.Descripcion);
            cmd.Parameters.AddWithValue("@Prioridad", model.Prioridad);
            cmd.Parameters.AddWithValue("@Completado", (model.Completado ? 1 : 0));

            using(var con = new SqliteConnection(connection))
            {
                cmd.Connection = con;
                cmd.Connection.Open();
                var inserted = cmd.ExecuteNonQuery();
            }
            return true;
        }

        private Tarea ParseTarea(SqliteDataReader reader)
        {
            var tarea = new Tarea();
            unchecked
            {
                tarea.ID= (int)reader.GetInt64(0);
                tarea.Descripcion = reader.GetString(1);
                tarea.Prioridad = (int)reader.GetInt64(2);
                tarea.Completado = (reader.GetInt64(3)==1);
            }
            return tarea;
        }

        private string SELECTTAREA = "SELECT ID, Descripcion, Prioridad, Completado FROM Tareas";

        public List<Tarea> LeerTareas()
        {
            var tareas = new List<Tarea>();
            //var cmd = new SqliteCommand("SELECT ID, Descripcion, Prioridad, Completado FROM Tareas");
            var cmd = new SqliteCommand(SELECTTAREA);

            using(var con = new SqliteConnection(connection))
            {
                cmd.Connection = con;
                cmd.Connection.Open();
                using(var reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        var tarea = ParseTarea(reader); //var tarea = new Tarea();
                        /* unchecked
                        {
                            tarea.ID= (int)reader.GetInt64(0);
                            tarea.Descripcion = reader.GetString(1);
                            tarea.Prioridad = (int)reader.GetInt64(2);
                            tarea.Completado = (reader.GetInt64(3)==1);
                        }*/
                        tareas.Add(tarea);
                    }
                }
            }
            return tareas;
        }

        public Tarea LeerTareasPorID(int id)
        {
            //var cmd = new SqliteCommand("SELECT ID, Descripcion, Prioridad, Completado FROM Tareas WHERE ID = @ID");
            var cmd = new SqliteCommand(SELECTTAREA+" WHERE ID = @ID");
            cmd.Parameters.AddWithValue("@ID", id);
            using(var con = new SqliteConnection(connection))
            {
                cmd.Connection = con;
                cmd.Connection.Open();
                using(var reader = cmd.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        var tarea = ParseTarea(reader); // var tarea = new Tarea(); 
                        /*unchecked
                        {
                            tarea.ID= (int)reader.GetInt64(0);
                            tarea.Descripcion = reader.GetString(1);
                            tarea.Prioridad = (int)reader.GetInt64(2);
                            tarea.Completado = (reader.GetInt64(3)==1);
                        }
                        */
                        return tarea;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
    }
}