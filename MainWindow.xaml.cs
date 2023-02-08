using System;
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

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private bool FirstStart = true;
        private bool IsYourSymbolX = true;
        private string PlayerSymbol = "";
        private string EnemySymbol = "";
        private string[,] FreeZones = { { "x1y3", "x2y3", "x3y3" }, { "x1y2", "x2y2", "x3y2" }, { "x1y1", "x2y1", "x3y1" } };
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if (FirstStart)
            {
                Start.Content = "Начать заново";
                FirstStart = false;
            }
            if (IsYourSymbolX)
            {
                PlayerSymbol = "x";
                EnemySymbol = "o";
                IsYourSymbolX = false;
            }
            else if (!IsYourSymbolX)
            {
                PlayerSymbol = "o";
                EnemySymbol = "x";
                IsYourSymbolX = true;

            }
            Clear(false);
            if (IsYourSymbolX)
            {
                RndFreeZone();
            }
        }
        private void Zone_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).Content = PlayerSymbol;
            (sender as Button).IsEnabled = false;
            switch ((sender as Button).Name)
            {
                case "x1y1":
                    FreeZones[0, 0] = "";
                    break;
                case "x2y1":
                    FreeZones[1, 0] = "";
                    break;
                case "x3y1":
                    FreeZones[2, 0] = "";
                    break;
                case "x1y2":
                    FreeZones[0, 1] = "";
                    break;
                case "x2y2":
                    FreeZones[1, 1] = "";
                    break;
                case "x3y2":
                    FreeZones[2, 1] = "";
                    break;
                case "x1y3":
                    FreeZones[0, 2] = "";
                    break;
                case "x2y3":
                    FreeZones[1, 2] = "";
                    break;
                case "x3y3":
                    FreeZones[2, 2] = "";
                    break;
            }
            bool win = CheckWinner();
            bool tide = CheckTide();
            if (!tide && !win)
            {
                RndFreeZone();
            }
        }
        public void RndFreeZone()
        {
            int x = 0;
            int y = 0;
            int[] mass = new int[2];
            do
            {
                mass = Rnd();
                x = (int)mass[0];
                y = (int)mass[1];
            } while (FreeZones[x, y] == "");
            RndTurn(x, y);
        }
        public int[] Rnd()
        {
            int[] mass = new int[2];
            Random rnd = new Random();
            mass[0] = rnd.Next(0, 3);
            mass[1] = rnd.Next(0, 3);
            return mass;
        }
        public void RndTurn(int x, int y)
        {
            switch (x)
            {
                case 0:
                    switch (y)
                    {
                        case 0:
                            x1y1.Content = EnemySymbol;
                            x1y1.IsEnabled = false;
                            break;
                        case 1:
                            x1y2.Content = EnemySymbol;
                            x1y2.IsEnabled = false;
                            break;
                        case 2:
                            x1y3.Content = EnemySymbol;
                            x1y3.IsEnabled = false;
                            break;
                    }
                    break;
                case 1:
                    switch (y)
                    {
                        case 0:
                            x2y1.Content = EnemySymbol;
                            x2y1.IsEnabled = false;
                            break;
                        case 1:
                            x2y2.Content = EnemySymbol;
                            x2y2.IsEnabled = false;
                            break;
                        case 2:
                            x2y3.Content = EnemySymbol;
                            x2y3.IsEnabled = false;
                            break;
                    }
                    break;
                case 2:
                    switch (y)
                    {
                        case 0:
                            x3y1.Content = EnemySymbol;
                            x3y1.IsEnabled = false;
                            break;
                        case 1:
                            x3y2.Content = EnemySymbol;
                            x3y2.IsEnabled = false;
                            break;  
                        case 2:
                            x3y3.Content = EnemySymbol;
                            x3y3.IsEnabled = false;
                            break;
                    }
                    break;
            }
            FreeZones[x, y] = "";
            CheckWinner();
            CheckTide();
        }
        public bool CheckWinner()
        {
            bool win = false;
            if (x1y1.Content == PlayerSymbol && x1y2.Content == PlayerSymbol && x1y3.Content == PlayerSymbol ||
                x1y1.Content == PlayerSymbol && x2y1.Content == PlayerSymbol && x3y1.Content == PlayerSymbol ||
                x1y1.Content == PlayerSymbol && x2y2.Content == PlayerSymbol && x3y3.Content == PlayerSymbol ||
                x1y3.Content == PlayerSymbol && x2y2.Content == PlayerSymbol && x3y1.Content == PlayerSymbol ||
                x2y1.Content == PlayerSymbol && x2y2.Content == PlayerSymbol && x2y3.Content == PlayerSymbol ||
                x3y1.Content == PlayerSymbol && x3y2.Content == PlayerSymbol && x3y3.Content == PlayerSymbol ||
                x1y2.Content == PlayerSymbol && x2y2.Content == PlayerSymbol && x3y2.Content == PlayerSymbol ||
                x1y3.Content == PlayerSymbol && x2y3.Content == PlayerSymbol && x3y3.Content == PlayerSymbol)
            {
                MessageBox.Show("Вы победили!");
                win = true;
                Clear(true);
            }
            else if (x1y1.Content == EnemySymbol && x1y2.Content == EnemySymbol && x1y3.Content == EnemySymbol ||
                x1y1.Content == EnemySymbol && x2y1.Content == EnemySymbol && x3y1.Content == EnemySymbol ||
                x1y1.Content == EnemySymbol && x2y2.Content == EnemySymbol && x3y3.Content == EnemySymbol ||
                x1y3.Content == EnemySymbol && x2y2.Content == EnemySymbol && x3y1.Content == EnemySymbol ||
                x2y1.Content == EnemySymbol && x2y2.Content == EnemySymbol && x2y3.Content == EnemySymbol ||
                x3y1.Content == EnemySymbol && x3y2.Content == EnemySymbol && x3y3.Content == EnemySymbol ||
                x1y2.Content == EnemySymbol && x2y2.Content == EnemySymbol && x3y2.Content == EnemySymbol ||
                x1y3.Content == EnemySymbol && x2y3.Content == EnemySymbol && x3y3.Content == EnemySymbol)
            {
                MessageBox.Show("Вы проиграли...");
                win = true;
                Clear(true);
            }
            return win;
        }
        public bool CheckTide()
        {
            bool Tide = true;
            List<Button> buttons = MyWindow.Children.OfType<Button>().ToList();
            foreach (var button in buttons)
            {
                if (button.Content == "")
                {
                    Tide = false;
                    break;
                }
                else
                {
                    Tide = true;
                }
            }
            if (Tide)
            {
                MessageBox.Show("Ничья.");
                Clear(true);
            }
            return Tide;
        }
        public void Clear(bool End)
        {
            List<Button> buttons = MyWindow.Children.OfType<Button>().ToList();
            foreach (var button in buttons)
            {
                button.Content = "";
                if (End)
                {
                    button.IsEnabled = false;
                }
                else
                {
                    button.IsEnabled = true;
                }
            }
            FreeZones = new [,]{ { "x1y3", "x2y3", "x3y3" }, { "x1y2", "x2y2", "x3y2" }, { "x1y1", "x2y1", "x3y1" } };
            Start.Content = "Начать заново";
            Start.IsEnabled = true;
        }
    }
}
