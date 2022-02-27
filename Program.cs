using System;
using System.IO;
using NLog.Web;

namespace ticketingSystemOOP
{
    class Program
    {   

        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
        static void Main(string[] args)
        {

            logger.Info("Program Started.");

            string file = "Tickets.csv";
            string choice;


            do
            {
                // ask user a question
                Console.WriteLine("1) Read data from file.");
                Console.WriteLine("2) Add data to file.");
                Console.WriteLine("Enter any other key to exit.");
                choice = Console.ReadLine();

                
                if (choice == "1")
                {
                    // read data from file
                    if (File.Exists(file))
                    {

                        // read data from file
                        StreamReader sr = new StreamReader(file);
                        while (!sr.EndOfStream)
                        {
                            string line = sr.ReadLine();
                            // convert string to array
                            //string[] arr = line.Split('|');
                            // display array data
                            Console.WriteLine(line);
                        }
                        sr.Close();
                    }
                    else
                    {
                        Console.WriteLine("File does not exist");
                        logger.Error("File does not exist.");
                    }
                }
                else if (choice == "2")
                {
                    // create file from data
                    StreamWriter sw = new StreamWriter(file, true);

                    Ticket ticket = new Ticket();

                    for (int i = 0; i < 7; i++)
                    {
                        // ask a question
                        Console.WriteLine("Enter a ticket (Y/N)?");
                        // input the response
                        string resp = Console.ReadLine().ToUpper();
                        // if the response is anything other than "Y", stop asking
                        if (resp != "Y") { break; }

                        Console.WriteLine("Enter Ticket ID (numbers)");
                        ticket.ticketID = Convert.ToInt16(Console.ReadLine());

                        Console.WriteLine("Enter Ticket Summary");
                        ticket.summary = Console.ReadLine();
                    
                        Console.WriteLine("Enter Ticket Status (Open, Closed)");
                        ticket.status = Console.ReadLine();

                        Console.WriteLine("Enter Ticket Priority");
                        ticket.priority = Console.ReadLine();

                        Console.WriteLine("Enter Your Name (Ticket Submitter, First and Last Name)");
                        ticket.submitter = Console.ReadLine();

                        Console.WriteLine("Enter who has been assigned the Ticket (Seperate Multiple Entries with comma, include First and Last name)");
                        ticket.assigned = Console.ReadLine();

                        Console.WriteLine("Enter who is watching the ticket (Seperate Multiple Entries with |, include First and Last name) ");
                        ticket.watching = Console.ReadLine();

                        sw.WriteLine($"{ticket.ticketID},{ticket.summary},{ticket.status},{ticket.priority},{ticket.submitter},{ticket.assigned},{ticket.watching}");
                    }
                    sw.Close();
                }
                logger.Info("User choice: {Choice}", choice);
            } while (choice == "1" || choice == "2");

            logger.Info("Program Ended.");
        }
    }
}
