using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class MsStoreImage
    {
        public long PkStoreImageId { get; set; }
        public string StoreImagePath { get; set; }
        public string StoreIamgeName { get; set; }
        public string StoreIamgeGuid { get; set; }
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
