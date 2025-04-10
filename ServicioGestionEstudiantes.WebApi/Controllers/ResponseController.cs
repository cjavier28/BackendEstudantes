using CapaNegocio;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace IntegracionNomina.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponseController : ControllerBase
    {
        public DefaultResponse response = new();

        [NonAction]
        public void SetDataResponse(object data)
        {
            response.Data = data;

            if (data is IEnumerable enumerableData && !(data is string))
            {
                response.TotalRecords = enumerableData.Cast<object>().Count();
            }
            else
            {
                response.TotalRecords = 0;
            }
        }

        [NonAction]
        public void SetMsgErrorResponse(Exception ex)
        {
            response.Message = GetExceptionMessage(ex);
            response.Success = false;
        }

        [NonAction]
        public void SetMessageResponse(string msg)
        {
            response.Message = msg;
        }

        [NonAction]
        public void SetErrorResponse(bool success)
        {
            response.Success = success;
        }

        [NonAction]
        public string GetExceptionMessage(Exception ex)
        {
            return ex.Message + $"{(ex.InnerException != null ? " => " + GetExceptionMessage(ex.InnerException) : "")}";
        }
    }
}
