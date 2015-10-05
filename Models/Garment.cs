using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fbuCloset.Models
{
    public class Garment
    {
        public string GarmentID { get; set; }
        public string attachID { get; set; }
        public string UserID { get; set; }
        public string SessionID { get; set; }
        public int ClothingTypeID { get; set; }
        public string DocDesc { get; set; }
        public string DocFileType { get; set; }
        public long DocSize { get; set; }
        public string CreateDate { get; set; }
        public string SessionUserID { get; set; }
        public int LoggedIN { get; set; }
    }
}