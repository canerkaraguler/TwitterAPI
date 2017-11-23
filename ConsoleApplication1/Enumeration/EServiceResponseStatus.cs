using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public enum EServiceResponseStatus
    {
        /// <summary>
        /// İşlem başarılı
        /// </summary>
        Success = 1,
        /// <summary>
        /// İşlem yapıldı ama uyarı var
        /// </summary>
        Warning = 2,
        /// <summary>
        /// İşlem başarısız
        /// </summary>
        Error = 3
    }
}
