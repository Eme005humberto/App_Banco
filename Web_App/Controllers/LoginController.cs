using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Web_App.Models;
using System.Data;
using System.Web.SessionState;
using System.Runtime.CompilerServices;
using System.Web.Security;

namespace Web_App.Controllers
{
    public class LoginController : Controller
    {
        static string CC = "Data Source=DESKTOP-A53I8GS\\SQLEXPRESS;Initial Catalog=WebAplicationRegisterEmployee;Integrated Security=true";
        // GET: Login
        public ActionResult InicioSesion()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ValidateUser(M_Register_User login)
        {
            
                //Encryptamos la clave del usuario
                login.Password_User = ConvertirSha256(login.Password_User);
                //Procedemos a realizar las operaiones por medio de un procedimiento almacenado
                using (SqlConnection con = new SqlConnection(CC))
                {
                    //Creamos un comando cmd
                    SqlCommand cmd = new SqlCommand("sp_Validar_Usuario",con);
                    cmd.Parameters.AddWithValue("Name_User",login.Name_User);
                    cmd.Parameters.AddWithValue("Password_User",login.Password_User);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();//Abrimos la conexion
                    //Convertimos el Id del Usuario para luego mostrarlo en formato de texto
                    login.IdUser = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                //Realizamos la validacion del IdUser para ver si existe el usuario si existe
                //Entramos al sistema
                FormsAuthentication.SetAuthCookie(login.Name_User,false);//Autenticamos al usuario
                    if(login.IdUser != 0) {
                        Session["usuario"] = login;//Almacenamos la informacion dentro de nuestra sesion
                        //Redirigimos al usuario a la siguiente ruta
                        return RedirectToAction("Index","Index");
                    }
                    else
                    {
                        //Muestra un mensaje
                        ViewData["Mensaje"] = "El Usuario ya Existe!!";
                        return View();
                    }
                } 
            }
        //Metodo para cerrar la sesion
        public ActionResult LoOut()
        {
            //Cerramos la cooki
            FormsAuthentication.SignOut();
            return RedirectToAction("InicioSesion","Login");
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