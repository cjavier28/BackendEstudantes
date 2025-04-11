using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServicioGestionEstudiantes.Negocio;
using ServicioGestionEstudiantes.WebApi.Controllers;


namespace ServicioGestionEstudiantes.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MateriaController : ResponseController
    {
        private readonly MateriaService _materiaService;


        public MateriaController(MateriaService materiaService)
        {
            _materiaService = materiaService;
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<DefaultResponse>> GetMateriasByIdPrograma(int id)
        {
            try
            {
                SetDataResponse(await _materiaService.GetMateriasPrograma(id));
            }
            catch (Exception ex)
            {
                SetMsgErrorResponse(ex);
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<DefaultResponse>> GetMateriaByEstudiante(string id)
        {
            try
            {
                SetDataResponse(await _materiaService.GetMateriaByEstudiante(id));
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
