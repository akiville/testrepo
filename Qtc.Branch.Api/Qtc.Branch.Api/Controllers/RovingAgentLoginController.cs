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
	public class RovingAgentLoginController : ApiController
	{
		// GET: api/RovingAgentLogin
		 public HttpResponseMessage Get(int id)
		{
			RovingAgentLoginCollection RovingAgentLogin_list = new RovingAgentLoginCollection();
			RovingAgentLogin RovingAgentLogin = new RovingAgentLogin();
			RovingAgentLogin = RovingAgentLoginManager.GetItem(id);
			RovingAgentLogin_list.Add(RovingAgentLogin);
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  RovingAgentLogin_list.Count, rovingagentlogins = RovingAgentLogin_list }, Configuration.Formatters.JsonFormatter);
		}

		// GET: api/RovingAgentLogin/5
		 public HttpResponseMessage Get(int rps_id, int branch_id, int roving_agent_id)
		{
			RovingAgentLoginCollection RovingAgentLogin_list = new RovingAgentLoginCollection();
			RovingAgentLoginCriteria RovingAgentLogin_criteria = new RovingAgentLoginCriteria();

            RovingAgentLogin_criteria.mRpsId = rps_id;
            RovingAgentLogin_criteria.mBranchId = branch_id;
            RovingAgentLogin_criteria.mRovingAgentId = roving_agent_id;

            RovingAgentLogin_list = RovingAgentLoginManager.GetList(RovingAgentLogin_criteria);
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  RovingAgentLogin_list.Count, rovingagentlogins = RovingAgentLogin_list }, Configuration.Formatters.JsonFormatter);
		}

		// POST: api/RovingAgentLogin
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(RovingAgentLogin RovingAgentLogin)
		{
			if (RovingAgentLogin.Validate())
			{
				int result;

				result = RovingAgentLoginManager.Save(RovingAgentLogin);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/RovingAgentLogin/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/RovingAgentLogin/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(RovingAgentLogin RovingAgentLogin)
		{
			try
			{
				int result;

				result = RovingAgentLoginManager.Delete(RovingAgentLogin);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}