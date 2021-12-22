using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class TrLocation
    {
        public long PkLocationId { get; set; }
        public string LocationLongitude { get; set; }
        public string LocationLatitude { get; set; }
        public DateTime? LocationDate { get; set; }
        public long? FkStoreId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string DeletedBy { get; set; }
    }
}
