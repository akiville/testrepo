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
	public class ShiftController : ApiController
	{
		// GET: api/Shift
		public HttpResponseMessage Get(int id)
        {
            ShiftCollection Shift_list = new ShiftCollection();
            Shift shift = new Shift();
            shift = ShiftManager.GetItem(id);
            Shift_list.Add(shift);
            return Request.CreateResponse(HttpStatusCode.OK, new { count = Shift_list.Count, shifts = Shift_list }, Configuration.Formatters.JsonFormatter);

        }

        // GET: api/Shift/5
        public HttpResponseMessage Get()
		{
			
			ShiftCollection Shift_list = new ShiftCollection();
			ShiftCriteria Shift_criteria = new ShiftCriteria();

			Shift_list = ShiftManager.GetList(Shift_criteria);
			
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  Shift_list.Count, shifts = Shift_list }, Configuration.Formatters.JsonFormatter);
				
		}

		// POST: api/Shift
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(Shift Shift)
		{
			if (Shift.Validate())
			{
				int result;

				result = ShiftManager.Save(Shift);

				return Request.CreateResponse(HttpStatusCode.OK, result); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/Shift/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/Shift/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(Shift Shift)
		{
			try
			{
				int result;

				result = ShiftManager.Delete(Shift);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}