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
	public class HrLetterHistoryMovementController : ApiController
	{
        // GET: api/HrLetterHistoryMovement
        public HttpResponseMessage Get(int id)
        {
            HrLetterHistoryMovementCollection HrLetterHistoryMovement_list = new HrLetterHistoryMovementCollection();
            HrLetterHistoryMovement hr_letter_history_movement = new HrLetterHistoryMovement();
            hr_letter_history_movement = HrLetterHistoryMovementManager.GetItem(id);
            HrLetterHistoryMovement_list.Add(hr_letter_history_movement);
            return Request.CreateResponse(HttpStatusCode.OK, new { count = HrLetterHistoryMovement_list.Count, hrletterhistorymovements = HrLetterHistoryMovement_list }, Configuration.Formatters.JsonFormatter);
        }

		// GET: api/HrLetterHistoryMovement/5
		 public HttpResponseMessage Get(int hr_letter_id , int dummy)
		{
			HrLetterHistoryMovementCollection HrLetterHistoryMovement_list = new HrLetterHistoryMovementCollection();
			HrLetterHistoryMovementCriteria HrLetterHistoryMovement_criteria = new HrLetterHistoryMovementCriteria();

            HrLetterHistoryMovement_criteria.mHrLetterId = hr_letter_id;
            HrLetterHistoryMovement_list = HrLetterHistoryMovementManager.GetList(HrLetterHistoryMovement_criteria);
			
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  HrLetterHistoryMovement_list.Count, hrletterhistorymovements = HrLetterHistoryMovement_list }, Configuration.Formatters.JsonFormatter);
				
		}

		// POST: api/HrLetterHistoryMovement
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(HrLetterHistoryMovement HrLetterHistoryMovement)
		{
			if (HrLetterHistoryMovement.Validate())
			{
				int result;

				result = HrLetterHistoryMovementManager.Save(HrLetterHistoryMovement);

				return Request.CreateResponse(HttpStatusCode.OK, result); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/HrLetterHistoryMovement/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/HrLetterHistoryMovement/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(HrLetterHistoryMovement HrLetterHistoryMovement)
		{
			try
			{
				int result;

				result = HrLetterHistoryMovementManager.Delete(HrLetterHistoryMovement);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}