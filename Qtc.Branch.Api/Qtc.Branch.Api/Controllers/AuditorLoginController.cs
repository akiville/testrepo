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
	public class AuditorLoginController : ApiController
	{
        // GET: api/AuditorLogin
        public HttpResponseMessage Get(int id)
        {
            AuditorLogin AuditorLogin = new AuditorLogin();

            AuditorLoginCollection AuditorLogin_list = new AuditorLoginCollection();

            AuditorLogin = AuditorLoginManager.GetItem(id);
            AuditorLogin_list.Add(AuditorLogin);
            return Request.CreateResponse(HttpStatusCode.OK, new { count = AuditorLogin_list.Count, auditorlogins = AuditorLogin_list }, Configuration.Formatters.JsonFormatter);

        }

        // GET: api/AuditorLogin/5
        public HttpResponseMessage Get(String username, String password)
		{
			AuditorLoginCollection AuditorLogin_list = new AuditorLoginCollection();
			AuditorLoginCriteria AuditorLogin_criteria = new AuditorLoginCriteria();

            AuditorLogin_criteria.mUsername = username;
            AuditorLogin_criteria.mPassword = password;

            AuditorLogin_list = AuditorLoginManager.GetList(AuditorLogin_criteria);
				
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  AuditorLogin_list.Count, auditorlogins = AuditorLogin_list }, Configuration.Formatters.JsonFormatter);
				
		}

		// POST: api/AuditorLogin
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(AuditorLogin AuditorLogin)
		{
			if (AuditorLogin.Validate())
			{
				int result;

				result = AuditorLoginManager.Save(AuditorLogin);

				return Request.CreateResponse(HttpStatusCode.OK, result); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/AuditorLogin/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/AuditorLogin/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(AuditorLogin AuditorLogin)
		{
			try
			{
				int result;

				result = AuditorLoginManager.Delete(AuditorLogin);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}