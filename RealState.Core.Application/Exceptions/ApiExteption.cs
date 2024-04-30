

using System.Globalization;

namespace RealState.Core.Application.Exceptions
{
    public class ApiExteption : Exception
    {
        public int ErrorCode { get; set; }

        public ApiExteption() : base()
        {
            
        }

        public ApiExteption(string message) : base(message)
        {

        }


        public ApiExteption(string message , int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
        //ESTO ES PARA QUE EL MENSAJE COJA EL IDIOMA EN EL QUE ESTAS TRABAJANDO
        public ApiExteption(string message, params object[] objects) : base(string.Format(CultureInfo.CurrentCulture,message,objects))
        {
           
        }
    }
}
