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
	public class BranchCashDetailsController : ApiController
	{
		// GET: api/BranchCashDetails
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		// GET: api/BranchCashDetails/5
		 public HttpResponseMessage Get(int branch_cash_id, int branch_id, DateTime sales_date)
		{
			//try
			//{
				BranchCashDetailsCollection BranchCashDetails_list = new BranchCashDetailsCollection();
				BranchCashDetailsCriteria BranchCashDetails_criteria = new BranchCashDetailsCriteria();

                BranchCashDetails_criteria.mBranchCashId = branch_cash_id;
                BranchCashDetails_criteria.mBranchId = branch_id;
                BranchCashDetails_criteria.mSalesDate = sales_date;

                BranchCashDetails_list = BranchCashDetailsManager.GetList(BranchCashDetails_criteria);
				if (BranchCashDetails_list.Count > 0 ) 
				{
                    return Request.CreateResponse(HttpStatusCode.OK, new { count = BranchCashDetails_list.Count, branchcashdetails = BranchCashDetails_list }, Configuration.Formatters.JsonFormatter);
                }
				else
				{
                return Request.CreateResponse(HttpStatusCode.OK, new { count = 0, branchcashdetails = BranchCashDetails_list }, Configuration.Formatters.JsonFormatter);
            }
			//}
			//catch
			//{
			//	return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error" );
			//}
		}

		// POST: api/BranchCashDetails
		//[HttpPost]
		public HttpResponseMessage Post(BranchCashDetails BranchCashDetails)
		{
			if (BranchCashDetails.Validate())
			{
				int result;

				result = BranchCashDetailsManager.Save(BranchCashDetails);

                return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter);
            }
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/BranchCashDetails/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/BranchCashDetails/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(BranchCashDetails BranchCashDetails)
		{
			try
			{
				int result;

				result = BranchCashDetailsManager.Delete(BranchCashDetails);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}