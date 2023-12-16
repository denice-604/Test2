using System;
using System.Collections.Generic;

namespace Test22.Models
{
    /// <summary>
    /// класс для хранения строчки файла, сгенерирован entity framework
    /// </summary>
    public partial class Account 
    {
        public int Id { get; set; }
        public int? Bank { get; set; }
        public double? IncomingBalanceActive { get; set; }
        public double? IncomingBalancePassive { get; set; }
        public double? DebitTurnover { get; set; }
        public double? CreditTurnover { get; set; }
        public double? InitialBalanceActive { get; set; }
        public double? InitialBalancePassive { get; set; }

        /// <summary>
        /// внешний ключ для класса
        /// </summary>
        public int? ClassId { get; set; }

        /// <summary>
        /// связь сгенерированная entity fremework для определения какому классу принадлежит запись
        /// </summary>
        public virtual Class? Class { get; set; }
    }
}
