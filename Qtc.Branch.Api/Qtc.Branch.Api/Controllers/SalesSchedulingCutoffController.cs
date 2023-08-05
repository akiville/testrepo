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
	public class SalesSchedulingCutoffController : ApiController
	{
        // GET: api/SalesSchedulingCutoff
        public HttpResponseMessage Get(int id)
        {
            SalesSchedulingCutoffCollection SalesSchedulingCutoff_list = new SalesSchedulingCutoffCollection();
            SalesSchedulingCutoff salesSchedulingCutOff;
            
            salesSchedulingCutOff = SalesSchedulingCutoffManager.GetItem(id);
            SalesSchedulingCutoff_list.Add(salesSchedulingCutOff);

            return Request.CreateResponse(HttpStatusCode.OK, new { count = SalesSchedulingCutoff_list.Count, salesschedulingcutoffs = salesSchedulingCutOff }, Configuration.Formatters.JsonFormatter);
        }

        // GET: api/SalesSchedulingCutoff/5
        public HttpResponseMessage Get(String date)
		{
			//try
			//{
			SalesSchedulingCutoffCollection SalesSchedulingCutoff_list = new SalesSchedulingCutoffCollection();
			SalesSchedulingCutoffCriteria SalesSchedulingCutoff_criteria = new SalesSchedulingCutoffCriteria();

            SalesSchedulingCutoff_criteria.mDate = date;

            SalesSchedulingCutoff_list = SalesSchedulingCutoffManager.GetList(SalesSchedulingCutoff_criteria);
				//if (SalesSchedulingCutoff_list.Count > 0 ) 
				//{
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  SalesSchedulingCutoff_list.Count, salesschedulingcutoffs = SalesSchedulingCutoff_list }, Configuration.Formatters.JsonFormatter);
				//}
				//else
				//{
				//	return Request.CreateResponse(HttpStatusCode.BadRequest, "No record found");
				//}
			//}
			//catch
			//{
			//	return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error" );
			//}
		}

		// POST: api/SalesSchedulingCutoff
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(SalesSchedulingCutoff SalesSchedulingCutoff)
		{
			if (SalesSchedulingCutoff.Validate())
			{
				int result;

				result = SalesSchedulingCutoffManager.Save(SalesSchedulingCutoff);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/SalesSchedulingCutoff/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/SalesSchedulingCutoff/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(SalesSchedulingCutoff SalesSchedulingCutoff)
		{
			try
			{
				int result;

				result = SalesSchedulingCutoffManager.Delete(SalesSchedulingCutoff);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}