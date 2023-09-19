using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaShop.Shared
{
    public class UserSession
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
        public double ExpiresIn { get; set; }
        public DateTime ExpiryTimeStamp { get; set; }
    }
}
