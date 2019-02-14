using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Employee.Models
{
    [Table("tbl_employeedetails")]
    public class Employeelist
    {

        public int Id { get; set; }
        public int EmployeeID { get; set; }
        public string Prefix { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime Dob { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string Designation { get; set; }

        

    }
}