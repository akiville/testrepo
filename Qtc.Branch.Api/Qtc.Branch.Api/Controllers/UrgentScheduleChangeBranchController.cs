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
	public class UrgentScheduleChangeBranchController : ApiController
	{
        // GET: api/UrgentScheduleChangeBranch
        public HttpResponseMessage Get(int id)
        {
            UrgentScheduleChangeBranchCollection UrgentScheduleChangeBranch_list = new UrgentScheduleChangeBranchCollection();
            UrgentScheduleChangeBranch UrgentScheduleChangeBranch = new UrgentScheduleChangeBranch();
            UrgentScheduleChangeBranch = UrgentScheduleChangeBranchManager.GetItem(id);
            UrgentScheduleChangeBranch_list.Add(UrgentScheduleChangeBranch);
            return Request.CreateResponse(HttpStatusCode.OK, new { count = UrgentScheduleChangeBranch_list.Count, urgentschedulechangebranchs = UrgentScheduleChangeBranch_list }, Configuration.Formatters.JsonFormatter);

        }

        // GET: api/UrgentScheduleChangeBranch/5
        public HttpResponseMessage Get(int urgent_schedule_change_id, String dummy)
		{
			
				UrgentScheduleChangeBranchCollection UrgentScheduleChangeBranch_list = new UrgentScheduleChangeBranchCollection();
				UrgentScheduleChangeBranchCriteria UrgentScheduleChangeBranch_criteria = new UrgentScheduleChangeBranchCriteria();

				UrgentScheduleChangeBranch_list = UrgentScheduleChangeBranchManager.GetList(UrgentScheduleChangeBranch_criteria);
				
				return Request.CreateResponse(HttpStatusCode.OK, new { count =  UrgentScheduleChangeBranch_list.Count, urgentschedulechangebranchs = UrgentScheduleChangeBranch_list }, Configuration.Formatters.JsonFormatter);
				
		}

		// POST: api/UrgentScheduleChangeBranch
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(UrgentScheduleChangeBranch UrgentScheduleChangeBranch)
		{
			if (UrgentScheduleChangeBranch.Validate())
			{
				int result;

				result = UrgentScheduleChangeBranchManager.Save(UrgentScheduleChangeBranch);

                return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter);
            }
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/UrgentScheduleChangeBranch/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/UrgentScheduleChangeBranch/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(UrgentScheduleChangeBranch UrgentScheduleChangeBranch)
		{
			try
			{
				int result;

				result = UrgentScheduleChangeBranchManager.Delete(UrgentScheduleChangeBranch);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}