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
            return "Server=LAPTOP-7SN4MG5E; Database= TrackingAndTraceNetCore; TrustServerCertificate=True; Trusted_Connection=True; User ID=sa; Password=pass@word1;";
        }
    }
}
