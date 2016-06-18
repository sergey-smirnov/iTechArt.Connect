using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace itechart.PerformanceReview.Data.Model
{
    public class Department : IEntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid DepartmentId { get; set; }

        public int SmgDepartmentId { get; set; }

        public string DepartmentCode { get; set; }

        public string Name { get; set; }

        #region mapped properties

        public virtual ICollection<User> Users { get; set; }

        #endregion
    }
}