using ConsultasAPI.Src.Modelos;
using ConsultasAPI.Src.Repositorios;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ConsultasAPI.Src.Controladores
{
    [ApiController]
    [Route("api/Pacientes")]
    [Produces("application/json")]
    public class PacienteControlador : ControllerBase
    {
        #region Atributos

        private readonly IPaciente _repositorio;

        #endregion

        #region Construtores

        public PacienteControlador(IPaciente repositorio)
        {
            _repositorio = repositorio;
        }

        #endregion


        #region Métodos

        /// <summary>
        /// Cadastrar Paciente
        /// </summary>
        /// <param name="paciente">Construtor para cadastrar paciente</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        /// POST /api/Pacientes
        /// {
        /// "nome": "Gustavo",
        /// "email": "gustavo@domain.com",
        /// "senha": "123456",
        /// "telefone": "99999999999"
        /// "documento": "99999999999"
        /// }
        /// </remarks>
        /// <response code="201">Retorna paciente cadastrado</response>
        /// <response code="401">Email já cadastrado</response>
        [HttpPost]
        public async Task<ActionResult> CadastrarPacienteAsync([FromBody] Paciente paciente)
        {
            try
            {
                await _repositorio.CadastrarPacienteAsync(paciente);
                return Created($"api/Pacientes/{paciente.Email}", paciente);
            }
            catch (Exception e)
            {
                return Unauthorized(e.Message);
            }
        }

        /// <summary>
        /// Pegar todos Pacientes
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna pacientes</response>
        /// <response code="204">Nenhum paciente cadastrado</response>
        [HttpGet]
        public async Task<ActionResult> PegarTodosPacientesAsync()
        {
            var lista = await _repositorio.PegarTodosPacientesAsync();
            if (lista.Count < 1) return NoContent();
            return Ok(lista);
        }

        #endregion
    }
}
