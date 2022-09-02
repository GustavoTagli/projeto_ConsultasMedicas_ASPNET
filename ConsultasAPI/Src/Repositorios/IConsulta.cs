using ConsultasAPI.Src.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsultasAPI.Src.Repositorios
{
    /// <summary>
    /// <para>Resumo: Responsavel por representar ações de CRUD de consulta</para>
    /// <para>Criado por: Gustavo</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 25/08/2022</para>
    /// </summary>
    public interface IConsulta
    {
        Task AgendarConsultaAsync(Consulta consulta);
        Task<List<Consulta>> PegarConsultasMarcadasMedicoAsync(int idMedico);
        Task<List<Consulta>> PegarConsultasMarcadasPacienteAsync(int idPaciente);
    }
}
