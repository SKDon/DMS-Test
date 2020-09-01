using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Test.Sujith.Krishantha.Models
{
    [Table("Student")]
    public class Student
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Name")]
        [StringLength(10)]
        public string Name { get; set; }

        [Column("Email")]
        [StringLength(50)]
        public string Email { get; set; }

        [Column("Contact")]
        [StringLength(20)]
        public string Contact { get; set; }

        [ForeignKey("Class")]
        [Column("Class_Id")]
        public int? Class_Id { get; set; }

        public virtual Class Class { get; set; }
    }


}