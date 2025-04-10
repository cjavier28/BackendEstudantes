using Azure;
using CapaNegocio;
using IntegracionNomina.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServicioGestionEstudiantes.Negocio.DTOS;



namespace SGEU.WebApi.Controllers
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
        public async Task<ActionResult<DefaultResponse>> Login(LoginDTO loginDto)
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
