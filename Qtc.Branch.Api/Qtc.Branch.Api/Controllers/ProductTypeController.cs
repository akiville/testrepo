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
	public class ProductTypeController : ApiController
	{
		// GET: api/ProductType
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		// GET: api/ProductType/5
		 public HttpResponseMessage Get(int id)
		{
			try
			{
				ProductTypeCollection ProductType_list = new ProductTypeCollection();
				ProductTypeCriteria ProductType_criteria = new ProductTypeCriteria();
                
                ProductType_list = ProductTypeManager.GetList(ProductType_criteria);
				if (ProductType_list.Count > 0 ) 
				{
					return Request.CreateResponse(HttpStatusCode.OK, ProductType_list);
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

		// POST: api/ProductType
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(ProductType ProductType)
		{
			if (ProductType.Validate())
			{
				int result;

				result = ProductTypeManager.Save(ProductType);

				return Request.CreateResponse(HttpStatusCode.OK, result); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/ProductType/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/ProductType/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(ProductType ProductType)
		{
			try
			{
				int result;

				result = ProductTypeManager.Delete(ProductType);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}