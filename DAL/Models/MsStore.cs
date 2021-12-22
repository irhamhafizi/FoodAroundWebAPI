using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class MsStore
    {
        public long PkStoreId { get; set; }
        public string StoreName { get; set; }
        public string StoreDescription { get; set; }
        public string StoreMerchantName { get; set; }
        public bool? StoreStatus { get; set; }
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
