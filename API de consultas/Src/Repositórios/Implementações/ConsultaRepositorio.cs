using ConsultasAPI.Src.Contextos;
using ConsultasAPI.Src.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultasAPI.Src.Repositorios.Implementacoes
{
    /// <summary>
    /// <para>Resumo: Classe responsavel por implementar IConsulta</para>
    /// <para>Criado por: Gustavo</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 25/08/2022</para>
    /// </summary>
    public class ConsultaRepositorio : IConsulta
    {
        #region Atributos

        private readonly ConsultasMedicasContexto _contexto;

        #endregion


        #region Construtores

        public ConsultaRepositorio(ConsultasMedicasContexto contexto)
        {
            _contexto = contexto;
        }

        #endregion


        #region Métodos

        /// <summary>
        /// <para>Resumo: Método assíncrono para marcar consultas</para>
        /// </summary>
        /// <param name="consulta">Construtor para agendar consulta</param>
        /// <exception cref="Exception">Id não pode ser nulo</exception>
        /// <exception cref="Exception">Horário da consulta não pode ser não nulo</exception>
        public async Task AgendarConsultaAsync(Consulta consulta)
        {
            if (!MedicoExiste(consulta.Medico.Id))
                throw new Exception("Id do médico não encontrado");
            if (!PacienteExiste(consulta.Paciente.Id))
                throw new Exception("Id do paciente não encontrado");
            if (ConsultaExisteMedico(consulta))
                throw new Exception("Médico já possui uma consulta marcada para este horario");
            if(ConsultaExistePaciente(consulta))
                throw new Exception("Paciente já possui uma consulta marcada para este horario");

            await _contexto.Consultas.AddAsync(new Consulta
            {
                Data = DateTime.Parse(consulta.Data).ToString("dd/MM/yyyy"),
                Hora = DateTime.Parse(consulta.Hora).ToString("HH\\hmm"),
                Medico = await _contexto.Medicos.FirstOrDefaultAsync(m => m.Id == consulta.Medico.Id),
                Paciente = await _contexto.Pacientes.FirstOrDefaultAsync(p => p.Id == consulta.Paciente.Id)
            });
            await _contexto.SaveChangesAsync();

            // Funções auxilar
            bool MedicoExiste(int id)
            {
                var aux = _contexto.Medicos.FirstOrDefault(m => m.Id == id);
                return aux != null;
            }

            bool PacienteExiste(int id)
            {
                var aux = _contexto.Pacientes.FirstOrDefault(p => p.Id == id);
                return aux != null;
            }

            bool ConsultaExisteMedico(Consulta consulta)
            {
                var aux = _contexto.Consultas.FirstOrDefault(
                    c => c.Data == DateTime.Parse(consulta.Data).ToString("dd/MM/yyyy")
                    && c.Hora == DateTime.Parse(consulta.Hora).ToString("HH\\hmm")
                    && c.Medico.Id == consulta.Medico.Id);
                return aux != null;
            }
            
            bool ConsultaExistePaciente(Consulta consulta)
            {
                var aux = _contexto.Consultas.FirstOrDefault(
                    c => c.Data == DateTime.Parse(consulta.Data).ToString("dd/MM/yyyy")
                    && c.Hora == DateTime.Parse(consulta.Hora).ToString("HH\\hmm")
                    && c.Paciente.Id == consulta.Paciente.Id);
                return aux != null;
            }
        }


        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar constulas marcadas com o médico</para>
        /// </summary>
        /// <param name="idMedico">Id do medico</param>
        public async Task<List<Consulta>> PegarConsultasMarcadasMedicoAsync(int idMedico)
        {
            return await _contexto.Consultas
                .Include(c => c.Medico)
                .Include(c => c.Paciente)
                .Where(c => c.Medico.Id == idMedico)
                .ToListAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar consultas do paciente</para>
        /// </summary>
        /// <param name="idPaciente">Id do paciente</param>
        public async Task<List<Consulta>> PegarConsultasMarcadasPacienteAsync(int idPaciente)
        {
            return await _contexto.Consultas
                .Include(c => c.Medico)
                .Include(c => c.Paciente)
                .Where(c => c.Paciente.Id == idPaciente)
                .ToListAsync();
        }

        #endregion
    }
}
