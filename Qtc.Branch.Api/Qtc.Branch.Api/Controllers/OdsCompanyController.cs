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
	public class OdsCompanyController : ApiController
	{
		// GET: api/OdsCompany
		public HttpResponseMessage Get(int id)
		{
            OdsCompanyCollection OdsCompany_list = new OdsCompanyCollection();
            OdsCompany odsCompany = new OdsCompany();

            odsCompany = OdsCompanyManager.GetItem(id);
            OdsCompany_list.Add(odsCompany);

            return Request.CreateResponse(HttpStatusCode.OK, new { count = OdsCompany_list.Count, odscompanys = OdsCompany_list }, Configuration.Formatters.JsonFormatter);

        }

        // GET: api/OdsCompany/5
        public HttpResponseMessage Get()
		{
			
			OdsCompanyCollection OdsCompany_list = new OdsCompanyCollection();
			OdsCompanyCriteria OdsCompany_criteria = new OdsCompanyCriteria();

			OdsCompany_list = OdsCompanyManager.GetList(OdsCompany_criteria);
				
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  OdsCompany_list.Count, odscompanys = OdsCompany_list }, Configuration.Formatters.JsonFormatter);
				
				
		}

		// POST: api/OdsCompany
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(OdsCompany OdsCompany)
		{
			if (OdsCompany.Validate())
			{
				int result;

				result = OdsCompanyManager.Save(OdsCompany);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/OdsCompany/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/OdsCompany/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(OdsCompany OdsCompany)
		{
			try
			{
				int result;

				result = OdsCompanyManager.Delete(OdsCompany);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}