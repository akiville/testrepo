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
	public class LmmCashCountController : ApiController
	{
        //GET: api/LmmCashCount
        public HttpResponseMessage Get(int id)
        {
            LmmCashCountCollection LmmCashCount_list = new LmmCashCountCollection();
            LmmCashCount lmmCashCount = new LmmCashCount();
            lmmCashCount = LmmCashCountManager.GetItem(id);
            LmmCashCount_list.Add(lmmCashCount);
            return Request.CreateResponse(HttpStatusCode.OK, new { count = LmmCashCount_list.Count, lmmcashcounts = LmmCashCount_list }, Configuration.Formatters.JsonFormatter);
        }

        // GET: api/LmmCashCount/5
        public HttpResponseMessage Get(int branch_id , DateTime start_date, DateTime end_date, int lmm_id)
		{
			
			LmmCashCountCollection LmmCashCount_list = new LmmCashCountCollection();
			LmmCashCountCriteria LmmCashCount_criteria = new LmmCashCountCriteria();

            LmmCashCount_criteria.mBranchId = branch_id;
            LmmCashCount_criteria.mStartDate = start_date;
            LmmCashCount_criteria.mEndDate = end_date;
            LmmCashCount_criteria.mLmmId = lmm_id;
            
            LmmCashCount_list = LmmCashCountManager.GetList(LmmCashCount_criteria);
			
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  LmmCashCount_list.Count, lmmcashcounts = LmmCashCount_list }, Configuration.Formatters.JsonFormatter);
			
		}

        public HttpResponseMessage Get( DateTime start_date, DateTime end_date, int lmm_id)
        {
            LmmCashCountCollection LmmCashCount_list = new LmmCashCountCollection();
            LmmCashCountCriteria LmmCashCount_criteria = new LmmCashCountCriteria();
            
            LmmCashCount_criteria.mStartDate = start_date;
            LmmCashCount_criteria.mEndDate = end_date;
            LmmCashCount_criteria.mLmmId = lmm_id;

            LmmCashCount_list = LmmCashCountManager.GetListCount(LmmCashCount_criteria);

            return Request.CreateResponse(HttpStatusCode.OK, new { count = LmmCashCount_list.Count, lmmcashcounts = LmmCashCount_list }, Configuration.Formatters.JsonFormatter);
        }

        // POST: api/LmmCashCount
        //[HttpPost]
        //[CustomAuthorizationFilter]
        public HttpResponseMessage Post(LmmCashCount LmmCashCount)
		{
			if (LmmCashCount.Validate())
			{
				int result;

				result = LmmCashCountManager.Save(LmmCashCount);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/LmmCashCount/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/LmmCashCount/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(LmmCashCount LmmCashCount)
		{
			try
			{
				int result;

				result = LmmCashCountManager.Delete(LmmCashCount);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}