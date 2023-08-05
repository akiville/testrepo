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
	public class SalesSchedulingConfirmationController : ApiController
	{
        //GET: api/SalesSchedulingConfirmation/5
        public HttpResponseMessage Get(int id)
        {
            SalesSchedulingConfirmationCollection SalesSchedulingConfirmation_list = new SalesSchedulingConfirmationCollection();
            SalesSchedulingConfirmation salesSchedulingConfirmation = new SalesSchedulingConfirmation();
            salesSchedulingConfirmation = SalesSchedulingConfirmationManager.GetItem(id);
            SalesSchedulingConfirmation_list.Add(salesSchedulingConfirmation);
            return Request.CreateResponse(HttpStatusCode.OK, new { count = SalesSchedulingConfirmation_list.Count, salesschedulingconfirmations = SalesSchedulingConfirmation_list }, Configuration.Formatters.JsonFormatter);
        }

        // GET: api/SalesSchedulingConfirmation
        public HttpResponseMessage Get(DateTime start_date, DateTime end_date, int lmm_id)
		{
			//try
			//{
				SalesSchedulingConfirmationCollection SalesSchedulingConfirmation_list = new SalesSchedulingConfirmationCollection();
				SalesSchedulingConfirmationCriteria SalesSchedulingConfirmation_criteria = new SalesSchedulingConfirmationCriteria();

                SalesSchedulingConfirmation_criteria.mStartDate = start_date;
                SalesSchedulingConfirmation_criteria.mEndDate = end_date;
                SalesSchedulingConfirmation_criteria.mLmmId = lmm_id;

                SalesSchedulingConfirmation_list = SalesSchedulingConfirmationManager.GetList(SalesSchedulingConfirmation_criteria);
				//if (SalesSchedulingConfirmation_list.Count > 0 ) 
				//{
					return Request.CreateResponse(HttpStatusCode.OK, new { count =  SalesSchedulingConfirmation_list.Count, salesschedulingconfirmations = SalesSchedulingConfirmation_list }, Configuration.Formatters.JsonFormatter);
				//}
				//else
				//{
				//	return Request.CreateResponse(HttpStatusCode.BadRequest, "No record found");
				//}
			//}
			//catch
			//{
			//	return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error" );
			//}
		}

		// POST: api/SalesSchedulingConfirmation
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(SalesSchedulingConfirmation SalesSchedulingConfirmation)
		{
			if (SalesSchedulingConfirmation.Validate())
			{
				int result;

				result = SalesSchedulingConfirmationManager.Save(SalesSchedulingConfirmation);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/SalesSchedulingConfirmation/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/SalesSchedulingConfirmation/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(SalesSchedulingConfirmation SalesSchedulingConfirmation)
		{
			try
			{
				int result;

				result = SalesSchedulingConfirmationManager.Delete(SalesSchedulingConfirmation);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}