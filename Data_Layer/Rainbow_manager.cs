using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Model_Layer;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;


namespace Data_Layer
{
  public class Rainbow_manager
  {
        public int insert_rainbow (rainbow r)
        {
            using (SqlConnection connection3 = new SqlConnection())
            {
                connection3.ConnectionString = ConfigurationManager.AppSettings["connectionstring"];
                connection3.Open();

                SqlCommand command3 = new SqlCommand("insert_rainbow", connection3);
                command3.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter = new SqlParameter("rainbow_name", r.rainbow_name);
                command3.Parameters.Add(parameter);

                SqlParameter parameter1 = new SqlParameter("rainbow_description", r.description);
                command3.Parameters.Add(parameter1);

                SqlParameter parameter2 = new SqlParameter("latitude", r.latitude);
                command3.Parameters.Add(parameter2);

                SqlParameter parameter3 = new SqlParameter("longitude", r.longitude);
                command3.Parameters.Add(parameter3);

                SqlParameter parameter4 = new SqlParameter("user_id",r.user_id);
                command3.Parameters.Add(parameter4);

                DataTable table = new DataTable();
                DataColumn column = new DataColumn("rainbowUrl");
                table.Columns.Add(column);
                if (r.photos != null)
                {
                    foreach (string item in r.photos)
                    {
                        DataRow row = table.NewRow();
                        row["rainbowUrl"] = item;
                        table.Rows.Add(row);
                    }
                }
                SqlParameter parameter5 = new SqlParameter("photos",table);
                command3.Parameters.Add(parameter5);

                int result = (Int32)command3.ExecuteScalar();
                connection3.Close();
                command3.Dispose();

                return result;

            }
        
        }

        public bool delete_rainbow(int id)
        {
            using (SqlConnection connection4 = new SqlConnection())
            {
                connection4.ConnectionString = ConfigurationManager.AppSettings["connectionstring"];
                connection4.Open();

                SqlCommand command = new SqlCommand("sp_delete_rainbow", connection4);
                command.CommandType = CommandType.StoredProcedure;
 
                SqlParameter parameter = new SqlParameter("rainbow_id",id);
                command.Parameters.Add(parameter);

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable table = new DataTable();
                adapter.Fill(table);

                var credentials = GoogleCredential.FromFile(ConfigurationManager.AppSettings["googlecredential"]);
                var client = StorageClient.Create(credentials);

                for (int i = 0; i < table.Rows.Count; i++)
                {
                   var photoUrl = table.Rows[i]["photourl"].ToString();
                    int j = photoUrl.IndexOf("/o/");
                    photoUrl = photoUrl.Substring(j + 3);
                    int q = photoUrl.IndexOf('?');
                    photoUrl = photoUrl.Substring(0, q);
                    photoUrl = photoUrl.Replace("%2F", "/");
                    client.DeleteObject("rainbowadventures-b65aa.appspot.com", photoUrl);
                }


                //int result = command.ExecuteNonQuery();
                connection4.Close();
                command.Dispose();

                //if (result >=1)
                //{
                    return true;

                //}
                //return false;

            }

        }

        public bool update_rainbow(int id ,string rainbow_name,string description)
        {
            using (SqlConnection connection5 = new SqlConnection())
            {
                connection5.ConnectionString = ConfigurationManager.AppSettings["connectionstring"];
                connection5.Open();

                SqlCommand command = new SqlCommand("sp_update_rainbow", connection5);
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter = new SqlParameter("update_name", rainbow_name);
                command.Parameters.Add(parameter);

                SqlParameter parameter1 = new SqlParameter("update_description", description);
                command.Parameters.Add(parameter1);

                SqlParameter parameter2 = new SqlParameter("rainbow_id", id);
                command.Parameters.Add(parameter2);

                int result = command.ExecuteNonQuery();
                connection5.Close();
                command.Dispose();

                if (result >= 1)
                {
                    return true;

                }
                return false;

            }

        }

        public List<rainbow> getall_rainbow(int user_id)
        {
            using (SqlConnection connection4 = new SqlConnection())
            {
                connection4.ConnectionString = ConfigurationManager.AppSettings["connectionstring"];
                connection4.Open();

                SqlCommand command = new SqlCommand("sp_getallRainbowbyUserId", connection4);
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter = new SqlParameter("user_id", user_id);
                command.Parameters.Add(parameter);

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable table = new DataTable();
                adapter.Fill(table);

                List<rainbow> rl = new List<rainbow>();
                
                for (int i = 0; i < table.Rows.Count ; i++)
                {
                    rainbow rn = new rainbow();
                    rn.rainbow_name = table.Rows[i]["rainbow_name"].ToString();
                    rn.longitude=table.Rows[i]["longitude"].ToString();
                    rn.latitude = table.Rows[i]["latitude"].ToString();
                    rn.id =       Convert.ToInt32(table.Rows[i]["id"]);
                   
                    rl.Add(rn);
                    
                }
                connection4.Close();
                command.Dispose();

                return rl;

            }
        }

        public rainbow GetRainbowDetailsByRainbowId(int rainbow_id)
        {
            using (SqlConnection connection6 =new SqlConnection() )
            {
                connection6.ConnectionString=ConfigurationManager.AppSettings["connectionstring"];
                connection6.Open();

                SqlCommand command2 = new SqlCommand("sp_getAllDetailsByRaibowId", connection6);
                command2.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter = new SqlParameter("rainbow_id", rainbow_id);
                command2.Parameters.Add(parameter);

                SqlDataAdapter dataAdapter = new SqlDataAdapter(command2);

                DataTable table = new DataTable();
                dataAdapter.Fill(table);
                rainbow r = new rainbow();
                
                List<string> myphotos = new List<string>();

                for (int i = 0; i < table.Rows.Count; i++)
                {
                 
                    r.rainbow_name = table.Rows[i]["rainbow_name"].ToString();
                    r.longitude = table.Rows[i]["longitude"].ToString();
                    r.latitude = table.Rows[i]["latitude"].ToString(); 
                     r.description = table.Rows[i]["rainbow_description"].ToString();
                     myphotos.Add(table.Rows[i]["photourl"].ToString());
                    r.id = Convert.ToInt32(table.Rows[i]["id"]);
                }
                r.photos = myphotos.ToArray();
                connection6.Close();
                command2.Dispose();
                return r;
            }
        }

  }
}
