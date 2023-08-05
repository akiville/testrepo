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
	public class GreenSlipRequestDetailController : ApiController
	{
		// GET: api/GreenSlipRequestDetail
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		// GET: api/GreenSlipRequestDetail/5
		 public HttpResponseMessage Get(int greenslip_request_id)
		{
			try
			{
				GreenSlipRequestDetailCollection GreenSlipRequestDetail_list = new GreenSlipRequestDetailCollection();
				GreenSlipRequestDetailCriteria GreenSlipRequestDetail_criteria = new GreenSlipRequestDetailCriteria();

                GreenSlipRequestDetail_criteria.mGreenSlipId = greenslip_request_id;

                GreenSlipRequestDetail_list = GreenSlipRequestDetailManager.GetList(GreenSlipRequestDetail_criteria);
                //if (GreenSlipRequestDetail_list.Count > 0 ) 
                //{
                return Request.CreateResponse(HttpStatusCode.OK, new { count =  GreenSlipRequestDetail_list.Count, greensliprequestdetails = GreenSlipRequestDetail_list }, Configuration.Formatters.JsonFormatter);
				//}
				//else
				//{
				//	return Request.CreateResponse(HttpStatusCode.BadRequest, "No record found");
				//}
			}
			catch
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error" );
			}
		}

		// POST: api/GreenSlipRequestDetail
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(GreenSlipRequestDetail GreenSlipRequestDetail)
		{
			if (GreenSlipRequestDetail.Validate())
			{
				int result;

				result = GreenSlipRequestDetailManager.Save(GreenSlipRequestDetail);

                return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter);
            }
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/GreenSlipRequestDetail/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/GreenSlipRequestDetail/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(GreenSlipRequestDetail GreenSlipRequestDetail)
		{
			try
			{
				int result;

				result = GreenSlipRequestDetailManager.Delete(GreenSlipRequestDetail);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}