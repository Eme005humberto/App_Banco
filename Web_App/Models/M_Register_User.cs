using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_App.Models
{
    public class M_Register_User
    {
        public int IdUser { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        [Required(ErrorMessage = "El campo Email es obligatorio")]
        [EmailAddress]
        public string Mail { get; set; }
        public string Phone { get; set; }
        [StringLength(10, MinimumLength = 3, ErrorMessage = "El usuario tiene que tener entre 3 a 10 caracteres")]
        public string Name_User { get; set; }
        [Required(ErrorMessage = "El campo contraseña es obligatorio")]
        public string Password_User { get; set; }
        public string Confirmar_Password { get; set; }
    }
}