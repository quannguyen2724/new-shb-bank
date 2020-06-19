using System;
using shb_bank.Controller;
using shb_bank.Entity;

namespace shb_bank.View
{
    public class GenerateMenu
    {
        public void GetMenu()
        {
            if (AccountController.currentAccount == null)
            {
                GuessView.GenerateGuessMenu();
            }
            else
            {
                if (AccountController.currentAccount.Role ==  AccountRole.User)
                {
                    UserView.GenerateUserMenu();
                }
                else if(AccountController.currentAccount.Role == AccountRole.Admin)
                {
                    AdminView.GenerateAdminMenu();
                }
            }
        }
    }
}