using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MvcWeb.MultipartRequest;
using System;
using System.IO;
using System.Threading.Tasks;
using TestAPI.MultipartRequest;

namespace TestAPI.Controllers
{
    [Route("api/test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpPost("posttest")]
        public ActionResult<string> PostTest()
        {
            var sr = new StreamReader(Request.Body);
            var stream = sr.ReadToEndAsync().Result;

            return "POST请求测试:" + stream;
        }

        [HttpGet("gettest")]
        public ActionResult<string> GetTest()
        {
            return "GET请求测试";
        }

        [HttpGet("gettest1")]
        public ActionResult<string> GetTest1()
        {
            return "GET请求测试";
        }

        #region 使用文件流上传
        [HttpPost("unloadfile")]
        [DisableFormValueModelBinding]
        public async Task<IActionResult> UploadFile()
        {
            // 获取程序运行根目录下的 LoadFile 目录
            string filepath = AppContext.BaseDirectory + @"LoadFile";
            MyFormValueProvider formModel;
            formModel = await Request.StreamFiles(filepath);


            var viewModel = new { 
                code=200,
                url=@"http://127.0.0.1:2234/"+ formModel.FileName
            };

            var bindingSuccessful = await TryUpdateModelAsync(viewModel, prefix: "",
                valueProvider: formModel);

            if (!bindingSuccessful)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
            }

            return Ok(viewModel);


        }
        #endregion
    }
}
