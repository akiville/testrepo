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
	public class RovingCheckListController : ApiController
	{
		// GET: api/RovingCheckList
		public HttpResponseMessage Get(int id)
        {
            RovingCheckListCollection RovingCheckList_list = new RovingCheckListCollection();
            RovingCheckList roving_check_list = new RovingCheckList();
            roving_check_list = RovingCheckListManager.GetItem(id);
            RovingCheckList_list.Add(roving_check_list);
            return Request.CreateResponse(HttpStatusCode.OK, new { count = RovingCheckList_list.Count, rovingchecklists = RovingCheckList_list }, Configuration.Formatters.JsonFormatter);

        }

        // GET: api/RovingCheckList/5
        public HttpResponseMessage Get()
		{
			
			RovingCheckListCollection RovingCheckList_list = new RovingCheckListCollection();
			RovingCheckListCriteria RovingCheckList_criteria = new RovingCheckListCriteria();

            RovingCheckList_list = RovingCheckListManager.GetList(RovingCheckList_criteria);
				
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  RovingCheckList_list.Count, rovingchecklists = RovingCheckList_list }, Configuration.Formatters.JsonFormatter);
				
		}

		// POST: api/RovingCheckList
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(RovingCheckList RovingCheckList)
		{
			if (RovingCheckList.Validate())
			{
				int result;

				result = RovingCheckListManager.Save(RovingCheckList);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/RovingCheckList/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/RovingCheckList/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(RovingCheckList RovingCheckList)
		{
			try
			{
				int result;

				result = RovingCheckListManager.Delete(RovingCheckList);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}