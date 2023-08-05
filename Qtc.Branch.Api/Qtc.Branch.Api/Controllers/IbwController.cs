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
	public class IbwController : ApiController
	{
        // GET: api/Ibw
        public HttpResponseMessage Get(int id)
        {
            try
            {
                IbwCollection Ibw_list = new IbwCollection();
                Ibw ibw = new Ibw();
                IbwCriteria Ibw_criteria = new IbwCriteria();

                ibw = IbwManager.GetItem(id);

                Ibw_list.Add(ibw);

                if (Ibw_list.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { count = Ibw_list.Count, ibws = Ibw_list }, Configuration.Formatters.JsonFormatter);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "No record found");
                }
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
            }
        }

        // GET: api/Ibw
        public HttpResponseMessage Get(int to_branch_id, int branch_id)
		{
			try
			{
				IbwCollection Ibw_list = new IbwCollection();
				IbwCriteria Ibw_criteria = new IbwCriteria();

                Ibw_criteria.mToBranchId = to_branch_id;
                Ibw_criteria.mBranchId = branch_id;

                Ibw_list = IbwManager.GetList(Ibw_criteria);
				if (Ibw_list.Count > 0 ) 
				{
					return Request.CreateResponse(HttpStatusCode.OK, new { count =  Ibw_list.Count, ibws = Ibw_list }, Configuration.Formatters.JsonFormatter);
				}
				else
				{
					return Request.CreateResponse(HttpStatusCode.BadRequest, "No record found");
				}
			}
			catch
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error" );
			}
		}

		// POST: api/Ibw
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(Ibw Ibw)
		{
			if (Ibw.Validate())
			{
				int result;

				result = IbwManager.Save(Ibw);

                return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter);
            }
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/Ibw/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/Ibw/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(Ibw Ibw)
		{
			try
			{
				int result;

				result = IbwManager.Delete(Ibw);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}