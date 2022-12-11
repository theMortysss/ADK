using DataAccessLibrary;
using GSS.StorageLogic;
using System;
using System.IO;
using System.Linq;
using System.Windows;

namespace GSS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private const string OrderDirectoryPath = @".\orders";

        public MainWindow()
        {
            if (!Directory.Exists(OrderDirectoryPath))
            {
                Directory.CreateDirectory(OrderDirectoryPath);
            }

            RemoveExpiredGoods();

            InitializeComponent();

        }

        private void RemoveExpiredGoods()
        {
            using (var context = new GoodsContext())
            {
                var unsoldSupplies = context.supplies.Where(s => s.RemainingQuantity > 0).ToList();

                foreach (var supply in unsoldSupplies)
                {
                    context.Entry(supply).Reference(s => s.Good).Load();
                    if (supply.DateSupplied.AddDays(supply.Good.ExpirationDaysCount) < DateTime.Today)
                        supply.RemainingQuantity = 0;
                }

                context.SaveChanges();
            }
        }

        private void GoodsListButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var sw = new StorageWindow();
            sw.Show();
        }

        private void OrderButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var ow = new OrderWindow(OrderDirectoryPath);
            ow.Show();
        }

        private void SupplyButton_OnClick(object sender, RoutedEventArgs e)
        {
            var sw = new SuppliesWindow();
            sw.Show();
        }

        private void SaleButton_OnClick(object sender, RoutedEventArgs e)
        {
            var sw = new SalesWindow();
            sw.Show();
        }
    }
}