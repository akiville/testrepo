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
	public class DtrLogsOvertimeController : ApiController
	{
		// GET: api/DtrLogsOvertime
		public HttpResponseMessage Get(int id)
		{
            DtrLogsOvertimeCollection DtrLogsOvertime_list = new DtrLogsOvertimeCollection();
            DtrLogsOvertime dtrLogOvertime = new DtrLogsOvertime();
            dtrLogOvertime = DtrLogsOvertimeManager.GetItem(id);
            DtrLogsOvertime_list.Add(dtrLogOvertime);
            return Request.CreateResponse(HttpStatusCode.OK, new { count = DtrLogsOvertime_list.Count, dtrlogsovertimes = DtrLogsOvertime_list }, Configuration.Formatters.JsonFormatter);

        }

        // GET: api/DtrLogsOvertime/5
        public HttpResponseMessage Get(int filed_by_id, int employee_id, int branch_ot, DateTime start_date, DateTime end_date, Boolean is_sales, int lmm_id)
		{
			
			DtrLogsOvertimeCollection DtrLogsOvertime_list = new DtrLogsOvertimeCollection();
			DtrLogsOvertimeCriteria DtrLogsOvertime_criteria = new DtrLogsOvertimeCriteria();

            DtrLogsOvertime_criteria.mFiledById = filed_by_id;
            DtrLogsOvertime_criteria.mEmployeeId = employee_id;
            DtrLogsOvertime_criteria.mBranchOt = branch_ot;
            DtrLogsOvertime_criteria.mStartDate = start_date;
            DtrLogsOvertime_criteria.mEndDate = end_date;
            DtrLogsOvertime_criteria.mIsSales = is_sales;
            DtrLogsOvertime_criteria.mLmmId = lmm_id;

            DtrLogsOvertime_list = DtrLogsOvertimeManager.GetList(DtrLogsOvertime_criteria);
				
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  DtrLogsOvertime_list.Count, dtrlogsovertimes = DtrLogsOvertime_list }, Configuration.Formatters.JsonFormatter);
				
		}

		// POST: api/DtrLogsOvertime
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(DtrLogsOvertime DtrLogsOvertime)
		{
			if (DtrLogsOvertime.Validate())
			{
				int result;

				result = DtrLogsOvertimeManager.Save(DtrLogsOvertime);

                return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter);
            }
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/DtrLogsOvertime/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/DtrLogsOvertime/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(DtrLogsOvertime DtrLogsOvertime)
		{
			try
			{
				int result;

				result = DtrLogsOvertimeManager.Delete(DtrLogsOvertime);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}