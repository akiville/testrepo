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
	public class DeviceConfigurationController : ApiController
	{
		// GET: api/DeviceConfiguration
		//public IEnumerable<string> Get()
		//{
		//	return new string[] { "value1", "value2" };
		//}

		// GET: api/DeviceConfiguration/5
		 public HttpResponseMessage Get()
		{
			//try
			//{
				DeviceConfigurationCollection DeviceConfiguration_list = new DeviceConfigurationCollection();
				DeviceConfigurationCriteria DeviceConfiguration_criteria = new DeviceConfigurationCriteria();

				DeviceConfiguration_list = DeviceConfigurationManager.GetList(DeviceConfiguration_criteria);
				//if (DeviceConfiguration_list.Count > 0 ) 
				//{
					return Request.CreateResponse(HttpStatusCode.OK, new { count =  DeviceConfiguration_list.Count, deviceconfigurations = DeviceConfiguration_list }, Configuration.Formatters.JsonFormatter);
			//	}
			//	else
			//	{
			//		return Request.CreateResponse(HttpStatusCode.BadRequest, "No record found");
			//	}
			//}
			//catch
			//{
			//	return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error" );
			//}
		}

		// POST: api/DeviceConfiguration
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(DeviceConfiguration DeviceConfiguration)
		{
			if (DeviceConfiguration.Validate())
			{
				int result;

				result = DeviceConfigurationManager.Save(DeviceConfiguration);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/DeviceConfiguration/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/DeviceConfiguration/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(DeviceConfiguration DeviceConfiguration)
		{
			try
			{
				int result;

				result = DeviceConfigurationManager.Delete(DeviceConfiguration);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}