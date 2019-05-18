using System;
using System.Net.Http;
using System.Windows;
using System.Net.Http.Headers;
using DesktopPresentationWPF.Models;

namespace DesktopPresentationWPF {
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private WpfProductModel GetData() {
            HttpClient client = new HttpClient {
                BaseAddress = new Uri("http://localhost:2021/Api/")
            };

            // TODO: Cambiar GetStringAsync por GetAsync y SERIALIZAR!

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = client.GetAsync("Product/1").Result;

            var prod = response.Content.ReadAsAsync<WpfProductModel>().Result;

            return prod;
        }

        // Client
        static HttpClient client = new HttpClient();

        private void GetConsulta_Click(object sender, RoutedEventArgs e) {
            // Consult from API and Modify Textbox
            WpfProductModel prod = GetData();
            consultaName.Text = prod.Id + " " + prod.ProductName + " Stock: " + prod.Quantity + " Price: " + prod.Price + " Vendor: " + prod.VendorId;
        }

        private void ShowConsulta_Click(object sender, RoutedEventArgs e) {
            // Show Obtained Name
            MessageBox.Show($"Name: { consultaName.Text }");
        }
    }
}
