﻿using System.Configuration;
using System.Data.SqlClient;

namespace MediumDomainModel
{
    public class SqlConnectionFactory
    {
        public static SqlConnection Create()
        {
            var connectionStr = ConfigurationManager
                .ConnectionStrings["Medium"]
                .ConnectionString;
            return new SqlConnection(connectionStr);
        }
    }
}