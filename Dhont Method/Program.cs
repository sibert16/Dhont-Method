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


        // Displays political partys 
        private static void DisplayWinningParties(List<Parties> partys)
        {
            foreach (Parties pobj in partys)
            {
                if (pobj.Seats > 0)
                {
                    Console.WriteLine(pobj);
                }
            }
        }

       
        private static int SumOfVotes(List<Parties> partys)
        {
            int totalVotes = 0;
            foreach (Parties pobj in partys)
            {
                totalVotes += pobj.Votes;
            }
            Console.WriteLine($"\nTotal votes counted : {totalVotes}");
            return totalVotes;
        }

        // Displays percent of votes for each party 
        private static void DisplayPercentageVotes(List<Parties> partys, int threshold, int totalvotes)
        {
            // Displays percent of votes for each party 
            Console.WriteLine($"Party(s) that have met the {threshold}% threshold :");
            foreach (Parties pobj in partys)
            {
                if (pobj.PercentOfVotes(totalvotes) > threshold)
                {
                    Console.WriteLine($"{pobj.Name} has {Math.Round(pobj.PercentOfVotes(totalvotes), 2)} % of total votes.");
                }

                else
                {
                    Console.WriteLine($"None");
                }

            }

        }

        // Method to do the main caclutions of the Dhon't method
        private static void CalculateDhondt(List<Parties> partys, int seatsCount)
        {
            // Find intial party with highest votes
            Parties biggestVote = partys.Aggregate((v1, v2) => v1.Votes > v2.Votes ? v1 : v2);
            biggestVote.Seats += 1;
            biggestVote.DivideParty();

            // Keep looping through partys and applying dhond't method until all seats are taken
            int totalSeatsCount = 0;
            while (totalSeatsCount != seatsCount)
            {
                Parties biggestVotes = partys.Aggregate((v1, v2) => v1.NewVotes > v2.NewVotes ? v1 : v2);
                biggestVotes.Seats += 1;
                biggestVotes.DivideParty();

                foreach (Parties pobj in partys)
                {
                    totalSeatsCount += pobj.Seats;
                }
                // If we havent reached desired seats count reset the total seats variable
                if (totalSeatsCount != seatsCount)
                {
                    totalSeatsCount = 0;
                }
            }
            Console.WriteLine($"\nWE HAVE {seatsCount} SEATS ALLOCATED:");
        }
    }
}



    }    }
}
