﻿
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServicioGestionEstudiantes.Negocio;
using ServicioGestionEstudiantes.WebApi.Controllers;


namespace ServicioGestionEstudiantes.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramaController : ResponseController
    {
        private readonly ProgramaService _programaService;


        public ProgramaController(ProgramaService programaService)
        {
            _programaService = programaService;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<DefaultResponse>> GetProgramas()
        {
            try
            {
                SetDataResponse(await _programaService.GetProgramas());
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
