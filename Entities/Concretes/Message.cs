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
        public string SenderUserId { get; set; }
        public string ReciverUserId { get; set; }
        public string Text { get; set; }
        public byte[] TextHash { get; set; }
        public byte[] TextSalt { get; set; }
        public DateTime? SendTime { get; set; }
        public DateTime? ReadTime { get; set; }
    }
}
