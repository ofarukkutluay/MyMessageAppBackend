using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Business.Constants
{
    public static class Messages
    {
        public static string Add(string entityName)
        {
            return entityName + " eklendi";
        }
        public static string GetAll = "Tüm data getirildi";
        public static string GetById(string entityName) => entityName + " getirildi";
        public static string Update(string entityName) => entityName + " güncellendi";
        public static string Delete(string entityName) => entityName + " silindi";
        public static string AuthorizationDenied = "Yetkiniz yok";
        public static string UserRegistered = "Kayıt edildi";
        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string PasswordError = "Şifre hatalı";
        public static string SuccessfulLogin = "Giriş başarılı";
        public static string UserAlreadyExists = "Kullanıcı Mevcut";
        public static string AccessTokenCreated = "Geçerli Token oluşturuldu";
        public static string PasswordChanged = "Şifre Değiştirildi";
        public static string AccessTokenDontCreated = "Geçerli Token oluşturulamadı";
        public static string IdNotFound = "Id girişi gereklidir";
        public static string ListEmpty = "Liste boş. Data getirilemedi";
    }
}
