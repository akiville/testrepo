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
	public class LmmController : ApiController
	{
		// GET: api/Lmm
		//public IEnumerable<string> Get()
		//{
		//	return new string[] { "value1", "value2" };
		//}

		// GET: api/Lmm/5
		 public HttpResponseMessage Get()
		{
			try
			{
				LmmCollection Lmm_list = new LmmCollection();
				LmmCriteria Lmm_criteria = new LmmCriteria();

				Lmm_list = LmmManager.GetList(Lmm_criteria);
				return Request.CreateResponse(HttpStatusCode.OK, new { count =  Lmm_list.Count, lmms = Lmm_list }, Configuration.Formatters.JsonFormatter);
				
			}
			catch
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error" );
			}
		}

		// POST: api/Lmm
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(Lmm Lmm)
		{
			if (Lmm.Validate())
			{
				int result;

				result = LmmManager.Save(Lmm);

                //return Request.CreateResponse(HttpStatusCode.OK, result); 
                return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter);
            }
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/Lmm/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/Lmm/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(Lmm Lmm)
		{
			try
			{
				int result;

				result = LmmManager.Delete(Lmm);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}