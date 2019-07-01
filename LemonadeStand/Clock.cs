using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
namespace LemonadeStand
{
    class Clock
    {
        private DateTime currentTime;
        private DateTime alarmTime;
        public bool alarm;
        //static readonly object _locker = new object();
        private Thread T;

        public Clock(DateTime Alarm)
        {
            this.alarmTime = Alarm;
            this.alarm = false;
            Console.WriteLine($"Its {DateTime.Now.Hour}:{DateTime.Now.Minute}");
        }

        public void pause()
        {

        }

        public void setTimer()
        {
            this.alarm = false;
            this.alarmTime = DateTime.Now;
            this.alarmTime.Minute.Equals(this.alarmTime.Minute+1);
            //Thread T = new Thread(tick);
            //T.Start();
        }

        public bool tick()
        {
            System.Threading.Thread.Sleep(1000);
            currentTime = DateTime.Now;

            
                if (DateTime.Now.Second > 30 && DateTime.Now.Second < 31)
                {
                    Console.WriteLine("{0}:{1}", currentTime.Hour, currentTime.Minute);
                }

                if (currentTime.Minute - alarmTime.Minute >= 0 && currentTime.Hour - alarmTime.Hour >= 0)
                {
                Console.Write("yes");
                    this.alarm = true;
                    return true;
                }
            return false;
        }

        static void Beep()
        {
            for (int i = 0; i < 2000; i++)
            {
                System.Threading.Thread.Sleep(500);
                Console.Beep();
            }
        }
    }
}
