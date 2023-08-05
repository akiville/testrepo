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
	public class TrainingAttendance2Controller : ApiController
	{
		// GET: api/TrainingAttendance2
		public HttpResponseMessage Get(int id)
        {
            TrainingAttendance2Collection TrainingAttendance2_list = new TrainingAttendance2Collection();
            TrainingAttendance2 trainingAttendance2 = new TrainingAttendance2();
            
            trainingAttendance2 = TrainingAttendance2Manager.GetItem(id);
            TrainingAttendance2_list.Add(trainingAttendance2);


            return Request.CreateResponse(HttpStatusCode.OK, new { count = TrainingAttendance2_list.Count, trainingattendance2s = TrainingAttendance2_list }, Configuration.Formatters.JsonFormatter);


        }

        // GET: api/TrainingAttendance2/5
        public HttpResponseMessage Get(int lmm_id, DateTime sales_date)
		{
			
		    TrainingAttendance2Collection TrainingAttendance2_list = new TrainingAttendance2Collection();
		    TrainingAttendance2Criteria TrainingAttendance2_criteria = new TrainingAttendance2Criteria();

            TrainingAttendance2_criteria.mLmmId = lmm_id;
            TrainingAttendance2_criteria.mSalesDate = sales_date;

            TrainingAttendance2_list = TrainingAttendance2Manager.GetList(TrainingAttendance2_criteria);
			
		    return Request.CreateResponse(HttpStatusCode.OK, new { count =  TrainingAttendance2_list.Count, trainingattendance2s = TrainingAttendance2_list }, Configuration.Formatters.JsonFormatter);
				
			
		}

		// POST: api/TrainingAttendance2
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(TrainingAttendance2 TrainingAttendance2)
		{
			if (TrainingAttendance2.Validate())
			{
				int result;

				result = TrainingAttendance2Manager.Save(TrainingAttendance2);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/TrainingAttendance2/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/TrainingAttendance2/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(TrainingAttendance2 TrainingAttendance2)
		{
			try
			{
				int result;

				result = TrainingAttendance2Manager.Delete(TrainingAttendance2);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}