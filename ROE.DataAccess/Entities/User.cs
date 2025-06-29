namespace ROE.DataAccess.Entities
{
    public class User
    {
        public int? PK_UserId { get; set; }
        public int? FK_CustomerId { get; set; }
        public int? FK_RoleId { get; set; }
        public string UserGUID { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
    }
}
