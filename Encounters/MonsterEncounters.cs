﻿using Labb3.UtilityTools;
using System;
using System.Collections.Generic;
using System.Text;
using Labb3.Monsters;
using Labb3.Items;
using Labb3.Character;
using Labb3.Store;
using static System.Threading.Thread;
using Labb3.Menues;

namespace Labb3.Encounters
{
    public class MonsterEncounters
    {
        static private Random rnd = new Random();


        public static void EncounterGenerator()
        {
            Console.Clear();

            List<string> monsterNames = new List<string>() { "Goblin", "Thief", "Banshee", "Cultist", "Mutant", "Hell Hound", "Elder Thing", "Deep One", " Silent One", "Necromancer", "Deci", "Ogre", "Gargoyle", "Troll", "Nymph", "Kobold", "Satyr", "Decided Rat", "Giant Spider", "Rabid Goblin", "Giant Spider" };
            List<string> lvl10Monsters = new List<string>() { "Azathoth", "B'gnu-Thun", "Bokrug", "Cthulhu", "Dagon", "Dimensional Shambler", "Dunwich Horror", "Formless Spawn", "Ghatanothoa", "Gloon", "Gnoph-Keh", "Great Old One", "Yog-Sothoth", "Yuggoth", "Innsmouth", "Shoggoth", "Outer God", "Nightgaut", "Nyarlathotep" };

            int[] expDropArray = new int[9] { 20, 55, 66, 100, 160, 266, 458, 800, 1422 }; //will use player lvl as index to face off against same lvl monster
                                                                                           //exp to lvl 

            int monsterIndex = rnd.Next(0, monsterNames.Count);

            if (Player.player.Lvl < 9)
            {
                Monster monster = new Monster()//Balance this 
                {
                    name = monsterNames[monsterIndex],
                    lvl = Player.player.Lvl,
                    hp = Player.player.Hp * 2,
                    dmg = Player.player.Hp / 2,
                    expDrop = expDropArray[Player.player.Lvl],
                    goldDrop = 100 * Player.player.Lvl
                };

                Tools.YellowLine("You decide to keep exploring the god forsaken dungeon..");
                Tools.YellowLine("You grab the doorknob to the next room and slowly turn it..");
                Tools.YellowLine("When you hear the door klick, you push open the door, ready to face whatever stands before you.");
                Tools.YellowLine("There before you stands a hideous creature..\n");

                StatsDuringFight(monster);

                Tools.BlueLine("Press to return");//Test
                Console.ReadKey();

            }
            else if (Player.player.Lvl >= 9)
            {
                Monster monster = new Monster()//Balance this
                {
                    name = monsterNames[monsterIndex],
                    lvl = Player.player.Lvl,
                    hp = 10000,
                    dmg = 500,
                    expDrop = 10000,
                    goldDrop = 10000
                };

                Tools.YellowLine("You decide to keep exploring the god forsaken dungeon..\n" +
                    "You grab the doorknob to the next room and slowly turn it..\n" +
                    "When you hear the door klick, you push open the door, ready to face whatever stands before you.\n" +
                    "There before you stands a hideous creature..\n");

                StatsDuringFight(monster);

                Tools.BlueLine("Press to return");//Test
                Console.ReadKey();

                Fight(monster, monsterIndex);

            }

            static void StatsDuringFight(Monster monster)
            {
                Tools.YellowLine("-----------------------------");
                Tools.Yellow("|| ");
                Console.WriteLine($"{monster.name}");
                Tools.Yellow("||");
                Tools.RedLine($"Level: {monster.lvl}");
                Tools.Yellow("||");
                Tools.RedLine($"Health: {monster.hp}");
                Tools.YellowLine("-----------------------------");
                Tools.Yellow("|| ");
                Console.WriteLine($"{Player.player.Name}");
                Tools.Yellow("||");
                Tools.GreenLine($"Level: {Player.player.Lvl}");
                Tools.Yellow("||");
                Tools.GreenLine($"Health: {Player.player.Hp}");
                Tools.Yellow("||");
                Tools.GreenLine($"Healing Potions: {Player.player.HealingPotions} flasks");
                Tools.YellowLine("-----------------------------\n");
            }

            static void FightingMenueText()
            {
                Console.WriteLine("-----------------------");
                Console.WriteLine("|| [A]ttack......... ||");//The fighting is turn based. First you strike, than the monster will attack you
                Console.WriteLine("|| [B]lock Attack... ||");//Block attack, take reduced/no dmg, and deal some back
                Console.WriteLine("|| [H]ealing Potion..||");//Take a swig from a potion, heals up health. Takes reduced damage while healing
                Console.WriteLine("|| [R]un Away........||");//Tries to escape. Chanse to be hit on the way out
                Console.WriteLine("|| [E]xit Game.......||");
                Console.WriteLine("-----------------------");
            }
            static void Fight(Monster monster, int monsterIndex)
            {

                FightingMenueText();
                StatsDuringFight(monster);

                char input = Console.ReadKey().KeyChar;
                int monsterChanseOnHit = rnd.Next(1, 3); //33% chance to hit on escape
                int dodge = rnd.Next(1, 5); //20% that the attack is dodged
                int playerDmg = Player.player.Dmg + Player.player.WeaponDmg;

                switch (input)
                {
                    case ('a')://Attack

                        //Remove monster index?

                        while (Player.player.Alive == true)
                        {
                            Console.WriteLine($"You raise your {Weapon.weapon.WeaponList[Player.player.WeaponIndex].Name} and attack the {monster.name}!");



                            if (dodge == 1)
                            {
                                Console.WriteLine("As you strike you miss your attack..");
                                Console.WriteLine($"The {monster.name} strikes you while you gather your wits");
                                Player.player.Hp -= monster.dmg / 2; //Player takes half monster dmg                               
                            }
                            else
                            {
                                Console.WriteLine($"As you strike the {monster.name} it screams in pain.");
                                Console.WriteLine($"The {monster.name} was delt {playerDmg}.");
                                monster.hp -= playerDmg;
                            }

                            if (Player.player.Hp <= 0)
                            {
                                Player.player.Alive = false;
                            }

                            if (monster.hp <= 0)
                            {
                                Console.WriteLine($"As you prepare for one more attack on the {monster.name}\n" +
                                    $"you suddenly come to a halt, as you see the lifeless corpse of your foe.\n" +
                                    $"It has fallen dead onto the floor and blood are seeping out from its open wounds..");
                                Console.WriteLine($"+ {monster.expDrop} experience poionts!");
                                Console.WriteLine($"+ {monster.goldDrop} gold added to pouch!");

                                Player.player.Exp += monster.expDrop;
                                Player.player.Gold += monster.goldDrop;
                                Player.ExpToLvl(Player.player.Exp); //Cheks if you can level up
                            }

                            //Now the monster will attack back!
                            Console.WriteLine($"The {monster.name} strikes you for {monster.dmg}!");
                            Player.player.Hp -= monster.dmg;
                            Fight(monster, monsterIndex);
                        }

                        break;

                    case ('b')://Block

                        Console.WriteLine($"You raise your {Weapon.weapon.WeaponList[Player.player.WeaponIndex].Name} in a defensive stance.");
                        if (monsterChanseOnHit == 1)//If monster hits
                        {
                            Console.WriteLine($"The {monster.name} strikes you with all their power and hits you for {monster.dmg / 2} damage.");
                            Player.player.Hp -= monster.dmg / 2;

                        }
                        else if (monsterChanseOnHit > 1) //Miss
                        {
                            Console.WriteLine($"The {monster.name} misses you with their attack and you frantically strikes back");
                            Console.WriteLine($"You hit the {monster.name} for {playerDmg / 2} damage.");
                            monster.hp -= playerDmg / 2;
                        }
                        Sleep(2000);
                        break;

                    case ('h')://Heal
                               //heal.
                        Sleep(2000);
                        break;

                    case ('r')://Run away

                        Console.WriteLine("With darting eyes you look for the door you just came in from.");
                        Console.WriteLine("You scurry around and head for the exit.");
                        Console.WriteLine($"But as you do the {monster.name} takes a swing at you!");

                        if (monsterChanseOnHit == 1) //Hit
                        {
                            Console.WriteLine($"The sweeping strike from the {monster.name} hits you for {monster.dmg / 2}.");
                            Player.player.Hp -= monster.dmg / 2;
                        }
                        else if (monsterChanseOnHit > 1)//Miss
                        {
                            Console.WriteLine("The {monster.name} barely misses you, as you swing the door shut.");
                            Sleep(2000);
                            MenuOptions.Options(); //Back to menu
                        }
                        Sleep(2000);
                        break;

                    case ('e')://Exit Game

                        Tools.ExitGame();
                        break;
                }

            }


        }
    }
}
