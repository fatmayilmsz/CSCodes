using System;

namespace Sifreleme
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
    class Crypto
    {
        public string[] _alphabets = new string[]
        {
            "TR:abcçdefgğhıijklmnoöprsştuüvyz ", "ENG:abcdefghijklmnopqrstuvwxyz "
        };

        public string _selectedAlphabet = null;
        public string _selectedAlphabetTag = null;
        public string _keyword = null;
        public string _encryptedAlphabet = null;
        public string _encryptedString = null;
        public string _decryptedString = null;

        public Crypto()
        {
        }

        ~Crypto()
        {
        }

        public void SetKeyword(string keyword)
        {
            _keyword = null;
            int colonIndex = -1;

            for (int i = 0; i < _selectedAlphabet.Length; i++)
            {
                if (_selectedAlphabet[i] == ':')
                {
                    colonIndex = i;
                }
            }

            string cleanedAlphabet = null;
            string alphabetTag = null;

            for (int i = 0; i < _selectedAlphabet.Length; i++)
            {
                if (i > colonIndex)
                {
                    cleanedAlphabet += _selectedAlphabet[i];
                }
                else if (i < colonIndex)
                {
                    alphabetTag += _selectedAlphabet[i];
                }
            }

            _selectedAlphabet = cleanedAlphabet;
            _selectedAlphabetTag = alphabetTag;

            bool isKeywordSafe = false;

            for (int i = 0; i < keyword.Length; i++)
            {
                for (int j = 0; j < _selectedAlphabet.Length; j++)
                {
                    if (keyword[i] == _selectedAlphabet[j])
                    {
                        isKeywordSafe = true;
                    }
                }
            }

            for (int i = 0; i < keyword.Length; i++)
            {
                int count = 0;

                for (int j = 0; j < keyword.Length; j++)
                {
                    if (keyword[i] == keyword[j])
                    {
                        count++;
                    }
                }

                if (count > 1)
                {
                    isKeywordSafe = false;
                }
            }

            string tempNumberString = "0123456789";

            for (int i = 0; i < keyword.Length; i++)
            {
                for (int j = 0; j < tempNumberString.Length; j++)
                {
                    if (keyword[i] == tempNumberString[j])
                    {
                        isKeywordSafe = false;
                    }
                }
            }

            if (isKeywordSafe)
            {
                _keyword = keyword;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("\n\nYour keyword cannot have any numbers and cannot contain repeating letters and unmatched chars to selected alphabet!\nPress any key to continue...");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadKey();
            }
        }

        public void SetAlphabet(int index)
        {
            if (index >= 0 && index < _selectedAlphabet.Length)
            {
                _selectedAlphabet = _alphabets[index];
                _encryptedAlphabet = null;
            }
        }

        public string Encrypt(string word)
        {
            if (word != null)
            {
                word = word.ToLower();
                _encryptedString = null;

                for (int i = 0; i < word.Length; i++)
                {
                    for (int j = 0; j < _selectedAlphabet.Length; j++)
                    {
                        if (word[i] == _selectedAlphabet[j])
                        {
                            _encryptedString += _encryptedAlphabet[j];
                        }
                    }
                }
            }

            return _encryptedString;
        }

        public void CreateEncryptedAlphabet()
        {
            _encryptedAlphabet = null;

            if (_keyword != null && _selectedAlphabet != null)
            {
                _encryptedAlphabet += _keyword;
                string tempString = null;
                bool isSafe = true;

                for (int j = 0; j < _selectedAlphabet.Length; j++)
                {
                    isSafe = true;

                    for (int k = 0; k < _keyword.Length && isSafe; k++)
                    {
                        if (_selectedAlphabet[j] == _keyword[k])
                        {
                            isSafe = false;
                        }
                    }

                    if (isSafe)
                    {
                        tempString += _selectedAlphabet[j];
                    }
                }

                _encryptedAlphabet += tempString;
            }
        }

        public string Decrypt(string word)
        {
            if (word != null && _encryptedAlphabet != null && _selectedAlphabet != null)
            {
                _decryptedString = null;

                for (int i = 0; i < word.Length; i++)
                {
                    for (int j = 0; j < _encryptedAlphabet.Length; j++)
                    {
                        if (word[i] == _encryptedAlphabet[j])
                        {
                            _decryptedString += _selectedAlphabet[j];
                        }
                    }
                }
            }

            return _decryptedString;
        }
    }
}
