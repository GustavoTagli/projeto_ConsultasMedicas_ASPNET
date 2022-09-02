using ConsultasAPI.Src.Modelos;
using Microsoft.EntityFrameworkCore;

namespace ConsultasAPI.Src.Contextos
{
    /// <summary>
    /// <para>Resumo: Classe contexto, responsável por carregar contexto e definir dBSets</para>
    /// <para>Criado por: Gustavo</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 25/08/2022</para>
    /// </summary>
    public class ConsultasMedicasContexto : DbContext
    {
        #region Atributos

        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Consulta> Consultas { get; set; }

        #endregion

        #region Construtores

        public ConsultasMedicasContexto(DbContextOptions<ConsultasMedicasContexto> opt) : base(opt)
        {

        }

        #endregion
    }
}
