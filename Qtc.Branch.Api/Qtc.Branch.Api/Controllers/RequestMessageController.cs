using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Collections.Generic;
using Qtc.Branch.Bll;
using Qtc.Branch.BusinessEntities;
using System.Web;
using System.IO;

namespace Qtc.Branch.Api.Controllers
{
	public class RequestMessageController : ApiController
	{
		// GET: api/RequestMessage
		//public IEnumerable<string> Get()
		//{
		//	return new string[] { "value1", "value2" };
		//}

		// GET: api/RequestMessage/5
		 public HttpResponseMessage Get(int topic_id, int user_id, int branch_id, int original_message_id, DateTime start_date, DateTime end_date)
		{
			//try
			//{
				RequestMessageCollection RequestMessage_list = new RequestMessageCollection();
				RequestMessageCriteria RequestMessage_criteria = new RequestMessageCriteria();

                RequestMessage_criteria.mUserId = user_id;
                RequestMessage_criteria.mTopicId = topic_id;
                RequestMessage_criteria.mBranchId = branch_id;
                RequestMessage_criteria.mOriginalMessageId = original_message_id;
                RequestMessage_criteria.mStartDate = start_date;
                RequestMessage_criteria.mEndDate = end_date;

                RequestMessage_list = RequestMessageManager.GetList(RequestMessage_criteria);
				if (RequestMessage_list.Count > 0 ) 
				{
					return Request.CreateResponse(HttpStatusCode.OK, new { count =  RequestMessage_list.Count, requestmessages = RequestMessage_list }, Configuration.Formatters.JsonFormatter);
				}
				else
				{
					return Request.CreateResponse(HttpStatusCode.BadRequest, "No record found");
				}
			//}
			//catch
			//{
			//	return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error" );
			//}
		}

        // POST: api/RequestMessage
        [HttpPost]
        public HttpResponseMessage Upload()
        {
            try
            {

                var request = HttpContext.Current.Request;

                var id = request.Form["id"];
                var branch_id = request.Form["branch_id"];
                var user_id = request.Form["user_id"];
                var topic_id = request.Form["topic_id"];
                var message = request.Form["message"];

                var picture = request.Files["picture"];

                var original_message_id = request.Form["original_message_id"];
                var is_seen = request.Form["is_seen"];
                var date_seen = request.Form["date_seen"];
                var message_date = request.Form["message_date"];
                var employee_id = request.Form["employee_id"];
                var incident_date = request.Form["incident_date"];

                byte[] bytes;
                using (var stream = new MemoryStream())
                {
                    picture.InputStream.CopyTo(stream);
                    bytes = stream.ToArray();
                }


                RequestMessage request_message = new RequestMessage();

                request_message.mId = Int32.Parse(id);
                request_message.mBranchId = Int32.Parse(branch_id);
                request_message.mUserId = Int32.Parse(user_id);
                request_message.mTopicId = Int32.Parse(topic_id);
                request_message.mMessage = message;
                request_message.mPicture = bytes;
                request_message.mOriginalMessageId = 0;
                request_message.mIsSeen = false;
                request_message.mDateSeen = DateTime.Now;
                request_message.mUserFullName = "";
                request_message.mMessageDate = DateTime.Now;
                request_message.mDatestamp = DateTime.Now;
                request_message.mEmployeeId = Int32.Parse(employee_id);
                request_message.mIncidentDate = DateTime.Parse(incident_date);

                if (request_message.Validate())
                {
                    int result;

                    result = RequestMessageManager.Save(request_message);

                    return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request");
                }

                //picture.SaveAs(HttpContext.Current.Server.MapPath("~/ReportImage/" + picture.FileName));
                //return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        // POST: api/RequestMessage
        //[HttpPost]
        //[CustomAuthorizationFilter]
        //public HttpResponseMessage Post(RequestMessage RequestMessage)
        //{
        //    if (RequestMessage.Validate())
        //    {
        //        int result;

        //        result = RequestMessageManager.Save(RequestMessage);

        //        return Request.CreateResponse(HttpStatusCode.OK, result);
        //    }
        //    else
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request");
        //    }
        //}



        // PUT: api/RequestMessage/5
        //[HttpPost]
        //[CustomAuthorizationFilter]
        public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/RequestMessage/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(RequestMessage RequestMessage)
		{
			try
			{
				int result;

				result = RequestMessageManager.Delete(RequestMessage);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}