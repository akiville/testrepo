using Qtc.Branch.Api.Utilities;
using Qtc.Branch.Bll;
using Qtc.Branch.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Qtc.Branch.Api.Controllers
{
    [RoutePrefix("api/ImageUpload")]
    public class ImageUploadController : ApiController
    {
        [Route("upload")]
        [HttpPost]
       public HttpResponseMessage Upload()
        {
            //try
            //{

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
            //}
            //catch
            //{
            //    return new HttpResponseMessage(HttpStatusCode.BadRequest);
            //}
        }
    }
}
