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
	public class LoeDetailController : ApiController
	{
		// GET: api/LoeDetail
		//public IEnumerable<string> Get()
		//{
		//	return new string[] { "value1", "value2" };
		//}

		// GET: api/LoeDetail/5
		 public HttpResponseMessage Get(int loe_id)
		{
			try
			{
				LoeDetailCollection LoeDetail_list = new LoeDetailCollection();
				LoeDetailCriteria LoeDetail_criteria = new LoeDetailCriteria();

                LoeDetail_criteria.mLoeId = loe_id;

                LoeDetail_list = LoeDetailManager.GetList(LoeDetail_criteria);
				if (LoeDetail_list.Count > 0 ) 
				{
					return Request.CreateResponse(HttpStatusCode.OK, new { count =  LoeDetail_list.Count, loedetails = LoeDetail_list }, Configuration.Formatters.JsonFormatter);
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

		// POST: api/LoeDetail
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(LoeDetail LoeDetail)
		{
			if (LoeDetail.Validate())
			{
				int result;

				result = LoeDetailManager.Save(LoeDetail);

                return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter);
            }
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/LoeDetail/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/LoeDetail/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(LoeDetail LoeDetail)
		{
			try
			{
				int result;

				result = LoeDetailManager.Delete(LoeDetail);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}