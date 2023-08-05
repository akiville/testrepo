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
	public class RovingAgentDeviceProfileController : ApiController
	{
		// GET: api/RovingAgentDeviceProfile
		 public HttpResponseMessage Get(int id)
		{
			RovingAgentDeviceProfileCollection RovingAgentDeviceProfile_list = new RovingAgentDeviceProfileCollection();
			RovingAgentDeviceProfile RovingAgentDeviceProfile = new RovingAgentDeviceProfile();
			RovingAgentDeviceProfile = RovingAgentDeviceProfileManager.GetItem(id);
			RovingAgentDeviceProfile_list.Add(RovingAgentDeviceProfile);
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  RovingAgentDeviceProfile_list.Count, rovingagentdeviceprofiles = RovingAgentDeviceProfile_list }, Configuration.Formatters.JsonFormatter);
		}

		// GET: api/RovingAgentDeviceProfile/5
		 public HttpResponseMessage Get(String device_id, int user_id)
		{
			RovingAgentDeviceProfileCollection RovingAgentDeviceProfile_list = new RovingAgentDeviceProfileCollection();
			RovingAgentDeviceProfileCriteria RovingAgentDeviceProfile_criteria = new RovingAgentDeviceProfileCriteria();

            RovingAgentDeviceProfile_criteria.mDeviceId = device_id;
            RovingAgentDeviceProfile_criteria.mUserId = user_id;

            RovingAgentDeviceProfile_list = RovingAgentDeviceProfileManager.GetList(RovingAgentDeviceProfile_criteria);
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  RovingAgentDeviceProfile_list.Count, rovingagentdeviceprofiles = RovingAgentDeviceProfile_list }, Configuration.Formatters.JsonFormatter);
		}

		// POST: api/RovingAgentDeviceProfile
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(RovingAgentDeviceProfile RovingAgentDeviceProfile)
		{
			if (RovingAgentDeviceProfile.Validate())
			{
				int result;

				result = RovingAgentDeviceProfileManager.Save(RovingAgentDeviceProfile);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/RovingAgentDeviceProfile/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/RovingAgentDeviceProfile/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(RovingAgentDeviceProfile RovingAgentDeviceProfile)
		{
			try
			{
				int result;

				result = RovingAgentDeviceProfileManager.Delete(RovingAgentDeviceProfile);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}