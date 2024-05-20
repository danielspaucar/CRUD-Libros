using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using ProyectoDSW.Models;
using System.Diagnostics;

namespace ProyectoDSW.Controllers
{
    public class LibrosController : Controller
    {
        //Cadena de conexion
        string cadenaConexion =
           "server=DESKTOP-3M0CRBT;" +
           "database=ProyectoDSW1;" +
           "Trusted_Connection=True;" +
           "Encrypt=False;" +
           "MultipleActiveResultSets=True;" +
           "TrustServerCertificate=False;";

        //Listado de Generos para el combo
        public IEnumerable<Generos> listadoGeneros()
        {
            List<Generos> lista = new List<Generos>();
            SqlConnection con = new SqlConnection(cadenaConexion);
            SqlCommand cmd;
            try
            {
                con.Open();
                cmd = new SqlCommand("usp_generos_select", con);
                cmd.CommandTimeout = 0;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                SqlDataReader reader = cmd.ExecuteReader();
                Generos objGeneros;
                while (reader.Read())
                {
                    objGeneros = new Generos()
                    {
                        idGenero = reader.GetInt32(0),
                        nGenero = reader.GetString(1)   
                    };
                    lista.Add(objGeneros);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return lista;
        }

        //Listado de Libros
        public IEnumerable<Libros> listadoTotal(string indicador)
        {
            
          List<Libros> lista = new List<Libros>();
            SqlConnection con = new SqlConnection(cadenaConexion);
            SqlCommand cmd;
            try
            {
                con.Open();
                cmd = new SqlCommand("usp_libros_crud", con);
                cmd.CommandTimeout = 0;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@indicador", indicador);
                cmd.Parameters.AddWithValue("@codigo", 0);
                cmd.Parameters.AddWithValue("@nombre", "");
                cmd.Parameters.AddWithValue("@autor", "");
                cmd.Parameters.AddWithValue("@genero", 0);
                cmd.Parameters.AddWithValue("@stock", 0);
                cmd.Parameters.AddWithValue("@precio", 0);
                SqlDataReader reader = cmd.ExecuteReader();
                Libros objLibros;
                while (reader.Read())
                {
                    objLibros = new Libros()
                    {
                       codigo = reader.GetInt32(0),
                       nombre = reader.GetString(1),
                       autor = reader.GetString(2),
                       idGenero = reader.GetInt32(3),
                       stock = reader.GetInt32(4),
                       precio = reader.GetDecimal(5)
                    };
                    lista.Add(objLibros);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return lista;
        }

        //Listado de Libros por nombre
        public IEnumerable<Libros> listadoTotal(string indicador, string nombre)
        {
            List<Libros> lista = new List<Libros>();
            SqlConnection con = new SqlConnection(cadenaConexion);
            SqlCommand cmd;
            try
            {
                con.Open();
                cmd = new SqlCommand("usp_libros_crud", con);
                cmd.CommandTimeout = 0;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@indicador", indicador);
                cmd.Parameters.AddWithValue("@codigo", 0);
                cmd.Parameters.AddWithValue("@nombre", nombre + "%");
                cmd.Parameters.AddWithValue("@autor", "");
                cmd.Parameters.AddWithValue("@genero", 0);
                cmd.Parameters.AddWithValue("@stock", 0);
                cmd.Parameters.AddWithValue("@precio", 0);
                SqlDataReader reader = cmd.ExecuteReader();
                Libros objLibros;
                while (reader.Read())
                {
                    objLibros = new Libros()
                    {
                        codigo = reader.GetInt32(0),
                        nombre = reader.GetString(1),
                        autor = reader.GetString(2),
                        idGenero = reader.GetInt32(3),
                        stock = reader.GetInt32(4),
                        precio = reader.GetDecimal(5)
                       
                    };
                    lista.Add(objLibros);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return lista;
        }

        //Listado por codigo
        public IEnumerable<Libros> listadoTotal(string indicador, int codigo)
        {
            List<Libros> lista = new List<Libros>();
            SqlConnection con = new SqlConnection(cadenaConexion);
            SqlCommand cmd;
            try
            {
                con.Open();
                cmd = new SqlCommand("usp_libros_crud", con);
                cmd.CommandTimeout = 0;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@indicador", indicador);
                cmd.Parameters.AddWithValue("@codigo", codigo);
                cmd.Parameters.AddWithValue("@nombre", "");
                cmd.Parameters.AddWithValue("@autor", "");
                cmd.Parameters.AddWithValue("@genero", 0);
                cmd.Parameters.AddWithValue("@stock", 0);
                cmd.Parameters.AddWithValue("@precio", 0);
                SqlDataReader reader = cmd.ExecuteReader();
                Libros objLibros;
                while (reader.Read())
                {
                    objLibros = new Libros()
                    {
                        codigo = reader.GetInt32(0),
                        nombre = reader.GetString(1),
                        autor = reader.GetString(2),
                        idGenero = reader.GetInt32(3),
                        stock = reader.GetInt32(4),
                        precio = reader.GetDecimal(5)   
                    };
                    lista.Add(objLibros);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return lista;
        }

        public int operacionLibros(string indicador, Libros libros)
        {
            int procesar = 0;
            SqlConnection con = new SqlConnection(cadenaConexion);
            SqlCommand cmd;
            try
            {
                con.Open();
                cmd = new SqlCommand("usp_libros_crud", con);
                cmd.CommandTimeout = 0;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@indicador", indicador);
                cmd.Parameters.AddWithValue("@codigo", libros.codigo);
                cmd.Parameters.AddWithValue("@nombre", libros.nombre);
                cmd.Parameters.AddWithValue("@autor", libros.autor);
                cmd.Parameters.AddWithValue("@genero", libros.idGenero);
                cmd.Parameters.AddWithValue("@stock", libros.stock);
                cmd.Parameters.AddWithValue("@precio", libros.precio);               
               
                procesar = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error : " + ex.ToString());
            }
            finally
            {
                con.Close();
            }
            return procesar;
        }

        //Listado para los libros
        public IActionResult Index()
        {


            return View(listadoTotal("listarTodos"));
        }

        //Listado para crear
        public IActionResult Create()
        {
            ViewBag.generos = new SelectList(listadoGeneros(), "idGenero", "nGenero");
            return View(new Libros());
        }

        [HttpPost]
        public IActionResult Create(Libros registro)
        {
        
            int procesar = operacionLibros("registrar", registro);
            if (procesar == 1)
            {
                ViewBag.mensajeValidacion = "Registro Ingresado Correctamente.";
            }
            else
            {
                ViewBag.mensajeValidacion = "Hubo un error al ingresar.";
            }
            ViewBag.generos = new SelectList(listadoGeneros(), "idGenero", "nGenero");
            return View(registro);
        }

        //Metodo para Buscar por nombre
        public IActionResult busquedaNombre(string nombre = "si")
        {
            return View(listadoTotal("listarPorNombre", nombre));
        }

        public IActionResult listadoPaginacion(string nombre, int paginaActual = 0)
        {
            int contadorTotal = listadoTotal("listarTodos").Count();
            int registrosPorHoja = 7;

            int numPaginas = contadorTotal % registrosPorHoja == 0
                            ? contadorTotal / registrosPorHoja
                            : (contadorTotal / registrosPorHoja) + 1;

            ViewBag.paginaActual = paginaActual;
            ViewBag.numPaginas = numPaginas;
            ViewBag.fecha = 2020;

            return View(
                listadoTotal("listarTodos").Skip(paginaActual * registrosPorHoja).Take(registrosPorHoja)
                );
        }

        //Metodo para Editar
        public IActionResult Edit(int id)
        {
            Libros lib = listadoTotal("listarTodos")
                .Where(
                    y => y.codigo == id
                ).FirstOrDefault();

            ViewBag.generos = new SelectList(listadoGeneros(),
                "idGenero", "nGenero", lib.idGenero);

            return View(lib);
        }

        [HttpPost]
        public IActionResult Edit(Libros libros)
        {
            int procesar = operacionLibros("actualizar", libros);
            if (procesar == 1)
            {
                ViewBag.mensajeValidacion = "Registro Actualizado correctamente";
            }
            else
            {
                ViewBag.mensajeValidacion = "Hubo un error al actualizar";
            }

            ViewBag.carreras = new SelectList(listadoGeneros(), "idGenero",
                "nGenero");
            return View(libros);
        }

        //Metodo para mostrar los detalles

        public IActionResult Details(int id)
        {
            Libros lib = listadoTotal("getXID", id).First();
            return View(lib);
        }

        //Metodo para eliminar

        public IActionResult Delete(int id)
        {
            Libros lib = listadoTotal("getXID", id).First();
            return View(lib);
        }

        [HttpPost]
        public IActionResult Delete(string codigo)
        {
            Libros lib = new Libros();
            lib.codigo = Convert.ToInt32(codigo);
            int operacion = operacionLibros("eliminar", lib);
            return RedirectToAction("Index");
        }

        public IActionResult Home()
        {

            return View();
        }
    }
}
