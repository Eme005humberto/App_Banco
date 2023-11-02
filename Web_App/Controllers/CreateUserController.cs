using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Web_App.Models;
using System.Web.SessionState;


namespace Web_App.Controllers
{
    public class CreateUserController : Controller
    {
        static string CC = "Data Source=DESKTOP-A53I8GS\\SQLEXPRESS;Initial Catalog=WebAplicationRegisterEmployee;Integrated Security=true";
        public ActionResult RegistrarUsuario()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create_User(M_Register_User registrar)
        {
            if(ModelState.IsValid)
            {
                //Declaramos 2 variables
                bool registrado;
                string mensaje;
                //Realizamos una validacion para encryptar la contraseña y validar si es la misma contraseña
                //al momento de confirmala
                if (registrar.Password_User == registrar.Confirmar_Password)
                {
                    //Encryptamos la contraseña
                    registrar.Password_User = ConvertirSha256(registrar.Password_User);
                }
                else
                {
                    //Caso contrario muestra un mensaje diciendo las contraseñas no coinciden
                    ViewData["Mensaje"] = "LAS CONTRASEÑAS NO COINCIDEN ;(";
                    return View("RegistrarUsuario");
                }
                //Procedemos a realizar las operaciones por medio del espacio using
                using (SqlConnection con = new SqlConnection(CC))
                {
                    //Creamos un cmd el cual nos va a servir para poder realizar los respectivos
                    //Procedimientos
                    SqlCommand cmd = new SqlCommand("sp_Registrarse", con);//Colocamos el procedimiento con la variable
                                                                           //de conexion
                    cmd.Parameters.AddWithValue("First_Name", registrar.First_Name);
                    cmd.Parameters.AddWithValue("Last_Name", registrar.Last_Name);
                    cmd.Parameters.AddWithValue("Mail", registrar.Mail);
                    cmd.Parameters.AddWithValue("Phone", registrar.Phone);
                    cmd.Parameters.AddWithValue("Name_User", registrar.Name_User);
                    cmd.Parameters.AddWithValue("Password_User", registrar.Password_User);
                    cmd.Parameters.Add("Registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                    //Le decimos el tipo de comando que va a ejecutar
                    cmd.CommandType = CommandType.StoredProcedure;//Procedimiento almacenado
                    con.Open();//Abrimos la conexion
                    cmd.ExecuteNonQuery();

                    //Hacemos una pequeña conversion de datos
                    registrado = Convert.ToBoolean(cmd.Parameters["Registrado"].Value);
                    mensaje = cmd.Parameters["Mensaje"].Value.ToString();//Recibmos el parametro de mensaje que es un varchar
                }
                ViewData["Mensaje"] = mensaje;//Mostramos un mensaje
                                              //Validamos si el usuario se registro correctamente nos direcciona a la siguiente web
                if (registrado)
                {
                    return View("RegistrarUsuario");
                }
                else
                {
                    return View("RegistrarUsuario");//Nos devuelve la vista si el proceso falla
                }
            }
            return View("RegistrarUsuario");
                
        }
        //Encrytamos la contraseña
        public static string ConvertirSha256(string texto)
        {
            //using System.Text;
            //USAR LA REFERENCIA DE "System.Security.Cryptography"

            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }
    }
    }