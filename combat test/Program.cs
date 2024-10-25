// See https://aka.ms/new-console-template for more information
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
//health
int player_hp = 20;
int enemy_hp = 30;

//damage
int player_damage = 0;
int enemy_damage = 4;

//cards
bool firePicked = false;
bool icePicked = false;
bool lightningPicked = false;
bool bluntPicked = false;
string spells = "";
string fire = "fire(3)";
string ice = "ice(2)";
string lightning = "lightning(4)";
string blunt = "blunt(1)";
Random random = new();

//fusions
bool firefusion = true;
bool icefusion = true;
bool lightningfusion = true;
bool bluntfusion = true;

//items
int potions = 3;
int poison = 1;
int poison_timer = 0;





while (player_hp > 0 && enemy_hp > 0)
{
    string action = "";

    // Loop until a valid action is chosen
    while (action != "Attack" && action != "A" && action != "a" &&
       action != "Defend" && action != "D" && action != "d" &&
       action != "Item" && action != "I" && action != "i")
    {
        Console.WriteLine("Please choose: Attack (A), Defend (D), Item (I)");
        action = Console.ReadLine();

        if (action != "Attack" && action != "A" && action != "a" &&
            action != "Defend" && action != "D" && action != "d" &&
            action != "Item" && action != "I" && action != "i")
        {
            Console.WriteLine("Invalid action. Please choose again.");
        }
    }

    switch (action)
    {
        case "Attack":
        case "attack":
        case "A":
        case "a":
            // player damage calculation
            while (spells != "end" && spells != "back") // "back" option added
            {
                Console.WriteLine(fire + " " + ice + " " + lightning + " " + blunt + ", end, or back to return");
                spells = Console.ReadLine();
                Console.WriteLine();

                if (spells == "fire" && !firePicked)
                {
                    player_damage += 3;
                    firePicked = true;
                    fire = "";
                    firefusion = true;
                }
                else if (spells == "fire" && firePicked == true)
                {
                    Console.WriteLine("you already picked this card");
                    Console.WriteLine();
                }

                if (spells == "ice" && !icePicked)
                {
                    player_damage += 2;
                    icePicked = true;
                    ice = "";
                    icefusion = true;
                }
                else if (spells == "ice" && icePicked == true)
                {
                    Console.WriteLine("you already picked this card");
                    Console.WriteLine();
                }

                if (spells == "lightning" && !lightningPicked)
                {
                    player_damage += 4;
                    lightningPicked = true;
                    lightning = "";
                    lightningfusion = true;
                }
                else if (spells == "lightning" && lightningPicked == true)
                {
                    Console.WriteLine("you already picked this card");
                    Console.WriteLine();
                }

                if (spells == "blunt" && !bluntPicked)
                {
                    player_damage += 1;
                    bluntPicked = true;
                    blunt = "";
                    bluntfusion = true;
                }
                else if (spells == "blunt" && bluntPicked == true)
                {
                    Console.WriteLine("you already picked this card");
                    Console.WriteLine();
                }
            }

            if (spells == "back")
            {
                spells = "";
                continue; // Return to the main action loop if "back" was chosen
            }

            //spell fusions
            // error after fusion is done and the cards haven't reset fusion damage is still done
            if (firePicked == true && icePicked == true && firefusion == true && icefusion == true)
            {
                player_damage -= 5;
                player_damage += 10;
                firefusion = false;
                icefusion = false;
            }
            if (lightningPicked == true && bluntPicked == true && lightningfusion == true && bluntfusion == true)
            {
                player_damage -= 5;
                player_damage += 7;
                lightningfusion = false;
                bluntfusion = false;
            }
            if (icePicked == true && bluntPicked == true && firePicked == false && icefusion == true && bluntfusion == true)
            {
                player_damage -= 3;
                player_damage += 4;
                icefusion = false;
                bluntfusion = false;
            }
            //player damage 
            Console.WriteLine("you did " + player_damage + " damage");
            enemy_hp -= player_damage;
            Console.WriteLine("enemy has " + enemy_hp + " health left");
            Console.WriteLine();
            player_damage = 0;
            spells = "";


            break;

        case "Defend":
        case "defend":
        case "D":
        case "d":
            //reducing incoming damage
            string Defensive_action = "";

            while (Defensive_action != "Y" && Defensive_action != "y" &&
                   Defensive_action != "Yes" && Defensive_action != "YES" &&
                   Defensive_action != "yes" && Defensive_action != "N" && 
                   Defensive_action != "n" && Defensive_action != "No" && 
                   Defensive_action != "NO" && Defensive_action != "no")
            {
                Console.WriteLine("do you want to defend Y/N (damage -2) or back to return");
                Defensive_action = Console.ReadLine();
                Console.WriteLine();

                if (Defensive_action != "Y" && Defensive_action != "y" &&
                   Defensive_action != "Yes" && Defensive_action != "YES" &&
                   Defensive_action != "yes" && Defensive_action != "N" &&
                   Defensive_action != "n" && Defensive_action != "No" && 
                   Defensive_action != "NO" && Defensive_action != "no")
                {
                    Console.WriteLine("Invalid action. Please choose again.");
                    Console.WriteLine();
                }
            }

            if (Defensive_action == "N" || Defensive_action == "n" || Defensive_action == "No" || Defensive_action == "NO" || Defensive_action == "no")
            {
                Defensive_action = "";
                continue; // Return to the main action loop if "back" was chosen
            }
            enemy_damage -= 2;
            break;

        case "Item":
        case "item":
        case "I":
        case "i":
            string item_used = "";
            

            while (item_used != "poison" && item_used != "poisons" &&
                  item_used != "potion" && item_used != "potions")
            {
                Console.WriteLine("what item do you want to use");
                Console.WriteLine("you have " + poison + " poisons");
                Console.WriteLine("you have " + potions + " potions");
                item_used = Console.ReadLine();
                Console.WriteLine();

                if (item_used != "poison" && item_used != "poisons" &&
                  item_used != "potion" && item_used != "potions")
                {
                    Console.WriteLine("Invalid action. Please choose again.");
                    Console.WriteLine();
                }
            }
            if (item_used == "poison" || item_used == "poisons")
            {
                //damage items
                poison_timer += 2;
            }
            else if (item_used == "potions")
            {
                //healing items
                Console.WriteLine("you have " + potions + " potions");
                if (player_hp < 10)
                {
                    player_hp += 10;
                    Console.WriteLine("You used a potion");
                    Console.WriteLine("you have " + player_hp + " health");
                    potions -= 1;
                }
                else if (player_hp > 10)
                {
                    player_hp = 20;
                    Console.WriteLine("You used a potion");
                    Console.WriteLine("you have " + player_hp + " health");
                    potions -= 1;
                }
            }

            if (item_used == "cancel") 
            {
                item_used = "";
                continue;
            }
            
            break;
    };
    // enemy damage
    player_hp -= enemy_damage;
    Console.WriteLine("the enemy did " + enemy_damage + " damage");
    Console.WriteLine("you have " + player_hp + " health");
    Console.WriteLine();
    enemy_damage = 4;

    //poison
    if (poison_timer > 0)
    {
        enemy_hp -= 3;
        Console.WriteLine("the enemy took 3 poison damage");
        Console.WriteLine("enemy has " + enemy_hp + " health left");
        Console.WriteLine();
        poison_timer--; 
    }
    // cards resetting for the next turn
    int i = 0;
    while (i < 3)
    {
        int random_number = random.Next(1, 11);

        if (random_number == 3)
        {
            firePicked = false;
            fire = "fire(3)";
        }
        if (random_number == 6)
        {
            icePicked = false;
            ice = "ice(2)";
        }
        if (random_number == 9)
        {
            lightningPicked = false;
            lightning = "lightning(4)";
        }

        i++;
    }
    //blunt status
    if (firePicked == true && icePicked == true && lightningPicked == true)
    {
        bluntPicked = false;
        blunt = "blunt(1)";
    }
    if (random.Next(1, 11) == 4 || random.Next(1, 11) == 7)
    {
        bluntPicked = false;
        blunt = "blunt(1)";
    }

    if (enemy_hp <= 0)
    {
        Console.WriteLine("you win");
    }
    if (player_hp <= 0)
    {
        Console.WriteLine("you died");
    }
}