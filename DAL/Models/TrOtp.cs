using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class TrOtp
    {
        public long PkOtpId { get; set; }
        public string Otp { get; set; }
        public long? FkLoginId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string DeletedBy { get; set; }
    }
}
