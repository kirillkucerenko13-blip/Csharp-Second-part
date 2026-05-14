using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Task2.GameDevObserver;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Player player = new Player();
            UIHealthBar healthBar = new UIHealthBar();
            SoundSystem sound = new SoundSystem();
            AchievementSystem achievements = new AchievementSystem();
            GameLogger logger = new GameLogger();

            healthBar.Connect(player);
            sound.Connect(player);
            achievements.Connect(player);
            logger.Connect(player);

            player.TakeDamage(30); 
            player.TakeDamage(25); 
            player.TakeDamage(30); 
            player.TakeDamage(20); 

    
        }
    }




}