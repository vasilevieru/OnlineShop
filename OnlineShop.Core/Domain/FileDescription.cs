using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Core.Domain
{
    public class FileDescription : BaseEntity
    {
        /// <summary>
        /// The original name of uploaded file
        /// </summary>
        public string OriginalName { get; set; }

        /// <summary>
        /// The full name of file
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// The file type, downloadable or no. (immage/download) 
        /// </summary>
        public string FileType { get; set; }

        /// <summary>
        /// The file size
        /// </summary>
        public long Size { get; set; }
    }
}
