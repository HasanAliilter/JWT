using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthServer.Core.Configuration
{
    public class Client
    {
        public string Id { get; set; }
        public string Secret { get; set; }
        public IList<string> Audiences { get; set; } //hangi apilere erişip erişemeyeceğini belirleriz
    }
}
