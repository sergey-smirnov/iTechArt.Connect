using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace itechart.PerformanceReview.Data.Model
{
    public class EntitiesUpdateHistory : IEntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid EntitiesUpdateHistoryId { get; set; }

        public DateTime UpdatedDateUtc { get; set; }

        #region fk

        public Guid EntityTypeId { get; set; }
        
        #endregion

        #region mapped properties

        public virtual EntityType EntityType { get; set; }

        #endregion
    }
}