using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace itechart.PerformanceReview.Data.Model
{
    public class User : IdentityUser, IEntityBase
    {
        [Required]
        public int SmgUserId { get; set; }

        public string SessionId { get; set; }

        #region fk

        [ForeignKey("UserProfile")] 
        public Guid UserProfileId { get; set; }

        [ForeignKey("Department")] 
        public Guid DepartmentId { get; set; }

        #endregion

        #region mapped properties

        public virtual UserProfile UserProfile { get; set; }

        public virtual Department Department { get; set; }

        #endregion
    }
}