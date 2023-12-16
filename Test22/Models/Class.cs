using System;
using System.Collections.Generic;

namespace Test22.Models
{
    /// <summary>
    /// Класс для хранения названия классов
    /// </summary>
    public partial class Class
    {
        public Class()
        {
            Accounts = new HashSet<Account>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        /// <summary>
        /// поле для определения какому файлу принадлежит класс
        /// </summary>
        public int? FileId { get; set; }

        public virtual File? File { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
