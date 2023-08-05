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
	public class CategoryController : ApiController
	{
		// GET: api/Category
		public HttpResponseMessage Get(String category)
		{
            try
            {
                CategoryCollection Category_list = new CategoryCollection();
                CategoryCriteria Category_criteria = new CategoryCriteria();

                Category_criteria.mName = category;

                Category_list = CategoryManager.GetList(Category_criteria);
                if (Category_list.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { count = Category_list.Count, categories = Category_list }, Configuration.Formatters.JsonFormatter);
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

		// GET: api/Category/5
		 public HttpResponseMessage Get(int id)
		{
			try
			{
				CategoryCollection Category_list = new CategoryCollection();
				CategoryCriteria Category_criteria = new CategoryCriteria();

                Category_criteria.mId = id;

                Category_list = CategoryManager.GetList(Category_criteria);
				if (Category_list.Count > 0 ) 
				{
					return Request.CreateResponse(HttpStatusCode.OK, Category_list);
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

		// POST: api/Category
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(Category Category)
		{
			if (Category.Validate())
			{
				int result;

				result = CategoryManager.Save(Category);

				return Request.CreateResponse(HttpStatusCode.OK, result); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/Category/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/Category/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(Category Category)
		{
			try
			{
				int result;

				result = CategoryManager.Delete(Category);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}