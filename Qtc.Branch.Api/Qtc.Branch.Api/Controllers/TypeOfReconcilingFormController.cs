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
	public class TypeOfReconcilingFormController : ApiController
	{
		// GET: api/TypeOfReconcilingForm
		 public HttpResponseMessage Get(int id)
		{
			TypeOfReconcilingFormCollection TypeOfReconcilingForm_list = new TypeOfReconcilingFormCollection();
			TypeOfReconcilingForm TypeOfReconcilingForm = new TypeOfReconcilingForm();
			TypeOfReconcilingForm = TypeOfReconcilingFormManager.GetItem(id);
			TypeOfReconcilingForm_list.Add(TypeOfReconcilingForm);
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  TypeOfReconcilingForm_list.Count, typeofreconcilingforms = TypeOfReconcilingForm_list }, Configuration.Formatters.JsonFormatter);
		}

		// GET: api/TypeOfReconcilingForm/5
		 public HttpResponseMessage Get()
		{
			TypeOfReconcilingFormCollection TypeOfReconcilingForm_list = new TypeOfReconcilingFormCollection();
			TypeOfReconcilingFormCriteria TypeOfReconcilingForm_criteria = new TypeOfReconcilingFormCriteria();

			TypeOfReconcilingForm_list = TypeOfReconcilingFormManager.GetList(TypeOfReconcilingForm_criteria);
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  TypeOfReconcilingForm_list.Count, typeofreconcilingforms = TypeOfReconcilingForm_list }, Configuration.Formatters.JsonFormatter);
		}

		// POST: api/TypeOfReconcilingForm
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(TypeOfReconcilingForm TypeOfReconcilingForm)
		{
			if (TypeOfReconcilingForm.Validate())
			{
				int result;

				result = TypeOfReconcilingFormManager.Save(TypeOfReconcilingForm);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/TypeOfReconcilingForm/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/TypeOfReconcilingForm/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(TypeOfReconcilingForm TypeOfReconcilingForm)
		{
			try
			{
				int result;

				result = TypeOfReconcilingFormManager.Delete(TypeOfReconcilingForm);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}