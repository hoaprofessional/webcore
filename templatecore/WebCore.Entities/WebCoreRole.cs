using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebCore.Utils.ModelHelper;

namespace WebCore.Entities
{
    [Table("WebCoreRoles")]
    public class WebCoreRole : IdentityRole<string>, IAuditable<string>
    {
        public WebCoreRole()
        {
            Id = Guid.NewGuid().ToString();
        }
        public WebCoreRole(string name) : this()
        {
            Name = name;
        }
        public WebCoreRole(string name, string id)
        {
            Name = name;
            Id = id;
        }
        public string Description { get; internal set; }

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
    }
}
