using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Data_Layer;
using System.IO;
using System.Net;
using System.Web.Http.Filters;
using logging_study;

namespace Rainbow_Adventure_web_service.Controllers
{
    public class RegisterController : ApiController
    {

        [HttpGet]
        [Route("registeruser")]
        public int Register_user(string username,string password,string email_id)
        {
            try
            {
                Register_Data_Access data_Access = new Register_Data_Access();
                return data_Access.Register_user(username, email_id, password);
            }

            catch(Exception ex)
            {
                string c = ex.Message;
              Program.logerror(c + "  " + "error_in_updateRainbow_method");
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            
        }

        [HttpGet]
        [Route("loginuser")]
        public int login_check(string username,string user_password)
        {
            try
            {
                Register_Data_Access register_Data = new Register_Data_Access();
                return register_Data.user_login(username, user_password);
            }
            catch (Exception ex)
            {
                string d = ex.Message;
                Program.logerror(d + "  " + "error_in_updateRainbow_method");
                throw new HttpResponseException(HttpStatusCode.NotAcceptable);
            }


            
        }


    }
}