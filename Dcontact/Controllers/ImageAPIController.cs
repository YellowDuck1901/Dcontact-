using NBitcoin.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Dcontact.Controllers
{
    public class ImageAPIController : ApiController
    {
        [HttpPost]
        [Route("api/ImageAPI/SaveFile")]
        public HttpResponseMessage SaveFile()
        {
            string path = HttpContext.Current.Server.MapPath("~/Uploads/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            //Create HTTP Response.
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

            //Check if Request contains File.
            //if (HttpContext.Current.Request.Files.Count == 0)
            //{
            //    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            //}
            try
            {
                HttpResponseMessage result = null;
                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count > 0)
                {
                    var docfiles = new List<string>();
                    foreach (string file in httpRequest.Files)
                    {
                        var postedFile = httpRequest.Files[file];
                        if (postedFile.ContentLength == 0)
                        {
                            continue;
                        }
                        postedFile.SaveAs(path);
                        docfiles.Add(path);
                    }
                    result = Request.CreateResponse(HttpStatusCode.Created, docfiles);
                }
                else
                {
                    result = Request.CreateResponse(HttpStatusCode.BadRequest);
                }
                return result;

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                throw;
            }

            //Read the File data from Request.Form collection.
            //HttpPostedFile postedFile = HttpContext.Current.Request.Files[0];
            //string fileName = Path.GetFileName(postedFile.FileName);
            //postedFile.SaveAs(path + fileName + ".jpg");
            //return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        //[HttpPost]
        //[Route("api/ImageAPI/GetFiles")]
        //public HttpResponseMessage GetFiles()
        //{
        //    FilesEntities entities = new FilesEntities();
        //    var files = from file in entities.tblFiles
        //                select new { id = file.id, Name = file.Name };
        //    return Request.CreateResponse(HttpStatusCode.OK, files);
        //}

        //[HttpGet]
        //[Route("api/ImageAPI/GetFile")]
        //public HttpResponseMessage GetFile(int fileId)
        //{
        //    //Create HTTP Response.
        //    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

        //    //Fetch the File data from Database.
        //    FilesEntities entities = new FilesEntities();
        //    tblFile file = entities.tblFiles.ToList().Find(p => p.id == fileId);

        //    //Set the Response Content.
        //    response.Content = new ByteArrayContent(file.Data);

        //    //Set the Response Content Length.
        //    response.Content.Headers.ContentLength = file.Data.LongLength;

        //    //Set the Content Disposition Header Value and FileName.
        //    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
        //    response.Content.Headers.ContentDisposition.FileName = file.Name;

        //    //Set the File Content Type.
        //    response.Content.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
        //    return response;
        //}
    }
}
