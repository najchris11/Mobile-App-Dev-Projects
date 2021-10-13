using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace cs {
    public class TimeChangeEventArgs : EventArgs {
        public int tics;
        public TimeChangeEventArgs(int t) {
            tics = t;
        }
    }
    public class Clock {
        int tics = 0;
        public delegate void TimeChangeHandler(object clock, EventArgs args);
        public event TimeChangeHandler OnTimeChange;
        public void Run() {
            for (int i = 0; i < 50; i++) {
                Thread.Sleep(100);
                tics++;
				TimeChangeEventArgs args = new TimeChangeEventArgs(tics);
				OnTimeChange?.Invoke(this, args);   // Same as below
				//if (OnTimeChange != null) {
				//	OnTimeChange(this, args);
				//}
			}
		}
    }
    public class Client1 {
        public Client1(Clock clock) {
            clock.OnTimeChange += Client1EventHandler;
        }
        public void Client1EventHandler(object sender, EventArgs args) {
            TimeChangeEventArgs tcEventArgs = (TimeChangeEventArgs)args;
            Console.WriteLine("Client1: " + tcEventArgs.tics);
        }
    }
    public class Client2 {
        public Client2(Clock clock) {
            clock.OnTimeChange += Client2EventHandler;
        }
        public void Client2EventHandler(object sender, EventArgs args) {
            TimeChangeEventArgs tcEventArgs = (TimeChangeEventArgs)args;
            if (tcEventArgs.tics % 10 == 0)
                Console.WriteLine("Client2: " + tcEventArgs);
        }
    }
    public class Events {
        public static void EventsMain(string[] args) {
            Console.WriteLine("Events");
            Clock theClock = new Clock();
            Client1 c1 = new Client1(theClock);
            Client2 c2 = new Client2(theClock);
            theClock.Run();
        }
    }
}
