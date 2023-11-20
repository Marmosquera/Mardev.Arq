namespace Mardev.Arq.Shared.Contracts
{
    public record ApplicationUser
    {
        public string UserId { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public List<string> Roles { get; set; } = new List<string>();
    }
}
