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
	public class BranchsController : ApiController
	{
		// GET: api/Branchs
		public HttpResponseMessage Get(String name, int lmm_id)
		{
            try
            {
                BranchsCollection Branchs_list = new BranchsCollection();
                BranchsCriteria Branchs_criteria = new BranchsCriteria();

                Branchs_criteria.mName = name;
                Branchs_criteria.mLmmId = lmm_id;

                Branchs_list = BranchsManager.GetList(Branchs_criteria);

                if (Branchs_list.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { count = Branchs_list.Count, branches = Branchs_list }, Configuration.Formatters.JsonFormatter);
                   
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "No record found");
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error: " + e.Message.ToString());
            }
        }

        // GET: api/Branchs
        public HttpResponseMessage Get()
        {
            try
            {
                BranchsCollection Branchs_list = new BranchsCollection();
                BranchsCriteria Branchs_criteria = new BranchsCriteria();

                Branchs_list = BranchsManager.GetList(Branchs_criteria);
                if (Branchs_list.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, Branchs_list);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "No record found");
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "System Error");
            }
        }

        // POST: api/Branchs
        //[HttpPost]
        //[CustomAuthorizationFilter]
        public HttpResponseMessage Post(Branchs Branchs)
		{
			if (Branchs.Validate())
			{
				int result;

				result = BranchsManager.Save(Branchs);

				return Request.CreateResponse(HttpStatusCode.OK, result); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/Branchs/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/Branchs/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(Branchs Branchs)
		{
			try
			{
				int result;

				result = BranchsManager.Delete(Branchs);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}