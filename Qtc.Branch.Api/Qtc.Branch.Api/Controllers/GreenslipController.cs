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
	public class GreenslipController : ApiController
	{
       

        //GET: api/Greenslip/1
        public HttpResponseMessage GetById(int id)
        {
            try
            {
                Greenslip greenslip = new Greenslip();

                greenslip = GreenslipManager.GetItem(id);

                if (greenslip != null)
                {
                    GreenslipCollection Greenslip_list = new GreenslipCollection();

                    Greenslip_list.Add(greenslip);

                    return Request.CreateResponse(HttpStatusCode.OK, new { count = Greenslip_list.Count, greenslips = Greenslip_list }, Configuration.Formatters.JsonFormatter);
                } else {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No Record Found");
                }
                

            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
            }
        }

        public HttpResponseMessage Get(int branch_id, String start_date, String end_date, int is_downloaded , int greenslip_id)
		{
			try
			{
                Boolean is_downloaded_value; 
                if (is_downloaded == 0)
                {
                    is_downloaded_value = false;
                }
                else
                {
                    is_downloaded_value = true;
                }


				GreenslipCollection Greenslip_list = new GreenslipCollection();
				GreenslipCriteria Greenslip_criteria = new GreenslipCriteria();

                Greenslip_criteria.mStartDate = start_date;
                Greenslip_criteria.mEndDate = end_date;
                Greenslip_criteria.mBranchId = branch_id;
                Greenslip_criteria.mIsDownloaded = is_downloaded_value;
                Greenslip_criteria.mId = greenslip_id;

                Greenslip_list = GreenslipManager.GetList(Greenslip_criteria);
				if (Greenslip_list.Count > 0 ) 
				{
                    return Request.CreateResponse(HttpStatusCode.OK, new { count = Greenslip_list.Count, greenslips = Greenslip_list }, Configuration.Formatters.JsonFormatter);
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

		// POST: api/Greenslip
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(Greenslip Greenslip)
		{
			if (Greenslip.Validate())
			{
				int result;

				result = GreenslipManager.Save(Greenslip);

                //return Request.CreateResponse(HttpStatusCode.OK, result); 
                return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter);
            }
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/Greenslip/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/Greenslip/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(Greenslip Greenslip)
		{
			try
			{
				int result;

				result = GreenslipManager.Delete(Greenslip);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}