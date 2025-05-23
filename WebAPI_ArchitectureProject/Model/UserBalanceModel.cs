namespace WebAPI_ArchitectureProject.Model
{
    public class UserBalanceModel
    {
        public required string Username { get; set; }
        public double Balance { get; set; }
        public int Quota { get; set; }
    }
}
