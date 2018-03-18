using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


namespace Data_Layer
{
   public class Register_Data_Access
    {

       public int Register_user(string username,string email_id , string pasword)
        {
             
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ConfigurationManager.AppSettings["connectionstring"];
                connection.Open();

                SqlCommand command = new SqlCommand("sp_register_user", connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter = new SqlParameter("email_id", email_id);
                command.Parameters.Add(parameter);

                SqlParameter parameter1 = new SqlParameter("user_name",username);
                command.Parameters.Add(parameter1);

                SqlParameter parameter2 = new SqlParameter("user_password", pasword);
                command.Parameters.Add(parameter2);

                int result = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
                command.Dispose();

                return result;

            }
        }

        public int user_login(string username,string user_password)
        {
            using(SqlConnection connection1=new SqlConnection())
            {
                connection1.ConnectionString = ConfigurationManager.AppSettings["connectionstring"];
                connection1.Open();

                SqlCommand command1 = new SqlCommand("get_login_user", connection1);
                command1.CommandType = CommandType.StoredProcedure;

                SqlParameter sql_Parameter = new SqlParameter("username", username);

                command1.Parameters.Add(sql_Parameter);

                SqlParameter sql_parameter1 = new SqlParameter("user_password", user_password);
                command1.Parameters.Add(sql_parameter1);

                int id = Convert.ToInt32(command1.ExecuteScalar());
                
                return id;


            }



        }



    }
}
