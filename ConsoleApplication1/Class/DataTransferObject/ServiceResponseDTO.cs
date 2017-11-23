using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    /// <summary>
    /// Servise gönderilen istek sonucunda oluşan cevabın bilgilerini içerir.
    /// </summary>
    /// <typeparam name="T">Servisten dönen cevabın içerdiği veri bilgilerini tutan tiptir</typeparam>
    public class ServiceResponseDTO<T>
    {
        /// <summary>
        /// Oluşan cevabın verileri.
        /// </summary>
        public T Data { get; private set; }

        /// <summary>
        /// Oluşan cevabın durumu.
        /// </summary>
        public EServiceResponseStatus Status { get; private set; }

        /// <summary>
        /// Oluşan cevaba eklenen mesaj.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Servisten gelen cevabın durumu başarılıysa, yeni obje yaratır.
        ///.</summary>
        /// <param name="data">Servisten gelen cevabı içeren parametredir.</param>
        public ServiceResponseDTO(T data)
        {
            this.Data = data;
            this.Status = EServiceResponseStatus.Success;
        }

        /// <summary>
        /// Servisten gelen cevabın durumu uyarı içeriyorsa, yeni obje yaratır.
        /// </summary>
        /// <param name="data">Servisten gelen cevabı içeren parametredir.</param>
        /// <param name="warningMessage">Uyarı mesajını içeren parametredir.</param>
        /// 
        public ServiceResponseDTO(T data, string warningMessage)
        {
            this.Data = data;
            this.Status = EServiceResponseStatus.Warning;
            this.Message = warningMessage;
        }

        /// <summary>
        /// Servisten gelen cevabın durumu hata içeriyorsa, yeni obje yaratır.
        /// </summary>
        /// <param name="errorMessage">Hata mesajını içeren parametredir</param>
        public ServiceResponseDTO(string errorMessage)
        {
            this.Status = EServiceResponseStatus.Error;
            this.Message = errorMessage;
        }
    }
}
