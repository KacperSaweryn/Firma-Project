using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.Helpers
{
    public class MessengerMessage<SenderType, ResponseType, ArgumentType>
    {
        public SenderType Sender { get; set; }
        public ResponseType Response { get; set; }
        public ArgumentType Argument { get; set; }


    }
}
