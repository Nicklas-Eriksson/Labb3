﻿using System;
using Labb3.Menues;
using Labb3.Character;
using Labb3.UtilityTools;
using Labb3.Story;
using static System.Threading.Thread;
using Labb3.Items;
using System.Collections.Generic;
using System.Media;


namespace Labb3.StartGame
{
    public static class Start
    {
        static public void Game()
        {
            //SoundPlayer player = new SoundPlayer();
            //player.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"C:\Users\salk1\Music\NinjaMusic.mp3";
            //player.Play();


            Console.Title = "Dungeons of Daggorath";

            //Logo.DoD();
            //Sleep(3500);
            //Logo.RdyP1();
            //Sleep(2500);
            //Messange.GameInfo();
            Tools.Yellow("Enter your name: ");
            Player.player.Name = Console.ReadLine().Trim();//Stor bokstav på första??
            Tools.YellowLine($"\nGreetings {Player.player.Name}..\n");

            Player.GodMode(); //Checks if user is admin or Robin
            /* Names to avtivate god mode: Hakk, hakk, Robin, robin, Robin Kamo, robin kamo */

            Console.ReadKey(); //Ta bort sen
            
            //Sleep(1400);
            //Messange.Intro();
            MenuOptions.Options();





        }
    }
}
