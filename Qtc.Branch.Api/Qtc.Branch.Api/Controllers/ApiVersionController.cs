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
	public class ApiVersionController : ApiController
	{
		// GET: api/ApiVersion
		//public IEnumerable<string> Get()
		//{
		//	return new string[] { "value1", "value2" };
		//}

		// GET: api/ApiVersion/5
		 public HttpResponseMessage Get(String status)
		{
			//try
			//{
				ApiVersionCollection ApiVersion_list = new ApiVersionCollection();
				ApiVersionCriteria ApiVersion_criteria = new ApiVersionCriteria();

                ApiVersion_criteria.mStatus = status;

                ApiVersion_list = ApiVersionManager.GetList(ApiVersion_criteria);
				//if (ApiVersion_list.Count > 0 ) 
				//{
                    return Request.CreateResponse(HttpStatusCode.OK, new { count = ApiVersion_list.Count, apiversions = ApiVersion_list }, Configuration.Formatters.JsonFormatter);
                    //return Request.CreateResponse(HttpStatusCode.OK, ApiVersion_list);
            //}
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

		// POST: api/ApiVersion
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(ApiVersion ApiVersion)
		{
			if (ApiVersion.Validate())
			{
				int result;

				result = ApiVersionManager.Save(ApiVersion);

				return Request.CreateResponse(HttpStatusCode.OK, result); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/ApiVersion/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/ApiVersion/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(ApiVersion ApiVersion)
		{
			try
			{
				int result;

				result = ApiVersionManager.Delete(ApiVersion);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}