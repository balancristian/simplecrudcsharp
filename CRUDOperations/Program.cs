using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DBconnect
{
    class ConnectToDB
    {
        private string localhost = "localhost";
        private string database = "csharp";
        private string user = "root";

        // create connection
        public string connection()
        {
            string connectionString = "server=" + localhost + ";" + "database=" + database + ";" + "uid=" + user + ";" + "pwd=";
            return connectionString;
        }
    }
    class Operation : ConnectToDB
    {
            // try connection 
            public void testConnection()
            {
                MySqlConnection tryConnection = new MySqlConnection(connection());
                try
                {
                    tryConnection.Open();
                    Console.WriteLine("Connection with database is establish");
                } catch(Exception ex)
                {
                    Console.WriteLine("Cannot open connection");
                }
            } 
            public string ReadCondition(string condition)
            {
            
               // see what operations were chosen
                switch (condition)
                {
                   case "i":
                     Console.WriteLine("Condition Insert");
                   break;
                   case "r":
                    Console.WriteLine("Condition Read");
                   break;
                   case "u":
                     Console.WriteLine("Condtion Update");
                   break;
                   case "d":
                     Console.WriteLine("Condition Delete");
                   break;
                  default:
                    Console.WriteLine("Please insert a correctly condition");
                    break;                  
                }
            return condition;
            }
          public void operationtData(string ReciveOperation, string connectionOperations)
          {
            MySqlConnection insertConnection = new MySqlConnection(connectionOperations);
            insertConnection.Open();
            string query = "";
            

            // Run operations 
            if (ReciveOperation == "i") // insert part
            {
                Console.WriteLine("Enter a name: ");
                string name = Console.ReadLine();
                Console.WriteLine("Enter a age: ");
                int age = int.Parse(Console.ReadLine());
                query = @"INSERT INTO tableInfo (name, age) VALUES (@name, @age)";
                MySqlCommand cmdInsert = new MySqlCommand(query, insertConnection); //creat command for operation in database
                cmdInsert.Parameters.AddWithValue("@name", name);
                cmdInsert.Parameters.AddWithValue("@age", age);
                cmdInsert.ExecuteNonQuery();
                Console.WriteLine("Values were added");
            }
            else if (ReciveOperation == "r") // read part
            {
                
                query = "SELECT * FROM tableInfo";
                MySqlCommand cmdRead = new MySqlCommand(query, insertConnection); //creat command for operation in database
                MySqlDataReader read = cmdRead.ExecuteReader();
                while (read.Read())
                {
                    Console.WriteLine("{0} {1} {2} ", read.GetInt32(0), read.GetString(1), read.GetInt32(2));
                }
            }
            else if (ReciveOperation == "u") // update part
            {
                Console.WriteLine("Enter a id: ");
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter a name: ");
                string name = Console.ReadLine();
                Console.WriteLine("Enter a age: ");
                int age = int.Parse(Console.ReadLine());
                query = @"UPDATE tableInfo SET name = @name,  age = @age WHERE id = @id";
                MySqlCommand cmdUpdate = new MySqlCommand(query, insertConnection); //creat command for operation in database
                cmdUpdate.Parameters.AddWithValue("@id", id);
                cmdUpdate.Parameters.AddWithValue("@name", name);
                cmdUpdate.Parameters.AddWithValue("@age", age);
                cmdUpdate.ExecuteNonQuery();
                Console.WriteLine("Values were updated");
            }
            else if(ReciveOperation == "d") // delete part
            {
                Console.WriteLine("Enter a id: ");
                int id = int.Parse(Console.ReadLine());             
                query = @"DELETE FROM tableInfo WHERE id = @id";
                MySqlCommand cmdDelete = new MySqlCommand(query, insertConnection); //creat command for operation in database
                cmdDelete.Parameters.AddWithValue("@id", id);
                cmdDelete.ExecuteNonQuery();
                Console.WriteLine("Values were deleted");
            }
          }
     
    }
        
    class Program
    {       
        static void Main(string[] args)
        {
            // Enter a condition for operation
            Console.WriteLine("Enter a condtion: ");
            string conditionOperation = Console.ReadLine();

            // Creat an object for all operation
            Operation start = new Operation();
            // create parameters to check conditinos
            string ReciveOperation = start.ReadCondition(conditionOperation);
            string connectionOperations = start.connection();
            start.testConnection();
            // call the method for operations
            start.operationtData(ReciveOperation, connectionOperations);
            Console.ReadLine();

     
        }
    }
}

