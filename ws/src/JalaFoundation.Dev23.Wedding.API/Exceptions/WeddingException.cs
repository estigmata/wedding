using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace JalaFoundation.Dev23.Wedding.API.Exceptions
{
    public class WeddingException : Exception
    {
        public WeddingException(): base("Please input only letters")
        {
                
        }
    }
}