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

    }
}
