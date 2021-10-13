using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace cs {
    public class Coffee {
    }
    public class Toast {
    }
    public class Breakfast {
        public static Coffee PourCoffee() {
            Console.WriteLine("Starting to pour coffee");
            Coffee result = new Coffee();
            Console.WriteLine("Finishing to pour coffee");
            return result;
        }
        public static async Task FryEggsAsync(int numEggs) {
            Console.WriteLine("Starting eggs");
            await Task.Delay(2000);
            Console.WriteLine("Done with eggs");
        }
        public static async Task FryBaconAsync(int piecesOfBacon) {
            Console.WriteLine("Starting bacon");
            await Task.Delay(1500);
            Console.WriteLine("Done with bacon");
        }
        public static async Task<Toast> ToastBreadAsync(int numSlices) {
            Console.WriteLine("Starting toast");
            Toast toast = new Toast();
            await Task.Delay(1000);
            Console.WriteLine("Done with toast");
            return toast;
        }
        public static void ApplyButter(Toast t) {
            Console.WriteLine("Applying butter");
        }
        public static void ApplyJam(Toast t) {
            Console.WriteLine("Applying jam");
        }
        public static async Task<Toast> MakeToastWithButterAndJamAsync(int number) {
            var toast = await ToastBreadAsync(number);
            ApplyButter(toast);
            ApplyJam(toast);
            return toast;
        }
        public static async Task HaveBreakfast() {
            Coffee cup = PourCoffee();
            Console.WriteLine("coffee is ready");
            var eggsTask = FryEggsAsync(2);
            var baconTask = FryBaconAsync(3);
            var toastTask = MakeToastWithButterAndJamAsync(2);

            var allTasks = new List<Task> { eggsTask, baconTask, toastTask };
            while (allTasks.Any()) {
                Task finished = await Task.WhenAny(allTasks);
                if (finished == eggsTask) {
                    Console.WriteLine("eggs are ready");
                } else if (finished == baconTask) {
                    Console.WriteLine("bacon is ready");
                } else if (finished == toastTask) {
                    Console.WriteLine("toast is ready");
                }
                allTasks.Remove(finished);
            }
            Console.WriteLine("Breakfast is ready!");
        }
        public static void BreakfastMain(string [] args) {
            Task breakfast = HaveBreakfast();
            breakfast.Wait();
            // work(4.0);
            // Task lunch = HaveLunch();
            // lunch.Wait();

        }
    }
}
