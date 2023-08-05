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
	public class RequestForIssuanceController : ApiController
	{
        //GET: api/RequestForIssuance
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}
    
        // GET: api/RequestForIssuance
        public HttpResponseMessage Get(int branch_id, Boolean is_ho_downloaded)
		{
			try
			{
				RequestForIssuanceCollection RequestForIssuance_list = new RequestForIssuanceCollection();
				RequestForIssuanceCriteria RequestForIssuance_criteria = new RequestForIssuanceCriteria();

                RequestForIssuance_criteria.mIsHoDownloaded = is_ho_downloaded;
                RequestForIssuance_criteria.mBranchId = branch_id;

                RequestForIssuance_list = RequestForIssuanceManager.GetList(RequestForIssuance_criteria);
				//if (RequestForIssuance_list.Count > 0 ) 
				//{
					return Request.CreateResponse(HttpStatusCode.OK, new { count =  RequestForIssuance_list.Count, requestforissuances = RequestForIssuance_list }, Configuration.Formatters.JsonFormatter);
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

		// POST: api/RequestForIssuance
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(RequestForIssuance RequestForIssuance)
		{
			if (RequestForIssuance.Validate())
			{
				int result;

				result = RequestForIssuanceManager.Save(RequestForIssuance);

                return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter);
            }
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/RequestForIssuance/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/RequestForIssuance/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(RequestForIssuance RequestForIssuance)
		{
			try
			{
				int result;

				result = RequestForIssuanceManager.Delete(RequestForIssuance);

                return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter);
            }
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}