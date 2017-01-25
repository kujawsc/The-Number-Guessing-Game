using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNumberGuessingGame
{
    class Program
    {
        //
        // Defining constant global variables
        //

        private const int MAX_NUMBER_OF_PLAYER_GUESSES = 4;
        private const int MAX_NUMBER_TO_GUESS = 10;

        //
        // Defining other global variables
        //

        private static int playersGuess;
        private static int numberToGuess;
        private static int roundNumber;
        private static int numberOfWins;
        private static int numberOfCurrentPlayerGuess;
        private static int[] numbersPlayerHasGuessed = new int[MAX_NUMBER_OF_PLAYER_GUESSES];
        private static bool playingGame;
        private static bool playingRound;
        private static bool numberGuessedCorrectly;
        private static Random random = new Random();

        static void Main(string[] args)
        {
            playingGame = DisplayWelcomeScreen();
            InitializeGame();
            DisplayClosingScreen();
        }

        public static bool DisplayWelcomeScreen()
        {
            Console.WriteLine("         Welcome to The Number Guessing Game!        ");
            Console.WriteLine();
            Console.WriteLine("         Would you like to play? Type yes or no               ");
            Console.WriteLine();

            string userResponse = Console.ReadLine().ToUpper();
                if (userResponse == "YES")
                {
                    playingGame = true;
                }

            return playingGame;
        }

        public static void DisplayRulesScreen()
        {
            Console.WriteLine("The computer will choose a secret number between 1 and 10.");
            Console.WriteLine("You will have four attempsts to guess the number. After each guess the computer will indicate if you have guessed correctly or whether your guess is high or low");
            Console.WriteLine("The computer will display if the guess is too low, too high");


            Console.WriteLine("Press any key to continue.");

            Console.ReadKey();
        }

        public static void InitializeGame()
        {
            DisplayReset();
            roundNumber = 0;
            DisplayRulesScreen();

            while (playingGame)
            {
                InitializeRound();
                while (!playingRound)
                {
                    DisplayGetPlayerGuessScreen();
                    DisplayPlayerGuessFeedback();
                    UpdateAndDisplayRoundStatus();
                }
                DisplayPlayerStats();
            }
            DisplayClosingScreen();
        }

        public static void InitializeRound()
        {
            DisplayReset();
            numberToGuess = GetNumberToGuess();
        }

        public static int GetNumberToGuess()
        {
            return random.Next(0, 11);
        }

        public static int DisplayGetPlayerGuessScreen()
        {
            Console.WriteLine("Enter your guess as a number", 1, 10);
            int.TryParse(Console.ReadLine(), out playersGuess);
            numbersPlayerHasGuessed[roundNumber] = playersGuess;

            return playersGuess;
        }

        public static void DisplayPlayerGuessFeedback()
        {
            DisplayReset();
            roundStats();
            if (roundNumber < MAX_NUMBER_OF_PLAYER_GUESSES - 1)
            {
                if (playersGuess == numberToGuess)
                {
                    Console.WriteLine("You guessed: " + playersGuess);
                    Console.WriteLine("Congratulations! You guessed the secret number.");
                    DisplayContinuePrompt();
                    playingRound = true;
                }
                else if (playersGuess < numberToGuess)
                {
                    Console.WriteLine("I'm sorry. You guessed too low.");
                    DisplayContinuePrompt();
                }
                else
                {
                    Console.WriteLine("I'm sorry. You guessed too high.");
                    DisplayContinuePrompt();
                }
            }
            else
            {
                if (playersGuess == numberToGuess)
                {
                    Console.WriteLine("You guessed: " + playersGuess);
                    Console.WriteLine("Congratulations! You guessed the secret number.");
                    DisplayContinuePrompt();
                    playingRound = true;
                }
                else
                {
                    playingRound = true;
                }
            }
        }

        public static void DiplayLoseScreen ()
        {
            Console.WriteLine("I am sorry you have run out of chances.");
            DisplayContinueQuitPrompt();
        }

        public static int UpdateAndDisplayRoundStatus()
        {
            numberOfCurrentPlayerGuess += 1;

                if (playersGuess == numberToGuess)
                {
                    numberOfWins += 1;
                    playingRound = true;
                }
                else if (playersGuess !=  numberToGuess && roundNumber < MAX_NUMBER_OF_PLAYER_GUESSES)
                {
                numbersPlayerHasGuessed[roundNumber] = playersGuess;
                Console.WriteLine("Storing your incorrect guess in array.");
                roundNumber += 1;
                }
                else if (playersGuess != numberToGuess && roundNumber >= MAX_NUMBER_OF_PLAYER_GUESSES)
                {
                playingRound = false;
                Console.WriteLine("Guesses used up. Ending game.");
                }
            Console.ReadKey();
            return roundNumber;
        }

        public static void roundStats ()
        {
            DisplayReset();
            Console.WriteLine("Rounds Played: " + roundNumber);
            Console.WriteLine("Current Random number: " + numberToGuess);
            DisplayNumbersGuessed();
        }

        public static void DisplayContinuePrompt()
        {
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        public static void DisplayPlayerStats()
        {
            int total;
            double percentageOfWins;
            DisplayReset();
            Console.WriteLine($"Round number: {roundNumber - 1}");
            Console.WriteLine($"Number of wins: {numberOfWins}");
            Console.WriteLine($"Number of Losses: {roundNumber - numberOfWins - 1}");
            Console.WriteLine($"Percentage of wins: {numberOfWins / roundNumber}");

            total = numberOfWins / roundNumber;
            percentageOfWins = (double)numberOfWins / total;
            Console.WriteLine($"Percentage of wins: {numberOfWins / roundNumber} %");

            DisplayContinuePrompt();
            DisplayContinueQuitPrompt();
        }

        public static void DisplayClosingScreen()
        {
            DisplayReset();
            Console.WriteLine("Thank you for playing");

            DisplayContinuePrompt();
        }

        public static void DisplayNumbersGuessed()
        {
            Console.Write($"Player's Guesses: ");
            foreach (int playerGuess in numbersPlayerHasGuessed)
            {
                Console.Write(playerGuess + " ");
            }
            Console.WriteLine();
        }

        public static void DisplayContinueQuitPrompt()
        {
            string userResponse;
            DisplayReset();
            Console.WriteLine("Would you like to continue? YES or NO:");
            userResponse = Console.ReadLine().ToUpper();

            if (userResponse == "YES")
            {
                playingGame = true;
                resetNumbersPlayerHasGuessed();
                InitializeGame();
            }
            else
            {
                playingGame = false;
            }
            DisplayContinuePrompt();
        }

        public static void resetNumbersPlayerHasGuessed()
        {
            playingRound = false;
            playersGuess = 0;
            for (int i = 0; i <= (MAX_NUMBER_OF_PLAYER_GUESSES - 1) ; i++)
            {
                numbersPlayerHasGuessed[i] = 0;
            }
            DisplayContinuePrompt();
        }

        public static void DisplayReset()
        {
            Console.Clear();
            Console.WriteLine("The Number Guessing Game!");
        }
    }
}

