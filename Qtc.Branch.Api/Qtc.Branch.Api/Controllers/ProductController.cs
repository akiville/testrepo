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
	public class ProductController : ApiController
	{
		// GET: api/Product
		 public HttpResponseMessage Get(int id)
		{
			ProductCollection Product_list = new ProductCollection();
			Product Product = new Product();
			Product = ProductManager.GetItem(id);
			Product_list.Add(Product);
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  Product_list.Count, products = Product_list }, Configuration.Formatters.JsonFormatter);
		}

		// GET: api/Product/5
		 public HttpResponseMessage Get(int rp_filing, String type)
		{
			ProductCollection Product_list = new ProductCollection();
			ProductCriteria Product_criteria = new ProductCriteria();
            Product_criteria.mRpFilingValue = rp_filing;

            Product_list = ProductManager.GetList(Product_criteria);
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  Product_list.Count, products = Product_list }, Configuration.Formatters.JsonFormatter);
		}

		// POST: api/Product
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(Product Product)
		{
			if (Product.Validate())
			{
				int result;

				result = ProductManager.Save(Product);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/Product/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/Product/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(Product Product)
		{
			try
			{
				int result;

				result = ProductManager.Delete(Product);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}