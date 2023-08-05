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
	public class NonComplianceTopicController : ApiController
	{
		// GET: api/NonComplianceTopic
		 public HttpResponseMessage Get(int id)
		{
			NonComplianceTopicCollection NonComplianceTopic_list = new NonComplianceTopicCollection();
			NonComplianceTopic NonComplianceTopic = new NonComplianceTopic();
			NonComplianceTopic = NonComplianceTopicManager.GetItem(id);
			NonComplianceTopic_list.Add(NonComplianceTopic);
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  NonComplianceTopic_list.Count, noncompliancetopics = NonComplianceTopic_list }, Configuration.Formatters.JsonFormatter);
		}

		// GET: api/NonComplianceTopic/5
		 public HttpResponseMessage Get()
		{
			NonComplianceTopicCollection NonComplianceTopic_list = new NonComplianceTopicCollection();
			NonComplianceTopicCriteria NonComplianceTopic_criteria = new NonComplianceTopicCriteria();

			NonComplianceTopic_list = NonComplianceTopicManager.GetList(NonComplianceTopic_criteria);
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  NonComplianceTopic_list.Count, noncompliancetopics = NonComplianceTopic_list }, Configuration.Formatters.JsonFormatter);
		}

		// POST: api/NonComplianceTopic
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(NonComplianceTopic NonComplianceTopic)
		{
			if (NonComplianceTopic.Validate())
			{
				int result;

				result = NonComplianceTopicManager.Save(NonComplianceTopic);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/NonComplianceTopic/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/NonComplianceTopic/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(NonComplianceTopic NonComplianceTopic)
		{
			try
			{
				int result;

				result = NonComplianceTopicManager.Delete(NonComplianceTopic);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}