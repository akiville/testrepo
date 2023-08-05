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
	public class ApiVersionChangelogController : ApiController
	{
		// GET: api/ApiVersionChangelog
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		// GET: api/ApiVersionChangelog/5
		 public HttpResponseMessage Get(int version_id)
		{
			try
			{
				ApiVersionChangelogCollection ApiVersionChangelog_list = new ApiVersionChangelogCollection();
				ApiVersionChangelogCriteria ApiVersionChangelog_criteria = new ApiVersionChangelogCriteria();

                ApiVersionChangelog_criteria.mVersionId = version_id;

                ApiVersionChangelog_list = ApiVersionChangelogManager.GetList(ApiVersionChangelog_criteria);
				if (ApiVersionChangelog_list.Count > 0 ) 
				{
                    return Request.CreateResponse(HttpStatusCode.OK, new { count = ApiVersionChangelog_list.Count, apiversionchangelogs = ApiVersionChangelog_list }, Configuration.Formatters.JsonFormatter);
                    //return Request.CreateResponse(HttpStatusCode.OK, ApiVersionChangelog_list);
                }
				else
				{
					return Request.CreateResponse(HttpStatusCode.BadRequest, "No record found");
				}
			}
			catch
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error" );
			}
		}

		// POST: api/ApiVersionChangelog
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(ApiVersionChangelog ApiVersionChangelog)
		{
			if (ApiVersionChangelog.Validate())
			{
				int result;

				result = ApiVersionChangelogManager.Save(ApiVersionChangelog);

				return Request.CreateResponse(HttpStatusCode.OK, result); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/ApiVersionChangelog/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/ApiVersionChangelog/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(ApiVersionChangelog ApiVersionChangelog)
		{
			try
			{
				int result;

				result = ApiVersionChangelogManager.Delete(ApiVersionChangelog);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}