using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace itechart.PerformanceReview.Data.Model
{
    public class UserProfile : IEntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserProfileId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string FirstNameEng { get; set; }

        public string LastNameEng { get; set; }

        public string Position { get; set; }

        public string Room { get; set; }

        public string PhotoUrl { get; set; }

        public DateTime Birthday { get; set; }

        public string SkypeId { get; set; }

        public string PhoneNumber { get; set; }

        public string DomainName { get; set; }

        public string Email { get; set; }

        //        #region fk
        //
        //        [ForeignKey("User")] 
        //        public string UserId { get; set; }
        //
        //        #endregion
        //
        //        #region mapped properties
        //
        //        public virtual User User { get; set; }
        //
        //        #endregion
    }
}