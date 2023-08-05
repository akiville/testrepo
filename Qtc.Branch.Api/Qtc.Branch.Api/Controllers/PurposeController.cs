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
	public class PurposeController : ApiController
	{
		// GET: api/Purpose
		//public IEnumerable<string> Get()
		//{
		//	return new string[] { "value1", "value2" };
		//}

		// GET: api/Purpose/5
		 public HttpResponseMessage Get()
		{
			try
			{
				PurposeCollection Purpose_list = new PurposeCollection();
				PurposeCriteria Purpose_criteria = new PurposeCriteria();

				Purpose_list = PurposeManager.GetList(Purpose_criteria);
				//if (Purpose_list.Count > 0 ) 
				//{
					return Request.CreateResponse(HttpStatusCode.OK, new { count =  Purpose_list.Count, purposes = Purpose_list }, Configuration.Formatters.JsonFormatter);
				//}
				//else
				//{
				//	return Request.CreateResponse(HttpStatusCode.BadRequest, "No record found");
				//}
			}
			catch
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error" );
			}
		}

		// POST: api/Purpose
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(Purpose Purpose)
		{
			if (Purpose.Validate())
			{
				int result;

				result = PurposeManager.Save(Purpose);

				return Request.CreateResponse(HttpStatusCode.OK, result); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/Purpose/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/Purpose/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(Purpose Purpose)
		{
			try
			{
				int result;

				result = PurposeManager.Delete(Purpose);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}