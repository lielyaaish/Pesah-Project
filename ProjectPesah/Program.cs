using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPesah
{
    class Program
    {
        static void Main(string[] args)
        {

            // My Passover Project
            SQLconnect2 PassoverProject = new SQLconnect2();

            Console.WriteLine("Welcome to my Passover Project! :)");
            Console.WriteLine("Please write two number to count the SUM:");
            int x = 0;
            int y = 0;

            do
            {
                if (x > 0)
                {
                    PassoverProject.AddxNumberToTable(x);
                }

                Console.Write($"Number A: ");
                x = Convert.ToInt32(Console.ReadLine());
                Console.Write($"Number B: ");
                y = Convert.ToInt32(Console.ReadLine());
                int c = x + y;

                Console.WriteLine($"SUM Answer: {x} + {y} = {c}");
                //Console.WriteLine($"Minus SUM Answer: {x} - {y} = {m}");
                Console.WriteLine();
                Console.WriteLine("----- NEW Numbers: -----");
                if (c <= 0)
                {
                    Console.WriteLine("--- Please write a number bigger then 0 ---");
                }
                Console.WriteLine();
            } while ((x >= 0) && (y >= 0));


            SQLconnect2.WriteAllResultsNumbers();
            //Close SQL connection
            SQLconnect2.Close();

        }
    }
}
