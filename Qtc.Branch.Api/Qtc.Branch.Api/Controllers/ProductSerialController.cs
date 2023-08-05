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
	public class ProductSerialController : ApiController
	{
		// GET: api/ProductSerial
		 public HttpResponseMessage Get(int id)
		{
			ProductSerialCollection ProductSerial_list = new ProductSerialCollection();
			ProductSerial ProductSerial = new ProductSerial();
			ProductSerial = ProductSerialManager.GetItem(id);
			ProductSerial_list.Add(ProductSerial);
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  ProductSerial_list.Count, productserials = ProductSerial_list }, Configuration.Formatters.JsonFormatter);
		}

		// GET: api/ProductSerial/5
		 public HttpResponseMessage Get()
		{
			ProductSerialCollection ProductSerial_list = new ProductSerialCollection();
			ProductSerialCriteria ProductSerial_criteria = new ProductSerialCriteria();

			ProductSerial_list = ProductSerialManager.GetList(ProductSerial_criteria);
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  ProductSerial_list.Count, productserials = ProductSerial_list }, Configuration.Formatters.JsonFormatter);
		}

		// POST: api/ProductSerial
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(ProductSerial ProductSerial)
		{
			if (ProductSerial.Validate())
			{
				int result;

				result = ProductSerialManager.Save(ProductSerial);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/ProductSerial/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/ProductSerial/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(ProductSerial ProductSerial)
		{
			try
			{
				int result;

				result = ProductSerialManager.Delete(ProductSerial);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}