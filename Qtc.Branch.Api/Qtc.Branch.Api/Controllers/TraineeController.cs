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
	public class TraineeController : ApiController
	{
        // GET: api/Trainee
        public HttpResponseMessage Get(int id)
        {
            TraineeCollection Trainee_list = new TraineeCollection();
            Trainee trainee = new Trainee();

            trainee = TraineeManager.GetItem(id);

            Trainee_list.Add(trainee);
            return Request.CreateResponse(HttpStatusCode.OK, new { count = Trainee_list.Count, trainees = Trainee_list }, Configuration.Formatters.JsonFormatter);

        }

        // GET: api/Trainee/5
        public HttpResponseMessage Get(int lmm_id, int employee_id )
		{
			//try
			//{
			TraineeCollection Trainee_list = new TraineeCollection();
			TraineeCriteria Trainee_criteria = new TraineeCriteria();

            Trainee_criteria.mLmmId = lmm_id;
            Trainee_criteria.mEmployeeId = employee_id;
            Trainee_list = TraineeManager.GetList(Trainee_criteria);
				//if (Trainee_list.Count > 0 ) 
				//{
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  Trainee_list.Count, trainees = Trainee_list }, Configuration.Formatters.JsonFormatter);
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

		// POST: api/Trainee
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(Trainee Trainee)
		{
			if (Trainee.Validate())
			{
				int result;

				result = TraineeManager.Save(Trainee);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/Trainee/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/Trainee/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(Trainee Trainee)
		{
			try
			{
				int result;

				result = TraineeManager.Delete(Trainee);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}