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

namespace Contraseñas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GenerarPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            GenerarPassword();
        }

        /* Método para generar la password */
        private void GenerarPassword()
        {
            // Generador de números aleatorios
            var rand = new Random();

            // Lista de caracteres para la contraseña (Pongo varias veces los caracteres especiales porque si no es muy difícil que salgan y es condición necesaria para la generación de la contraseña, por lo que de no hacerlo se generan contraseñas excesivamente largas)
            string characters = "abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ0123456789....;;;;@@@@";

            // Longitud de la contraseña
            int passwordLength = 8;

            // StringBuilder para construir la contraseña
            StringBuilder passwordBuilder = new StringBuilder();

            // Nos aseguramos de que la contraseña tenga al menos una letra mayúscula, minúscula, número y símbolo
            bool hasUpperCase = false;
            bool hasLowerCase = false;
            bool hasNumber = false;
            bool hasSymbol = false;

            while (!hasUpperCase || !hasLowerCase || !hasNumber || !hasSymbol || passwordBuilder.Length < passwordLength)
            {
                // Generamos un índice aleatorio
                int index = rand.Next(characters.Length);

                // Obtenemos el caracter
                char character = characters[index];

                // Agregamos el caracter a la contraseña
                passwordBuilder.Append(character);

                if (char.IsUpper(character))
                {
                    hasUpperCase = true;
                }
                else if (char.IsLower(character))
                {
                    hasLowerCase = true;
                }
                else if (char.IsNumber(character))
                {
                    hasNumber = true;
                }
                else if (character == '.' || character == ';' || character == '@')
                {
                    hasSymbol = true;
                }
            }

            // Asignamos la contraseña al TextBox
            PasswordTextBlock.Text = passwordBuilder.ToString();
        }

        /* Método para comprobar que la contraseña tiene 8 o más carácteres */
        private bool Tiene8oMasCaracteres(String password)
        {
            return password.Length >= 8;
        }

        /* Método para comprobar que la contraseña tiene 8 o más carácteres */
        private bool TieneMinusculaYMayuscula(String password)
        {
            bool contieneMayuscula = false;
            bool contieneMinuscula = false;

            for (int i = 0; i < password.Length; i++)
            {
                if (char.IsUpper(password[i]))
                {
                    contieneMayuscula = true;
                }
                else if (char.IsLower(password[i]))
                {
                    contieneMinuscula = true;
                }

                if (contieneMayuscula && contieneMinuscula)
                {
                    return true;
                }
            }

            return false;
        }

        /*Método para comprobar si la contraseña tiene algún número*/
        private bool TieneNumero(string password)
        {
            for (int i = 0; i < password.Length; i++)
            {
                char character = password[i];

                if (char.IsDigit(character))
                {
                    return true;
                }
            }

            return false;
        }
    

        /* Método para comprobar si la contraseña tiene alguno de los carácteres especiales definidos */
        private bool TieneCaracterEspecial(string password)
        {
            for (int i = 0; i < password.Length; i++)
            {
                char caracter = password[i];
                if (caracter == '.' || caracter == ';' || caracter == '@')
                {
                    return true;
                }
            }
            return false;
        }

        private void ComprobarPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            ComprobarPassword();
        }

        /*Método para comprobar si la password es correcta*/
        private void ComprobarPassword()
        {
            String password = ComprobarPasswordTextBlock.Text;

            StringBuilder avisoBuilder = new StringBuilder();

            if (!Tiene8oMasCaracteres(password))
            {
                avisoBuilder.Append("\tNo contiene al menos 8 caracteres.\n");

            } if (!TieneMinusculaYMayuscula(password))
            {
                avisoBuilder.Append("\tDebe contener una minúscula y una mayúscula.\n");
            } if(!TieneNumero(password)){

                avisoBuilder.Append("\tDebe contener un número.\n");

            }if(!TieneCaracterEspecial(password))
            {
                avisoBuilder.Append("\tDebe contener un carácter especial: [.;@]");
            }

            if(Tiene8oMasCaracteres(password) && TieneMinusculaYMayuscula(password) && TieneNumero(password) && TieneCaracterEspecial(password))
            {
                MessageBox.Show("Su contraseña es 100% segura, cumple con todos los requisitos.", "OK", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show(this, "Su contraseña no es segura. No se cumplen los siguientes requisitos: \n" + avisoBuilder.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        /*Método para poder copiar la contraseña al portapapeles al hacer click con el mouse*/
        private void PasswordTextBlock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBlock textBlock)
            {
                Clipboard.SetText(textBlock.Text);
            }
        }
    } 
}
    

    
    
