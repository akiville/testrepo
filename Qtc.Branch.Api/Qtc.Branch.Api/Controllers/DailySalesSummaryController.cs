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
	public class DailySalesSummaryController : ApiController
	{
		// GET: api/DailySalesSummary
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		// GET: api/DailySalesSummary/5
		 public HttpResponseMessage Get(int branch_id, DateTime sales_date, int id)
		{
			//try
			//{
				DailySalesSummaryCollection DailySalesSummary_list = new DailySalesSummaryCollection();
				DailySalesSummaryCriteria DailySalesSummary_criteria = new DailySalesSummaryCriteria();

                DailySalesSummary_criteria.mBranchId = branch_id;
                DailySalesSummary_criteria.mInventoryDate = sales_date;
                DailySalesSummary_criteria.mId = id;


                DailySalesSummary_list = DailySalesSummaryManager.GetList(DailySalesSummary_criteria);
				if (DailySalesSummary_list.Count > 0 ) 
				{
                    return Request.CreateResponse(HttpStatusCode.OK, new { count = DailySalesSummary_list.Count, daily_sales_summary = DailySalesSummary_list }, Configuration.Formatters.JsonFormatter);
                    //return Request.CreateResponse(HttpStatusCode.OK, AddBack_list);
                    //return Request.CreateResponse(HttpStatusCode.OK, DailySalesSummary_list);
				}
				else
				{
                    return Request.CreateResponse(HttpStatusCode.OK, new { count = 0, daily_sales_summary = DailySalesSummary_list }, Configuration.Formatters.JsonFormatter);
                }
			//}
			//catch
			//{
			//	return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error" );
			//}
		}

		// POST: api/DailySalesSummary
		//[HttpPost]
		public HttpResponseMessage Post(DailySalesSummary DailySalesSummary)
		{
            //DailySalesSummary.mSignature = null;

			if (DailySalesSummary.Validate())
			{
				int result;

				result = DailySalesSummaryManager.Save(DailySalesSummary);

                return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter);
            }
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/DailySalesSummary/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/DailySalesSummary/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(DailySalesSummary DailySalesSummary)
		{
			try
			{
				int result;

				result = DailySalesSummaryManager.Delete(DailySalesSummary);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}