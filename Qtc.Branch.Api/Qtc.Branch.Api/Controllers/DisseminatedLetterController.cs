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
	public class DisseminatedLetterController : ApiController
	{
        // GET: api/DisseminatedLetter
        public HttpResponseMessage Get(int id)
        {
            DisseminatedLetterCollection DisseminatedLetter_list = new DisseminatedLetterCollection();
            DisseminatedLetter disseminated_letter = new DisseminatedLetter();

            disseminated_letter = DisseminatedLetterManager.GetItem(id);

            DisseminatedLetter_list.Add(disseminated_letter);
            
            return Request.CreateResponse(HttpStatusCode.OK, new { count = DisseminatedLetter_list.Count, disseminatedletters = DisseminatedLetter_list }, Configuration.Formatters.JsonFormatter);
        }

        // GET: api/DisseminatedLetter/5
        public HttpResponseMessage Get(int lmm_id, String type)
		{
			//try
			//{
				DisseminatedLetterCollection DisseminatedLetter_list = new DisseminatedLetterCollection();
				DisseminatedLetterCriteria DisseminatedLetter_criteria = new DisseminatedLetterCriteria();

                DisseminatedLetter_criteria.mLmmId = lmm_id;
                DisseminatedLetter_criteria.mType = type;

                DisseminatedLetter_list = DisseminatedLetterManager.GetList(DisseminatedLetter_criteria);
				//if (DisseminatedLetter_list.Count > 0 ) 
				//{
					return Request.CreateResponse(HttpStatusCode.OK, new { count =  DisseminatedLetter_list.Count, disseminatedletters = DisseminatedLetter_list }, Configuration.Formatters.JsonFormatter);
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

		// POST: api/DisseminatedLetter
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(DisseminatedLetter DisseminatedLetter)
		{
			if (DisseminatedLetter.Validate())
			{
				int result;

				result = DisseminatedLetterManager.Save(DisseminatedLetter);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/DisseminatedLetter/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/DisseminatedLetter/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(DisseminatedLetter DisseminatedLetter)
		{
			try
			{
				int result;

				result = DisseminatedLetterManager.Delete(DisseminatedLetter);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}