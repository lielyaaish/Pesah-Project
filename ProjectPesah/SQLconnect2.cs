using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPesah
{
    public class SQLconnect2 : InterFaceNumberAddingToTABLE
    {
        static SqlCommand cmd = new SqlCommand();

        static SQLconnect2()
        {
            cmd.Connection = new SqlConnection(@"Data Source=.;Initial Catalog=Passover Project;Integrated Security=True");

            cmd.Connection.Open();
        }

        public static void Close()
        {
            cmd.Connection.Close();
        }

        //Adding x User Number
        public void AddxNumberToTable(int x)
        {
            SqlCommand cmd2 = new SqlCommand();
            cmd2.Connection = new SqlConnection(@"Data Source=.;Initial Catalog=Passover Project;Integrated Security=True");

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"INSERT INTO X_TABLE (UserNumberX) VALUES ({x})";
            cmd.ExecuteNonQuery();

            cmd2.Connection.Close();
        }

        //Adding y User Number
        public void AddyNumberToTable(int y)
        {
            SqlCommand cmd2 = new SqlCommand();
            cmd2.Connection = new SqlConnection(@"Data Source=.;Initial Catalog=Passover Project;Integrated Security=True");

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"INSERT INTO Y_TABLE (UserNumberY) VALUES ({y})";
            cmd.ExecuteNonQuery();

            cmd2.Connection.Close();
        }

        //Adding User Numbers to The Results TABLE
        public void AddingNumberToTheResults(object NumbersResult)
        {
            dynamic UserNumbers = NumbersResult;
            SqlCommand cmd2 = new SqlCommand();
            cmd2.Connection = new SqlConnection(@"Data Source=.;Initial Catalog=Passover Project;Integrated Security=True");

            //Open Connection
            cmd2.Connection.Open();

            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = ($"INSERT INTO SUMresults (X,OPERATION,Y) VALUES ({UserNumbers.X},'{UserNumbers.Operation}',{UserNumbers.Y}");
            cmd2.ExecuteNonQuery();

            //Close Connection
            cmd2.Connection.Close();
        }

        //Updating the Results x,y SUM TABLE
        public void UpdatingSUMNumbersToTheTables()
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"SELECT XTable.X X" +
                              "Operation.Operation" +
                              "YTable.Y Y " +
                              "FROM XTable " +
                              "Cross Join Operation" +
                              "Cross Join YTABLE" +
                              "WHERE x >= 0 AND y >= 0";
            //Numbers Readers
            using (SqlDataReader NumbersReader = cmd.ExecuteReader(CommandBehavior.Default))
            {
                while (NumbersReader.Read())
                {
                    var NumbersResult = new
                    {
                        x = (int)NumbersReader["x"],
                        y = (int)NumbersReader["y"],
                        Operation = (string)NumbersReader["Operation"]
                    };

                    AddingXYintoTheTABLEresults(NumbersResult);
                }
            }
        }

        public void AddingXYintoTheTABLEresults(object ResultTable)
        {
            dynamic UserNumbers = ResultTable;
            SqlCommand cmd2 = new SqlCommand();
            cmd2.Connection = new SqlConnection(@"Data Source=.;Initial Catalog=Passover Project;Integrated Security=True");
            
            //Open SQL connection
            cmd2.Connection.Open();

            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = $"INSERT INTO SUMresults" +
                                "(X, Y, OPERATION) Values" +
                                "({UserNumbers.X}," +
                                "{UserNumbers.Y}," +
                                "'{UserNumbers.Operation}')";
            cmd2.ExecuteNonQuery();

            //Closing SQL connection
            cmd2.Connection.Close();
        }

        public void UpdateIDnumbersIntoResults(double result, int ResultID)
        {
            SqlCommand cmd3 = new SqlCommand();
            cmd3.Connection = new SqlConnection(@"Data Source=.;Initial Catalog=Passover Project;Integrated Security=True");
            
            //Open SQL connection
            cmd3.Connection.Open();
            cmd3.CommandType = CommandType.Text;
            cmd3.CommandText = $"UPDATE SUMresults SET results = {result}" +
                                "WHERE ID = {ResultID}";
            cmd3.ExecuteNonQuery();

            //Close SQL connection
            cmd3.Connection.Close();
        }


        // Hishuvim - + * /
        double Hishuvim(object X, object Operation, object Y)
        {

            switch (Operation)
            {
                case "Minus":
                    {
                        int x = (Convert.ToInt32(X));
                        int y = (Convert.ToInt32(Y));

                        if ((x > 0) && (y>0))
                            return (x - y);
                        return -1;

                        /*
                        if ((y >= 0) && (x >= 0))
                        {
                            return (x - y);
                        }
                        return -1;
                        */
                    }

                case "SUM": return ((Convert.ToInt32(X)) + (Convert.ToInt32(Y)));
                case "Kefel": return ((Convert.ToInt32(X)) * (Convert.ToInt32(Y)));
                case "Lehalek": return ((Convert.ToInt32(X)) / (Convert.ToInt32(Y)));

                default: return (Convert.ToInt32(X)) + (Convert.ToInt32(Y));
            }
        }

        //Updaing the results Values
        public void UpdateTABLEresults()
        {
            double SUMresults = 0d;
            int IDforResults = 1;

            SqlCommand cmd2 = new SqlCommand();
            cmd2.Connection = new SqlConnection(@"Data Source=.;Initial Catalog=Passover Project;Integrated Security=True");

            //Open Connection cmd2
            cmd2.Connection.Open();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = $"SELECT * FROM NumbersResult";
            SqlDataReader reader = cmd2.ExecuteReader(CommandBehavior.Default);

            while (reader.Read())
            {
                var TableNumbersResults = new
                {
                    X = reader["X"],
                    Y = reader["Y"],
                    Operation = reader["Operation"]
                };
                SUMresults = Hishuvim(reader["X"], reader["NumbersReader"], reader["Y"]);
                UpdateIDnumbersIntoResults(SUMresults, IDforResults++);
            }
        }

        public void MoveAllResultoSQL()
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"SELECT * FROM SUMresults";
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
            while (reader.Read())
            {
                Console.WriteLine($"ID {reader["ID"]}" +
                                    $"X {reader["X"]}" +
                                    $"NumbersReader {reader["NumbersReader"]}" +
                                    $"y {reader["y"]}" +
                                    $"Result {reader["Result"]}");
            }
        }

        public static void WriteAllResultsNumbers()
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"SELECT * FROM SUMresults";
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
            while (reader.Read())
            {
                Console.WriteLine($"ID: {reader["ID"]}" +
                                  $"x.Number1: {reader["X"]}" +
                                  $"y.Number2: {reader["Y"]}" +
                                  $"Operation: {reader["Operation"]}" +
                                  $"Result: {reader["Result"]}");
            }
        }

        //THE END OF PESAH PROJECT! :)

    }
}
