using ConsultasAPI.Src.Modelos;
using ConsultasAPI.Src.Repositorios;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ConsultasAPI.Src.Controladores
{
    [ApiController]
    [Route("api/Medicos")]
    [Produces("application/json")]
    public class MedicoControlador : ControllerBase
    {
        #region Atributos

        private readonly IMedico _repositorio;

        #endregion

        #region Construtores

        public MedicoControlador(IMedico repositorio)
        {
            _repositorio = repositorio;
        }

        #endregion


        #region Métodos

        /// <summary>
        /// Cadastrar novo Medico
        /// </summary>
        /// <param name="medico">Construtor para cadastrar medico</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        /// POST /api/Medicos
        /// {
        /// "nome": "Gustavo",
        /// "email": "gustavo@domain.com",
        /// "senha": "123456",
        /// }
        /// </remarks>
        /// <response code="201">Retorna medico cadastrado</response>
        /// <response code="401">E-mail já cadastrado</response>
        [HttpPost]
        public async Task<ActionResult> NovoMedicoAsync([FromBody] Medico medico)
        {
            try
            {
                await _repositorio.NovoMedicoAsync(medico);
                return Created($"api/Medicos/{medico.Email}", medico);
            }
            catch (Exception e)
            {
                return Unauthorized(e.Message);
            }
        }

        /// <summary>
        /// Pegar todos os Medicos
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna medicos</response>
        /// <response code="204">Nenhum medico cadastrado</response>
        [HttpGet]
        public async Task<ActionResult> PegarTodosMedicosAsync()
        {
            var lista = await _repositorio.PegarTodosMedicosAsync();
            if (lista.Count < 1) return NoContent();
            return Ok(lista);
        }

        #endregion
    }
}
