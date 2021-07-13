using AFPCrecer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AFPCrecer.Controllers
{
    public class ProductoController : Controller
    {
        string connectionString = @"Data Source=DESKTOP-STLGPF3;User Id=sa;Password=kaspar21;Initial Catalog=PruebaAFPCrecer;";

        public async Task<IActionResult> Index()
        {
            List<Vehiculo> lstVehiculos = new List<Vehiculo>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT Id, Modelo, Marca, Descripcion, Tipo FROM Producto", con))
                {
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader;

                    con.Open();
                    reader = cmd.ExecuteReader();

                    while (await reader.ReadAsync())
                    {
                        Vehiculo vehiculo = new Vehiculo
                        {
                            Id = Convert.ToInt32(reader["Id"].ToString()),
                            Modelo = reader["Modelo"].ToString(),
                            Marca = reader["Marca"].ToString(),
                            Descripcion = reader["Descripcion"].ToString(),
                            Tipo = reader["Tipo"].ToString() == "A" ? "Alquiler" : "Venta"
                        };

                        lstVehiculos.Add(vehiculo);
                    }
                }
            }

            return View(lstVehiculos);
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            Vehiculo vehiculo = new Vehiculo();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT Id, Modelo, Marca, Descripcion, Tipo FROM Producto WHERE Id=" + id, con))
                {
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader;

                    con.Open();
                    reader = cmd.ExecuteReader();

                    if (await reader.ReadAsync())
                    {
                        vehiculo = new Vehiculo
                        {
                            Id = Convert.ToInt32(reader["Id"].ToString()),
                            Modelo = reader["Modelo"].ToString(),
                            Marca = reader["Marca"].ToString(),
                            Descripcion = reader["Descripcion"].ToString(),
                            Tipo = reader["Tipo"].ToString() == "A" ? "Alquiler" : "Venta"
                        };
                    }

                }
            }

            return View(vehiculo);
        }

        [HttpPost]
        public IActionResult Create(Vehiculo vehiculo)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("InsertUpdate_Producto", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@marca", SqlDbType.VarChar).Value = vehiculo.Marca;
                    cmd.Parameters.Add("@modelo", SqlDbType.VarChar).Value = vehiculo.Modelo;
                    cmd.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = vehiculo.Descripcion;
                    cmd.Parameters.Add("@tipo", SqlDbType.VarChar).Value = vehiculo.Tipo;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(Vehiculo vehiculo)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("InsertUpdate_Producto", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@marca", SqlDbType.VarChar).Value = vehiculo.Marca;
                    cmd.Parameters.Add("@modelo", SqlDbType.VarChar).Value = vehiculo.Modelo;
                    cmd.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = vehiculo.Descripcion;
                    cmd.Parameters.Add("@tipo", SqlDbType.VarChar).Value = vehiculo.Tipo;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = vehiculo.Id;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            return RedirectToAction("Index");
        }
    }
}
