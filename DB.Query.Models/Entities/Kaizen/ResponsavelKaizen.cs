//------------------------------------------------------------------------------
// <auto-generated>
//     O código foi gerado por uma ferramenta.
//     Versão de Tempo de Execução:4.0.30319.42000
//
//     As alterações ao arquivo poderão causar comportamento incorreto e serão perdidas se
//     o código for gerado novamente.
// </auto-generated>
//------------------------------------------------------------------------------

using DB.Query.Models.DataAnnotations;


namespace DB.Query.Models.Entities.Kaizen
{
    
    [Table("Responsavel_Kaizen")]
    public class ResponsavelKaizen : Kaizen
    {
        /// <summary>
        /// Propiedade mapeada para a definição de Codigo
        /// </summary>
        [PrimaryKey(Identity=true)]
        [RequiredColumn("Codigo")]
        public int Codigo { get; set; }
        /// <summary>
        /// Propiedade mapeada para a definição de Codigo_Usuario
        /// </summary>
        [RequiredColumn("Codigo_Usuario")]
        [ColumnAttribute("Codigo_Usuario")]
        public int Codigo_Usuario { get; set; }
        /// <summary>
        /// Propiedade mapeada para a definição de Codigo_Area
        /// </summary>
        [RequiredColumn("Codigo_Area")]
        [ColumnAttribute("Codigo_Area")]
        public int Codigo_Area { get; set; }
        /// <summary>
        /// Propiedade mapeada para a definição de Codigo_Depto
        /// </summary>
        [RequiredColumn("Codigo_Depto")]
        [ColumnAttribute("Codigo_Depto")]
        public int Codigo_Depto { get; set; }
        /// <summary>
        /// Propiedade mapeada para a definição de Codigo_Setor
        /// </summary>
        [ColumnAttribute("Codigo_Setor")]
        public System.Nullable<int> Codigo_Setor { get; set; }
        /// <summary>
        /// Propiedade mapeada para a definição de Codigo_Processo
        /// </summary>
        [ColumnAttribute("Codigo_Processo")]
        public System.Nullable<int> Codigo_Processo { get; set; }
        /// <summary>
        /// Propiedade mapeada para a definição de Codigo_Workstation
        /// </summary>
        [ColumnAttribute("Codigo_Workstation")]
        public System.Nullable<int> Codigo_Workstation { get; set; }
    }
}
