using DAL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class StoreBLL
    {
        public static void InsertStoreImage(byte[] bytes, string fileName, string creator, long storeID)
        {
            using (FoodAroundDBContext context = new FoodAroundDBContext())
            {
                string folderPath = "..\\Pictures\\StorePicture";

                string tempFileName = Guid.NewGuid().ToString();

                File.WriteAllBytes(folderPath + tempFileName, bytes);

                MsStoreImage storeImage = new MsStoreImage();

                storeImage.StoreImagePath = folderPath;
                storeImage.StoreIamgeGuid = tempFileName;
                storeImage.StoreIamgeName = fileName;
                storeImage.FkStoreId = storeID;
                storeImage.CreatedDate = DateTime.Now;
                storeImage.CreatedBy = UserBLL.GetUser(new MsLogin() { PkLoginId = (Convert.ToInt64(creator)) }).LoginPhoneNumber;

                context.MsStoreImages.Add(storeImage);
                context.SaveChanges();
            }
        }


        public static string UpdateStore(byte[] bytes, string fileName, MsStore newStore, string modifier)
        {
            using (FoodAroundDBContext context = new FoodAroundDBContext())
            {
                MsStore store = context.MsStores.Where(item => ((item.FkLoginId == Convert.ToInt64(modifier)) && (item.IsDeleted == false))).FirstOrDefault();

                if (bytes != null)
                {
                    InsertStoreImage(bytes, fileName, modifier, store.PkStoreId);
                }

                store.StoreName = newStore.StoreName;
                store.StoreDescription = newStore.StoreDescription;
                store.ModifiedDate = DateTime.Now;
                store.ModifiedBy = UserBLL.GetUser(new MsLogin() { PkLoginId = (Convert.ToInt64(modifier)) }).LoginPhoneNumber;

                context.SaveChanges();

                return "SUCCESS";
            }
        }

        public static MsStore GetStore(MsStore store)
        {
            using (FoodAroundDBContext context = new FoodAroundDBContext())
            {
                if (store.PkStoreId != 0)
                {
                    return context.MsStores.Where(item => ((item.PkStoreId == store.PkStoreId) && (item.IsDeleted == false))).FirstOrDefault();
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
