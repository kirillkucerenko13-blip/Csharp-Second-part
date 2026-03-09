using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Task1.PaternObserver;

namespace Task1
{

    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            TemperatureSensor sensor = new TemperatureSensor();

            Display display = new Display();
            AirConditioner ac = new AirConditioner();
            SecuritySystem security = new SecuritySystem();

            display.Connect(sensor);
            ac.Connect(sensor);
            security.Connect(sensor);

            sensor.SetTemperature(22); 
            sensor.SetTemperature(10); 
            sensor.SetTemperature(45); 
            sensor.SetTemperature(2);  
        }
    }
}