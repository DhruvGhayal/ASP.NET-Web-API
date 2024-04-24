using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaveImages.Model;

namespace SaveImages.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploaderController : ControllerBase
    {
        [HttpPost]
        [Route("UploadFile")]
        public Response UploadFile([FromForm] FileModel fileModel)
        {
            Response response = new Response();
            try
            {
                string path = Path.Combine(@"D:\FullStack\My_Work\.NET using SQL\SaveImageASPNetCore\SavedImages", fileModel.FileName);
                using (Stream stream = new FileStream(path, FileMode.Create))
                {
                    fileModel.file.CopyTo(stream);
                }
                response.StatusCode = 200;
                response.ErrorMessage = "Image created sucessfully";
            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.ErrorMessage = "Some error occured"+ ex.Message;
            }
            return response;
        }
    }
}
