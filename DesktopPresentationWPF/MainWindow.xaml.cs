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

        private string GetData() {
            HttpClient client = new HttpClient {
                BaseAddress = new Uri("http://localhost:2618/Api/") // BaseAddress = new Uri("http://localhost:2021/");
                // Error 404: Ya probado con 2618/, 2618/api, 2618/api/, api con mayúsculas y minúsculas...
            };

            // TODO: Cambiar GetStringAsync por GetAsync y SERIALIZAR!

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.GetStringAsync("Product/1").Result; // ("api/Product/1").Result;
            return response;
        }

        // Client
        static HttpClient client = new HttpClient();

        private void GetConsulta_Click(object sender, RoutedEventArgs e) {
            // Consult from API and Modify Textbox
            consultaName.Text = GetData();
        }

        private void ShowConsulta_Click(object sender, RoutedEventArgs e) {
            // Show Obtained Name
            MessageBox.Show($"Name: { consultaName.Text }");
        }
    }
}
