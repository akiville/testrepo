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
	public class TypeOfLetterController : ApiController
	{
		// GET: api/TypeOfLetter
		 public HttpResponseMessage Get(int id)
		{
			TypeOfLetterCollection TypeOfLetter_list = new TypeOfLetterCollection();
			TypeOfLetter TypeOfLetter = new TypeOfLetter();
			TypeOfLetter = TypeOfLetterManager.GetItem(id);
			TypeOfLetter_list.Add(TypeOfLetter);
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  TypeOfLetter_list.Count, typeofletters = TypeOfLetter_list }, Configuration.Formatters.JsonFormatter);
		}

		// GET: api/TypeOfLetter/5
		 public HttpResponseMessage Get()
		{
			TypeOfLetterCollection TypeOfLetter_list = new TypeOfLetterCollection();
			TypeOfLetterCriteria TypeOfLetter_criteria = new TypeOfLetterCriteria();

			TypeOfLetter_list = TypeOfLetterManager.GetList(TypeOfLetter_criteria);
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  TypeOfLetter_list.Count, typeofletters = TypeOfLetter_list }, Configuration.Formatters.JsonFormatter);
		}

		// POST: api/TypeOfLetter
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(TypeOfLetter TypeOfLetter)
		{
			if (TypeOfLetter.Validate())
			{
				int result;

				result = TypeOfLetterManager.Save(TypeOfLetter);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/TypeOfLetter/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/TypeOfLetter/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(TypeOfLetter TypeOfLetter)
		{
			try
			{
				int result;

				result = TypeOfLetterManager.Delete(TypeOfLetter);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}