using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServicioGestionEstudiantes.Negocio;
using ServicioGestionEstudiantes.Negocio.DTOS;
using ServicioGestionEstudiantes.WebApi.Controllers;



namespace ServicioGestionEstudiantes.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ResponseController
    {
        private readonly AuthService _authService;


        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<DefaultResponse>> Login(LoginDto loginDto)
        {
            try
            {
                SetDataResponse(await _authService.Login(loginDto));
            }
            catch (Exception ex)
            {
                SetMsgErrorResponse(ex);
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
