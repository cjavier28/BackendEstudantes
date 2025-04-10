using CapaNegocio;
using IntegracionNomina.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServicioGestionEstudiantes.Negocio.DTOS;



namespace SGEU.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudianteController : ResponseController
    {
        private readonly EstudianteService _estudianteService;
        public EstudianteController(EstudianteService estudianteService)
        {
            _estudianteService = estudianteService;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<DefaultResponse>> GetEstudiantes()
        {
            try
            {
                SetDataResponse(await _estudianteService.GetEstudiantes());
            }
            catch (Exception ex)
            {
                SetMsgErrorResponse(ex);
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<DefaultResponse>> GetEstudianteByPrograma(int id)
        {
            try
            {
                SetDataResponse(await _estudianteService.GetEstudiantesByPrograma(id));
            }
            catch (Exception ex)
            {
                SetMsgErrorResponse(ex);
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<DefaultResponse>> GetEstudiantesByMateria(int id)
        {
            try
            {
                SetDataResponse(await _estudianteService.GetEstudianteByMateria(id));
            }
            catch (Exception ex)
            {
                SetMsgErrorResponse(ex);
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<DefaultResponse>> CreateEstudiante(EstudianteDTO estudiante)
        {
            try
            {
                SetDataResponse(await _estudianteService.CreateEstudiante(estudiante));
            }
            catch (Exception ex)
            {
                SetMsgErrorResponse(ex);
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<DefaultResponse>> RegistrarMateriasEstudiante([FromBody] CreateMateriasEstudianteDTO request)
        {
            try
            {
                SetDataResponse(await _estudianteService.RegistrarMateriasEstudiante(request.IdEstudiante, request.IdMaterias));
            }
            catch (Exception ex)
            {
                SetMsgErrorResponse(ex);
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut("[action]/{idEstudiante}/{idPrograma}")]
        public async Task<ActionResult<DefaultResponse>> UpdateEstudiante(string idEstudiante, int idPrograma)
        {
            try
            {
                SetDataResponse(await _estudianteService.UpdateEstudiante(idEstudiante, idPrograma));
            }
            catch (Exception ex)
            {
                SetMsgErrorResponse(ex);
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("[action]")]
        public async Task<ActionResult<DefaultResponse>> DeleteMateriaEstudiante([FromQuery] string idEstudiante, [FromQuery] int idMateria)
        {
            try
            {
                var result = await _estudianteService.DeleteMateriaEstudiante(idEstudiante, idMateria);

                return Ok(new DefaultResponse { Data = result });
            }
            catch (Exception ex)
            {
                SetMsgErrorResponse(ex);
                return BadRequest(response);
            }
        }

    }
}
