using CommonLib;
namespace HillClimberDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                Random rand = new();
                Console.WriteLine("Enter a initial string:");
                string initial = Console.ReadLine();
                Console.WriteLine("Enter a target string:");
                string target = Console.ReadLine();
                var initialList = initial.ToDoubleList();
                var targetList = target.ToDoubleList();
                double mae = initialList.MAE(targetList);
                Console.WriteLine($"Current: {initial} -> Target: {target} | MAE: {mae}");
                while (mae > 0)
                {
                    List<double> lastState = new(initialList);
                    Console.Write("Current: ");
                    initialList[rand.Next(initialList.Count)] += Math.Round(rand.NextDouble() * 2 - 1);
                    foreach(char c in initialList)
                    {
                        Console.Write(c);
                    }
                    Console.WriteLine($" -> {target} | MAE: {mae}");
                    if(initialList.MAE(targetList) < mae)
                    {
                        Console.WriteLine($"Improvement found! Last MAE: {mae} -> New MAE: {initialList.MAE(targetList)}");
                        mae = initialList.MAE(targetList);
                    }
                    else
                    {
                        Console.WriteLine($"No improvement. Last MAE: {mae} < New MAE: {initialList.MAE(targetList)}");
                        initialList = lastState;
                    }
                }
                Console.WriteLine($"Target string '{target}' reached from initial string '{initial}'!");
            }
        }
    }
}
