using System;

namespace Dhont_Method
{
    class Program
    {
        static void Main(string[] args)
        {
            // file path to text file o
            string filepath = @"Assessment1Data.txt";
             // Store values in a list of string
            List<string> file = File.ReadAllLines(filepath).ToList();

            // Puts each party into a list 
            List<Party> partys = new List<Party>();
            foreach (string line in file)
            {
                string[] items = line.Split(',');
                Party pobj = new Party(items[0], Convert.ToInt32(items[1]));
                partys.Add(pobj);
            }

            // User to enter threshold. Converts string to int 
            Console.WriteLine("\nEnter the threshold, if any, as a percentage.You can enter any number between 0 and 100.");
            int thresHold = Convert.ToInt32(Console.ReadLine());

            // User to enter how many seats they want to allocate
            Console.WriteLine("\nEnter the number of seats to be contested");
            int seatsCount = Convert.ToInt32(Console.ReadLine());

            // Dhon't method
            int totalVotes = SumOfVotes(partys);
            DisplayPercentageVotes(partys, thresHold, totalVotes);
            CalculateDhondt(partys, seatsCount);
            DisplayWinningParties(partys);

            Console.ReadKey();
        }





    }    }
}
