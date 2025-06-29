using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROE.DataAccess.Entities
{
    public class Product_Role
    {
        public int? PK_RoleId { get; set; }
        public string RoleGUID { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
    }
}
