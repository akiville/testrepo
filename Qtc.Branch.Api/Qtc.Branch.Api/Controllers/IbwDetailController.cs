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
	public class IbwDetailController : ApiController
	{
		// GET: api/IbwDetail
		//public IEnumerable<string> Get()
		//{
		//	return new string[] { "value1", "value2" };
		//}

		// GET: api/IbwDetail/5
		 public HttpResponseMessage Get(int ibw_id)
		{
			try
			{
				IbwDetailCollection IbwDetail_list = new IbwDetailCollection();
				IbwDetailCriteria IbwDetail_criteria = new IbwDetailCriteria();

                IbwDetail_criteria.mIbwId = ibw_id;

                IbwDetail_list = IbwDetailManager.GetList(IbwDetail_criteria);
				if (IbwDetail_list.Count > 0 ) 
				{
					return Request.CreateResponse(HttpStatusCode.OK, new { count =  IbwDetail_list.Count, ibwdetails = IbwDetail_list }, Configuration.Formatters.JsonFormatter);
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

		// POST: api/IbwDetail
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(IbwDetail IbwDetail)
		{
			if (IbwDetail.Validate())
			{
				int result;

				result = IbwDetailManager.Save(IbwDetail);

                return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter);
            }
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/IbwDetail/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/IbwDetail/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(IbwDetail IbwDetail)
		{
			try
			{
				int result;

				result = IbwDetailManager.Delete(IbwDetail);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}