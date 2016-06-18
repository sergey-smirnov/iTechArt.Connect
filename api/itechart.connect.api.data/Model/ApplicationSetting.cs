using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace itechart.PerformanceReview.Data.Model
{
    public class ApplicationSetting : IEntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ApplicationSettingId { get; set; }

        public long MinDataSynchronizationInterval { get; set; }

        public bool IsDefault { get; set; }

        public int EmployeesAccessRule { get; set; }

        public int DepartmentsAccessRule { get; set; }
    }
}