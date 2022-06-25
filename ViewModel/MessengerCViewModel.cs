using KingIT.Infrastructure.Commands.Base;
using KingIT.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace KingIT.ViewModel
{
    public class MessengerCViewModel : ViewModel.Base.ViewModel
    {
        private List<ShopCentres> _shopCentres;
        public List<ShopCentres> ShopCentres { get => _shopCentres; set => Set(ref _shopCentres, value); }

        public List<string> CityCentres { get; set; }
        public static ShopCentres SelectedShopCentr { get; set; }

        #region Сортировки
        private string _statusSort;
        public string StatusSort
        {
            get => _statusSort;
            set
            {
                KingITEntities db = new KingITEntities();
                ShopCentres = db.ShopCentres.Where(s => s.Status != "Удален").ToList();
                _statusSort = value;

                if(_citySort == null || CitySort == "")
                {
                    var list = db.ShopCentres.Where(i => i.Status == StatusSort).ToList();
                    ShopCentres = list;
                }
                else if(_citySort != null)
                {
                    var list_city_status = db.ShopCentres.Where(i => i.Status == StatusSort && i.City == CitySort).ToList();
                    ShopCentres = list_city_status;
                }
            }
        }

        private string _citySort;
        public string CitySort 
        {
            get => _citySort;
            set
            {
                _citySort = value;
                KingITEntities db = new KingITEntities();
                ShopCentres = db.ShopCentres.Where(s => s.Status != "Удален").ToList();
                if (_statusSort == null && CitySort != "")
                {
                    var list = db.ShopCentres.Where(i => i.City == CitySort).ToList();
                    ShopCentres = list;
                }
                else if(_statusSort != null && CitySort == "")
                {
                    var list = db.ShopCentres.Where(i => i.Status == StatusSort).ToList();
                    ShopCentres = list;
                }
                else if(_statusSort != null)
                {
                    var list_city_status = db.ShopCentres.Where(i => i.Status == StatusSort && i.City == CitySort).ToList();
                    ShopCentres = list_city_status;
                }
            }
        }
        #endregion

        #region Команды
        public ICommand DeleteShop { get; }

        public bool CanDeleteShopExecute(object parametrs)
        {
            if (SelectedShopCentr != null) return true;
            return false;
        }
        public void OnDeleteShopExecute(object parametrs)
        {
            using(KingITEntities db = new KingITEntities())
            {
                ShopCentres shopCentres = db.ShopCentres.Where(sh => sh.ShopCentr_ID == SelectedShopCentr.ShopCentr_ID).FirstOrDefault();

                var result = MessageBox.Show("Вы действительно хотите удалить ТЦ?", " ", MessageBoxButton.YesNo);
                if (shopCentres != null)
                {
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            shopCentres.Status = "Удален";
                            db.SaveChanges();
                            ShopCentres = db.ShopCentres.ToList();
                            break;
                        case MessageBoxResult.No:
                            break;
                    }

                   
                }
            }
        }

        public ICommand EditShop { get; }

        public bool CanEditShopExecute(object parametrs)
        {
            if (SelectedShopCentr != null) return true;
            return false;
        }
        public void OnEditShopExecute(object parametrs) { }
        #endregion

        public MessengerCViewModel()
        {
            KingITEntities db = new KingITEntities();

            ShopCentres = db.ShopCentres.Where(s => s.Status != "Удален").ToList();
            CityCentres = db.ShopCentres.Select(s => s.City).Distinct().ToList();
            CityCentres.Add("");

            DeleteShop = new LambdaCommand(OnDeleteShopExecute, CanDeleteShopExecute);
            EditShop = new LambdaCommand(OnEditShopExecute, CanEditShopExecute);
        }
    }
}
