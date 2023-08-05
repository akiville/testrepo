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
    public class HRLetterRequestSystemController : ApiController
    {
     
        // POST: api/HRLetterRequestSystem
        //[HttpPost]
        //[CustomAuthorizationFilter]
        public HttpResponseMessage Post()
        {
            HrLetterRequest HrLetterRequest = new HrLetterRequest();

            var request = HttpContext.Current.Request;
            HrLetterRequest.mId = Int32.Parse(request.Form["id"]);
            HrLetterRequest.mEmployeeId = Int32.Parse(request.Form["employee_id"]);
            HrLetterRequest.mBranchId = Int32.Parse(request.Form["branch_id"]);
            HrLetterRequest.mDurationFrom = DateTime.Parse(request.Form["duration_from"]);
            HrLetterRequest.mDurationTo  = DateTime.Parse(request.Form["duration_to"]);
            HrLetterRequest.mStartDate = DateTime.Parse(request.Form["start_date"]);
            HrLetterRequest.mEndDate = DateTime.Parse(request.Form["end_date"]);
            HrLetterRequest.mTypeOfLetterId = Int32.Parse(request.Form["type_of_letter_id"]);
            HrLetterRequest.mUserId = Int32.Parse(request.Form["user_id"]);
            HrLetterRequest.mRemarks = request.Form["remarks"];
            HrLetterRequest.mBranchIdTo = Int32.Parse(request.Form["branch_id_to"]);
            HrLetterRequest.mCopiesCount = Int32.Parse(request.Form["copies_count"]);
            HrLetterRequest.mRequestedBy = Int32.Parse(request.Form["requested_by"]);
            HrLetterRequest.mRequestReleasedDate = DateTime.Parse(request.Form["request_release_date"]);
            HrLetterRequest.mAgencyId = Int32.Parse(request.Form["agency_id"]);
            HrLetterRequest.mFileName = request.Form["file_name"];
            HrLetterRequest.mUserFullName = "";

            var file = request.Files["file"];

            byte[] bytes;
            using (var stream = new MemoryStream())
            {
                file.InputStream.CopyTo(stream);
                bytes = stream.ToArray();
            }
            
            if (HrLetterRequest.Validate())
            {
                file.SaveAs(HttpContext.Current.Server.MapPath("~/HRLetterRequest/" + file.FileName));

                int result;

                result = HrLetterRequestManager.Save(HrLetterRequest);

                //return Request.CreateResponse(HttpStatusCode.OK, result); 
                return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request");
            }


        }
    }
}