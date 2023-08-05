using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Collections.Generic;
using Qtc.Branch.Bll;
using Qtc.Branch.BusinessEntities;
using System.Web;
using System.IO;

namespace Qtc.Branch.Api.Controllers
{
	public class BranchCashController : ApiController
	{
		// GET: api/BranchCash
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		// GET: api/BranchCash/5
		 public HttpResponseMessage Get(int branch_id, String start_date, String end_date)
		{
			//try
			//{
				BranchCashCollection BranchCash_list = new BranchCashCollection();
				BranchCashCriteria BranchCash_criteria = new BranchCashCriteria();

                BranchCash_criteria.mBranchId = branch_id;
                BranchCash_criteria.mStartDate = start_date;
                BranchCash_criteria.mEndDate = end_date;

                BranchCash_list = BranchCashManager.GetList(BranchCash_criteria);
				if (BranchCash_list.Count > 0 ) 
				{
                    return Request.CreateResponse(HttpStatusCode.OK, new { count = BranchCash_list.Count, branch_cash = BranchCash_list }, Configuration.Formatters.JsonFormatter);
                    //return Request.CreateResponse(HttpStatusCode.OK, BranchCash_list);
				}
				else
				{
                    return Request.CreateResponse(HttpStatusCode.OK, new { count = BranchCash_list.Count, branch_cash = BranchCash_list }, Configuration.Formatters.JsonFormatter);
                }
			//}
			//catch
			//{
			//	return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error" );
			//}
		}

		// POST: api/BranchCash
		[HttpPost]
		public HttpResponseMessage Post(BranchCash BranchCash)
		{
			if (BranchCash.Validate())
			{
				int result;

				result = BranchCashManager.Save(BranchCash);

                return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter);
            }
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

        //POST: api/BranchCash
        //[HttpPost]
        //public HttpResponseMessage PostUpdate()
        //{
        //    try
        //    {
        //        var request = HttpContext.Current.Request;

        //        var id = request.Form["id"];
        //        var is_deposited = request.Form["is_deposited"];
        //        var cash_explanation = request.Form["cash_explanation"];
        //        var deposit_date = request.Form["deposite_date"];
        //        var deposited_by_id = request.Form["deposited_by_id"];
        //        var deposit_slip_image_name = request.Form["deposit_slip_image_name"];
        //        var picture = request.Files["picture"];

        //        byte[] bytes;
        //        using (var stream = new MemoryStream())
        //        {
        //            picture.InputStream.CopyTo(stream);
        //            bytes = stream.ToArray();
        //        }

        //        BranchCash branchcash = new BranchCash();

        //        branchcash = BranchCashManager.GetItem(Int32.Parse(id));

        //        branchcash.mDepositedById = Int32.Parse(deposited_by_id);
        //        branchcash.mIsDeposited = Int32.Parse(is_deposited);
        //        branchcash.mDepositDate = DateTime.Parse(deposit_date);
        //        branchcash.mDepositSlipImageName = deposit_slip_image_name;

        //        if (branchcash.Validate())
        //        {
        //            picture.SaveAs(HttpContext.Current.Server.MapPath("~/DepositSlip/" + picture.FileName));

        //            int result;

        //            result = BranchCashManager.Save(branchcash);

        //            return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter);
        //        }
        //        else
        //        {
        //            return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request");
        //        }

        //    }
        //    catch
        //    {
        //        return new HttpResponseMessage(HttpStatusCode.BadRequest);
        //    }
         
        //}

        // PUT: api/BranchCash
        //[HttpPost]
        //[CustomAuthorizationFilter]
  //      public HttpResponseMessage Put()
		//{
  //          try
  //          {
  //              var request = HttpContext.Current.Request;

  //              var id = request.Form["id"];
  //              var is_deposited = request.Form["is_deposited"];
  //              var cash_explanation = request.Form["cash_explanation"];
  //              var deposit_date = request.Form["deposit_date"];
  //              var deposited_by_id = request.Form["deposited_by_id"];
  //              var deposit_slip_image_name = request.Form["deposit_slip_image_name"];
  //              var picture = request.Files["picture"];

  //              byte[] bytes;
  //              using (var stream = new MemoryStream())
  //              {
  //                  picture.InputStream.CopyTo(stream);
  //                  bytes = stream.ToArray();
  //              }

  //              BranchCash branchcash = new BranchCash();

  //              branchcash = BranchCashManager.GetItem(Int32.Parse(id));

  //              branchcash.mDepositedById = Int32.Parse(deposited_by_id);
  //              branchcash.mIsDeposited = Int32.Parse(is_deposited);
  //              branchcash.mDepositDate = DateTime.Parse(deposit_date);
  //              branchcash.mDepositSlipImageName = deposit_slip_image_name;

  //              if (branchcash.Validate())
  //              {
  //                  picture.SaveAs(HttpContext.Current.Server.MapPath("~/DepositSlip/" + picture.FileName));

  //                  int result;
  //                  result = BranchCashManager.Save(branchcash);
  //                  return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter);
  //              }
  //              else
  //              {
  //                  return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request");
  //              }

  //          }
  //          catch
  //          {
  //              return new HttpResponseMessage(HttpStatusCode.BadRequest);
  //          }
  //      }

        // DELETE: api/BranchCash/5
        //[HttpPost]
        //[CustomAuthorizationFilter]
        public HttpResponseMessage Delete(BranchCash BranchCash)
		{
			try
			{
				int result;

				result = BranchCashManager.Delete(BranchCash);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}