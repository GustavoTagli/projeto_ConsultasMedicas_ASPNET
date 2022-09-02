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
    /// <para>Resumo: Classe responsavel por implementar IPaciente</para>
    /// <para>Criado por: Gustavo</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 25/08/2022</para>
    public class PacienteRepositorio : IPaciente
    {
        #region Atributos
        
        private readonly ConsultasMedicasContexto _contexto;

        #endregion
        

        #region Construtures

        public PacienteRepositorio(ConsultasMedicasContexto contexto)
        {
            _contexto = contexto;
        }

        #endregion


        #region Métodos

        /// <summary>
        /// <para>Resumo: Método assíncrono para cadastrar paciente</para>
        /// </summary>
        /// <param name="paciente">Construtor para cadastrar paciente</param>
        /// <exception cref="Exception">E-mail tem que ser nulo</exception>
        public async Task CadastrarPacienteAsync(Paciente paciente)
        {
            if (EmailExiste(paciente.Email)) throw new Exception("E-mail já cadastrado");

            await _contexto.Pacientes.AddAsync(new Paciente
            {
                Nome = paciente.Nome,
                Email = paciente.Email,
                Senha = paciente.Senha,
                Telefone = paciente.Telefone,
                Documento = paciente.Documento
            });
            await _contexto.SaveChangesAsync();

            // Funções auxiliares
            bool EmailExiste(string email)
            {
                var aux = _contexto.Pacientes.FirstOrDefault(m => m.Email == email);
                return aux != null;
            }
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar todos os pacientes</para>
        /// </summary>
        public async Task<List<Paciente>> PegarTodosPacientesAsync()
        {
            return await _contexto.Pacientes.ToListAsync();
        }

        #endregion
    }
}
