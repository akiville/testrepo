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
	public class RfscController : ApiController
	{
		// GET: api/Rfsc
		public HttpResponseMessage Get(int id)
		{
            Rfsc rfsc = new Rfsc();
            rfsc = RfscManager.GetItem(id);
            RfscCollection rfsc_list = new RfscCollection();

            rfsc_list.Add(rfsc);
            return Request.CreateResponse(HttpStatusCode.OK, new { count = 1, rfscs = rfsc_list }, Configuration.Formatters.JsonFormatter);

        }

		// GET: api/Rfsc/5
		 public HttpResponseMessage Get(int encoder_id, DateTime start_date, DateTime end_date, int status_id)
		{
			try
			{
				RfscCollection Rfsc_list = new RfscCollection();
				RfscCriteria Rfsc_criteria = new RfscCriteria();

                Rfsc_criteria.mEncoderId = encoder_id;
                Rfsc_criteria.mStartDate = start_date;
                Rfsc_criteria.mEndDate = end_date;
                Rfsc_criteria.mStatusId = status_id;

                Rfsc_list = RfscManager.GetList(Rfsc_criteria);
		
				return Request.CreateResponse(HttpStatusCode.OK, new { count =  Rfsc_list.Count, rfscs = Rfsc_list }, Configuration.Formatters.JsonFormatter);
				
			}
			catch
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error" );
			}
		}

		// POST: api/Rfsc
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(Rfsc Rfsc)
		{
			if (Rfsc.Validate())
			{
				int result;

				result = RfscManager.Save(Rfsc);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/Rfsc/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/Rfsc/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(Rfsc Rfsc)
		{
			try
			{
				int result;

				result = RfscManager.Delete(Rfsc);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}