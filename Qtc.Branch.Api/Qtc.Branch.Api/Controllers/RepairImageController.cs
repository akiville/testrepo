using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Collections.Generic;
using Qtc.Branch.Bll;
using Qtc.Branch.BusinessEntities;
using System.Web;
using System.IO;

namespace Qtc.Branch.Api.Controllers
{
	public class RepairImageController : ApiController
	{
		// GET: api/RepairImage
		 public HttpResponseMessage Get(int id)
		{
			RepairImageCollection RepairImage_list = new RepairImageCollection();
			RepairImage RepairImage = new RepairImage();
			RepairImage = RepairImageManager.GetItem(id);
			RepairImage_list.Add(RepairImage);
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  RepairImage_list.Count, repairimages = RepairImage_list }, Configuration.Formatters.JsonFormatter);
		}

		// GET: api/RepairImage/5
		 public HttpResponseMessage Get(int request_repair_id, String type)
		{
			RepairImageCollection RepairImage_list = new RepairImageCollection();
			RepairImageCriteria RepairImage_criteria = new RepairImageCriteria();

            RepairImage_criteria.mRequestRepairId = request_repair_id;

            RepairImage_list = RepairImageManager.GetList(RepairImage_criteria);
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  RepairImage_list.Count, repairimages = RepairImage_list }, Configuration.Formatters.JsonFormatter);
		}

		// POST: api/RepairImage
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post()
		{
            var request = HttpContext.Current.Request;
            var id = request.Form["id"];
            var request_repair_id = request.Form["request_repair_id"];
            var name = request.Form["name"];
            var image_file_name = request.Form["image_file_name"];
            var description = request.Form["description"];
            var disable = request.Form["disable"];
            var record_id = request.Form["record_id"];
            var picture = request.Files["picture"];

            byte[] bytes;
            using (var stream = new MemoryStream())
            {
                picture.InputStream.CopyTo(stream);
                bytes = stream.ToArray();
            }

            RepairImage repair_image = new RepairImage();

            repair_image.mId = Int32.Parse(id);
            repair_image.mRequestRepairId = Int32.Parse(request_repair_id);
            repair_image.mName = name;
            repair_image.mDescription = description;
            repair_image.mImageFileName = image_file_name;
            repair_image.mDisable = Boolean.Parse(disable);
            repair_image.mRecordId = Int32.Parse(record_id);

            if (repair_image.Validate())
            {

                picture.SaveAs(HttpContext.Current.Server.MapPath("~/RepairImage/" + picture.FileName));

                int result;
                result = RepairImageManager.Save(repair_image);
                //return Request.CreateResponse(HttpStatusCode.OK, result); 
                return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request");
            }

            //if (repair_image.Validate())
            //{
            //    int result;

            //    result = RepairImageManager.Save(repair_image);

            //    //return Request.CreateResponse(HttpStatusCode.OK, result); 
            //    return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter);
            //}
            //else
            //{
            //    return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request");
            //}


        }

		// PUT: api/RepairImage/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/RepairImage/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(RepairImage RepairImage)
		{
			try
			{
				int result;

				result = RepairImageManager.Delete(RepairImage);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}