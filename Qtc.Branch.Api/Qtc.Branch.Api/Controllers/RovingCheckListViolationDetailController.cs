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
	public class RovingCheckListViolationDetailController : ApiController
	{
		// GET: api/RovingCheckListViolationDetail
		 public HttpResponseMessage Get(int id)
		{
			RovingCheckListViolationDetailCollection RovingCheckListViolationDetail_list = new RovingCheckListViolationDetailCollection();
			RovingCheckListViolationDetail RovingCheckListViolationDetail = new RovingCheckListViolationDetail();
			RovingCheckListViolationDetail = RovingCheckListViolationDetailManager.GetItem(id);
			RovingCheckListViolationDetail_list.Add(RovingCheckListViolationDetail);
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  RovingCheckListViolationDetail_list.Count, rovingchecklistviolationdetails = RovingCheckListViolationDetail_list }, Configuration.Formatters.JsonFormatter);
		}

		// GET: api/RovingCheckListViolationDetail/5
		 public HttpResponseMessage Get(int roving_checklist_violation, int rps_id, int rps_chklist_id, int violation_id)
		{
			RovingCheckListViolationDetailCollection RovingCheckListViolationDetail_list = new RovingCheckListViolationDetailCollection();
			RovingCheckListViolationDetailCriteria RovingCheckListViolationDetail_criteria = new RovingCheckListViolationDetailCriteria();

			RovingCheckListViolationDetail_list = RovingCheckListViolationDetailManager.GetList(RovingCheckListViolationDetail_criteria);
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  RovingCheckListViolationDetail_list.Count, rovingchecklistviolationdetails = RovingCheckListViolationDetail_list }, Configuration.Formatters.JsonFormatter);
		}

        // POST: api/RovingCheckListViolationDetail
        [HttpPost]
        //[CustomAuthorizationFilter]
        public HttpResponseMessage Post()
        {
            var request = HttpContext.Current.Request;
            var id = request.Form["id"];
            var roving_checklist_violation_id = request.Form["roving_checklist_violation_id"];
            var rps_id = request.Form["rps_id"];
            var rps_chklist_id = request.Form["rps_chklist_id"];
            var violation_id = request.Form["violation_id"];
            var image_file_name = request.Form["image_file_name"];
            var remarks = request.Form["remarks"];
            var record_id = request.Form["record_id"];
            var disable = request.Form["disable"];
            var picture = request.Files["picture"];

            byte[] bytes;
            using (var stream = new MemoryStream())
            {
                picture.InputStream.CopyTo(stream);
                bytes = stream.ToArray();
            }

            RovingCheckListViolationDetail rovingCheckListViolationDetail = new RovingCheckListViolationDetail();

            rovingCheckListViolationDetail.mId = Int32.Parse(id);
            rovingCheckListViolationDetail.mRovingChecklistViolationId = Int32.Parse(roving_checklist_violation_id);
            rovingCheckListViolationDetail.mRpsId = Int32.Parse(rps_id);
            rovingCheckListViolationDetail.mRpsChklistId = Int32.Parse(rps_chklist_id);
            rovingCheckListViolationDetail.mViolationId = Int32.Parse(violation_id);
            rovingCheckListViolationDetail.mImageFileName = image_file_name;
            rovingCheckListViolationDetail.mRemarks = remarks;
            rovingCheckListViolationDetail.mRecordId = Int32.Parse(record_id);
            rovingCheckListViolationDetail.mDisable = Boolean.Parse(disable);

            if (rovingCheckListViolationDetail.Validate())
            {

                picture.SaveAs(HttpContext.Current.Server.MapPath("~/RovingPlanImage/" + picture.FileName));

                int result;
                result = RovingCheckListViolationDetailManager.Save(rovingCheckListViolationDetail);
                //return Request.CreateResponse(HttpStatusCode.OK, result); 
                return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request");
            }
        }

		// PUT: api/RovingCheckListViolationDetail/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/RovingCheckListViolationDetail/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(RovingCheckListViolationDetail RovingCheckListViolationDetail)
		{
			try
			{
				int result;

				result = RovingCheckListViolationDetailManager.Delete(RovingCheckListViolationDetail);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}