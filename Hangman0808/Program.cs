namespace Hangman0808
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int maxNumberOfRetries = 6;
            const string wildCard = "_";

            List<string> usedLetters = new List<string>();
            int retryCount = 0;

            string printableWord = "";

            string wordToGuess = GetWord();
           
            // replace letters with placeholders in printable version
            foreach (var letter in wordToGuess)
            {
                printableWord += wildCard;
            }

            PrintHelper.PrintWord(printableWord);

            // game loop
            while (true)
            {
                string letterFromUser = GetLetterFromUser(usedLetters);

                if (wordToGuess.ToUpper().Contains(letterFromUser.ToUpper()))
                {
                    // convert to char array to use iondex for replacing letters
                    var charArr = printableWord.ToCharArray();

                    // swap _ to actual letter
                    for (int i = 0; i < wordToGuess.Length; i++)
                    {
                        if (wordToGuess[i].ToString().ToUpper() == letterFromUser.ToUpper())
                        {
                            charArr[i] = letterFromUser.ToUpper()[0];
                        }
                    }

                    // re-assign new value
                    printableWord = string.Join("", charArr);

                }
                else
                {
                    if (!usedLetters.Contains(letterFromUser.ToUpper()))
                    {
                        usedLetters.Add(letterFromUser.ToUpper());
                    }

                    ++retryCount;
                }

                PrintHelper.PrintWord(printableWord);
                PrintHelper.PrintResult(retryCount, usedLetters);

                if(!printableWord.Contains(wildCard))
                {
                    Console.WriteLine("You won !!!");
                    return;
                }

                if (retryCount >= maxNumberOfRetries)
                {
                    Console.WriteLine("Game over");
                    return; //break;
                }
            }
        }

        static string GetLetterFromUser(List<string> usedLetters)
        {
            string letterFromUser;

            // get letter from user
            while (true)
            {
                Console.WriteLine("Please enter a letter:");
                letterFromUser = Console.ReadLine();

                if (string.IsNullOrEmpty(letterFromUser))
                {
                    Console.WriteLine("Please enter a letter!");
                    continue;
                }

                if (letterFromUser.Length > 1)
                {
                    Console.WriteLine("Please enter only one letter!");
                    continue;
                }

                if(usedLetters.Contains(letterFromUser.ToUpper()))
                {
                    Console.WriteLine("You already used this letter!");
                    continue;
                }

                return letterFromUser;
            }
        }

        static string GetWord()
        {
            string[] words = { "cat", "dog", "bird" };

            Random random = new Random();

            int index = random.Next(words.Length);

            string wordToGuess = words[index];

            return wordToGuess;
        }
    }
}