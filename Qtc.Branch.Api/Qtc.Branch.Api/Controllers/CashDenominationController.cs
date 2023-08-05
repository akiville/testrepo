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
	public class CashDenominationController : ApiController
	{
		//// GET: api/CashDenomination
		//public IEnumerable<string> Get()
		//{
		//	return new string[] { "value1", "value2" };
		//}

		// GET: api/CashDenomination/5
		 public HttpResponseMessage Get()
		{
			try
			{
				CashDenominationCollection CashDenomination_list = new CashDenominationCollection();
				CashDenominationCriteria CashDenomination_criteria = new CashDenominationCriteria();

				CashDenomination_list = CashDenominationManager.GetList(CashDenomination_criteria);
				if (CashDenomination_list.Count > 0 ) 
				{
                    return Request.CreateResponse(HttpStatusCode.OK, new { count = CashDenomination_list.Count, cash_denomination = CashDenomination_list }, Configuration.Formatters.JsonFormatter);
                    //return Request.CreateResponse(HttpStatusCode.OK, CashDenomination_list);
				}
				else
				{
                    return Request.CreateResponse(HttpStatusCode.OK, new { count = CashDenomination_list.Count, cash_denomination = CashDenomination_list }, Configuration.Formatters.JsonFormatter);
                    //return Request.CreateResponse(HttpStatusCode.BadRequest, "No record found");
                }
			}
			catch
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error" );
			}
		}

		// POST: api/CashDenomination
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(CashDenomination CashDenomination)
		{
			if (CashDenomination.Validate())
			{
				int result;

				result = CashDenominationManager.Save(CashDenomination);

				return Request.CreateResponse(HttpStatusCode.OK, result); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/CashDenomination/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/CashDenomination/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(CashDenomination CashDenomination)
		{
			try
			{
				int result;

				result = CashDenominationManager.Delete(CashDenomination);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}