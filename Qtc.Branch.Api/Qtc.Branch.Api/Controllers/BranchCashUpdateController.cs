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
    public class BranchCashUpdateController : ApiController
    {
        // GET: BranchCashUpdate
        //public ActionResult Index()
        //{
        //    return View();
        //}

        // POST: api/BranchCash
        [HttpPost]
        public HttpResponseMessage Post()
        {
            try
            {
                var request = HttpContext.Current.Request;

                var id = request.Form["id"];
                var is_deposited = request.Form["is_deposited"];
                var cash_explanation = request.Form["cash_explanation"];
                var deposit_date = request.Form["deposit_date"];
                var deposited_by_id = request.Form["deposited_by_id"];
                var deposit_slip_image_name = request.Form["deposit_slip_image_name"];
                var picture = request.Files["picture"];

                byte[] bytes;
                using (var stream = new MemoryStream())
                {
                    picture.InputStream.CopyTo(stream);
                    bytes = stream.ToArray();
                }

                BranchCash branchcash = new BranchCash();

                branchcash = BranchCashManager.GetItem(Int32.Parse(id));

                branchcash.mDepositedById = Int32.Parse(deposited_by_id);
                branchcash.mIsDeposited = Int32.Parse(is_deposited);
                branchcash.mDepositDate = DateTime.Parse(deposit_date);
                branchcash.mDepositSlipImageName = deposit_slip_image_name;
                branchcash.mCashExplanation = cash_explanation;

                if (branchcash.Validate())
                {
                    picture.SaveAs(HttpContext.Current.Server.MapPath("~/DepositSlip/" + picture.FileName));

                    int result;
                    result = BranchCashManager.Save(branchcash);
                    return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request");
                }

            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
    }
}