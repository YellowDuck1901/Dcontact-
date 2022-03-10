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
        //[Route("api/ImageAPI/UploadFiles")]
        //[HttpPost]
        //public async Task<HttpResponseMessage> UploadFiles()
        //{

        //    if (!Request.Content.IsMimeMultipartContent())
        //    {
        //        throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
        //    }
        //    string root = HttpContext.Current.Server.MapPath("~/Uploads");
        //    if (!Directory.Exists(root))
        //    {
        //        Directory.CreateDirectory(root);
        //    }
        //    var provider = new MultipartFormDataStreamProvider(root);
        //    try
        //    {
        //        // Read the form data.
        //        await Request.Content.ReadAsMultipartAsync(provider);

        //        // This illustrates how to get the file names.
        //        string success = "null";
        //        foreach (MultipartFileData file in provider.FileData)
        //        {
        //            success = "1";
        //            Trace.WriteLine(file.Headers.ContentDisposition.FileName);
        //            success = file.Headers.ContentDisposition.FileName;
        //            Trace.WriteLine("Server file path: " + file.LocalFileName);
        //        }
        //        return Request.CreateResponse(HttpStatusCode.OK, success);
        //    }
        //    catch (System.Exception e)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
        //    }
        //    //Send OK Response to Client.
        //}


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
            string fileName = Path.GetFileName("avt_check.png");

            //Save the File.
            postedFile.SaveAs(path + fileName);

            //Send OK Response to Client.
            return Request.CreateResponse(HttpStatusCode.OK, fileName);
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
