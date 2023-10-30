using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public  class Conexion
    {

        public static string GetConnectionString()
        {
            return "Server=LEMUSHUAWEI\\SQLEXPRESS; Database= TrackingAndTraceNetCore; TrustServerCertificate=True; Trusted_Connection=True; User ID=LEMUSHUAWEI\\lemus; Password=;";
        }
    }
}
