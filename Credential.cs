using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Employee.Models
{
    [Table("tbl_EmployeeCredentials")]
    public class Credential
    {
        public int ID { get; set; }

        public int CredentailID { get; set; }
        public int EmployeeID { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
        public string Isactive { get; set; }

        
    }
}