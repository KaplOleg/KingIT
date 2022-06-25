using KingIT.Infrastructure.Commands.Base;
using KingIT.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace KingIT.ViewModel
{
    public class PavilionsViewModel : Base.ViewModel
    {
        public List<Pavilions> pavilions;
        public List<Pavilions> PavilionsList { get => pavilions; set => Set(ref pavilions, value); }
        
        public Pavilions AddPavilion { get; set; }
        public static Pavilions SelectedPavilion { get; set; }

        public List<string> StatusList { get; set; }
        public List<string> AllStatusList { get; set; }
        public List<string> TCList { get; set; }
        public List<int?> FloorsList { get; set; }

        #region Сортировки
        private string _sortStatus;
        public string SortStatus
        {
            get => _sortStatus;

            set
            {
                KingITEntities db = new KingITEntities();
                PavilionsList = db.Pavilions.Where(p => p.ShopCenter_Id == MessengerCViewModel.SelectedShopCentr.ShopCentr_ID).ToList();
                _sortStatus = value;
                if (_floorStatus == 0)
                {
                    var list = PavilionsList.Where(p => p.Status == SortStatus).ToList();
                    PavilionsList = list;
                }
                else if (_floorStatus != 0)
                {
                    var list = PavilionsList.Where(p => p.Status == SortStatus && p.Floor == FloorStatus).ToList();
                    PavilionsList = list;
                }
            }
        }

        private int _floorStatus;
        public int FloorStatus
        {
            get => _floorStatus;

            set
            {
                KingITEntities db = new KingITEntities();
                PavilionsList = db.Pavilions.Where(p => p.ShopCenter_Id == MessengerCViewModel.SelectedShopCentr.ShopCentr_ID).ToList();
                _floorStatus = value;
                if (_sortStatus == null)
                {
                    var list = PavilionsList.Where(p => p.Floor == FloorStatus).ToList();
                    PavilionsList = list;
                }
                else if (_sortStatus != null)
                {
                    var list = PavilionsList.Where(p => p.Status == SortStatus && p.Floor == FloorStatus).ToList();
                    PavilionsList = list;
                }
            }
        }

        private double minValue;
        public double MinValue { get => minValue; set { minValue = value; SortMinMaxValue(); } }
        private double maxValue;
        public double MaxValue { get => maxValue; set { maxValue = value; SortMinMaxValue(); } }

        private void SortMinMaxValue()
        {
            KingITEntities db = new KingITEntities(); 
            PavilionsList = db.Pavilions.Where(p => p.ShopCenter_Id == MessengerCViewModel.SelectedShopCentr.ShopCentr_ID &&
                                                    p.Status != "Удален").ToList();
            PavilionsList = PavilionsList.Where(t => t.Square >= minValue && t.Square <= maxValue).ToList();
        }
        #endregion

        #region Комманды
        public ICommand AddPavilionCommand { get; }
        public bool OnAddPavilionCommandExecuted(object param) => true;
        public void CanAddPavilionCommandExecute(object param)
        {
            using (KingITEntities db = new KingITEntities())
            {
                Pavilions pavilion = new Pavilions()
                {
                    Pavilion_Number = AddPavilion.Pavilion_Number,
                    ShopCenter_Id = MessengerCViewModel.SelectedShopCentr.ShopCentr_ID,
                    Floor = AddPavilion.Floor,
                    Status = AddPavilion.Status,
                    Square = AddPavilion.Square,
                    CostPerSq_m = AddPavilion.CostPerSq_m,
                    Cof_Added_value = AddPavilion.Cof_Added_value
                };

                db.Pavilions.Add(pavilion);
                db.SaveChanges();
                MessageBox.Show("Добавлено успешно!");
            }
        }

        public ICommand DelPavilionCommand { get; }
        public bool OnDelPavilionCommandExecuted(object param)
        {
            if (SelectedPavilion != null) return true;
            return false;
        }
        public void CanDelPavilionCommandExecute(object param)
        {
           using(KingITEntities db = new KingITEntities())
           {
                Pavilions pavilion = db.Pavilions.Where(p => p.Pavilion_Number == SelectedPavilion.Pavilion_Number).FirstOrDefault();

                var result = MessageBox.Show("Вы действительно хотите удалить этот павильон?", " ", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        pavilion.Status = "Удален";
                        db.SaveChanges();
                        PavilionsList = db.Pavilions.Where(p => p.ShopCenter_Id == MessengerCViewModel.SelectedShopCentr.ShopCentr_ID &&
                                                                p.Status != "Удален").ToList();
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }
        }

        public ICommand EditPavilionCommand { get; }
        public bool OnEditPavilionCommandExecuted(object param) => true;
        public void CanEditPavilionCommandExecute(object param)
        {
            using (KingITEntities db = new KingITEntities())
            {
                Pavilions pavilion = db.Pavilions.Where(sh => sh.Pavilion_Number == SelectedPavilion.Pavilion_Number).FirstOrDefault();
                var result = MessageBox.Show("Вы дейтствительно хотит редактировать данные?", " ", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        pavilion.Floor = SelectedPavilion.Floor;
                        pavilion.Status = SelectedPavilion.Status;
                        pavilion.Square = SelectedPavilion.Square;
                        pavilion.CostPerSq_m = SelectedPavilion.CostPerSq_m;
                        pavilion.Cof_Added_value = SelectedPavilion.Cof_Added_value;
                        db.SaveChanges();
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }
        }
        #endregion

        public PavilionsViewModel()
        {
            KingITEntities db = new KingITEntities();
            PavilionsList = db.Pavilions.Where(p => p.ShopCenter_Id == MessengerCViewModel.SelectedShopCentr.ShopCentr_ID &&
                                                    p.Status != "Удален").ToList();

            AddPavilion = new Pavilions();
            StatusList = PavilionsList.Select(s => s.Status).Distinct().ToList();
            AllStatusList = db.Pavilions.Select(S => S.Status).Distinct().ToList();
            TCList = db.ShopCentres.Select(s => s.Name).Distinct().ToList();
            FloorsList = PavilionsList.Select(s => s.Floor).Distinct().ToList();

            #region Команды
            AddPavilionCommand = new LambdaCommand(CanAddPavilionCommandExecute, OnAddPavilionCommandExecuted);
            DelPavilionCommand = new LambdaCommand(CanDelPavilionCommandExecute, OnDelPavilionCommandExecuted);
            EditPavilionCommand = new LambdaCommand(CanEditPavilionCommandExecute, OnEditPavilionCommandExecuted);
            #endregion
        }
    }
}
