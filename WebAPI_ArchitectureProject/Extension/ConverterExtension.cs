namespace WebAPI_ArchitectureProject.Extension
{
    public static class ConverterExtension
    {
        public static WebAPI_ArchitectureProject.Model.UserBalanceModel ToUserBalanceModel(this DataAccessLayer.Models.User user)
        {
            return new WebAPI_ArchitectureProject.Model.UserBalanceModel
            {
                Username = user.Username,
                Balance = user.Balance,
                Quota = user.Quota,
            };
        }
    }
}
