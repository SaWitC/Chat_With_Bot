using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotServer.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Class,AllowMultiple =false)]
    public class ServiceAttribute:Attribute
    {
    }
}
