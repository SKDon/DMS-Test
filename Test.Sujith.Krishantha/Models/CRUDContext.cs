using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Test.Sujith.Krishantha.Models
{
    public class CRUDContext: DbContext
    {
        public CRUDContext()
            : base("name=CRUDContext")
        {
        }

        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<Student> Students { get; set; }
    }
}