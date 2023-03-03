using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UEWPF3_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Person osoba = null;
        public MainWindow()
        {
            InitializeComponent();
            InitBinding();
        }
        private void InitBinding()
        {
            osoba = new Person(1, "Jan", "Kowalski", 25);
            gridPersonForm.DataContext = osoba;
        }
        private void btnOdczytaj_Click(object sender, RoutedEventArgs e)
        {
            var input = File.ReadAllLines(@"..\..\..\osoba.csv");
            var data = input[0].Split(';');
            osoba.PersonId = int.Parse(data[0]);
            osoba.FirstName = data[1];
            osoba.LastName = data[2];
            osoba.Age = int.Parse(data[3]);

            // wymuszam odświeżenie widoku
            gridPersonForm.DataContext = null;
            gridPersonForm.DataContext = osoba;
        }


        string lastCorrectIdValue = string.Empty;

        private bool validateIdField(string id)
        {
            if (id == null || id.Length == 0) return false;
            if (!int.TryParse(id, out int result)) return false;
            if (result > 100 || result < 0) return false;
            return true;
        }

        private bool validateName(string name)
        {
            if (name == null || name.Length == 0 || name.Length > 20) return false;
            return true;
        }

        private bool validateAge(string age)
        {
            if (age == null || age.Length == 0) return false;
            if (!int.TryParse(age, out int result)) return false;
            if (result < 0 || result > 130) return false;
            return true;
        }

        public string? validateFields()
        {
            bool id = true;
            bool name = true;
            bool surname = true;
            bool age = true;



            id = validateIdField(idField.Text);
            name = validateName(nameField.Text);
            surname = validateName(surnameField.Text);
            age = validateAge(ageField.Text);

            if(id && name && surname && age)
            {
                return null;
                // null jeśli nie ma zastrzeżeń
            }

            var errors = "";
            if(!id)
            {
                errors += "ID jest niepoprawne. Musi być w zakresie 1-100 i nie może być puste.\n";
            }
            if(!name)
            {
                errors += "Imię jest niepoprawne. Musi mieć mniej niż 20 znaków i nie może być puste.\n";
            }
            if(!surname)
            {
                errors += "Nazwisko jest niepoprawne. Musi mieć mniej niż 20 znaków i nie może być puste.\n";
            }
            if(!age)
            {
                errors += "Wiek jest niepoprawny. Musi być w zakresie 0-130 i nie może być pusty.\n";
            }
            return errors;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var validation = validateFields();
            if(validation != null)
            {
                MessageBox.Show("Wystąpiły następujące problemy:\n\n" + validation);
                return;
            }

            // brak błędów, zapisujemy

            File.WriteAllText(@"..\..\..\osoba.csv", $"{osoba.PersonId};{osoba.FirstName};{osoba.LastName};{osoba.Age}");
        }
    }
}
