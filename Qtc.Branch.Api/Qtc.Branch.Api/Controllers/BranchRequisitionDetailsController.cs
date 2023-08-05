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
	public class BranchRequisitionDetailsController : ApiController
	{
		// GET: api/BranchRequisitionDetails
		public HttpResponseMessage Get(int id)
		{
            BranchRequisitionDetailsCollection BranchRequisitionDetails_list = new BranchRequisitionDetailsCollection();
            BranchRequisitionDetails branchRequisitionDetails = new BranchRequisitionDetails();
            branchRequisitionDetails = BranchRequisitionDetailsManager.GetItem(id);
            BranchRequisitionDetails_list.Add(branchRequisitionDetails);
            return Request.CreateResponse(HttpStatusCode.OK, new { count = BranchRequisitionDetails_list.Count, branchrequisitiondetailss = BranchRequisitionDetails_list }, Configuration.Formatters.JsonFormatter);
            
        }

        // GET: api/BranchRequisitionDetails/5
        public HttpResponseMessage Get(int branch_id , DateTime sales_date, int branch_requisition_id)
		{
			BranchRequisitionDetailsCollection BranchRequisitionDetails_list = new BranchRequisitionDetailsCollection();
			BranchRequisitionDetailsCriteria BranchRequisitionDetails_criteria = new BranchRequisitionDetailsCriteria();

            BranchRequisitionDetails_criteria.mBranchId = branch_id;
            BranchRequisitionDetails_criteria.mSalesDate = sales_date;
            BranchRequisitionDetails_criteria.mBranchRequisitionId = branch_requisition_id;

            BranchRequisitionDetails_list = BranchRequisitionDetailsManager.GetList(BranchRequisitionDetails_criteria);
				
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  BranchRequisitionDetails_list.Count, branchrequisitiondetailss = BranchRequisitionDetails_list }, Configuration.Formatters.JsonFormatter);
			
		}

		// POST: api/BranchRequisitionDetails
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(BranchRequisitionDetails BranchRequisitionDetails)
		{
			if (BranchRequisitionDetails.Validate())
			{
				int result;

				result = BranchRequisitionDetailsManager.Save(BranchRequisitionDetails);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/BranchRequisitionDetails/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/BranchRequisitionDetails/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(BranchRequisitionDetails BranchRequisitionDetails)
		{
			try
			{
				int result;

				result = BranchRequisitionDetailsManager.Delete(BranchRequisitionDetails);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}