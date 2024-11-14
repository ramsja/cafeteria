using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient; // Agrega esta línea

namespace cafeteria
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SqlConnection connection;

        public MainWindow()
        {
            InitializeComponent();
            InitializeDatabaseConnection();
        }

        private void InitializeDatabaseConnection()
        {
            // Usa la cadena de conexión configurada en el Explorador de Servidores
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=cafeteria;Integrated Security=True;";
            connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                MessageBox.Show("¡Conexión a la base de datos exitosa!");
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        private void IngresarUsuario_Click(object sender, RoutedEventArgs e)
        {
            string nombre = txtNombre.Text;
            string email = txtEmail.Text;
            string password = txtPassword.Password;
            string tipoUsuario = ((ComboBoxItem)cmbTipoUsuario.SelectedItem).Content.ToString();

            string query = "INSERT INTO Usuarios (Nombre, TipoUsuario, Email, Password) VALUES (@Nombre, @TipoUsuario, @Email, @Password)";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@TipoUsuario", tipoUsuario);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);

                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Usuario ingresado exitosamente.");
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Error al ingresar usuario: {ex.Message}");
                }
            }
        }
    }
}
