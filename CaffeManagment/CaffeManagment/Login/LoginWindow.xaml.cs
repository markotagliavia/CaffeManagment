﻿using CaffeManagment.Common;
using CaffeManagment.Model;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CaffeManagment.Login
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : MetroWindow, INotifyPropertyChanged
    {
        #region Fields
        private System.Windows.Media.Color c1;
        private System.Windows.Media.Brush _firmColor;
        private System.Windows.Media.Color c2;
        private System.Windows.Media.Brush _lightFirmColor;
        private System.Windows.Media.Color c3;
        private System.Windows.Media.Brush _backgroundColor;
        private System.Windows.Media.Brush _buttonColor;
        private System.Windows.Media.Brush _labelColor;
        #endregion

        #region INotifiedProperty Block
        protected void OnPropertyChanged(string porpName)
        {
            var temp = PropertyChanged;
            if (temp != null)
                temp(this, new PropertyChangedEventArgs(porpName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion


        public LoginWindow()
        {
            c1 = System.Windows.Media.Color.FromArgb(255, 68, 95, 245);
            c2 = System.Windows.Media.Color.FromArgb(255, 128, 170, 255);
            c3 = System.Windows.Media.Color.FromArgb(255, 37, 44, 50);
            FirmColor = new SolidColorBrush(c1);
            LightFirmColor = new SolidColorBrush(c2);
            BackgroundColor = new SolidColorBrush(c3);
            ButtonColor = new SolidColorBrush(c1);
            LabelColor = new SolidColorBrush(c1);
            InitializeComponent();
            this.DataContext = this;
        }

        #region Constructors Block
        public Brush FirmColor
        {
            get { return _firmColor; }
            set
            {
                _firmColor = value;
                OnPropertyChanged("FirmColor");
            }
        }

        public Brush LightFirmColor
        {
            get { return _lightFirmColor; }
            set
            {
                _lightFirmColor = value;
                OnPropertyChanged("LightFirmColor");
            }
        }

        public Brush BackgroundColor
        {
            get { return _backgroundColor; }
            set
            {
                _backgroundColor = value;
                OnPropertyChanged("BackgroundColor");
            }
        }

        public Brush ButtonColor
        {
            get { return _buttonColor; }
            set
            {
                _buttonColor = value;
                OnPropertyChanged("ButtonColor");
            }
        }

        public Brush LabelColor
        {
            get { return _labelColor; }
            set
            {
                _labelColor = value;
                OnPropertyChanged("LabelColor");
            }
        }
        #endregion

        #region EventsColors
        private void usernameTextBoxFocus(object sender, RoutedEventArgs e)
        {
            if (usernameTextBox.Text.Equals("Korisničko ime")) usernameTextBox.Text = "";
            Storyboard sb = new Storyboard() { Duration = TimeSpan.FromSeconds(0.7), BeginTime = TimeSpan.Zero };

            ColorAnimation colAnim = new ColorAnimation();
            colAnim.To = c1;
            colAnim.Duration = new Duration(TimeSpan.FromSeconds(0.7));
            colAnim.AutoReverse = false;

            sb.Children.Add(colAnim);

            Storyboard.SetTarget(colAnim, kvadrat1);
            Storyboard.SetTargetProperty(colAnim, new PropertyPath("Fill.Color"));
            sb.Begin();
        }

        private void usernameTextBoxUnFocus(object sender, RoutedEventArgs e)
        {
            Storyboard sb = new Storyboard() { Duration = TimeSpan.FromSeconds(0.7), BeginTime = TimeSpan.Zero };

            ColorAnimation colAnim = new ColorAnimation();
            colAnim.To = System.Windows.Media.Color.FromArgb(255, 177, 177, 177);
            colAnim.Duration = new Duration(TimeSpan.FromSeconds(0.7));
            colAnim.AutoReverse = false;

            sb.Children.Add(colAnim);

            Storyboard.SetTarget(colAnim, kvadrat1);
            Storyboard.SetTargetProperty(colAnim, new PropertyPath("Fill.Color"));
            sb.Begin();
        }

        private void passwordBoxFocus(object sender, RoutedEventArgs e)
        {
            Storyboard sb = new Storyboard() { Duration = TimeSpan.FromSeconds(0.7), BeginTime = TimeSpan.Zero };

            ColorAnimation colAnim = new ColorAnimation();
            colAnim.To = c1;
            colAnim.Duration = new Duration(TimeSpan.FromSeconds(0.7));
            colAnim.AutoReverse = false;

            sb.Children.Add(colAnim);

            Storyboard.SetTarget(colAnim, kvadrat2);
            Storyboard.SetTargetProperty(colAnim, new PropertyPath("Fill.Color"));
            sb.Begin();
        }

        private void passwordBoxUnFocus(object sender, RoutedEventArgs e)
        {
            Storyboard sb = new Storyboard() { Duration = TimeSpan.FromSeconds(0.7), BeginTime = TimeSpan.Zero };

            ColorAnimation colAnim = new ColorAnimation();
            colAnim.To = System.Windows.Media.Color.FromArgb(255, 177, 177, 177);
            colAnim.Duration = new Duration(TimeSpan.FromSeconds(0.7));
            colAnim.AutoReverse = false;

            sb.Children.Add(colAnim);

            Storyboard.SetTarget(colAnim, kvadrat2);
            Storyboard.SetTargetProperty(colAnim, new PropertyPath("Fill.Color"));
            sb.Begin();
        }

        private void prijavaMouseEnter(object sender, MouseEventArgs e)
        {
            ButtonColor = LightFirmColor;
        }

        private void prijavaMouseLeave(object sender, MouseEventArgs e)
        {
            ButtonColor = FirmColor;
        }

        private void labelEnter(object sender, MouseEventArgs e)
        {
            LabelColor = LightFirmColor;
        }

        private void labelLeave(object sender, MouseEventArgs e)
        {
            LabelColor = FirmColor;
        }
        #endregion

        #region EventsLogic
        private void MetroWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
            {
                prijaviSeButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }

        private void prijaviSe(object sender, RoutedEventArgs e)
        {
            string inputUsername = usernameTextBox.Text;
            string inputPassword = sha256(passBox.Password);

            if (DataSourceUtil.Instance.ReadLicence())
            {
                if (inputUsername == "admin" && inputPassword == sha256("admin"))
                {
                    User u = new User();
                    u.Username = inputUsername;
                    u.HashPassword = inputPassword;//ovo sve popravi
                    u.Role = SecurityManager.Role.Admin;
                    MainWindow mw = new MainWindow(u);
                    mw.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Vaša licenca je istekla. Javite se osobi koja je napravila softver ako želite da produžite licencu.");
                Application.Current.Shutdown();
            }
        }

        #endregion EventsLogic

        public string sha256(string randomString)
        {
            System.Security.Cryptography.SHA256Managed crypt = new System.Security.Cryptography.SHA256Managed();
            System.Text.StringBuilder hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(randomString), 0, Encoding.UTF8.GetByteCount(randomString));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
    }
}
