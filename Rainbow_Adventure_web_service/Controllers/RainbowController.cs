using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Data_Layer;
using Newtonsoft.Json.Linq;
using Model_Layer;
using System.IO;
using System.Web.Http.Filters;
using log4net;
using logging_study;
using System.Web.Http.Controllers;
using System.Diagnostics;

namespace Rainbow_Adventure_web_service.Controllers
{
     
    public class RainbowController : ApiController
    {
        [HttpPost]
        [myaction]
        [Route("insert_rainbow")]		        
		 public int add_rainbow([FromBody] rainbow r)
        {
            try
            { 
                Rainbow_manager rainbow = new Rainbow_manager();
                return rainbow.insert_rainbow(r);
            }
            catch (Exception ex)
            {
                string m = ex.Message;
                Program p = new Program();
             
               Program.logerror(m + "  " + "error_in_insertRainbow_method");

                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }

        }
    

        [HttpGet]
        [Route("delete_rainbow")]
        public bool delete_rainbow(int id)
        {
            try
            {
                Rainbow_manager rainbow = new Rainbow_manager();
                return rainbow.delete_rainbow(id);
            }
            catch (Google.GoogleApiException e)
            {
                string a = e.Message;
                Program.logerror(a + "  " + "google api error in_deleteRainbow_method");
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                string a = ex.Message;
                Program.logerror(a + "  " + "error_in_deleteRainbow_method");
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

        }

            [HttpGet]
        [Route("usernameIdByName")]

        public int get_id(string name)
        {
            try
            {
                Rainbow_manager rainbow = new Rainbow_manager();
                return rainbow.Get_id_by_username(name);

            }
            catch (Exception ex)
            {
                string b = ex.Message;
                Program.logerror(b + "  " + "error_in_getallRainbow_method");
                throw new HttpResponseException(HttpStatusCode.NotAcceptable);
            }


        }
        

       [HttpGet]
       [Route("getallRainbowbyUserId")]
        public List<rainbow> GetList(int user_id)
        {
            try
            {
                Rainbow_manager rainbow = new Rainbow_manager();
                return rainbow.getall_rainbow(user_id);
            }
            catch (Exception ex)
            {
                string b = ex.Message;
                Program.logerror(b + "  " + "error_in_getallRainbow_method");
                throw new HttpResponseException(HttpStatusCode.ServiceUnavailable);
            }
            
             
            
        }

        [HttpPost]
        [Route("update_rainbow")]
        public bool rainbow_update([FromBody] rainbow r)
        {
            try
            {
                Rainbow_manager rainbow = new Rainbow_manager();
                return rainbow.update_rainbow(r.id,r.rainbow_name,r.description);
            }
            catch (Exception ex)
            {
                string c = ex.Message;
                Program.logerror(c + "  " + "error_in_updateRainbow_method");
                throw new HttpResponseException(HttpStatusCode.ServiceUnavailable);
            }



        }

        [HttpGet]
        [Route("Single_Rainbow_Details")]
        public rainbow getSingleRainbowDetails(int rainbow_id)
        {
             Rainbow_manager rainbowDetails = new Rainbow_manager();
             return rainbowDetails.GetRainbowDetailsByRainbowId(rainbow_id);

        }

    }
        public class myexception : ExceptionFilterAttribute
        {
            public override void OnException(HttpActionExecutedContext context)
            {
                string myerrror_message = context.Exception.Message;
                string myerror_strack = context.Exception.StackTrace;
                File.WriteAllText("C:\\Code\\New Text Document.txt", myerrror_message + "    "+ myerror_strack);
                context.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

    public class myactionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            string a =  string.Format("Action Method {0} executing at {1}", actionContext.ActionDescriptor.ActionName, DateTime.Now.ToShortDateString());
            Program.loginfo(a);
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            string a = string.Format("Action Method {0} executed at {1}", actionExecutedContext.ActionContext.ActionDescriptor.ActionName, DateTime.Now.ToShortDateString());
            Program.loginfo(a);
            
        }
    }
}
