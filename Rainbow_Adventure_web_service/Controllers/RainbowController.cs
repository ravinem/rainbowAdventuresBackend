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


namespace Rainbow_Adventure_web_service.Controllers
{
     
    public class RainbowController : ApiController
    {
        [HttpPost]
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
            catch (Exception ex)
            {
                string a = ex.Message;
                Program.logerror(a + "  " + "error_in_deleteRainbow_method");
                throw new HttpResponseException(HttpStatusCode.RequestTimeout);
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
        public bool rainbow_uipdate([FromBody] rainbow r)
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
      
    
}
