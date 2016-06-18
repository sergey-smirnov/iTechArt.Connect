using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace itechart.PerformanceReview.Data.Model
{
    public class EntityType : IEntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid EntityTypeId { get; set; }

        public string Name { get; set; }

        #region constants

        public const string DepartmentEntityName = "Department";
        public const string UserEntityName = "User";

        #endregion
    }
}