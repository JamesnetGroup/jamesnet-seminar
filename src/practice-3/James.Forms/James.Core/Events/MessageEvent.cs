using James.Core.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace James.Core.Events
{
    public class MessageEvent : PubSubEvent<MessageModel>
    {
    }
}
