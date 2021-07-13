using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using AFPCrecer.Models;

namespace AFPCrecer.Controllers
{
    public class ClienteController : Controller
    {
        string connectionString = @"Data Source=DESKTOP-STLGPF3;User Id=sa;Password=kaspar21;Initial Catalog=PruebaAFPCrecer;";

        public async Task<IActionResult> Index()
        {
            List<Cliente> lstClientes = new List<Cliente>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT Id, Nombres, Apellidos, Edad, Correo, Telefono FROM Cliente", con))
                {
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader;

                    con.Open();
                    reader = cmd.ExecuteReader();

                    while (await reader.ReadAsync()) 
                    {
                        Cliente cliente = new Cliente
                        {
                            Id = Convert.ToInt32(reader["Id"].ToString()),
                            Nombres = reader["Nombres"].ToString(),
                            Apellidos = reader["Apellidos"].ToString(),
                            Edad = Convert.ToInt32(reader["Edad"].ToString()),
                            Correo = reader["Correo"].ToString(),
                            Telefono = reader["Telefono"].ToString()
                        };

                        lstClientes.Add(cliente);
                    }

                }
            }

            return View(lstClientes);
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Edit(int id) 
        {
            Cliente cliente = new Cliente();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT Id, Nombres, Apellidos, Edad, Correo, Telefono FROM Cliente WHERE Id=" + id, con))
                {
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader;

                    con.Open();
                    reader = cmd.ExecuteReader();

                    if (await reader.ReadAsync())
                    {
                        cliente = new Cliente
                        {
                            Id = Convert.ToInt32(reader["Id"].ToString()),
                            Nombres = reader["Nombres"].ToString(),
                            Apellidos = reader["Apellidos"].ToString(),
                            Edad = Convert.ToInt32(reader["Edad"].ToString()),
                            Correo = reader["Correo"].ToString(),
                            Telefono = reader["Telefono"].ToString()
                        };
                    }

                }
            }

            return View(cliente);
        }

        [HttpPost]
        public IActionResult Create(Cliente cliente) 
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("InsertUpdate_Cliente", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = cliente.Nombres;
                    cmd.Parameters.Add("@apellido", SqlDbType.VarChar).Value = cliente.Apellidos;
                    cmd.Parameters.Add("@edad", SqlDbType.SmallInt).Value = cliente.Edad;
                    cmd.Parameters.Add("@correo", SqlDbType.VarChar).Value = cliente.Correo;
                    cmd.Parameters.Add("@telefono", SqlDbType.VarChar).Value = cliente.Telefono;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(Cliente cliente)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("InsertUpdate_Cliente", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = cliente.Nombres;
                    cmd.Parameters.Add("@apellido", SqlDbType.VarChar).Value = cliente.Apellidos;
                    cmd.Parameters.Add("@edad", SqlDbType.SmallInt).Value = cliente.Edad;
                    cmd.Parameters.Add("@correo", SqlDbType.VarChar).Value = cliente.Correo;
                    cmd.Parameters.Add("@telefono", SqlDbType.VarChar).Value = cliente.Telefono;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = cliente.Id;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            return RedirectToAction("Index");
        }
    }
}
