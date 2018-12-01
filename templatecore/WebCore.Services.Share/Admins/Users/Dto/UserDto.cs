using System;
using System.Collections.Generic;
using System.Text;

namespace WebCore.Services.Share.Admins.Users.Dto
{
    //WebCoreUser
    public class UserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? JoinDate { get; set; }
        public string JobTitle { get; set; }
        public string Contract { get; set; }
        public string Carrer { get; set; }
        public string Name { get; set; }
        public Guid? UpdateToken { get; set; }
        public long? RecordStatus { get; set; }

    }
}
