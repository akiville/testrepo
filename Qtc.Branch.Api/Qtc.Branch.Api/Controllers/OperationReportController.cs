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
	public class OperationReportController : ApiController
	{
        // GET: api/OperationReport
        public HttpResponseMessage Get(int id)
		{
            OperationReportCollection OperationReport_list = new OperationReportCollection();
            OperationReport operationReport = new OperationReport();

            operationReport = OperationReportManager.GetItem(id);
            OperationReport_list.Add(operationReport);
            return Request.CreateResponse(HttpStatusCode.OK, new { count = OperationReport_list.Count, operationreports = OperationReport_list }, Configuration.Formatters.JsonFormatter);

        }

        // GET: api/OperationReport/5
        public HttpResponseMessage Get(int lmm_id, int branch_id, DateTime start_date, DateTime end_date)
		{
		    OperationReportCollection OperationReport_list = new OperationReportCollection();
		    OperationReportCriteria OperationReport_criteria = new OperationReportCriteria();

            OperationReport_criteria.mLmId = lmm_id;
            OperationReport_criteria.mBranchId = branch_id;
            OperationReport_criteria.mStartDate = start_date;
            OperationReport_criteria.mEndDate = end_date;
            OperationReport_list = OperationReportManager.GetList(OperationReport_criteria);
				
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  OperationReport_list.Count, operationreports = OperationReport_list }, Configuration.Formatters.JsonFormatter);
			
		}

		// POST: api/OperationReport
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(OperationReport OperationReport)
		{
			if (OperationReport.Validate())
			{
				int result;

				result = OperationReportManager.Save(OperationReport);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/OperationReport/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/OperationReport/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(OperationReport OperationReport)
		{
			try
			{
				int result;

				result = OperationReportManager.Delete(OperationReport);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}