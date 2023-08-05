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
	public class NovExplanationController : ApiController
	{
		// GET: api/NovExplanation
		public HttpResponseMessage Get(int id)
		{
            NovExplanation novExplanation = new NovExplanation();

            novExplanation = NovExplanationManager.GetItem(id);
            return Request.CreateResponse(HttpStatusCode.OK, new { count = 1, novexplanations = novExplanation }, Configuration.Formatters.JsonFormatter);

        }

        // GET: api/NovExplanation/5
        public HttpResponseMessage Get(String module_name, int record_id, int employee_id)
		{
			
			NovExplanationCollection NovExplanation_list = new NovExplanationCollection();
			NovExplanationCriteria NovExplanation_criteria = new NovExplanationCriteria();

            NovExplanation_criteria.mModuleName = module_name;
            NovExplanation_criteria.mRecordId = record_id;
            NovExplanation_criteria.mEmployeeId = employee_id;

            NovExplanation_list = NovExplanationManager.GetList(NovExplanation_criteria);
				
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  NovExplanation_list.Count, novexplanations = NovExplanation_list }, Configuration.Formatters.JsonFormatter);
			
			
		}

		// POST: api/NovExplanation
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(NovExplanation NovExplanation)
		{
			if (NovExplanation.Validate())
			{
				int result;

				result = NovExplanationManager.Save(NovExplanation);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/NovExplanation/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/NovExplanation/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(NovExplanation NovExplanation)
		{
			try
			{
				int result;

				result = NovExplanationManager.Delete(NovExplanation);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}