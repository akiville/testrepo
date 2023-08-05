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
	public class HrLetterRequestController : ApiController
	{
		// GET: api/HrLetterRequest
		 public HttpResponseMessage Get(int id)
		{
			HrLetterRequestCollection HrLetterRequest_list = new HrLetterRequestCollection();
			HrLetterRequest HrLetterRequest = new HrLetterRequest();
			HrLetterRequest = HrLetterRequestManager.GetItem(id);
			HrLetterRequest_list.Add(HrLetterRequest);
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  HrLetterRequest_list.Count, hrletterrequests = HrLetterRequest_list }, Configuration.Formatters.JsonFormatter);
		}

		// GET: api/HrLetterRequest/5
		 public HttpResponseMessage Get(int id, String type)
		{
			HrLetterRequestCollection HrLetterRequest_list = new HrLetterRequestCollection();
			HrLetterRequestCriteria HrLetterRequest_criteria = new HrLetterRequestCriteria();

            HrLetterRequest_criteria.mId = id;

            HrLetterRequest_list = HrLetterRequestManager.GetList(HrLetterRequest_criteria);
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  HrLetterRequest_list.Count, hrletterrequests = HrLetterRequest_list }, Configuration.Formatters.JsonFormatter);
		}

		// POST: api/HrLetterRequest
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(HrLetterRequest HrLetterRequest)
		{
			if (HrLetterRequest.Validate())
			{
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

		// PUT: api/HrLetterRequest/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/HrLetterRequest/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(HrLetterRequest HrLetterRequest)
		{
			try
			{
				int result;

				result = HrLetterRequestManager.Delete(HrLetterRequest);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}