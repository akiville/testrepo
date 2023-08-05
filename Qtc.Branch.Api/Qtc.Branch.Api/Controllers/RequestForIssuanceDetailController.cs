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
	public class RequestForIssuanceDetailController : ApiController
	{
		// GET: api/RequestForIssuanceDetail
		//public IEnumerable<string> Get()
		//{
		//	return new string[] { "value1", "value2" };
		//}

		// GET: api/RequestForIssuanceDetail/5
		 public HttpResponseMessage Get(int request_for_issuance_id)
		{
			try
			{
				RequestForIssuanceDetailCollection RequestForIssuanceDetail_list = new RequestForIssuanceDetailCollection();
				RequestForIssuanceDetailCriteria RequestForIssuanceDetail_criteria = new RequestForIssuanceDetailCriteria();
            
                RequestForIssuanceDetail_criteria.mRequestForIssuanceId = request_for_issuance_id;

                RequestForIssuanceDetail_list = RequestForIssuanceDetailManager.GetList(RequestForIssuanceDetail_criteria);
				//if (RequestForIssuanceDetail_list.Count > 0 ) 
				//{
					return Request.CreateResponse(HttpStatusCode.OK, new { count =  RequestForIssuanceDetail_list.Count, requestforissuancedetails = RequestForIssuanceDetail_list }, Configuration.Formatters.JsonFormatter);
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

		// POST: api/RequestForIssuanceDetail
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(RequestForIssuanceDetail RequestForIssuanceDetail)
		{
			if (RequestForIssuanceDetail.Validate())
			{
				int result;

				result = RequestForIssuanceDetailManager.Save(RequestForIssuanceDetail);

                return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter);
            }
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/RequestForIssuanceDetail/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/RequestForIssuanceDetail/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(RequestForIssuanceDetail RequestForIssuanceDetail)
		{
			try
			{
				int result;

				result = RequestForIssuanceDetailManager.Delete(RequestForIssuanceDetail);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}