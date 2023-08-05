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
	public class ContactController : ApiController
	{
		// GET: api/Contact
		public HttpResponseMessage Get()
		{
            try
            {
                ContactCollection Contact_list = new ContactCollection();
                ContactCriteria Contact_criteria = new ContactCriteria();

                Contact_list = ContactManager.GetList(Contact_criteria);
                if (Contact_list.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { count = Contact_list.Count, contacts = Contact_list }, Configuration.Formatters.JsonFormatter);
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

		// GET: api/Contact/5
		 public HttpResponseMessage Get(String group_name)
		{
			try
			{
				ContactCollection Contact_list = new ContactCollection();
				ContactCriteria Contact_criteria = new ContactCriteria();

				Contact_list = ContactManager.GetList(Contact_criteria);
				if (Contact_list.Count > 0 ) 
				{
                    return Request.CreateResponse(HttpStatusCode.OK, new { count = Contact_list.Count, contacts = Contact_list }, Configuration.Formatters.JsonFormatter);
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

		// POST: api/Contact
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(Contact Contact)
		{
			if (Contact.Validate())
			{
				int result;

				result = ContactManager.Save(Contact);

                return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter);
            }
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/Contact/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/Contact/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(Contact Contact)
		{
			try
			{
				int result;

				result = ContactManager.Delete(Contact);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}