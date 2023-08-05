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
	public class RequestForScheduleChangeController : ApiController
	{
		// GET: api/RequestForScheduleChange
		public HttpResponseMessage Get(int id)
		{
            try
            {
                RequestForScheduleChangeCollection RequestForScheduleChange_list = new RequestForScheduleChangeCollection();
                RequestForScheduleChangeCriteria RequestForScheduleChange_criteria = new RequestForScheduleChangeCriteria();
                RequestForScheduleChange requestForScheduleChange;

                requestForScheduleChange = RequestForScheduleChangeManager.GetItem(id);


                return Request.CreateResponse(HttpStatusCode.OK, new { count = 1, requestforschedulechanges = requestForScheduleChange }, Configuration.Formatters.JsonFormatter);
               
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
            }
        }

		// GET: api/RequestForScheduleChange/5
		 public HttpResponseMessage Get(int lmm_id, DateTime start_date, DateTime end_date)
		{
			try
			{
				RequestForScheduleChangeCollection RequestForScheduleChange_list = new RequestForScheduleChangeCollection();
				RequestForScheduleChangeCriteria RequestForScheduleChange_criteria = new RequestForScheduleChangeCriteria();

                RequestForScheduleChange_criteria.mLmmId = lmm_id;
                RequestForScheduleChange_criteria.mStartDate = start_date;
                RequestForScheduleChange_criteria.mEndDate = end_date;

                RequestForScheduleChange_list = RequestForScheduleChangeManager.GetList(RequestForScheduleChange_criteria);
				
				return Request.CreateResponse(HttpStatusCode.OK, new { count =  RequestForScheduleChange_list.Count, requestforschedulechanges = RequestForScheduleChange_list }, Configuration.Formatters.JsonFormatter);
				
			}
			catch
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error" );
			}
		}

		// POST: api/RequestForScheduleChange
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(RequestForScheduleChange RequestForScheduleChange)
		{
			if (RequestForScheduleChange.Validate())
			{
				int result;

				result = RequestForScheduleChangeManager.Save(RequestForScheduleChange);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/RequestForScheduleChange/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/RequestForScheduleChange/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(RequestForScheduleChange RequestForScheduleChange)
		{
			try
			{
				int result;

				result = RequestForScheduleChangeManager.Delete(RequestForScheduleChange);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}