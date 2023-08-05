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
	public class BranchRequisitionController : ApiController
	{
		// GET: api/BranchRequisition
		public HttpResponseMessage Get(int id)
		{
            BranchRequisitionCollection BranchRequisition_list = new BranchRequisitionCollection();
            BranchRequisition branchRequisition = new BranchRequisition();

            branchRequisition = BranchRequisitionManager.GetItem(id);
            BranchRequisition_list.Add(branchRequisition);
            return Request.CreateResponse(HttpStatusCode.OK, new { count = BranchRequisition_list.Count, branchrequisitions = BranchRequisition_list }, Configuration.Formatters.JsonFormatter);

        }

        // GET: api/BranchRequisition/5
        public HttpResponseMessage Get(int branch_id, DateTime sales_date, String code, int employee_id, String status, int lmm_id)
		{
			
			BranchRequisitionCollection BranchRequisition_list = new BranchRequisitionCollection();
			BranchRequisitionCriteria BranchRequisition_criteria = new BranchRequisitionCriteria();

            BranchRequisition_criteria.mBranchId = branch_id;
            BranchRequisition_criteria.mSalesDate = sales_date;
            BranchRequisition_criteria.mCode = code;
            BranchRequisition_criteria.mEmployeeId = employee_id;
            BranchRequisition_criteria.mStatus = status;
            BranchRequisition_criteria.mLmmId = lmm_id;


            BranchRequisition_list = BranchRequisitionManager.GetList(BranchRequisition_criteria);
				
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  BranchRequisition_list.Count, branchrequisitions = BranchRequisition_list }, Configuration.Formatters.JsonFormatter);
			
		}

		// POST: api/BranchRequisition
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(BranchRequisition BranchRequisition)
		{
			if (BranchRequisition.Validate())
			{
				int result;

				result = BranchRequisitionManager.Save(BranchRequisition);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/BranchRequisition/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/BranchRequisition/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(BranchRequisition BranchRequisition)
		{
			try
			{
				int result;

				result = BranchRequisitionManager.Delete(BranchRequisition);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}