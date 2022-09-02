using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ConsultasAPI.Src.Modelos
{
    /// <summary>
    /// <para>Resumo: Classe responsavel por representar tb_pacientes no banco.</para>
    /// <para>Criado por: Gustavo</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 25/08/2022</para>
    /// </summary>
    [Table("tb_paciente")]
    public class Paciente
    {
        #region Atributos
        
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public string Nome { get; set; }
        
        public string Email { get; set; }
        
        public string Senha { get; set; }
        
        public string Telefone { get; set; }
        
        public string Documento { get; set; }

        [JsonIgnore, InverseProperty("Paciente")]
        public List<Consulta> MinhasConsultas { get; set; }

        #endregion
    }
}
