using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Media;

namespace Petshop
{
    public class Validator
    {
        private readonly Control _control;
        public string _content { get ; set; }

        private bool _success = true;

        public Validator (Control control, string content)
        {
            _control = control;
            _content = content;
        }

        public Validator MinCharacters(int count)
        {
            if (_content.Length < count)
                _success = false;

            return this;
        }

        public Validator isSNP()
        {
            if (_content.Split(' ').Length != 3)
            {
                _success = false;
            }

            return this;
        }

        public Validator isEmail()
        {
            Regex Email = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

            if (!Email.IsMatch(_content))
            {
                _success = false;
            }

            return this;
        }

        public Validator isPhone()
        {
            Regex Phone = new Regex(@"\+\d{8,15}");

            if (!Phone.IsMatch(_content))
            {
                _success = false;
            }

            return this;
        }

        public Validator Validate()
        {
            if (_success == false)
            {
                _control.Background = Brushes.Red;
                
                //MessageBox.Show("Некоторые поля введены некорректно");
                return this;
            }

            _control.Background = Brushes.White;
            return this;

        }

        public void addToList(List<Control> textBoxes)
        {
            if (_control.Background == Brushes.Red)
            {
                return;
            }
            else
            {
                textBoxes.Add(_control);
            }
        }
    }
}
