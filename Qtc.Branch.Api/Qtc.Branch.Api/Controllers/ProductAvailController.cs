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
	public class ProductAvailController : ApiController
	{
		//// GET: api/ProductAvail
		//public IEnumerable<string> Get()
		//{
		//	return new string[] { "value1", "value2" };
		//}

		// GET: api/ProductAvail/5
		 public HttpResponseMessage Get(int branch_id, int id, int category_id, DateTime inventory_date)
		{
            //try
            //{
            ProductAvailCollection ProductAvail_list = new ProductAvailCollection();
			ProductAvailCriteria ProductAvail_criteria = new ProductAvailCriteria();

            ProductAvail_criteria.mBranchId = branch_id;
            ProductAvail_criteria.mId = id;
            ProductAvail_criteria.mCategoryId = category_id;
            ProductAvail_criteria.mInventoryDate = inventory_date;

            ProductAvail_list = ProductAvailManager.GetList(ProductAvail_criteria);
				//if (ProductAvail_list.Count > 0 ) 
				//{
            return Request.CreateResponse(HttpStatusCode.OK, new { count = ProductAvail_list.Count, productavails = ProductAvail_list }, Configuration.Formatters.JsonFormatter);
                //return Request.CreateResponse(HttpStatusCode.OK, ProductAvail_list);
                //            }
                //else
                //{
                //                return Request.CreateResponse(HttpStatusCode.OK, new { count = ProductAvail_list.Count, product_avails = ProductAvail_list }, Configuration.Formatters.JsonFormatter);
                //                //return Request.CreateResponse(HttpStatusCode.BadRequest, "No record found");
                //}
            //}
            //catch
            //{
            //    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
            //}
        }

        // GET: api/ProductAvail/5
        public HttpResponseMessage GetListForDownload(Boolean is_ho_downloaded)
        {
            try
            {
                ProductAvailCollection ProductAvail_list = new ProductAvailCollection();
                ProductAvailCriteria ProductAvail_criteria = new ProductAvailCriteria();

                ProductAvail_criteria.mIsHoDownloaded = is_ho_downloaded;

                ProductAvail_list = ProductAvailManager.GetListForDownload(ProductAvail_criteria);
                //if (ProductAvail_list.Count > 0)
                //{
                    return Request.CreateResponse(HttpStatusCode.OK, new { count = ProductAvail_list.Count, productavails = ProductAvail_list }, Configuration.Formatters.JsonFormatter);
                    //return Request.CreateResponse(HttpStatusCode.OK, ProductAvail_list);
                //}
                //else
                //{
                //    return Request.CreateResponse(HttpStatusCode.OK, new { count = ProductAvail_list.Count, product_avails = ProductAvail_list }, Configuration.Formatters.JsonFormatter);
                //    //return Request.CreateResponse(HttpStatusCode.BadRequest, "No record found");
                //}
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
            }
        }

        // POST: api/ProductAvail
        //[HttpPost]
        public HttpResponseMessage Post(ProductAvail ProductAvail)
		{
			if (ProductAvail.Validate())
			{
				int result;

				result = ProductAvailManager.Save(ProductAvail);
                return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter);
                //return Request.CreateResponse(HttpStatusCode.OK, result); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/ProductAvail/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/ProductAvail/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(ProductAvail ProductAvail)
		{
			try
			{
				int result;

				result = ProductAvailManager.Delete(ProductAvail);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}