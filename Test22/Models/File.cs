using System;
using System.Collections.Generic;

namespace Test22.Models
{
    /// <summary>
    /// класс для хранения файлов которые были считаны
    /// </summary>
    public partial class File
    {
        public File()
        {
            Classes = new HashSet<Class>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        /// <summary>
        /// свьзь файла с каждым его классом
        /// </summary>

        public virtual ICollection<Class> Classes { get; set; }
    }
}
