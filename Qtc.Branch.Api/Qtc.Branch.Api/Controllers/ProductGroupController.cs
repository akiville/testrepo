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
	public class ProductGroupController : ApiController
	{
		//// GET: api/ProductGroup
		//public IEnumerable<string> Get()
		//{
		//	return new string[] { "value1", "value2" };
		//}

		// GET: api/ProductGroup/5
		 public HttpResponseMessage Get()
		{
			try
			{
				ProductGroupCollection ProductGroup_list = new ProductGroupCollection();
				ProductGroupCriteria ProductGroup_criteria = new ProductGroupCriteria();

				ProductGroup_list = ProductGroupManager.GetList(ProductGroup_criteria);
				if (ProductGroup_list.Count > 0 ) 
				{
					return Request.CreateResponse(HttpStatusCode.OK, new { count =  ProductGroup_list.Count, productgroups = ProductGroup_list }, Configuration.Formatters.JsonFormatter);
				}
				else
				{
                    return Request.CreateResponse(HttpStatusCode.OK, new { count = ProductGroup_list.Count, productgroups = ProductGroup_list }, Configuration.Formatters.JsonFormatter);
                }
			}
			catch
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error" );
			}
		}

		// POST: api/ProductGroup
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(ProductGroup ProductGroup)
		{
			if (ProductGroup.Validate())
			{
				int result;

				result = ProductGroupManager.Save(ProductGroup);

				return Request.CreateResponse(HttpStatusCode.OK, result); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/ProductGroup/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/ProductGroup/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(ProductGroup ProductGroup)
		{
			try
			{
				int result;

				result = ProductGroupManager.Delete(ProductGroup);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}