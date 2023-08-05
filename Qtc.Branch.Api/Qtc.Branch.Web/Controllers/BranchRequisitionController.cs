using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Qtc.Branch.BusinessEntities;
using Qtc.qPos.Web;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Qtc.Branch.Web.Controllers
{
    public class BranchRequisitionController : Controller
    {
        // GET: BranchRequisition
        public ActionResult Index(BranchRequisition branchRequisition )
        {
            return View();
        }

        
        public ActionResult Create()
        {
            var branchRequisition = Session["branchRequisition"] as BranchRequisition;
            //ViewBag.EmployeeName = branchRequisition.mEmployeeName;
            //ViewBag.SalesDate = branchRequisition.mSalesDate;


            branchRequisition.mBranchRequisitionDetail = getBranchRequisitionDetailsCollection(branchRequisition.mBranchId, branchRequisition.mSalesDate, branchRequisition.mId);
            return View("Create", branchRequisition);
        }

        [HttpPost]
        public ActionResult Create(BranchRequisition branchRequisition)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    HttpClientManager.ClientHeaders(client);
                    branchRequisition.mDateUpdated = DateTime.Now;
                    branchRequisition.mLmmName = "";
                    branchRequisition.mBranchName = "";
                    branchRequisition.mEmployeeName = "";
                    branchRequisition.mRemarks = "";
                    branchRequisition.mUserFullName = "";
                    branchRequisition.mEmployeeRemarks = "";
                    branchRequisition.mLmmRemarks = "";
                    foreach (BranchRequisitionDetails item in branchRequisition.mBranchRequisitionDetail)
                    {
                        item.mRemarks = "";
                        item.mUserFullName = "";
                        item.mUserId = branchRequisition.mUserId;
                    }
                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<BranchRequisition>("BranchRequisition", branchRequisition);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        //return RedirectToAction("Index");//return ton index page
                        return Json(new { success = true, responseText = "Branch Requisition save successfully." }, JsonRequestBehavior.AllowGet);
                    }
                    else //if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        //return api validation message
                        var messageResult = result.Content.ReadAsAsync<string>();
                        //ModelState.AddModelError(string.Empty, messageResult.Result);
                        return Json(new { success = false, responseText = messageResult.Result }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch
            {
                //ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                return Json(new { success = false, responseText = "Server Error. Please contact administrator." }, JsonRequestBehavior.AllowGet);
            }
        }

        private BranchRequisitionDetailsCollection getBranchRequisitionDetailsCollection(int branch_id, DateTime sales_date, int branch_requisition_id)
        {
            BranchRequisitionDetailsCollection branchRequisitionDetailsCollection = new BranchRequisitionDetailsCollection();
            using (var client = new HttpClient())
            {
                //HTTP POST
                var getTask = client.GetStringAsync(ConfigurationManager.AppSettings["APIURL"] + "BranchRequisitionDetails?branch_id=" + branch_id  + "&sales_date=" + sales_date  + "&branch_requisition_id=" + branch_requisition_id );

                getTask.Wait();
                var result = getTask.Result;
                if (result != null & result.Length > 50)
                {
                    var parsedObject = JObject.Parse(result);
                    branchRequisitionDetailsCollection = JsonConvert.DeserializeObject<BranchRequisitionDetailsCollection>(parsedObject["branchrequisitiondetailss"].ToString());
                }
            }

            return branchRequisitionDetailsCollection;

        }
    }
}