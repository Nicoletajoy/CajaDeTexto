using System;
using System.Windows;
using System.Xml.Linq;

namespace CajaDeTexto
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            NombreVinculado.Text = NombreTextBox.Text;
            TelefonoVinculado.Text = TelefonoTextBox.Text;

            GuardarButton.IsEnabled = !string.IsNullOrWhiteSpace(NombreTextBox.Text) && !string.IsNullOrWhiteSpace(TelefonoTextBox.Text);
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            string nombre = NombreTextBox.Text;
            string telefono = TelefonoTextBox.Text;

            XElement contacto = new XElement("Contacto",
                new XElement("Nombre", nombre),
                new XElement("Telefono", telefono)
            );

            XDocument doc;
            string filePath = "contactos.xml";

            if (System.IO.File.Exists(filePath))
            {
                doc = XDocument.Load(filePath);
                doc.Root.Add(contacto);
            }
            else
            {
                doc = new XDocument(new XElement("Contactos", contacto));
            }

            doc.Save(filePath);
            MessageBox.Show("Contacto guardado exitosamente.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);

            // Limpiar el formulario
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            NombreTextBox.Text = string.Empty;
            TelefonoTextBox.Text = string.Empty;
            NombreVinculado.Text = string.Empty;
            TelefonoVinculado.Text = string.Empty;
            GuardarButton.IsEnabled = false;
        }
    }
}