using System;
using shb_bank.Controller;
using shb_bank.Entity;

namespace shb_bank.View
{
    public class GenerateMenu
    {
        public void GetMenu(Account account)
        {
            if (account == null)
            {
                GuessView.GenerateGuessMenu();
            }
            else
            {
                if (account.Role ==  AccountRole.User)
                {
                    UserView.GenerateUserMenu();
                }
                else if (account.Role == AccountRole.Admin)
                {
                    AdminView.GenerateAdminMenu();
                }
            }
        }
    }
}