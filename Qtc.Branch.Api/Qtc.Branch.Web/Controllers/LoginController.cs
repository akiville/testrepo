using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Bll;
using System.Net.Http;
using Newtonsoft.Json;
using System.Configuration;
using Newtonsoft.Json.Linq;

namespace Qtc.Branch.Web.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        // GET: Login
        public ActionResult Index(string returnURL)
        {
            var branchRequisition = new BranchRequisition();
            try
            {
                // We do not want to use any existing identity information  
                EnsureLoggedOut();

                // Store the originating URL so we can attach it to a form field  
                branchRequisition.mReturnUrl = returnURL;
                //ViewBag.Branch = DropDownHelper.getBranch(0);
                return View(branchRequisition);
            }
            catch
            {
                throw;
            }

            

            return View();
        }

        //GET: EnsureLoggedOut  
        private void EnsureLoggedOut()
        {
            // If the request is (still) marked as authenticated we send the user to the logout action  
            if (Request.IsAuthenticated)
                Logout();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            try
            {

                // First we clean the authentication ticket like always  
                //required NameSpace: using System.Web.Security;  
                FormsAuthentication.SignOut();

                // Second we clear the principal to ensure the user does not retain any authentication  
                //required NameSpace: using System.Security.Principal;  
                HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);

                Session.Clear();
                System.Web.HttpContext.Current.Session.RemoveAll();

                // Last we redirect to a controller/action that requires authentication to ensure a redirect takes place  
                // this clears the Request.IsAuthenticated flag since this triggers a new request  
                return RedirectToAction("Index", "Login");
            }
            catch
            {
                throw;
            }
        }

        //GET: SignInAsync   
        private void SignInRemember(string userName, bool isPersistent = false)
        {
            // Clear any lingering authencation data  
            FormsAuthentication.SignOut();

            // Write the authentication cookie  
            FormsAuthentication.SetAuthCookie(userName, isPersistent);
        }

        //GET: RedirectToLocal  
        private ActionResult RedirectToLocal(string returnURL = "" )
        {
            try
            {
                // If the return url starts with a slash "/" we assume it belongs to our site  
                // so we will redirect to this "action"  
                if (!string.IsNullOrWhiteSpace(returnURL) && Url.IsLocalUrl(returnURL))
                    return Redirect(returnURL);

                // If we cannot verify if the url is local to our host we redirect to a default location  
                return RedirectToAction("Create", "BranchRequisition");
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(BranchRequisition branchRequisition)
        {
            //string OldHASHValue = string.Empty;
            //byte[] SALT = new byte[512];


            using (var client = new HttpClient())
            {
                var serializedProduct = JsonConvert.SerializeObject("");

                HttpResponseMessage restResponse = new HttpResponseMessage();

                //restResponse = await client.GetAsync(ConfigurationManager.AppSettings["APIURL"] + "/User");

                restResponse = await client.GetAsync(ConfigurationManager.AppSettings["APIURL"] + "/BranchRequisition?branch_id=" + branchRequisition.mBranchId + "&sales_date=" + DateTime.Now.ToString() + "&code=" + branchRequisition.mCode + "&employee_id=" + branchRequisition.mEmployeeId + "&status=SINGLE&lmm_id=0");

                if (restResponse.IsSuccessStatusCode)
                {
                    using (HttpContent content = restResponse.Content)
                    {

                        //List<User> user_list = new List<User>();
                        BranchRequisitionCollection branchRequisitionCollection = new BranchRequisitionCollection();
                        String result = content.ReadAsStringAsync().Result;

                        if (result.Contains("No record found"))
                        {
                            return View("Create", branchRequisition);
                        }
                        else
                        {
                            //SignInRemember(user.mUsername, user.mRemember);
                            //user_list = JsonConvert.DeserializeObject<List<User>>(result);
                            if (result != null & result.Length > 50)
                            {
                                var parsedObject = JObject.Parse(result);
                                branchRequisitionCollection = JsonConvert.DeserializeObject<BranchRequisitionCollection>(parsedObject["branchrequisitions"].ToString());
                                branchRequisition = branchRequisitionCollection[0];
                                //Session["username"] = branchRequisitionCollection[0].mUsername;
                                //Session["UserID"] = branchRequisitionCollection[0].mId;
                                ViewBag.UserName = branchRequisition.mEmployeeName;

                                SignInRemember(branchRequisition.mEmployeeName, true);

                                //Session["TokenNumber"] = user.mHash;
                                Session["Username"] = branchRequisition.mEmployeeName;
                                Session["EmployeeId"] = branchRequisition.mEmployeeId;
                                Session["BranchRequisition"] = branchRequisition;
                                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                                //    Session["TokenNumber"] + ":" + Session["Username"]);

                                //try
                                //{
                                //var parsedObject = JObject.Parse(result);
                                //var userObject = (JObject)parsedObject["users"];

                                //user.mId = Convert.ToInt32(userObject.SelectToken("mId").ToString());
                                //user.mFirstname = (userObject.SelectToken("mFirstName").ToString());
                                //user.mMiddlename = (userObject.SelectToken("mMiddleName").ToString());
                                //user.mLastname = (userObject.SelectToken("mLastName").ToString());
                                //user.mUsername = (userObject.SelectToken("mUserName").ToString());
                                //user.mPassword = (userObject.SelectToken("mPassword").ToString());
                                //user.mOverride = Convert.ToBoolean((userObject.SelectToken("mOverride").ToString()));
                                //user.mCashier = Convert.ToBoolean((userObject.SelectToken("mCashier").ToString()));
                                //user.mIsAdmin = Convert.ToBoolean((userObject.SelectToken("mIsAdmin").ToString()));
                                ////user.mRemarks = userObject.SelectToken("mRemarks").ToString();
                                //user.mEmployeeId = Convert.ToInt32((userObject.SelectToken("mEmployeeId").ToString()));
                                ////user.mHash = userObject.SelectToken("mHash").ToString();
                                ////converting string to byte array
                                ////byte[] array = Encoding.ASCII.GetBytes(userObject.SelectToken("mSalt").ToString());
                                ////user.mSalt = array;




                                //Session["UserID"] = user.mId;
                            }

                            if (branchRequisition == null)
                            {
                                //Login Fail  
                                TempData["ErrorMSG"] = "Access Denied! Wrong Credential";
                                return View("Index", branchRequisition);
                            }
                            else
                            {
                                Session["branchRequisition"] = branchRequisition;
                                return RedirectToLocal(branchRequisition.mReturnUrl);
                            }
                        }
                    }
                }
                else
                {
                    TempData["ErrorMSG"] = "Access Denied! Wrong Credential";
                    return RedirectToAction("Index", "Login");
                }
                
            }
        }

    }
}