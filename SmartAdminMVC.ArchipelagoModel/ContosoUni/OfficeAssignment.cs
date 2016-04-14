using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartAdminMvc.ArchipelagoModel.ContosoUni
{
    public class OfficeAssignment
    {
        [Key]
        [ForeignKey("Instructor")]
        public int InstructorID { get; set; }
        //Doesn't exist without Instructor assigned to it - primary key is also InstructorID
        //Dependent on Instructor - foreign key is InstructorID

        [StringLength(50)]
        [Display(Name = "Office Location")]
        public string Location { get; set; }
        public virtual Instructor Instructor { get; set; }
        //1-1 relationship
    }
}