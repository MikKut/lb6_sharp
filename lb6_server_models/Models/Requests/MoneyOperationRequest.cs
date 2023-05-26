using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lb6_server.Models.Requests
{
    public class MoneyOperationRequest
    {
        [Range(0, double.MaxValue,
        ErrorMessage = "Amount must be more then 0")]
        public decimal Amount { get; set; }
        public string Name { get; set; }
    }
}
