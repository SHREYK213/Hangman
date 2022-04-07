using System;
using System.IO;
namespace Hangman
{
    class Hangman
    {
        static char[] letters;
        static string correctWord;
        static Player player;
        public static void Main(string[] args)
        {
            try
            {
                StartGame();
                PlayGame();
                EndGame();

            }
            catch
            {
                Console.WriteLine("OOPS, Something went wrong");
            }
        }

        private static void StartGame()
        {
            string[] words;
            try
            {
                words = File.ReadAllLines(@"C:\Users\avita\OneDrive\Desktop\Hangman\words.txt");
            }
            catch
            {
                words = new string[] { "tree", "dog", "cat" };
            }
            Random random = new Random();
            correctWord=words[random.Next(0, words.Length)];

            letters = new char[correctWord.Length];
            for (int i = 0; i < correctWord.Length; i++)
            {
                letters[i] = '-';                
            }
            AskForUsersName();
        }

        static void AskForUsersName()
        {
            Console.WriteLine("Enter your user name");
            string input = Console.ReadLine();
            if (input.Length >= 2)
            {
                Console.WriteLine("We're good to go");
                player = new Player(input);                            //Name was valid
            }
            else
            {
                Console.WriteLine("Name too short");    //Name wasn't valid
                AskForUsersName();
            }
                
        }

        private static void PlayGame()
        {
            do
            {
                Console.Clear();
                DisplayMaskedWord();
                char guessedLetter = AskingForletter();
                CheckLetter(guessedLetter);

            } while (correctWord != new string(letters));

            Console.Clear();
        }

        private static void CheckLetter(char guessedLetter)
        {
            for (int i = 0; i < correctWord.Length; i++)
            {
                if (guessedLetter == correctWord[i])
                {
                    letters[i] = guessedLetter;
                    player.Score++;
                }
            }
        }

        static void DisplayMaskedWord()
        {
            foreach (char c in letters)
            {
                Console.Write(c);
            }
            Console.WriteLine();
        }
        
        static char AskingForletter()
        {
            string input;
            do
            {
                Console.WriteLine("Enter a letter");
                input = Console.ReadLine();
            } while (input.Length != 1);         

            var letter =input[0];

            if (!player.GuessedLetters.Contains(letter))
                player.GuessedLetters.Add(letter);
            return letter;
        }

        private static void EndGame()
        {
            Console.WriteLine("Congrats");
            Console.WriteLine($"ThankYou for Playing {player.Name}");
            Console.WriteLine($"Guesses {player.GuessedLetters.Count} score:{player.Score}");


        }
    }
}
