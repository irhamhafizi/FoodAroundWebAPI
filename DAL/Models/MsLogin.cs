using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class MsLogin
    {
        public long PkLoginId { get; set; }
        public string LoginPhoneNumber { get; set; }
        public string LoginPin { get; set; }
        public bool LoginActivation { get; set; }
        public string LoginSalt { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string DeletedBy { get; set; }
    }
}
