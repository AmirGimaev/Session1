using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ГимаевДЭ.Windows
{
    /// <summary>
    /// Логика взаимодействия для окна ProductList.xaml
    /// </summary>
    public partial class ProductList : Window
    {
        public ProductList()
        {
            InitializeComponent();
        }


        // Срабатывает по окончанию инициализации окна.
        private void WindowIsLoaded(object sender, RoutedEventArgs e)
        {
            UpdateData();
        }


        // Срабатывет при выборе типа фильтрации.
        private void SelectFilter(object sender, EventArgs e)
        {
            UpdateData();
        }


        // Срабатывает при выборе вида сортировки.
        private void SelectSort(object sender, EventArgs e)
        {
            UpdateData();
        }



        /// <summary>
        /// Сортровка списка продуктов по цене.
        /// </summary>
        /// <param name="products">Ссылка на экземпляр, хранящий список продуктов.</param>
        private void SortProducts(ref List<Products> products)
        {
            if (SortProductComboBox.SelectedIndex == 0)
                products = products.OrderBy(o => o.Cost).ToList();
            else
                products = products.OrderByDescending(o => o.Cost).ToList();
        }

        /// <summary>
        /// Фильрация списка продуктов по скидке.
        /// </summary>
        /// <param name="products">Ссылка на экземпляр, хранящий список продуктов.</param>
        private void FilterProducts(ref List<Products> products)
        {
            if (FilterComboBox.SelectedIndex != 0)
            {
                switch (FilterComboBox.Text)
                {
                    case "0 - 9,99%":
                        products = products.Where(w => w.Discount >= 0 & w.Discount < 10).ToList();
                        break;
                    case "10 - 14,99%":
                        products = products.Where(w => w.Discount >= 10 & w.Discount < 15).ToList();
                        break;
                    case "15% и более":
                        products = products.Where(w => w.Discount >= 15).ToList();
                        break;
                }
            }
        }

        /// <summary>
        /// Выводит данные из таблицы Products со фильтрацией и сортировкой.
        /// </summary>
        public void UpdateData()
        {
            // Загрузка данных из таблицы Products.
            var _products = FlowersDataBaseEntities.GetDataBase().Products.ToList();

            FilterProducts(ref _products);

            SortProducts(ref _products);

            // Вывод количества выведенных записей и количество всего записей с таблице.
            CountItemsTextBlock.Text = $"{_products.Count} из {FlowersDataBaseEntities.GetDataBase().Products.ToList().Count}";

            // Загрузка данных в ListView.
            ProductsListView.ItemsSource = _products;
        }



    }
}
