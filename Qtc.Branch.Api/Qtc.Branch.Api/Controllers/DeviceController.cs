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
	public class DeviceController : ApiController
	{
		//// GET: api/Device
		//public IEnumerable<string> Get()
		//{
		//	return new string[] { "value1", "value2" };
		//}

		// GET: api/Device/5
		 public HttpResponseMessage Get(int employee_id, String status, String device_serial_no)
		{
			//try
			//{
				DeviceCollection Device_list = new DeviceCollection();
				DeviceCriteria Device_criteria = new DeviceCriteria();

                Device_criteria.mEmployeeId = employee_id;
                Device_criteria.mStatus = status;
                Device_criteria.mDeviceSerialNo = device_serial_no;
                Device_list = DeviceManager.GetList(Device_criteria);
				
                return Request.CreateResponse(HttpStatusCode.OK, new { count = Device_list.Count, devices = Device_list }, Configuration.Formatters.JsonFormatter);
                
			//}
			//catch
			//{
			//	return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error" );
			//}
		}

		// POST: api/Device
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(Device Device)
		{
			if (Device.Validate())
			{
				int result;

				result = DeviceManager.Save(Device);

                return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter);
            }
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/Device/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/Device/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(Device Device)
		{
			try
			{
				int result;

				result = DeviceManager.Delete(Device);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}