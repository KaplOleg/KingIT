using KingIT.Infrastructure.Commands.Base;
using KingIT.Model;
using KingIT.View;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace KingIT.ViewModel
{
    public class AuthorizationViewModel
    {
        public string Login { get; set; }

        public string Password { get; set; }

        private int count = 1;

        #region Комманды
        public ICommand AuthorizationCommand { get; }

        public bool CanAuthorizationCommandExecute(object parametrs) => true;
        public void OnAuthorizationCommandExecuted(object parametrs)
        {
            using(KingITEntities db = new KingITEntities())
            {
                var emp = (from employee in db.Employees.ToList()
                           where employee.Login.ToUpper() == Login.ToUpper() && employee.Password == Password
                           select employee).FirstOrDefault();

                if (emp != null)
                {
                    MessageBox.Show("Успешно!");
                    MessengerC messenger = new MessengerC();
                    
                    messenger.Show();
                    count = 1;
                }
                else if(count < 3)
                {
                    MessageBox.Show("Не Успешно!");
                    count++;
                }
                else
                {
                    MessageBox.Show("Введите каптчу!");
                }
            }
        }
        #endregion

        public AuthorizationViewModel()
        {
            AuthorizationCommand = new LambdaCommand(OnAuthorizationCommandExecuted, CanAuthorizationCommandExecute);
        }
    }
}
