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
	public class ApiActionController : ApiController
	{
        // GET: api/ApiAction/1
        //public HttpResponseMessage GetById(int id)
        //{
        //    try
        //    {
        //        ApiAction api_action = new ApiAction();

        //        api_action = ApiActionManager.GetItem(id);

        //        return Request.CreateResponse(HttpStatusCode.OK, new { count = 1, ApiAction = api_action }, Configuration.Formatters.JsonFormatter);

               
        //    }
        //    catch
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
        //    }
        //}

		// GET: api/ApiAction
		 public HttpResponseMessage Get(int branch_id, Boolean action_taken)
		{
			try
			{
				ApiActionCollection ApiAction_list = new ApiActionCollection();
				ApiActionCriteria ApiAction_criteria = new ApiActionCriteria();

                ApiAction_criteria.mBranchId = branch_id;
                ApiAction_criteria.mActionTaken = action_taken;
       
                ApiAction_list = ApiActionManager.GetList(ApiAction_criteria);
				//if (ApiAction_list.Count > 0 ) 
				//{
                    return Request.CreateResponse(HttpStatusCode.OK, new { count = ApiAction_list.Count, ApiAction = ApiAction_list }, Configuration.Formatters.JsonFormatter);
                    //return Request.CreateResponse(HttpStatusCode.OK, ApiAction_list);
    //            }
				//else
				//{
    //                //return Request.CreateResponse(HttpStatusCode.OK, new { count = 0, ApiAction = ApiAction_list }, Configuration.Formatters.JsonFormatter);
    //                return Request.CreateResponse(HttpStatusCode.BadRequest, "No record found");
    //            }
			}
			catch
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error" );
			}
		}

		// POST: api/ApiAction
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(ApiAction ApiAction)
		{
			if (ApiAction.Validate())
			{
                try
{
                    int result;

                    result = ApiActionManager.Save(ApiAction);

                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                catch (Exception e)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message.ToString());
                }
               
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/ApiAction/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/ApiAction/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(ApiAction ApiAction)
		{
			try
			{
				int result;

				result = ApiActionManager.Delete(ApiAction);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}