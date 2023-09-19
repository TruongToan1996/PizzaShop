namespace PizzaShopAPI.Authentication
{
    public class UserAccountService
    {
        private List<UserAccount> _usersAccountList;
        public UserAccountService()
        {
            _usersAccountList = new List<UserAccount>

            {
                new UserAccount{ UserName = "admin", Password = "admin", Role = "Administrator" },
                new UserAccount { UserName = "user", Password = "user", Role = "User" }
            };
        }
        public UserAccount? GetUserAccountByUserName(string userName)
        {
            return _usersAccountList.FirstOrDefault(x => x.UserName == userName);
        }
    }
}
