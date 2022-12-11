using System;
using DataAccessLibrary;
using GSS.StorageLogic;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using DataAccessLibrary.models;
using Microsoft.EntityFrameworkCore;

namespace GSS
{
    public partial class OrderWindow : Window
    {
        private readonly List<GoodBatch> _order;
        private readonly List<Sale> _sales;
        private readonly List<Department> _depts;
        private readonly StorageParameters _storageParams;
        private readonly CollectionView _view;
        private readonly string _dirpath;

        public OrderWindow(string orderDirPath)
        {
            _dirpath = orderDirPath;
            _order = new List<GoodBatch>();

            InitializeComponent();
            OrderGrid.ItemsSource = _order;
            _view = (CollectionView)CollectionViewSource.GetDefaultView(OrderGrid.ItemsSource);
            var groupDescription = new PropertyGroupDescription("DepartmentName");
            _view.GroupDescriptions?.Add(groupDescription);

            using (var db = new GoodsContext())
            {
                var supplies = db.supplies.ToList();
                _sales = db.sales.ToList();
                _depts = db.departments.Include(d => d.Goods).ToList();
                _storageParams = StorageCalculate.CalculateStorageParameters(_depts);
            }
        }

        private void CreateOrderButton_Click(object sender, RoutedEventArgs e)
        {
            var autoOrder = OrderCalculator.MakeOrder(_depts, _sales, _storageParams);

            _order.Clear();
            _order.AddRange(autoOrder);
            _view.Refresh();
        }

        private void SaveOrderButton_Click(object sender, RoutedEventArgs e)
        {
            var path = _dirpath + $"\\Order_{DateTime.Today.AddDays(1):yyyy-MM-dd}.txt";

            if (File.Exists(path))
                File.Delete(path);

            using (var sw = File.AppendText(path))
            {
                foreach (var goodBatch in _order)
                    sw.WriteLine($"{goodBatch.Good,-50} {goodBatch.Amount}");
            }

            MessageBox.Show("Заказ сохранен.");
        }
    }
}