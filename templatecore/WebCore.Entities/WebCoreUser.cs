using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebCore.Utils.ModelHelper;

namespace WebCore.Entities
{
    [Table("WebCoreUsers")]
    public class WebCoreUser : IdentityUser, IAuditable<string>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? JoinDate { get; set; }
        public string JobTitle { get; set; }
        public string Contract { get; set; }
        public string Carrer { get; set; }
        public WebCoreUser()
        {
            Id = Guid.NewGuid().ToString();
        }

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

        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? UpdateToken { get; set; }

        public long? RecordStatus { get; set; }
    }
}
