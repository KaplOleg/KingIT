using KingIT.Model;
using System.Collections.Generic;
using System.Linq;

namespace KingIT.ViewModel.EmployeesViewModel
{
    public class EmployeViewModel : Base.ViewModel
    {
        private List<Employees> _employeesList;
        public List<Employees> EmployeesList { get => _employeesList; set => Set(ref _employeesList, value); }

        private string _searchEmployee;
        public string SearchEmployee
        {
            get => _searchEmployee;

            set
            {
                Set(ref _searchEmployee, value);

                KingITEntities db = new KingITEntities();
                
                var list = db.Employees.ToList().Where(e => e.FullName.ToLower().Contains(_searchEmployee.ToLower())).ToList();
                EmployeesList = list;
            }
        }

        public EmployeViewModel()
        {
            KingITEntities db = new KingITEntities();
            EmployeesList = db.Employees.ToList();
        }
    }
}
