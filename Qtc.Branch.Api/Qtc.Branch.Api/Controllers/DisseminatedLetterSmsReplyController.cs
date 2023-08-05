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
	public class DisseminatedLetterSmsReplyController : ApiController
	{
		// GET: api/DisseminatedLetterSmsReply
		//public IEnumerable<string> Get()
		//{
		//	return new string[] { "value1", "value2" };
		//}

		// GET: api/DisseminatedLetterSmsReply/5
		// public HttpResponseMessage Get(int id)
		//{
		//	try
		//	{
		//		DisseminatedLetterSmsReplyCollection DisseminatedLetterSmsReply_list = new DisseminatedLetterSmsReplyCollection();
		//		DisseminatedLetterSmsReplyCriteria DisseminatedLetterSmsReply_criteria = new DisseminatedLetterSmsReplyCriteria();

		//		DisseminatedLetterSmsReply_list = DisseminatedLetterSmsReplyManager.GetList(DisseminatedLetterSmsReply_criteria);
		//		if (DisseminatedLetterSmsReply_list.Count > 0 ) 
		//		{
		//			return Request.CreateResponse(HttpStatusCode.OK, new { count =  DisseminatedLetterSmsReply_list.Count, disseminatedlettersmsreplys = DisseminatedLetterSmsReply_list }, Configuration.Formatters.JsonFormatter);
		//		}
		//		else
		//		{
		//			return Request.CreateResponse(HttpStatusCode.BadRequest, "No record found");
		//		}
		//	}
		//	catch
		//	{
		//		return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error" );
		//	}
		//}

		// POST: api/DisseminatedLetterSmsReply
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(DisseminatedLetterSmsReply DisseminatedLetterSmsReply)
		{
			if (DisseminatedLetterSmsReply.Validate())
			{
				int result;

				result = DisseminatedLetterSmsReplyManager.Save(DisseminatedLetterSmsReply);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/DisseminatedLetterSmsReply/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/DisseminatedLetterSmsReply/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(DisseminatedLetterSmsReply DisseminatedLetterSmsReply)
		{
			try
			{
				int result;

				result = DisseminatedLetterSmsReplyManager.Delete(DisseminatedLetterSmsReply);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}