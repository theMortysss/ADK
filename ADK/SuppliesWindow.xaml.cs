using DataAccessLibrary;
using DataAccessLibrary.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace GSS
{
    public partial class SuppliesWindow : Window
    {
        private readonly CollectionView _view;
        private readonly List<Supplie> _supplies;

        public SuppliesWindow()
        {
            InitializeComponent();

            List<Goods> goods;
            using (var context = new GoodsContext())
            {
                goods = context.goods.ToList();
                _supplies = context.supplies.ToList();
            }

            GoodComboBox.ItemsSource = goods;
            SuppliesView.ItemsSource = _supplies;
            _view = (CollectionView)CollectionViewSource.GetDefaultView(SuppliesView.ItemsSource);
        }

        private void AddSupplyButton_Click(object sender, RoutedEventArgs e)
        {
            var good = (Goods)GoodComboBox.SelectedItem;
            var amountParsed = int.TryParse(AmountBox.Text, out var amount);
            var date = DatePicker.SelectedDate ?? DateTime.Today;

            if (GoodComboBox.SelectedIndex != -1 && amountParsed)
            {
                Supplie supply;

                using (var context = new GoodsContext())
                {
                    supply = new Supplie(context.goods.First(g => g.Id == good.Id), amount, date);
                    context.supplies.Add(supply);
                    context.SaveChanges();
                }

                _supplies.Add(supply);
                _view.Refresh();
            }
            else
            {
                MessageBox.Show("Данные о поставке заполнены неверно.");
            }
        }
    }
}