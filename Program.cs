using System;

namespace Lab5_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            char choice;
            string userName;

            while (true)
            {
                Console.Write("Do you want to log in (l) or sign up (s) (e to exit)?: ");
                choice = Console.ReadKey().KeyChar;
                Console.WriteLine("\n\n");

                switch (choice)
                {
                    case 'l':
                    case 's':
                        Console.Write("Enter your username: ");
                        userName = Console.ReadLine();
                        UserAuthentication.AuthenticateUser(userName, choice);
                        break;
                    case 'e':
                        break;
                    default:
                        Console.WriteLine("Error! Unrecognized command");
                        break;
                }

                if (choice == 'e') break;
            }
        }
    }

    internal static class UserAuthentication
    {
        public static void AuthenticateUser(string username, char action)
        {
            // Simulate user authentication logic
            if (action == 'l')
            {
                Console.WriteLine($"Welcome back, {username}! You are now logged in.");
            }
            else if (action == 's')
            {
                Console.WriteLine($"Account created for {username}. You are now signed up.");
            }
        }
    }
}
