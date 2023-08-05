using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Collections.Generic;
using Qtc.Branch.Bll;
using Qtc.Branch.BusinessEntities;
using System.IO;
using System.Web;

namespace Qtc.Branch.Api.Controllers
{
	public class RovingPlanScheduledActualChecklistImageController : ApiController
	{
		// GET: api/RovingPlanScheduledActualChecklistImage
		public HttpResponseMessage Get(int id)
        {
            RovingPlanScheduledActualChecklistImageCollection RovingPlanScheduledActualChecklistImage_list = new RovingPlanScheduledActualChecklistImageCollection();
            RovingPlanScheduledActualChecklistImage roving_plan_schedule_actual_checklist_image = new RovingPlanScheduledActualChecklistImage();

            roving_plan_schedule_actual_checklist_image = RovingPlanScheduledActualChecklistImageManager.GetItem(id);
            RovingPlanScheduledActualChecklistImage_list.Add(roving_plan_schedule_actual_checklist_image);
            return Request.CreateResponse(HttpStatusCode.OK, new { count = RovingPlanScheduledActualChecklistImage_list.Count, rovingplanscheduledactualchecklistimages = RovingPlanScheduledActualChecklistImage_list }, Configuration.Formatters.JsonFormatter);
        }

		// GET: api/RovingPlanScheduledActualChecklistImage/5
		 public HttpResponseMessage Get(int roving_checklist_actual_id, String remarks)
		{
			
			RovingPlanScheduledActualChecklistImageCollection RovingPlanScheduledActualChecklistImage_list = new RovingPlanScheduledActualChecklistImageCollection();
			RovingPlanScheduledActualChecklistImageCriteria RovingPlanScheduledActualChecklistImage_criteria = new RovingPlanScheduledActualChecklistImageCriteria();
            RovingPlanScheduledActualChecklistImage_criteria.mRovingChecklistActualId = roving_checklist_actual_id;

            RovingPlanScheduledActualChecklistImage_list = RovingPlanScheduledActualChecklistImageManager.GetList(RovingPlanScheduledActualChecklistImage_criteria);
				
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  RovingPlanScheduledActualChecklistImage_list.Count, rovingplanscheduledactualchecklistimages = RovingPlanScheduledActualChecklistImage_list }, Configuration.Formatters.JsonFormatter);
				
		}

        // POST: api/RovingPlanScheduledActualChecklistImage
        //[HttpPost]
        //[CustomAuthorizationFilter]
        //public HttpResponseMessage Post(RovingPlanScheduledActualChecklistImage RovingPlanScheduledActualChecklistImage)
        //{
        //	if (RovingPlanScheduledActualChecklistImage.Validate())
        //	{
        //		int result;

        //		result = RovingPlanScheduledActualChecklistImageManager.Save(RovingPlanScheduledActualChecklistImage);

        //		//return Request.CreateResponse(HttpStatusCode.OK, result); 
        //		return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
        //	}
        //	else
        //	{
        //		return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
        //	}
        //}

        // POST: api/RovingPlanScheduledActualChecklistImage
        [HttpPost]
        public HttpResponseMessage Post()
        {
            //try
            //{
            var request = HttpContext.Current.Request;

            var id = request.Form["id"];
            var roving_checklist_actual_id = request.Form["roving_checklist_actual_id"];
            var image_url = request.Form["image_url"];
            var remarks = request.Form["remarks"];
            var disable = request.Form["disable"];
            var record_id = request.Form["record_id"];
            var rps_id = request.Form["rps_id"];
            var rc_id = request.Form["rc_id"];
            var picture = request.Files["picture"];

            byte[] bytes;
            using (var stream = new MemoryStream())
            {
                picture.InputStream.CopyTo(stream);
                bytes = stream.ToArray();
            }

            RovingPlanScheduledActualChecklistImage RovingPlanScheduledActualChecklistImage = new RovingPlanScheduledActualChecklistImage();

            RovingPlanScheduledActualChecklistImage.mId = Int32.Parse(id);
            RovingPlanScheduledActualChecklistImage.mRovingChecklistActualId = Int32.Parse(roving_checklist_actual_id);
            RovingPlanScheduledActualChecklistImage.mImageUrl = image_url;
            RovingPlanScheduledActualChecklistImage.mRemarks = remarks;
            RovingPlanScheduledActualChecklistImage.mDisable = Boolean.Parse(disable);
            RovingPlanScheduledActualChecklistImage.mRecordId = Int32.Parse(record_id);
            RovingPlanScheduledActualChecklistImage.mRpsId = Int32.Parse(rps_id);
            RovingPlanScheduledActualChecklistImage.mRcId = Int32.Parse(rc_id);
            RovingPlanScheduledActualChecklistImage.mDatestamp = DateTime.Now;

            if (RovingPlanScheduledActualChecklistImage.Validate())
            {
                picture.SaveAs(HttpContext.Current.Server.MapPath("~/RovingPlanImage/" + picture.FileName));

                int result;
                result = RovingPlanScheduledActualChecklistImageManager.Save(RovingPlanScheduledActualChecklistImage);
                return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request");
            }
                //BranchCash branchcash = new BranchCash();

                //branchcash = BranchCashManager.GetItem(Int32.Parse(id));

                //branchcash.mDepositedById = Int32.Parse(deposited_by_id);
                //branchcash.mIsDeposited = Int32.Parse(is_deposited);
                //branchcash.mDepositDate = DateTime.Parse(deposit_date);
                //branchcash.mDepositSlipImageName = deposit_slip_image_name;
                //branchcash.mCashExplanation = cash_explanation;

                //if (branchcash.Validate())
                //{
                //    picture.SaveAs(HttpContext.Current.Server.MapPath("~/DepositSlip/" + picture.FileName));

                //    int result;
                //    result = BranchCashManager.Save(branchcash);
                //    return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter);
                //}
                //else
                //{
                //    return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request");
                //}

            //}
            //catch
            //{
            //    return new HttpResponseMessage(HttpStatusCode.BadRequest);
            //}
        }

        // PUT: api/RovingPlanScheduledActualChecklistImage/5
        //[HttpPost]
        //[CustomAuthorizationFilter]
        public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/RovingPlanScheduledActualChecklistImage/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(RovingPlanScheduledActualChecklistImage RovingPlanScheduledActualChecklistImage)
		{
			try
			{
				int result;

				result = RovingPlanScheduledActualChecklistImageManager.Delete(RovingPlanScheduledActualChecklistImage);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}