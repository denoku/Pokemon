using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.Extensions.Logging;
using Sabio.Models.Domain;
using Sabio.Services;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;
using System;
using System.Collections.Generic;

namespace Sabio.Web.Api.Controllers
{
    [Route("api/lookups")]
    [ApiController]
    public class LookUpApiController : BaseApiController
    {
        private ILookUpService _service = null;
        private IAuthenticationService<int> _authService = null;

        public LookUpApiController(ILookUpService service, 
            IAuthenticationService<int> authService
            ,ILogger<LookUpApiController> logger) : base(logger)
        {
            _service = service;
            _authService = authService;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<ItemsResponse<LookUp>> GetLookUp(string tablename)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                List<LookUp> list = _service.GetLookUp(tablename);
                if (list == null)
                {
                    code = 404;
                    response = new ErrorResponse("Not found");
                }
                else
                {
                    response = new ItemsResponse<LookUp> { Items = list };
                }
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
            }
            return StatusCode(code, response);
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult<Dictionary<string, List<LookUp>>> GetTypes(string[] tablenames)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                Dictionary<string, List<LookUp>> lookup = _service.GetTypes(tablenames);

                if(lookup == null)
                {
                    code = 404;
                    response = new ErrorResponse("Not Found");
                }
                else
                {
                    response = new ItemResponse<Dictionary<string, List<LookUp>>> { Item = lookup };
                }
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
            }
            return StatusCode(code, response) ;
        }
    }
}
