//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Web_App
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class tblInicioSesion
    {
        public int IdUser { get; set; }
        [Required(ErrorMessage = "El campo Email es obligatorio")]
        public string U_User { get; set; }
        [Required(ErrorMessage = "El campo Email es obligatorio")]
        public string C_password { get; set; }
    }
}