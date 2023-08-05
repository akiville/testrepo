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
	public class LoeController : ApiController
	{
		// GET: api/Loe
		//public IEnumerable<string> Get()
		//{
		//	return new string[] { "value1", "value2" };
		//}

		// GET: api/Loe/5
		 public HttpResponseMessage Get(int branch_id)
		{
			try
			{
				LoeCollection Loe_list = new LoeCollection();
				LoeCriteria Loe_criteria = new LoeCriteria();

                Loe_criteria.mBranchId = branch_id;

				Loe_list = LoeManager.GetList(Loe_criteria);
				if (Loe_list.Count > 0 ) 
				{
					return Request.CreateResponse(HttpStatusCode.OK, new { count =  Loe_list.Count, loes = Loe_list }, Configuration.Formatters.JsonFormatter);
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

		// POST: api/Loe
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(Loe Loe)
		{
			if (Loe.Validate())
			{
				int result;

				result = LoeManager.Save(Loe);

                return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter);
            }
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/Loe/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/Loe/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(Loe Loe)
		{
			try
			{
				int result;

				result = LoeManager.Delete(Loe);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}