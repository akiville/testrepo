using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Collections.Generic;
using Qtc.Branch.Bll;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Api.Models;

namespace Qtc.Branch.Api.Controllers
{
	public class AddBackController : ApiController
	{
        // GET: api/AddBack
        public HttpResponseMessage GetById(int id)
        {
            try
            {
                AddBack addBack = new AddBack();

                addBack = AddBackManager.GetItem(id);

                if (addBack != null)
                {
                    AddBackCollection AddBack_list = new AddBackCollection();
                    AddBack_list.Add(addBack);

                    return Request.CreateResponse(HttpStatusCode.OK, new { count = 1, add_back = AddBack_list }, Configuration.Formatters.JsonFormatter);
                }
                else {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No Record Found");
                }


            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
            }
        }

        // GET: api/AddBack/5
        public HttpResponseMessage Get(String add_back_status, int branch_id)
		{
            try
            {
                AddBackCollection AddBack_list = new AddBackCollection();
                AddBackCriteria AddBack_criteria = new AddBackCriteria();

                AddBack_criteria.mAddBackStatus = add_back_status;
                AddBack_criteria.mBranchId = branch_id;

                AddBack_list = AddBackManager.GetList(AddBack_criteria);
                if (AddBack_list.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { count = AddBack_list.Count, add_back = AddBack_list }, Configuration.Formatters.JsonFormatter);
                    //return Request.CreateResponse(HttpStatusCode.OK, AddBack_list);
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

		// POST: api/AddBack
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(AddBack AddBack)
		{
            //AddBack AddBack = new AddBack();
            ////AddBack = AddBackManager.GetItem(AddBackApi.id);

            //AddBack.mId = AddBackApi.id;
            //AddBack.mProductAvailId = AddBackApi.product_avail_id;
            //AddBack.mBranchId = AddBackApi.branch_id;
            //AddBack.mPersonnelId = AddBackApi.personnel_id;
            //AddBack.mProductId = AddBackApi.product_id;
            //AddBack.mSalesDate = AddBackApi.sales_date;
            //AddBack.mAddBackQty = AddBackApi.add_back_qty;
            //AddBack.mAddBackReason = AddBackApi.add_back_reason;
            //AddBack.mAddBackStatus = AddBackApi.add_back_status;
            //AddBack.mPriorQty = AddBackApi.prior_qty;
            //AddBack.mAvailQty = AddBackApi.avail_qty;
            //AddBack.mApprovedById = AddBackApi.approved_by_id;
            //AddBack.mApprovalDate = AddBackApi.approval_date;
            //AddBack.mApprovalRemarks = AddBackApi.approval_remarks;
            //AddBack.mDatestamp = DateTime.Now;
            //AddBack.mUserFullName = "";


            if (AddBack.Validate())
			{
				int result;

				result = AddBackManager.Save(AddBack);

				//return Request.CreateResponse(HttpStatusCode.OK, result);
                return Request.CreateResponse(HttpStatusCode.OK, new { resul = result }, Configuration.Formatters.JsonFormatter);
            }
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/AddBack/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/AddBack/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(AddBack AddBack)
		{
			try
			{
				int result;

				result = AddBackManager.Delete(AddBack);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}