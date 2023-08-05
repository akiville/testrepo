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
	public class DisseminatedLetterExtensionRequestController : ApiController
	{
        // GET: api/DisseminatedLetterExtensionRequest
        public HttpResponseMessage Get(int id)
        {
            DisseminatedLetterExtensionRequestCollection DisseminatedLetterExtensionRequest_list = new DisseminatedLetterExtensionRequestCollection();
            DisseminatedLetterExtensionRequest disseminated_letter_extenstion_request = new DisseminatedLetterExtensionRequest();
            disseminated_letter_extenstion_request = DisseminatedLetterExtensionRequestManager.GetItem(id);
            DisseminatedLetterExtensionRequest_list.Add(disseminated_letter_extenstion_request);
            return Request.CreateResponse(HttpStatusCode.OK, new { count = DisseminatedLetterExtensionRequest_list.Count, disseminatedletterextensionrequests = DisseminatedLetterExtensionRequest_list }, Configuration.Formatters.JsonFormatter);
        }

        // GET: api/DisseminatedLetterExtensionRequest/5
        public HttpResponseMessage Get(int disseminated_letter_id, int lmm_id )
		{
			
			DisseminatedLetterExtensionRequestCollection DisseminatedLetterExtensionRequest_list = new DisseminatedLetterExtensionRequestCollection();
			DisseminatedLetterExtensionRequestCriteria DisseminatedLetterExtensionRequest_criteria = new DisseminatedLetterExtensionRequestCriteria();

            DisseminatedLetterExtensionRequest_criteria.mDisseminatedLetterId = disseminated_letter_id;
            DisseminatedLetterExtensionRequest_criteria.mLmmId = lmm_id;

            DisseminatedLetterExtensionRequest_list = DisseminatedLetterExtensionRequestManager.GetList(DisseminatedLetterExtensionRequest_criteria);
				
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  DisseminatedLetterExtensionRequest_list.Count, disseminatedletterextensionrequests = DisseminatedLetterExtensionRequest_list }, Configuration.Formatters.JsonFormatter);
				
		}

		// POST: api/DisseminatedLetterExtensionRequest
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(DisseminatedLetterExtensionRequest DisseminatedLetterExtensionRequest)
		{
			if (DisseminatedLetterExtensionRequest.Validate())
			{
				int result;

				result = DisseminatedLetterExtensionRequestManager.Save(DisseminatedLetterExtensionRequest);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/DisseminatedLetterExtensionRequest/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/DisseminatedLetterExtensionRequest/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(DisseminatedLetterExtensionRequest DisseminatedLetterExtensionRequest)
		{
			try
			{
				int result;

				result = DisseminatedLetterExtensionRequestManager.Delete(DisseminatedLetterExtensionRequest);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}