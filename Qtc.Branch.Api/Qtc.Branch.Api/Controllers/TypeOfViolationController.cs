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
	public class TypeOfViolationController : ApiController
	{
		// GET: api/TypeOfViolation
		 public HttpResponseMessage Get(int id)
		{
			TypeOfViolationCollection TypeOfViolation_list = new TypeOfViolationCollection();
			TypeOfViolation TypeOfViolation = new TypeOfViolation();
			TypeOfViolation = TypeOfViolationManager.GetItem(id);
			TypeOfViolation_list.Add(TypeOfViolation);
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  TypeOfViolation_list.Count, typeofviolations = TypeOfViolation_list }, Configuration.Formatters.JsonFormatter);
		}

		// GET: api/TypeOfViolation/5
		 public HttpResponseMessage Get()
		{
			TypeOfViolationCollection TypeOfViolation_list = new TypeOfViolationCollection();
			TypeOfViolationCriteria TypeOfViolation_criteria = new TypeOfViolationCriteria();

			TypeOfViolation_list = TypeOfViolationManager.GetList(TypeOfViolation_criteria);
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  TypeOfViolation_list.Count, typeofviolations = TypeOfViolation_list }, Configuration.Formatters.JsonFormatter);
		}

		// POST: api/TypeOfViolation
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(TypeOfViolation TypeOfViolation)
		{
			if (TypeOfViolation.Validate())
			{
				int result;

				result = TypeOfViolationManager.Save(TypeOfViolation);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/TypeOfViolation/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/TypeOfViolation/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(TypeOfViolation TypeOfViolation)
		{
			try
			{
				int result;

				result = TypeOfViolationManager.Delete(TypeOfViolation);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}