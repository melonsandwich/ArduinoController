using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoController.Utilities.Exceptions
{
    public class IncorrectPinValueException : Exception
    {
        public IncorrectPinValueException(string message) : base(message)
        {
            
        }
    }
}
