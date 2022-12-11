using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using DataAccessLibrary;
using DataAccessLibrary.models;
using Microsoft.EntityFrameworkCore;

namespace GSS
{
    public partial class SalesWindow : Window
    {
        private readonly List<Sale> _sales;
        private readonly CollectionView _view;

        public SalesWindow()
        {
            InitializeComponent();

            List<Goods> goods;
            using (var context = new GoodsContext())
            {
                goods = context.goods.Include(g => g.Supplies).ToList();
                _sales = context.sales.ToList();
            }

            GoodComboBox.ItemsSource = goods;
            SalesView.ItemsSource = _sales;
            _view = (CollectionView)CollectionViewSource.GetDefaultView(SalesView.ItemsSource);
        }

        private void AddSaleButton_OnClick(object sender, RoutedEventArgs e)
        {
            var good = (Goods)GoodComboBox.SelectedItem;
            var amountParsed = int.TryParse(AmountBox.Text, out var amount);
            var date = DatePicker.SelectedDate ?? DateTime.Today;

            if (GoodComboBox.SelectedIndex != -1 && amountParsed && amount <= good.AvailableAmount)
            {
                Sale sale;

                using (var context = new GoodsContext())
                {
                    var goodSupplies = context.supplies
                        .Where(s => s.GoodId == good.Id && s.RemainingQuantity > 0)
                        .OrderBy(s => s.DateSupplied)
                        .ToList();

                    DistributeGoodsFromSupplies(amount, goodSupplies);

                    sale = new Sale(context.goods.First(g => g.Id == good.Id), amount, date);
                    context.sales.Add(sale);
                    context.SaveChanges();

                    MaxAmountLabel.Content = "/" + sale.Good.AvailableAmount.ToString();
                }

                _sales.Add(sale);
                _view.Refresh();
            }
            else
            {
                MessageBox.Show("Данные о продаже заполнены неверно.");
            }
        }

        private static void DistributeGoodsFromSupplies(int amount, List<Supplie> goodSupplies)
        {
            var index = 0;
            var remainingAmount = amount;
            while (remainingAmount > 0)
            {
                var supply = goodSupplies[index];

                if (remainingAmount >= supply.RemainingQuantity)
                {
                    remainingAmount -= supply.RemainingQuantity;
                    supply.RemainingQuantity = 0;
                    supply.DateSold = DateTime.Today;
                }
                else
                {
                    supply.RemainingQuantity -= remainingAmount;
                    remainingAmount = 0;
                }
                index++;
            }
        }

        private void GoodComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (GoodComboBox.SelectedIndex == -1)
                return;

            var good = (Goods)GoodComboBox.SelectedItem;
            MaxAmountLabel.Content = "/" + good.AvailableAmount.ToString();
        }
    }
}