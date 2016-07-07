using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bulk_copier
{
    class Program
    {
        static void Main(string[] args)
        {
            var sourceConnString = ConfigurationManager.ConnectionStrings["SOURCE"].ToString();
            var destinationConnString = ConfigurationManager.ConnectionStrings["DESTINATION"].ToString();

            using (var sourceConn = new SqlConnection(sourceConnString))
            {
                sourceConn.Open();
                using (var sourceCmd = sourceConn.CreateCommand())
                {

                }
            }

        }
    }
}
