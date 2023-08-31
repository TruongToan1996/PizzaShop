using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaShop.Shared
{
    public class BaseResponse<T>
    {
        public T? DataResult { get; set; }
        public int ResponseCode{ get; set; }
        public string? Msg { get; set; }
    }
}
