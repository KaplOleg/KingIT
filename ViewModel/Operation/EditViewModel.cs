using KingIT.Infrastructure.Commands.Base;
using KingIT.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace KingIT.ViewModel.Operation
{
    public class EditViewModel
    {
        public  List<string> StatusList { get; set; }

        private ShopCentres _editShopCentr = MessengerCViewModel.SelectedShopCentr;
        public ShopCentres EditShopCentr { get => _editShopCentr; set => _editShopCentr = value; }

        
        public ShopCentres AddShopCentr { get; set; }


        public ICommand EditCommand { get; }

        public bool CanEditCommandExecute(object param) => true;
        public void OnEditCommandExecuted(object param)
        {
            using(KingITEntities db = new KingITEntities())
            {
                ShopCentres shopCentres = db.ShopCentres.Where(sh => sh.ShopCentr_ID == EditShopCentr.ShopCentr_ID).FirstOrDefault();
                var result = MessageBox.Show("Вы дейтствительно хотит редактировать данные?", " ", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        shopCentres.Name = EditShopCentr.Name;
                        shopCentres.Status = EditShopCentr.Status;
                        shopCentres.NumOfPavilion = EditShopCentr.NumOfPavilion;
                        shopCentres.City = EditShopCentr.City;
                        shopCentres.Price = EditShopCentr.Price;
                        shopCentres.NumOfFloors = EditShopCentr.NumOfFloors;
                        shopCentres.KoofAddedValue = EditShopCentr.KoofAddedValue;
                        db.SaveChanges();
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }
        }

        public ICommand AddComand { get; }

        public bool CanAddComandExecute(object param) => true;
        public void OnAddComandExecuted(object param)
        {
            using (KingITEntities db = new KingITEntities())
            {
                var last = db.ShopCentres.ToList().LastOrDefault();

                ShopCentres shopCentres = new ShopCentres()
                {
                    ShopCentr_ID = last.ShopCentr_ID + 1,
                    Name = AddShopCentr.Name,
                    Status = AddShopCentr.Status,
                    NumOfPavilion = AddShopCentr.NumOfPavilion,
                    City = AddShopCentr.City,
                    Price = AddShopCentr.Price,
                    NumOfFloors = AddShopCentr.NumOfFloors,
                    KoofAddedValue = AddShopCentr.KoofAddedValue
                    //ДОБАВИТЬ ФОТО
                };
       
                db.ShopCentres.Add(shopCentres);
                db.SaveChanges();
                MessageBox.Show("Данные успешно сохранены!");
            }
        }

        public EditViewModel()
        {
            KingITEntities db = new KingITEntities();
            StatusList = db.ShopCentres.Select(s => s.Status).Distinct().ToList();
            StatusList.Remove("Удален");

            AddShopCentr = new ShopCentres();

            EditCommand = new LambdaCommand(OnEditCommandExecuted, CanEditCommandExecute);
            AddComand = new LambdaCommand(OnAddComandExecuted, CanAddComandExecute);
        }
    }
}
