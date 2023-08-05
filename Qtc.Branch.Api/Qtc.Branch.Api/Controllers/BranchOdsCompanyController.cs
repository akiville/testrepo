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
	public class BranchOdsCompanyController : ApiController
	{
		// GET: api/BranchOdsCompany
		public HttpResponseMessage Get(int id)
		{
            BranchOdsCompanyCollection BranchOdsCompany_list = new BranchOdsCompanyCollection();
            BranchOdsCompany branchOdsCompany = new BranchOdsCompany();

            branchOdsCompany = BranchOdsCompanyManager.GetItem(id);

            BranchOdsCompany_list.Add(branchOdsCompany);

            return Request.CreateResponse(HttpStatusCode.OK, new { count = BranchOdsCompany_list.Count, branchodscompanys = BranchOdsCompany_list }, Configuration.Formatters.JsonFormatter);

        }

        // GET: api/BranchOdsCompany/5
        public HttpResponseMessage Get(int branch_id, int odc_company_id)
		{
			BranchOdsCompanyCollection BranchOdsCompany_list = new BranchOdsCompanyCollection();
			BranchOdsCompanyCriteria BranchOdsCompany_criteria = new BranchOdsCompanyCriteria();
            BranchOdsCompany_criteria.mBranchId = branch_id;
            BranchOdsCompany_criteria.mOdcCompanyId = odc_company_id;

            BranchOdsCompany_list = BranchOdsCompanyManager.GetList(BranchOdsCompany_criteria);
				
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  BranchOdsCompany_list.Count, branchodscompanys = BranchOdsCompany_list }, Configuration.Formatters.JsonFormatter);
				
		
		}

		// POST: api/BranchOdsCompany
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(BranchOdsCompany BranchOdsCompany)
		{
			if (BranchOdsCompany.Validate())
			{
				int result;

				result = BranchOdsCompanyManager.Save(BranchOdsCompany);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/BranchOdsCompany/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/BranchOdsCompany/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(BranchOdsCompany BranchOdsCompany)
		{
			try
			{
				int result;

				result = BranchOdsCompanyManager.Delete(BranchOdsCompany);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}