using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsultasAPI.Src.Modelos
{
    /// <summary>
    /// <para>Resumo: Classe responsavel por representar tb_consultas no banco.</para>
    /// <para>Criado por: Gustavo</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 25/08/2022</para>
    /// </summary>
    [Table("tb_consultas")]
    public class Consulta
    {
        #region Atributos
        
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
            
        public string Data { get; set; }
        
        public string Hora { get; set; }

        [ForeignKey("fk_medico")]
        public Medico Medico { get; set; }
        
        [ForeignKey("fk_paciente")]
        public Paciente Paciente { get; set; }

        #endregion
    }
}
