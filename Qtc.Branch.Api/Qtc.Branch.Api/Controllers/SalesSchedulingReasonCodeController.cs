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
	public class SalesSchedulingReasonCodeController : ApiController
	{
        // GET: api/SalesSchedulingReasonCode
        public HttpResponseMessage Get(int id)
        {
            try
            {
                SalesSchedulingReasonCode salesSchedulingReasonCode = new SalesSchedulingReasonCode();


                salesSchedulingReasonCode = SalesSchedulingReasonCodeManager.GetItem(id);

                return Request.CreateResponse(HttpStatusCode.OK, new { count = 1, salesschedulingreasoncodes = salesSchedulingReasonCode }, Configuration.Formatters.JsonFormatter);

            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
            }
        }

		// GET: api/SalesSchedulingReasonCode/5
		 public HttpResponseMessage Get()
		{
			try
			{
				SalesSchedulingReasonCodeCollection SalesSchedulingReasonCode_list = new SalesSchedulingReasonCodeCollection();
				SalesSchedulingReasonCodeCriteria SalesSchedulingReasonCode_criteria = new SalesSchedulingReasonCodeCriteria();

				SalesSchedulingReasonCode_list = SalesSchedulingReasonCodeManager.GetList(SalesSchedulingReasonCode_criteria);
				
				return Request.CreateResponse(HttpStatusCode.OK, new { count =  SalesSchedulingReasonCode_list.Count, salesschedulingreasoncodes = SalesSchedulingReasonCode_list }, Configuration.Formatters.JsonFormatter);
				
			}
			catch
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error" );
			}
		}

		// POST: api/SalesSchedulingReasonCode
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(SalesSchedulingReasonCode SalesSchedulingReasonCode)
		{
			if (SalesSchedulingReasonCode.Validate())
			{
				int result;

				result = SalesSchedulingReasonCodeManager.Save(SalesSchedulingReasonCode);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/SalesSchedulingReasonCode/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/SalesSchedulingReasonCode/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(SalesSchedulingReasonCode SalesSchedulingReasonCode)
		{
			try
			{
				int result;

				result = SalesSchedulingReasonCodeManager.Delete(SalesSchedulingReasonCode);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}