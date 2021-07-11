using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.Concretes;
using Core.Entities.MongoDb;

namespace Entities.Concretes
{
    public class Message:EntityBase, IEntity
    {
        public User SenderUser { get; set; }
        public User ReciverUser { get; set; }
        public string Text { get; set; }
        public DateTime? SendTime { get; set; }
        public DateTime? ReadTime { get; set; }
    }
}
