using ConsultasAPI.Src.Contextos;
using ConsultasAPI.Src.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultasAPI.Src.Repositorios.Implementacoes
{
    /// <summary>
    /// <para>Resumo: Classe responsavel por implementar IMedico</para>
    /// <para>Criado por: Gustavo</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 25/08/2022</para>
    /// </summary>>
    public class MedicoRepositorio : IMedico
    {
        #region Atributos
        
        private readonly ConsultasMedicasContexto _contexto;

        #endregion

        #region Construtores

        public MedicoRepositorio(ConsultasMedicasContexto contexto)
        {
            _contexto = contexto;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// <para>Resumo: Método assíncrono para cadastrar novo médico</para>
        /// </summary>
        /// <param name="medico">Construtor para cadastrar medico</param>
        /// <exception cref="Exception">E-mail tem que ser nulo</exception>
        public async Task NovoMedicoAsync(Medico medico)
        {
            if (EmailExiste(medico.Email)) throw new Exception("E-mail já cadastrado");
            
            await _contexto.Medicos.AddAsync(
                new Medico
                {
                    Nome = medico.Nome,
                    Email = medico.Email,
                    Senha = medico.Senha
                });
            await _contexto.SaveChangesAsync();

            // Funções auxiliares
            bool EmailExiste(string email)
            {
                var aux = _contexto.Medicos.FirstOrDefault(m => m.Email == email);
                return aux != null;
            }
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar todos os médicos</para>
        /// </summary>
        public async Task<List<Medico>> PegarTodosMedicosAsync()
        {
            return await _contexto.Medicos.ToListAsync();
        }

        #endregion
    }
}
