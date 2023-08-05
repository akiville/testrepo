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
	public class BranchProductController : ApiController
	{
		// GET: api/BranchProduct
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		// GET: api/BranchProduct/5
		 public HttpResponseMessage Get(int lmm_id)
		{
			//try
			//{
				BranchProductCollection BranchProduct_list = new BranchProductCollection();
				BranchProductCriteria BranchProduct_criteria = new BranchProductCriteria();

                BranchProduct_criteria.mLmmId = lmm_id;

                BranchProduct_list = BranchProductManager.GetList(BranchProduct_criteria);
                if (BranchProduct_list.Count > 0)
                {
                    //return Request.CreateResponse(HttpStatusCode.OK, BranchProduct_list);
                    return Request.CreateResponse(HttpStatusCode.OK, new { count = BranchProduct_list.Count, branchproducts = BranchProduct_list }, Configuration.Formatters.JsonFormatter);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { count = 0, branchproducts = BranchProduct_list }, Configuration.Formatters.JsonFormatter);
                }
   //         }
			//catch
			//{
			//	return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error" );
			//}
		}

		// POST: api/BranchProduct
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(BranchProductApi BranchProductApi)
		{
            BranchProduct BranchProduct = new BranchProduct();

            BranchProduct.mId = BranchProductApi.Id;
            BranchProduct.mBranchId = BranchProductApi.BranchId;
            BranchProduct.mProductId = BranchProductApi.ProductId;
            BranchProduct.mUserId = BranchProductApi.UserId;
            BranchProduct.mUserFullName = "";
            BranchProduct.mDatestamp = DateTime.Now;

            if (BranchProduct.Validate())
			{
				int result;

				result = BranchProductManager.Save(BranchProduct);

                return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter);
            }
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/BranchProduct/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/BranchProduct/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(BranchProduct BranchProduct)
		{
			try
			{
				int result;

				result = BranchProductManager.Delete(BranchProduct);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}