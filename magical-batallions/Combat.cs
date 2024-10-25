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

namespace Wpftest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {using System;
using System.Windows;
using System.Windows.Controls;

namespace WPFGame
    {
        public partial class MainWindow : Window
        {
            // Game variables
            private int player_hp = 20;
            private int enemy_hp = 30;
            private int player_damage = 0;

            private bool firePicked = false;
            private bool icePicked = false;
            private bool lightningPicked = false;
            private bool bluntPicked = false;

            //fusions
            bool firefusion = true;
            bool icefusion = true;
            bool lightningfusion = true;
            bool bluntfusion = true;

            public MainWindow()
            {
                InitializeComponent();
                UpdateHealthUI();
            }

            private void UpdateHealthUI()
            {
                PlayerHealth.Text = $"Player HP: {player_hp}";
                EnemyHealth.Text = $"Enemy HP: {enemy_hp}";
            }

            private void DisplayMessage(string message)
            {
                GameLog.Text = message;
            }

            private void AttackButton_Click(object sender, RoutedEventArgs e)
            {
                AttackOptionsPanel.Visibility = Visibility.Visible;
                AttackOptionsCancel.Visibility = Visibility.Visible;
                DisplayMessage("Choose your attack type: Fire, Ice, Lightning, or Blunt.");
            }

            private void FireButton_Click(object sender, RoutedEventArgs e)
            {
                if (!firePicked)
                {
                    player_damage += 3;
                    firePicked = true;
                    DisplayMessage("You picked Fire. Damage is now " + player_damage);
                }
                else
                {
                    DisplayMessage("Fire is already picked.");
                }
            }

            private void IceButton_Click(object sender, RoutedEventArgs e)
            {
                if (!icePicked)
                {
                    player_damage += 2;
                    icePicked = true;
                    DisplayMessage("You picked Ice. Damage is now " + player_damage);
                }
                else
                {
                    DisplayMessage("Ice is already picked.");
                }
            }

            private void LightningButton_Click(object sender, RoutedEventArgs e)
            {
                if (!lightningPicked)
                {
                    player_damage += 4;
                    lightningPicked = true;
                    DisplayMessage("You picked Lightning. Damage is now " + player_damage);
                }
                else
                {
                    DisplayMessage("Lightning is already picked.");
                }
            }

            private void BluntButton_Click(object sender, RoutedEventArgs e)
            {
                if (!bluntPicked)
                {
                    player_damage += 1;
                    bluntPicked = true;
                    DisplayMessage("You picked Blunt. Damage is now " + player_damage);
                }
                else
                {
                    DisplayMessage("Blunt is already picked.");
                }
            }

            private void EndButton_Click(object sender, RoutedEventArgs e)
            {
                CompleteAttack();
            }

            private void CompleteAttack()
            {
                AttackOptionsPanel.Visibility = Visibility.Collapsed;
                AttackOptionsCancel.Visibility = Visibility.Collapsed;

                //spell fusions
                // error after fusion is done and the cards haven't reset fusion damage is still done
                if (firePicked == true && icePicked == true && firefusion == true && icefusion == true)
                {
                    DisplayMessage("fire and ice fusion");
                    player_damage -= 5;
                    player_damage += 10;
                    firefusion = false;
                    icefusion = false;
                }
                if (lightningPicked == true && bluntPicked == true && lightningfusion == true && bluntfusion == true)
                {
                    DisplayMessage("lightning and blunt fusion");
                    player_damage -= 5;
                    player_damage += 7;
                    lightningfusion = false;
                    bluntfusion = false;
                }
                if (icePicked == true && bluntPicked == true && firePicked == false && icefusion == true && bluntfusion == true)
                {
                    DisplayMessage("ice and blunt fusion");
                    player_damage -= 3;
                    player_damage += 4;
                    icefusion = false;
                    bluntfusion = false;
                }

                // Apply damage to enemy and update UI
                enemy_hp -= player_damage;
                DisplayMessage("You did " + player_damage + " damage. Enemy has " + enemy_hp + " health left.");
                UpdateHealthUI();

                // Reset for the next attack
                player_damage = 0;
                firePicked = icePicked = lightningPicked = bluntPicked = false;

                // Check for win/lose conditions
                if (enemy_hp <= 0)
                {
                    DisplayMessage("You won!");
                }
                else if (player_hp <= 0)
                {
                    DisplayMessage("You lost!");
                }
            }

            private void DefendButton_Click(object sender, RoutedEventArgs e)
            {
                DisplayMessage("You chose to defend.");
                // Implement defense logic here
            }

            private void ItemButton_Click(object sender, RoutedEventArgs e)
            {
                DisplayMessage("You used an item.");
                // Implement item logic here
            }
        }
    }

    public MainWindow()
        {
            InitializeComponent();
        }
    }
}