namespace Todo.Persistence.Domain
{

    public class Users : AuditEntity
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = default!;
        public string EmailAddress { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
