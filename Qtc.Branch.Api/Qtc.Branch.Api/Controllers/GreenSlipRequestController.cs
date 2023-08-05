using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Collections.Generic;
using Qtc.Branch.Bll;
using Qtc.Branch.BusinessEntities;


namespace Qtc.Branch.Api.Controllers
{
	public class GreenSlipRequestController : ApiController
	{
		// GET: api/GreenSlipRequest
		public HttpResponseMessage Get(int id)
		{
            try
            {
                GreenSlipRequestCollection GreenSlipRequest_list = new GreenSlipRequestCollection();
                GreenSlipRequest greenslip_request = new GreenSlipRequest();

                greenslip_request = GreenSlipRequestManager.GetItem(id);

                GreenSlipRequest_list.Add(greenslip_request);
                

                return Request.CreateResponse(HttpStatusCode.OK, new { count = GreenSlipRequest_list.Count, greensliprequests = GreenSlipRequest_list }, Configuration.Formatters.JsonFormatter);

            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
            }
        }

		// GET: api/GreenSlipRequest
		 public HttpResponseMessage Get(int branch_id, Boolean taken_action, Boolean is_downloaded, int id)
		{
			try
			{
				GreenSlipRequestCollection GreenSlipRequest_list = new GreenSlipRequestCollection();
				GreenSlipRequestCriteria GreenSlipRequest_criteria = new GreenSlipRequestCriteria();

                GreenSlipRequest_criteria.mId = id;
                GreenSlipRequest_criteria.mBranchId = branch_id;
                GreenSlipRequest_criteria.mTakenAction = taken_action;
                GreenSlipRequest_criteria.mIsDownloaded = is_downloaded;

                GreenSlipRequest_list = GreenSlipRequestManager.GetList(GreenSlipRequest_criteria);
				
				return Request.CreateResponse(HttpStatusCode.OK, new { count =  GreenSlipRequest_list.Count, greensliprequests = GreenSlipRequest_list }, Configuration.Formatters.JsonFormatter);
				
			}
			catch
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error" );
			}
		}

		// POST: api/GreenSlipRequest
		//[HttpPost]
		public HttpResponseMessage Post(GreenSlipRequest GreenSlipRequest)
		{
			if (GreenSlipRequest.Validate())
			{
				int result;

				result = GreenSlipRequestManager.Save(GreenSlipRequest);

                return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter);
            }
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/GreenSlipRequest/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/GreenSlipRequest/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(GreenSlipRequest GreenSlipRequest)
		{
			try
			{
				int result;

				result = GreenSlipRequestManager.Delete(GreenSlipRequest);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}