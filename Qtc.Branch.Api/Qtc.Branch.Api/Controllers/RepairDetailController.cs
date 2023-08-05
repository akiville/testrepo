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
	public class RepairDetailController : ApiController
	{
		// GET: api/RepairDetail
		 public HttpResponseMessage Get(int id)
		{
			RepairDetailCollection RepairDetail_list = new RepairDetailCollection();
			RepairDetail RepairDetail = new RepairDetail();
			RepairDetail = RepairDetailManager.GetItem(id);
			RepairDetail_list.Add(RepairDetail);
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  RepairDetail_list.Count, repairdetails = RepairDetail_list }, Configuration.Formatters.JsonFormatter);
		}

		// GET: api/RepairDetail/5
		 public HttpResponseMessage Get()
		{
			RepairDetailCollection RepairDetail_list = new RepairDetailCollection();
			RepairDetailCriteria RepairDetail_criteria = new RepairDetailCriteria();

			RepairDetail_list = RepairDetailManager.GetList(RepairDetail_criteria);
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  RepairDetail_list.Count, repairdetails = RepairDetail_list }, Configuration.Formatters.JsonFormatter);
		}

		// POST: api/RepairDetail
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(RepairDetail RepairDetail)
		{
			if (RepairDetail.Validate())
			{
				int result;

				result = RepairDetailManager.Save(RepairDetail);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/RepairDetail/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/RepairDetail/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(RepairDetail RepairDetail)
		{
			try
			{
				int result;

				result = RepairDetailManager.Delete(RepairDetail);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}