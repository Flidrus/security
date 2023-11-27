using System;
using System.Collections;
using System.Security.Cryptography;
using System.Text;

namespace LAB5_3
{
    internal class Program
    {
        static void Main()
        {
            UserList userList = new UserList(); // Список користувачів
            User currentUser = null; // Поточний користувач

            while (true)
            {
                Console.WriteLine("Available commands:");
                Console.WriteLine("s - Sign up");
                Console.WriteLine("l - Log in");
                Console.WriteLine("e - Log out");
                Console.WriteLine("Please, choose the necessary command:");
                string input = Console.ReadLine();

                switch (input.ToLower())
                {
                    case "s":
                        User newUser = SignUp(userList);
                        userList.Add(newUser);
                        break;
                    case "l":
                        currentUser = LogIn(userList);
                        break;
                    case "e":
                        currentUser = LogOut(currentUser);
                        break;
                }
            }
        }

        static User SignUp(UserList userList)
        {
            Console.Clear();
            Console.WriteLine("Signing up");
            Console.WriteLine("Please, enter your login:");
            string login = Console.ReadLine();
            Console.WriteLine("Please, enter your password:");
            string password = Console.ReadLine();
            User newUser = new User(login, password);
            Console.WriteLine("Success! You've been signed up.");
            return newUser;
        }

        static User LogIn(UserList userList)
        {
            Console.WriteLine("Please, enter your login:");
            string login = Console.ReadLine();
            Console.WriteLine("Please, enter your password:");
            string password = Console.ReadLine();

            User user = userList.GetUser(login);
            if (user != null && user.ValidatePassword(password))
            {
                Console.WriteLine("Success! Logged in as " + user.Login);
                return user;
            }
            else
            {
                Console.WriteLine("Invalid login or password!");
                return null;
            }
        }

        static User LogOut(User currentUser)
        {
            if (currentUser != null)
            {
                Console.WriteLine("Logging out " + currentUser.Login);
            }
            else
            {
                Console.WriteLine("No user is currently logged in.");
            }
            return null;
        }
    }

    public class User
    {
        public string Login { get; }
        private byte[] SaltedHashPassword { get; }

        public User(string login, string password)
        {
            Login = login;
            byte[] salt = PBKDF2.GenerateSalt();
            SaltedHashPassword = PBKDF2.HashPassword(Encoding.UTF8.GetBytes(password), salt, 10000);
        }

        public bool ValidatePassword(string password)
        {
            byte[] enteredPassword = PBKDF2.HashPassword(Encoding.UTF8.GetBytes(password), SaltedHashPassword, 10000);
            return StructuralComparisons.StructuralEqualityComparer.Equals(enteredPassword, SaltedHashPassword);
        }
    }

    public class UserList
    {
        private User[] users = new User[10];
        private int count = 0;

        public void Add(User user)
        {
            users[count++] = user;
        }

        public User GetUser(string login)
        {
            foreach (User user in users)
            {
                if (user != null && user.Login == login)
                {
                    return user;
                }
            }
            return null;
        }
    }

    public class PBKDF2
    {
        public static byte[] GenerateSalt()
        {
            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[32];
                randomNumberGenerator.GetBytes(randomNumber);
                return randomNumber;
            }
        }

        public static byte[] HashPassword(byte[] toBeHashed, byte[] salt, int numberOfRounds)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds))
            {
                return rfc2898.GetBytes(20);
            }
        }
    }
}



