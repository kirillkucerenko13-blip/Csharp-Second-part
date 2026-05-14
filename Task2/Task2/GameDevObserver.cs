using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    internal class GameDevObserver
    {
        public class Player
        {
            public delegate void DamageEventHandler(int damageTaken, int currentHealth);
            public event DamageEventHandler OnDamaged;

            public int Health { get; private set; } = 100;

            public void TakeDamage(int amount)
            {
                Health -= amount;
                if (Health < 0) Health = 0;

                Console.WriteLine($"\n[Гравець]: Отримано {amount} урону! Залишилось HP: {Health}");

                OnDamaged?.Invoke(amount, Health);
            }
        }

        public class UIHealthBar
        {
            public void Connect(Player player)
            {
                player.OnDamaged += OnPlayerDamaged;
            }

            private void OnPlayerDamaged(int damage, int health)
            {
                Console.WriteLine($"[UI]: Оновлення здоров'я... Поточне HP: {health}%");
            }
        }

        public class SoundSystem
        {
            public void Connect(Player player)
            {
                player.OnDamaged += OnPlayerDamaged;
            }

            private void OnPlayerDamaged(int damage, int health)
            {
                Console.WriteLine("[Звук]: Відтворення звуку 'Ouch!'");

                if (health <= 20 && health > 0)
                {
                    Console.WriteLine("[Звук]: УВАГА! Відтворення тривожного серцебиття (Критичний стан!)");
                }
            }
        }
        public class AchievementSystem
        {
            private bool _halfHealthUnlocked = false;
            private bool _firstDeathUnlocked = false;

            public void Connect(Player player)
            {
                player.OnDamaged += OnPlayerDamaged;
            }

            private void OnPlayerDamaged(int damage, int health)
            {
                if (health <= 50 && !_halfHealthUnlocked)
                {
                    Console.WriteLine("[Досягнення]: Розблоковано 'Half Health' (Половина здоров'я)!");
                    _halfHealthUnlocked = true;
                }

                if (health <= 0 && !_firstDeathUnlocked)
                {
                    Console.WriteLine("[Досягнення]: Розблоковано 'First Death' (Перша смерть)!");
                    _firstDeathUnlocked = true;
                }
            }
        }

        public class GameLogger
        {
            public void Connect(Player player)
            {
                player.OnDamaged += OnPlayerDamaged;
            }

            private void OnPlayerDamaged(int damage, int health)
            {
                Console.WriteLine($"[Лог]: Запис у файл: Отримано {damage} урону, стан гравця: {health} HP.");
            }
        }
    }
}
