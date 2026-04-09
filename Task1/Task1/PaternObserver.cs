using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    internal class PaternObserver
    {
        public class TemperatureSensor
        {
            public delegate void TemperatureEventHandler(double temp);

            public event TemperatureEventHandler TemperatureChanged;

            public void SetTemperature(double temp)
            {
                Console.WriteLine($"\n[Датчик]: Виміряно температуру {temp}°C");
                TemperatureChanged?.Invoke(temp);
            }
        }
       
        public class Display
        {
            public void Connect(TemperatureSensor sensor)
            {
                sensor.TemperatureChanged += OnTemperatureChanged;
            }

            private void OnTemperatureChanged(double temp)
            {
                Console.WriteLine($"[Display]: {temp}°C");
            }
        }

        public class AirConditioner
        {
 
            public void Connect(TemperatureSensor sensor)
            {
                sensor.TemperatureChanged += OnTemperatureChanged;
            }

            private void OnTemperatureChanged(double temp)
            {
                if (temp < 17)
                    Console.WriteLine("[AC]: Увімкнено обігрів");
                else if (temp > 25)
                    Console.WriteLine("[AC]: Увімкнено охолодження");
                else
                    Console.WriteLine("[AC]: Очікування");
            }
        }
        public class SecuritySystem
        {
            public void Connect(TemperatureSensor sensor)
            {
                sensor.TemperatureChanged += OnTemperatureChanged;
            }

            private void OnTemperatureChanged(double temp)
            {
                if (temp > 40)
                    Console.WriteLine("[Security]: УВАГА! ПЕРЕГРІВ!");
                else if (temp < 5)
                    Console.WriteLine("[Security]: УВАГА! ЗАМЕРЗАННЯ!");
            }
        }
    }
}
