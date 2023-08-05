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
	public class NonComplianceController : ApiController
	{
		// GET: api/NonCompliance
		 public HttpResponseMessage Get(int id)
		{
			NonComplianceCollection NonCompliance_list = new NonComplianceCollection();
			NonCompliance NonCompliance = new NonCompliance();
			NonCompliance = NonComplianceManager.GetItem(id);
			NonCompliance_list.Add(NonCompliance);
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  NonCompliance_list.Count, noncompliances = NonCompliance_list }, Configuration.Formatters.JsonFormatter);
		}

		// GET: api/NonCompliance/5
		 public HttpResponseMessage Get()
		{
			NonComplianceCollection NonCompliance_list = new NonComplianceCollection();
			NonComplianceCriteria NonCompliance_criteria = new NonComplianceCriteria();

			NonCompliance_list = NonComplianceManager.GetList(NonCompliance_criteria);
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  NonCompliance_list.Count, noncompliances = NonCompliance_list }, Configuration.Formatters.JsonFormatter);
		}

		// POST: api/NonCompliance
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(NonCompliance NonCompliance)
		{
			if (NonCompliance.Validate())
			{
				int result;

				result = NonComplianceManager.Save(NonCompliance);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/NonCompliance/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/NonCompliance/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(NonCompliance NonCompliance)
		{
			try
			{
				int result;

				result = NonComplianceManager.Delete(NonCompliance);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}