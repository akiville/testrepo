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
	public class SalesScheduleController : ApiController
	{
        // GET: api/SalesSchedule
        public HttpResponseMessage Get(DateTime start_date, DateTime end_date, int lmm_id)
        {
            //try
            //{
            SalesScheduleCollection SalesSchedule_list = new SalesScheduleCollection();
            SalesScheduleCriteria SalesSchedule_criteria = new SalesScheduleCriteria();

            SalesSchedule_criteria.mStartDate = start_date;
            SalesSchedule_criteria.mEndDate = end_date;
            SalesSchedule_criteria.mLmmId = lmm_id;

            SalesSchedule_list = SalesScheduleManager.GetList(SalesSchedule_criteria);
            
                //return Request.CreateResponse(HttpStatusCode.OK, SalesSchedule_list);
            return Request.CreateResponse(HttpStatusCode.OK, new { count = SalesSchedule_list.Count, sales_schedules = SalesSchedule_list }, Configuration.Formatters.JsonFormatter);

            //}
            //catch
            //{
            //    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
            //}
        }

		// GET: api/SalesSchedule/5
		 public HttpResponseMessage Get(int id)
		{
			//try
			//{
				SalesScheduleCollection SalesSchedule_list = new SalesScheduleCollection();
				SalesScheduleCriteria SalesSchedule_criteria = new SalesScheduleCriteria();

				SalesSchedule_list = SalesScheduleManager.GetList(SalesSchedule_criteria);
				if (SalesSchedule_list.Count > 0 ) 
				{
					return Request.CreateResponse(HttpStatusCode.OK, SalesSchedule_list);
				}
				else
				{
					return Request.CreateResponse(HttpStatusCode.BadRequest, "No record found");
				}
			//}
			//catch
			//{
			//	return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error" );
			//}
		}

		// POST: api/SalesSchedule
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(SalesSchedule SalesSchedule)
		{
			if (SalesSchedule.Validate())
			{
				int result;

				result = SalesScheduleManager.Save(SalesSchedule);

                return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter);
            }
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/SalesSchedule/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/SalesSchedule/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(SalesSchedule SalesSchedule)
		{
			try
			{
				int result;

				result = SalesScheduleManager.Delete(SalesSchedule);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}