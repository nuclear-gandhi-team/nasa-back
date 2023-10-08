using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nasa.BLL.Exceptions
{
    public class CurrentFiresApiException : Exception
    {
        public CurrentFiresApiException() : base("Something went wrong while api call to Current Fires API")
        {
        }
    }
}
