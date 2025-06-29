using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROE.DataAccess.Entities
{
    public class Customers
    {
        public int? PK_CustomerId { get; set; }
        public string CustomerGUID { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public string ROName { get; set; } = string.Empty;
        public string RODescription { get; set; } = string.Empty;
    }
}
