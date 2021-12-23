using DAL.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UserBLL
    {
        public static string Token(string userID)
        {
            //Create claims details based on the user information
            Claim[] claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "InventoryAuthenticationServer"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
                new Claim("UserID", userID)
            };

            //Set session expiration time
            //dalam menit
            int tokenExpiration = 60;

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("qwertyqwertyqwertyqwertyqwertyqwerty"));
            SigningCredentials signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken("InventoryAuthenticationServer", "InventoryAuthenticationServer", claims, expires: DateTime.Now.AddMinutes(tokenExpiration), signingCredentials: signIn);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static long AddUser(Register newUser)
        {
            using (FoodAroundDBContext context = new FoodAroundDBContext())
            {
                MsLogin user = new MsLogin();

                user.LoginPhoneNumber = user.CreatedBy = newUser.PhoneNumber;
                user.LoginSalt = Guid.NewGuid().ToString();
                user.LoginPin = NawaEncryption.Common.Encrypt(newUser.Pin, user.LoginSalt);
                user.CreatedDate = DateTime.Now;

                context.MsLogins.Add(user);
                context.SaveChanges();

                return user.PkLoginId;
            }
        }

        public static void AddStore(Register newUser, long creator)
        {
            using (FoodAroundDBContext context = new FoodAroundDBContext())
            {
                MsStore store = new MsStore();

                store.StoreMerchantName = newUser.Name;
                store.FkLoginId = creator;
                store.CreatedDate = DateTime.Now;
                store.CreatedBy = newUser.PhoneNumber;

                context.MsStores.Add(store);
                context.SaveChanges();
            }
        }

        public static string AddOTP(Register newUser)
        {
            string charString = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
            char[] charOTP = new char[4];

            Random random = new Random();

            for (int i = 0; i < charOTP.Length; i++)
            {
                charOTP[i] = charString[random.Next(charString.Length)];
            }

            string stringOTP = new string(charOTP);

            using (FoodAroundDBContext context = new FoodAroundDBContext())
            {
                TrOtp OTP = new TrOtp();

                OTP.Otp = stringOTP;
                OTP.FkLoginId = context.MsLogins.Where(item => item.LoginPhoneNumber == newUser.PhoneNumber).FirstOrDefault().PkLoginId;
                OTP.CreatedDate = DateTime.Now;
                OTP.CreatedBy = newUser.PhoneNumber;

                context.TrOtps.Add(OTP);
                context.SaveChanges();
            }

            return stringOTP;
        }

        public static string Register(Register newUser)
        {
            using (FoodAroundDBContext context = new FoodAroundDBContext())
            {
                if ((string.IsNullOrEmpty(newUser.PhoneNumber)) || (string.IsNullOrEmpty(newUser.Pin)))
                {
                    return "Failed to register\nPlease input phone number and pin";
                }
                else
                {
                    if (context.MsLogins.Where(item => item.LoginPhoneNumber == newUser.PhoneNumber).FirstOrDefault() == null)
                    {
                        if(newUser.Pin == newUser.RepeatPin)
                        {
                            long userID = AddUser(newUser);
                            string OTP = AddOTP(newUser);

                            AddStore(newUser, userID);

                            string output = OTP + "\n" + Token(userID.ToString());

                            return output;
                        }
                        else
                        {
                            return "You entered different pins";
                        }
                        
                    }
                    else
                    {
                        return "Failed to register\nPhone number already exists";
                    }
                }
            }
        }

        public static string Activation(long ID, string OTP)
        {
            using (FoodAroundDBContext context = new FoodAroundDBContext())
            {
                TrOtp TrOTP = context.TrOtps.Where(item => item.FkLoginId == ID).FirstOrDefault();

                if(TrOTP != null)
                {
                    if(TrOTP.Otp == OTP)
                    {
                        MsLogin user = context.MsLogins.Where(item => item.PkLoginId == ID).FirstOrDefault();

                        user.LoginActivation = true;

                        context.SaveChanges();

                        return "User activation successfull";
                    }
                    else
                    {
                        return "Wrong OTP";
                    }
                }
                else
                {
                    return "OTP not found";
                }
            }
        }

        public static string Login(MsLogin user)
        {
            using (FoodAroundDBContext context = new FoodAroundDBContext())
            {
                MsLogin userCheck = context.MsLogins.Where(item => item.LoginPhoneNumber == user.LoginPhoneNumber).FirstOrDefault();

                if (userCheck != null)
                {
                    user.LoginPin = NawaEncryption.Common.Encrypt(user.LoginPin, userCheck.LoginSalt);

                    if (user.LoginPin == userCheck.LoginPin)
                    {
                        if(userCheck.LoginActivation == true)
                        {
                            return Token(Convert.ToString(userCheck.PkLoginId));
                        }
                        else
                        {
                            return "User not activated";
                        }
                    }
                    else
                    {
                        return "Wrong Passsword";
                    }
                }
                else
                {
                    return "Phone number not found";
                }
            }
        }

        public static MsLogin GetUser(MsLogin user)
        {
            using (FoodAroundDBContext context = new FoodAroundDBContext())
            {
                if ((user.PkLoginId != 0) && (user.LoginPhoneNumber != null))
                {
                    return context.MsLogins.Where(item => ((item.PkLoginId == user.PkLoginId) && (item.LoginPhoneNumber == user.LoginPhoneNumber) && (item.IsDeleted == false))).FirstOrDefault();
                }
                else if (user.PkLoginId != 0)
                {
                    return context.MsLogins.Where(item => ((item.PkLoginId == user.PkLoginId) && (item.IsDeleted == false))).FirstOrDefault();
                }
                else if (user.LoginPhoneNumber != null)
                {
                    return context.MsLogins.Where(item => ((item.LoginPhoneNumber == user.LoginPhoneNumber) && (item.IsDeleted == false))).FirstOrDefault();
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
