using ConsultasAPI.Src.Modelos;
using ConsultasAPI.Src.Repositorios;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ConsultasAPI.Src.Controladores
{
    [ApiController]
    [Route("api/Consultas")]
    [Produces("application/json")]
    public class ConsultaControlador : ControllerBase
    {
        #region Atributos

        private readonly IConsulta _repositorio;
        
        #endregion


        #region Construtores
        
        public ConsultaControlador(IConsulta repositorio)
        {
            _repositorio = repositorio;
        }

        #endregion


        #region Métodos

        /// <summary>
        /// Marcar Consulta
        /// </summary>
        /// <param name="consulta">Construtor para criar consulta</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        /// POST /api/Consultas
        /// {
        /// "data": "01/01",
        /// "hora": "12:00",
        /// "medico": {
        ///      "id": 1
        ///     },
        /// "paciente": {
        ///      "id": 1
        ///     }
        /// }
        /// </remarks>
        /// <response code="201">Retorna a postagem criada</response>
        /// <response code="401">Horário não disponível</response>
        [HttpPost]
        public async Task<ActionResult> AgendarConsultaAsync([FromBody] Consulta consulta)
        {
            try
            {
                await _repositorio.AgendarConsultaAsync(consulta);
                return Created($"api/Consultas/{consulta.Id}", consulta);
            }
            catch (Exception e)
            {
                return Unauthorized(e.Message);
            }
        }

        /// <summary>
        /// Pegar Consultas do Medico pelo Id
        /// </summary>
        /// <param name="idMedico">Id do medico</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna as consultas marcadas</response>
        /// <response code="204">Nenhuma consulta marcada</response>
        [HttpGet("Medico/{idMedico}")]
        public async Task<ActionResult> PegarConsultasMarcadasMedicoAsync([FromRoute] int idMedico)
        {
            var consultas = await _repositorio.PegarConsultasMarcadasMedicoAsync(idMedico);
            if (consultas.Count < 1) return NoContent();
            return Ok(consultas);
        }

        /// <summary>
        /// Pegar Consultas do Paciente pelo Id
        /// </summary>
        /// <param name="idPaciente">Id do paciente</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna as consultas marcadas</response>
        /// <response code="204">Nenhuma consulta marcada</response>
        [HttpGet("Paciente/{idPaciente}")]
        public async Task<ActionResult> PegarConsultasMarcadasPacienteAsync([FromRoute] int idPaciente)
        {
            var consultas = await _repositorio.PegarConsultasMarcadasPacienteAsync(idPaciente);
            if (consultas.Count < 1) return NoContent();
            return Ok(consultas);
        }

        #endregion
    }
}
