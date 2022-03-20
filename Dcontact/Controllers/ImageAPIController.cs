using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Dcontact.Controllers
{
    public class ImageAPIController : ApiController
    {
        [Route("api/ImageAPI/UploadFiles")]
        [HttpPost]
        public HttpResponseMessage UploadFiles()
        {
            //Create the Directory.
            string path = HttpContext.Current.Server.MapPath("~/Uploads/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //Fetch the File.
            HttpPostedFile postedFile = HttpContext.Current.Request.Files[0];

            //Fetch the File Name.
            string fileName = Util.UUID.getUUID() + Path.GetFileName(".png");

            //Save the File.
            postedFile.SaveAs(path + fileName);

            //Send OK Response to Client.
            return Request.CreateResponse(HttpStatusCode.OK, $"/Uploads/{fileName}");
        }


        //[HttpPost]
        //[Route("api/ImageAPI/GetFiles")]
        //public HttpResponseMessage GetFiles()
        //{
        //    string path = HttpContext.Current.Server.MapPath("~/Uploads/");

        //    //Fetch the Image Files.
        //    List<string> images = new List<string>();

        //    //Extract only the File Names to save data.
        //    foreach (string file in Directory.GetFiles(path))
        //    {
        //        images.Add(Path.GetFileName(file));
        //    }

        //    return Request.CreateResponse(HttpStatusCode.OK, images);
        //}
    }
  
}
