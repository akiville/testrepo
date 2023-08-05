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
	public class GreenSlipDetailController : ApiController
	{
		//// GET: api/GreenSlipDetail
		//public IEnumerable<string> Get()
		//{
		//	return new string[] { "value1", "value2" };
		//}

		// GET: api/GreenSlipDetail
		 public HttpResponseMessage Get(int greenslip_id)
		{
			try
			{
				GreenSlipDetailCollection GreenSlipDetail_list = new GreenSlipDetailCollection();
				GreenSlipDetailCriteria GreenSlipDetail_criteria = new GreenSlipDetailCriteria();

                GreenSlipDetail_criteria.mGreenSlipId = greenslip_id;
                
                GreenSlipDetail_list = GreenSlipDetailManager.GetList(GreenSlipDetail_criteria);
				if (GreenSlipDetail_list.Count > 0 ) 
				{
                    return Request.CreateResponse(HttpStatusCode.OK, new { count = GreenSlipDetail_list.Count, greenslip_details = GreenSlipDetail_list }, Configuration.Formatters.JsonFormatter);
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

		// POST: api/GreenSlipDetail
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(GreenSlipDetail GreenSlipDetail)
		{
			if (GreenSlipDetail.Validate())
			{
				int result;

				result = GreenSlipDetailManager.Save(GreenSlipDetail);

                return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter);
            }
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/GreenSlipDetail/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/GreenSlipDetail/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(GreenSlipDetail GreenSlipDetail)
		{
			try
			{
				int result;

				result = GreenSlipDetailManager.Delete(GreenSlipDetail);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}