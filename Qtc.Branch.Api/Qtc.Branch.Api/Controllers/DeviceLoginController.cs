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
	public class DeviceLoginController : ApiController
	{
        // GET: api/DeviceLogin/5
        public HttpResponseMessage Get(int id)
        {
            DeviceLogin deviceLogin;

            deviceLogin = DeviceLoginManager.GetItem(id);

            return Request.CreateResponse(HttpStatusCode.OK, new { count = 1, devicelogins = deviceLogin }, Configuration.Formatters.JsonFormatter);

        }

        // GET: api/DeviceLogin
        public HttpResponseMessage Get(int id , int employee_id, String status, DateTime start_date, DateTime end_date)
		{
			DeviceLoginCollection DeviceLogin_list = new DeviceLoginCollection();
			DeviceLoginCriteria DeviceLogin_criteria = new DeviceLoginCriteria();

            DeviceLogin_criteria.mId = id;
            DeviceLogin_criteria.mEmployeeId = employee_id;
            DeviceLogin_criteria.mStatus = status;
            DeviceLogin_criteria.mStartDate = start_date;
            DeviceLogin_criteria.mEndDate = end_date;

            DeviceLogin_list = DeviceLoginManager.GetList(DeviceLogin_criteria);
				
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  DeviceLogin_list.Count, devicelogins = DeviceLogin_list }, Configuration.Formatters.JsonFormatter);
			
		}

		// POST: api/DeviceLogin
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(DeviceLogin DeviceLogin)
		{
			if (DeviceLogin.Validate())
			{
				int result;

				result = DeviceLoginManager.Save(DeviceLogin);

                return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter);
            }
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/DeviceLogin/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/DeviceLogin/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(DeviceLogin DeviceLogin)
		{
			try
			{
				int result;

				result = DeviceLoginManager.Delete(DeviceLogin);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}