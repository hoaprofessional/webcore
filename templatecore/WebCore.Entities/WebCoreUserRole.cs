using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebCore.Utils.ModelHelper;

namespace WebCore.Entities
{
    [Table("WebCoreUserRoles")]
    public class WebCoreUserRole : IdentityUserRole<string>, IAuditable<string>
    {
        public string Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        /// <summary>
        /// UserName Created
        /// </summary>
        [MaxLength(256)]
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// UserName Update
        /// </summary>
        [MaxLength(256)]
        public string ModifiedBy { get; set; }
        public long? RecordStatus { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? UpdateToken { get; set; }

        public WebCoreUserRole()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
