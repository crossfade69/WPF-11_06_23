﻿using System;
using System.Collections.Generic;
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

namespace WpfProjekt.MVVM.View
{
    /// <summary>
    /// Logika interakcji dla klasy LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
            else
            {
                WindowState = WindowState.Maximized;
            }
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private bool isDragging;
        private Point startPoint;

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDragging = true;
            startPoint = e.GetPosition(null);
        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
        }

        private void WindowDrag(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Border_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging && e.LeftButton == MouseButtonState.Pressed)
            {
                Point currentPoint = e.GetPosition(null);
                Window window = Window.GetWindow(this);

                double offsetX = currentPoint.X - startPoint.X;
                double offsetY = currentPoint.Y - startPoint.Y;

                window.Left += offsetX;
                window.Top += offsetY;
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Password;

            if (ValidateLogin(username, password))
            {
                // Successful login
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show(); // Show the main program window
                Close(); // Close the login window
            }
            else
            {
                // Incorrect login
                MessageBox.Show("Incorrect username or password!");
            }
        }
        private bool ValidateLogin(string username, string password)
        {
            // Add your login validation logic here
            // For simplicity, let's assume the correct username is "admin" and password is "password"
            return (username == "admin" && password == "password");
        }

        private void UsernameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}

