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
	public class UserController : ApiController
	{
		// GET: api/User
		//public IEnumerable<string> Get()
		//{
		//	return new string[] { "value1", "value2" };
		//}

		// GET: api/User/5
		 public HttpResponseMessage Get(String user_name, String password)
		{
			try
			{
				UserCollection User_list = new UserCollection();
				UserCriteria User_criteria = new UserCriteria();

                User_criteria.mUserName = user_name;
                User_criteria.mPassword = password;

                User_list = UserManager.GetList(User_criteria);
				
				return Request.CreateResponse(HttpStatusCode.OK, new { count =  User_list.Count, users = User_list }, Configuration.Formatters.JsonFormatter);
				
			}
			catch
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error" );
			}
		}

		// POST: api/User
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(User User)
		{
			if (User.Validate())
			{
				int result;

				result = UserManager.Save(User);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/User/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/User/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(User User)
		{
			try
			{
				int result;

				result = UserManager.Delete(User);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}