using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace UniversityManagementSystem_Elegant.Gateway
{
    public class BaseGateway
    {
        public string connectionString;
        public SqlConnection connection;
        public BaseGateway()
        {
          connectionString =
          WebConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
          connection = new SqlConnection(connectionString);
        }
    }
}