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
	public class RequestTopicController : ApiController
	{
		// GET: api/RequestTopic
		//public IEnumerable<string> Get()
		//{
		//	return new string[] { "value1", "value2" };
		//}

		// GET: api/RequestTopic/5
		 public HttpResponseMessage Get()
		{
			try
			{
				RequestTopicCollection RequestTopic_list = new RequestTopicCollection();
				RequestTopicCriteria RequestTopic_criteria = new RequestTopicCriteria();


				RequestTopic_list = RequestTopicManager.GetList(RequestTopic_criteria);
				if (RequestTopic_list.Count > 0 ) 
				{
					return Request.CreateResponse(HttpStatusCode.OK, new { count =  RequestTopic_list.Count, requesttopics = RequestTopic_list }, Configuration.Formatters.JsonFormatter);
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

		// POST: api/RequestTopic
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(RequestTopic RequestTopic)
		{
			if (RequestTopic.Validate())
			{
				int result;

				result = RequestTopicManager.Save(RequestTopic);

				return Request.CreateResponse(HttpStatusCode.OK, result); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/RequestTopic/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/RequestTopic/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(RequestTopic RequestTopic)
		{
			try
			{
				int result;

				result = RequestTopicManager.Delete(RequestTopic);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}