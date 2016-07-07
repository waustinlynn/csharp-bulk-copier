using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace bulk_copier
{
    class Program
    {
        private static string sourceConnString;
        private static string destinationConnString;
        static void Main(string[] args)
        {
            sourceConnString = ConfigurationManager.ConnectionStrings["SOURCE"].ToString();
            destinationConnString = ConfigurationManager.ConnectionStrings["DESTINATION"].ToString();

            using (var sourceConn = new SqlConnection(sourceConnString))
            {
                sourceConn.Open();
                using (var sourceCmd = sourceConn.CreateCommand())
                {
                    sourceCmd.CommandText = ConfigurationManager.AppSettings["sourceQuery"];
                    using (var reader = sourceCmd.ExecuteReader())
                    {
                        using (var dt = new DataTable())
                        {
                            for (var i = 0; i < reader.FieldCount; i++)
                            {
                                dt.Columns.Add(new DataColumn(reader.GetName(i)));
                            }
                            while (reader.Read())
                            {
                                DataRow dr = dt.NewRow();
                                for (var j = 0; j < reader.FieldCount; j++)
                                {
                                    dr[j] = reader[j];
                                }
                                dt.Rows.Add(dr);
                                if (dt.Rows.Count < 1000) continue;
                                BulkCopyToDestination(dt);
                                dt.Clear();
                            }
                            if (dt.Rows.Count > 0)
                            {
                                BulkCopyToDestination(dt);
                            }
                        }
                    }
                }
            }

        }

        private static void BulkCopyToDestination(DataTable dt)
        {
            using (var destConn = new SqlConnection(destinationConnString))
            {
                destConn.Open();
                using (var bulk = new SqlBulkCopy(destConn))
                {
                    bulk.BulkCopyTimeout = 900;
                    bulk.DestinationTableName = ConfigurationManager.AppSettings["destinationTable"];
                    bulk.WriteToServer(dt);
                }
            }
        }
    }
}
