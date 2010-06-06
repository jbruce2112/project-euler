using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace ProjectEuler
{
    class Program
    {

        static void Main(string[] args)
        {
            DateTime start = DateTime.Now;

            ProblemFourteen();

            DateTime finish = DateTime.Now;
            TimeSpan diff = finish - start;
            Console.WriteLine("Solution took " + diff.Minutes + " min " + diff.Seconds + " sec " + diff.Milliseconds + " ms.");
        }

        /* *** PROBLEM FIFTEEN***
         * * 
         * Starting in the top left corner of a 2x2 grid, there are 6 routes 
         * (without backtracking) to the bottom right corner.
         * 
         * How many routes are there through a 20x20 grid?
         * 
         *      _ _ 
         *     |   |
         *     |_ _|
         */
        private struct coord
        {
            public int x = 0;
            public int y = 0;
        }

        static void ProblemFifteen()
        {
            int x_len = 2, y_len = 2;
            int routes = 0;
            List<coord[]> traveled_paths = new List<coord[]>();
            bool more_paths = true;
            
            coord[] curr_path = new coord[x_len*y_len];
            coord pos = new coord();

            do
            {
                if (!hasTraveled(curr_path, traveled_paths))
                {

                }
            }
            while (pos.x != x_len && pos.y != y_len);
            
        }

        // TODO: Perhaps some refactoring here.
        static bool hasTraveled(coord[] curr_path, List<coord[]> traveled_paths)
        {
            for (int j = 0; j < curr_path.Length; j++)
            {
                foreach (coord[] path in traveled_paths)
                {
                    bool path_traveled = true;

                    for (int i = 0; i < path.Length; i++)
                    {
                        if (path[i].x != curr_path[j].x && path[i].y != curr_path[j].y)
                        {
                            path_traveled = false;
                            break;
                        }
                    }
                    if (path_traveled)
                        return true;
                }
            }
            return false;   // if we got here we didn't find any paths traveled
        }

        /* *** PROBLEM FOURTEEN
         * 
         * The following iterative sequence is defined for the set of positive integers:
         * 
         * n -> n/2 (n is even)
         * n -> 3n + 1 (n is odd)
         * 
         * Using the rule above and starting with 13, we generate the following sequence:
         * 13,  40,  20,  10,  5,  16,  8,  4,  2,  1
         * 
         * It can be seen that this sequence (starting at 13 and finishing at 1) contains 10 terms. 
         * Although it has not been proved yet (Collatz Problem), it is thought that all starting numbers finish at 1.
         * 
         * Which starting number, under one million, produces the longest chain?
         * 
         * NOTE: Once the chain starts the terms are allowed to go above one million.
         */
        static void ProblemFourteen()
        {
            int max = 1000000, longestLength = 0, answer = 0;

            for (int i = 1; i < max; i++)
            {
                long result = i;
                int seqLength = 1;

                do
                {
                    if (result % 2 == 0)
                    {
                        result = result / 2;
                    }
                    else
                    {
                        result = 3 * result + 1;
                    }

                    seqLength++;
                }
                while (result != 1);

                if (seqLength > longestLength)
                {
                    longestLength = seqLength;
                    answer = i;
                }
            }
            Console.WriteLine("The starting number, under one million that produces the longest chain is\n " + answer);
        }

        /* *** PROBLEM THIRTEEN ***
         * 
         * Work out the first ten digits of the sum of the following one-hundred 50-digit numbers.
         * 
         *  *** see txt file ***
         */
        static void ProblemThirteen()
        {
            string path = "C:\\Users\\John\\Documents\\Visual Studio 2010\\Projects\\ProjectEuler\\ProjectEuler\\problem13.txt";
            string[] bigNumbers = new string[100];
            int numLength = 50;

            // Read from file
            try
            {
                StreamReader sr = new StreamReader(path);
                string line;
                int count = 0;

                while ((line = sr.ReadLine()) != null)
                {
                    bigNumbers[count] = line;
                    count++;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            int r = 0;  // remainder
            int num1 = 0, num2 = 0, result = 0;
            string total = null;
            List<int> answer = new List<int>(50);

            // amount of numbers
            for (int i = 0; i < bigNumbers.Length-1; i++)
            {
                // length of numbers
                for (int j = numLength-1; j >= 0; j--)
                {
                    if (total == null)
                    {
                        num1 = Int16.Parse(bigNumbers[i].ElementAt(j).ToString());
                    }
                    else
                    {
                        num1 = Int16.Parse(total.ElementAt(j).ToString());
                    }
                    num2 = Int16.Parse(bigNumbers[i + 1].ElementAt(j).ToString());

                    if (j > 0 && num1 + num2 + r > 9)
                    {
                        result = num1 + num2 + r - 10;
                        r = 1;
                    }
                    else
                    {
                        result = num1 + num2 + r;
                        r = 0;
                    }
                    answer.Add(result);
                }

                StringBuilder sb = new StringBuilder();
                for (int k = answer.Count-1; k >= 0; k--)
                {
                    sb.Append(answer.ElementAt(k));
                }
                total = sb.ToString();

                // reset numlength since addition may have altered length
                numLength = total.Length;
                answer.Clear();

                if (i < bigNumbers.Length-2)
                {
                    StringBuilder sbNum = new StringBuilder(bigNumbers[i+2]);
                    while (sbNum.Length < numLength)
                    {
                        sbNum.Insert(0, "0");
                    }
                    bigNumbers[i + 2] = sbNum.ToString();
                }
            }

            char[] arr = total.ToCharArray();
            StringBuilder approxTotal = new StringBuilder();
            for (int i = 0; i < 10; i++)
            {
                approxTotal.Append(arr[i]);
            }

           Console.WriteLine("The first 10 digits of the sum of the 100 50-digit numbers is " + approxTotal);
        }

        /* *** PROBLEM TWELVE ***
         * The sequence of triangle numbers is generated by adding the natural numbers. 
         * So the 7th triangle number would be 1 + 2 + 3 + 4 + 5 + 6 + 7 = 28. 
         * The first ten terms would be:
         * 1, 3, 6, 10, 15, 21, 28, 36, 45, 55, ...
         * 
         * Let us list the factors of the first seven triangle numbers:
         * 
         * 1: 1
         * 3: 1,3
         * 6: 1,2,3,6
         * 10: 1,2,5,10
         * 15: 1,3,5,15
         * 21: 1,3,7,21
         * 28: 1,2,4,7,14,28
         * 
         * We can see that 28 is the first triangle number to have over five divisors.
         * 
         * What is the value of the first triangle number to have over five hundred divisors?
         */
        static void ProblemTwelve()
        {
            long lastTriangleNumber = 0;
            long nextTriangleNumber = 0;
            long numTriangleNumbers = 1;
            int numDivisors = 0, numDivisorsNeeded = 500;
            bool foundNumber = false;

            while (!foundNumber)
            {
                numDivisors = 0;    // reset

                nextTriangleNumber = lastTriangleNumber + numTriangleNumbers;

                int max = (int)Math.Sqrt(nextTriangleNumber);

                bool isPerfectSquare = Math.Sqrt(nextTriangleNumber) == max;

                /*
                 * A little help & research into a more efficient method for finding the factors
                 * other than a brute-force from 1 to trianglenumber check.
                 * 
                 * http://stackoverflow.com/questions/277368/euler-project-help-problem-12-prime-factors-and-the-like
                 * 
                 * http://mathforum.org/library/drmath/view/57151.html
                 */
                for (int factor = 1; factor <= max; factor++)
                {
                    if (nextTriangleNumber % factor == 0)
                    {
                        numDivisors += 2;
                    }
                }

                if (isPerfectSquare)
                    numDivisors--;

                if (numDivisors > numDivisorsNeeded)
                {
                    foundNumber = true;
                }

                numTriangleNumbers++;
                lastTriangleNumber = nextTriangleNumber;
            }
            Console.WriteLine("The value of the first triangle number to have over five hundred divisors is " + nextTriangleNumber);
        }

        /* *** PROBLEM ELEVEN ***
         * 
         *What is the greatest product of four adjacent numbers in any direction 
         *(up, down, left, right, or diagonally) in the 20 x 20 grid?
         */
        static void ProblemEleven()
        {
            int[] grid = { 08, 02, 22, 97, 38, 15, 00, 40, 00, 75, 04, 05, 07, 78, 52, 12, 50, 77, 91, 08,
                           49, 49, 99, 40, 17, 81, 18, 57, 60, 87, 17, 40, 98, 43, 69, 48, 04, 56, 62, 00,
                           81, 49, 31, 73, 55, 79, 14, 29, 93, 71, 40, 67, 53, 88, 30, 03, 49, 13, 36, 65,
                           52, 70, 95, 23, 04, 60, 11, 42, 69, 24, 68, 56, 01, 32, 56, 71, 37, 02, 36, 91,
                           22, 31, 16, 71, 51, 67, 63, 89, 41, 92, 36, 54, 22, 40, 40, 28, 66, 33, 13, 80,
                           24, 47, 32, 60, 99, 03, 45, 02, 44, 75, 33, 53, 78, 36, 84, 20, 35, 17, 12, 50,
                           32, 98, 81, 28, 64, 23, 67, 10, 26, 38, 40, 67, 59, 54, 70, 66, 18, 38, 64, 70,
                           67, 26, 20, 68, 02, 62, 12, 20, 95, 63, 94, 39, 63, 08, 40, 91, 66, 49, 94, 21,
                           24, 55, 58, 05, 66, 73, 99, 26, 97, 17, 78, 78, 96, 83, 14, 88, 34, 89, 63, 72,
                           21, 36, 23, 09, 75, 00, 76, 44, 20, 45, 35, 14, 00, 61, 33, 97, 34, 31, 33, 95,
                           78, 17, 53, 28, 22, 75, 31, 67, 15, 94, 03, 80, 04, 62, 16, 14, 09, 53, 56, 92,
                           16, 39, 05, 42, 96, 35, 31, 47, 55, 58, 88, 24, 00, 17, 54, 24, 36, 29, 85, 57,
                           86, 56, 00, 48, 35, 71, 89, 07, 05, 44, 44, 37, 44, 60, 21, 58, 51, 54, 17, 58,
                           19, 80, 81, 68, 05, 94, 47, 69, 28, 73, 92, 13, 86, 52, 17, 77, 04, 89, 55, 40,
                           04, 52, 08, 83, 97, 35, 99, 16, 07, 97, 57, 32, 16, 26, 26, 79, 33, 27, 98, 66,
                           88, 36, 68, 87, 57, 62, 20, 72, 03, 46, 33, 67, 46, 55, 12, 32, 63, 93, 53, 69,
                           04, 42, 16, 73, 38, 25, 39, 11, 24, 94, 72, 18, 08, 46, 29, 32, 40, 62, 76, 36,
                           20, 69, 36, 41, 72, 30, 23, 88, 34, 62, 99, 69, 82, 67, 59, 85, 74, 04, 36, 16,
                           20, 73, 35, 29, 78, 31, 90, 01, 74, 31, 49, 71, 48, 86, 81, 16, 23, 57, 05, 54,
                           01, 70, 54, 71, 83, 51, 54, 69, 16, 92, 33, 48, 61, 43, 52, 01, 89, 19, 67, 48 };

            // since array access is 0-indexed, we
            // take that into account for length & height.
            int rowLength = 19, rowHeight = 19;
            int num1 = 0, num2 = 0, num3 = 0, num4 = 0;
            int largestProduct = 0, product = 0;

            for (int x = 0; x < rowLength; x++)
            {
                for (int y = 0; y < rowHeight; y++)
                {
                    // check above
                    if (y >= 3)
                    {
                        num1 = grid[x + (y * rowLength)];
                        num2 = grid[x + ((y - 1) * rowLength)];
                        num3 = grid[x + ((y - 2) * rowLength)];
                        num4 = grid[x + ((y - 3) * rowLength)];

                        product = num1 * num2 * num3 * num4;
                        if (product > largestProduct)
                            largestProduct = product;
                    }

                    // check below
                    if (y <= rowHeight - 3)
                    {
                        num1 = grid[x + (y * rowLength)];
                        num2 = grid[x + ((y + 1) * rowLength)];
                        num3 = grid[x + ((y + 2) * rowLength)];
                        num4 = grid[x + ((y + 3) * rowLength)];

                        product = num1 * num2 * num3 * num4;
                        if (product > largestProduct)
                            largestProduct = product;
                    }

                    //check left
                    if (x >= 3)
                    {
                        num1 = grid[x + (y * rowLength)];
                        num2 = grid[(x - 1) + y * rowLength];
                        num3 = grid[(x - 2) + y * rowLength];
                        num4 = grid[(x - 3) + y * rowLength];

                        product = num1 * num2 * num3 * num4;
                        if (product > largestProduct)
                            largestProduct = product;
                    }

                    //check right
                    if (x <= rowLength-3)
                    {
                        num1 = grid[x + (y * rowLength)];
                        num2 = grid[(x + 1) + y * rowLength];
                        num3 = grid[(x + 2) + y * rowLength];
                        num4 = grid[(x + 3) + y * rowLength];

                        product = num1 * num2 * num3 * num4;
                        if (product > largestProduct)
                            largestProduct = product;
                    }

                    //check diagonal
                    if (x <= rowLength - 3 && y <= rowHeight - 3)
                    {
                        num1 = grid[x + (y * rowLength)];
                        num2 = grid[(x + 1) + ((y + 1) * rowLength)];
                        num3 = grid[(x + 2) + ((y + 2) * rowLength)];
                        num4 = grid[(x + 3) + ((y + 3) * rowLength)];

                        product = num1 * num2 * num3 * num4;
                        if (product > largestProduct)
                            largestProduct = product;
                    }
                }
            }
            Console.WriteLine("The greatest product of four adjacent numbers in any direction is " + largestProduct);
        }


        /* *** PROBLEM TEN ***
         * The sum of the primes below 10 is 2 + 3 + 5 + 7 = 17.
         * 
         * Find the sum of all the primes below two million.
         */
        static void ProblemTen()
        {
            List<int> primes = new List<int>();
            primes.Add(2);
            int bounds = 2000000;
            long sum = 2;

            for (int i = 3; i < bounds; i++)
            {

                /*
                    * Using the fundemental therom of arithmetic (?)
                    * we can check for primarity by using our list
                    * of primes. Since we build the list 
                    * as we go, a prime number such as 7 will only return as 
                    * not prime if we have already found it (it's in our list).
                    * 
                    * example: finding 7
                    * is 7 % 2 == 0? no
                    * is 7 % 3 == 0? no
                    * is 7 % 5 == 0? no
                    * 
                    * then add 7 to list
                    * 
                    * Never does this algorithm ask if 7 % 7 == 0,
                    * in which case it does and it would not mark it as prime
                    */
                bool isPrime = true;

                foreach (int p in primes)
                {
                    if (i % p == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }

                if (isPrime)
                {
                    if (primes.Count < 25)
                    {
                        Console.WriteLine(i);
                    }
                    primes.Add(i);
                    sum += i;
                }
            }
            Console.WriteLine("The sum of all primes below two million is " + sum);
        }

        /* *** PROBLEM NINE ***
         * A Pythagorean triplet is a set of three natural numbers, a < b < c, for which,
         * a^2 + b^2 = c^2
         * 
         * For example, 3^2 + 4^2 = 9 + 16 = 25 = 52.
         * 
         * There exists exactly one Pythagorean triplet for which a + b + c = 1000.
         * Find the product abc.
         * 
         * Num iterations = 1 billion!
         * worst case O(n^3)!!!
         */
        static void ProblemNine()
        {
            int a = -1, b = -1, c = -1, bounds = 1000;
            bool foundTriplet = false;

            // 995 is upper bound since
            // a+b+c = 100 & a < b < c

            // i = a, j = b, k = c
            for (int i = 1; i < bounds; i++)
            {
                for (int j = 2; j < bounds; j++)
                {
                    for (int k = 3; k < bounds; k++)
                    {
                        if (i + j + k == bounds && (i * i + j * j == k * k))
                        {
                            foundTriplet = true;
                            a = i;
                            b = j;
                            c = k;
                            break;
                        }
                    }
                    if (foundTriplet)
                        break;
                }
                if (foundTriplet)
                    break;
            }

            Console.WriteLine(" A " + a);
            Console.WriteLine(" B " + b);
            Console.WriteLine(" C " + c);
            Console.WriteLine("The product of the pythagorean triplet for which a + b + c = 1000 is " + a*b*c);
        }

        /*  *** PROBLEM EIGHT ***
         * Find the greatest product of five consecutive digits in the 1000-digit number.
         */
        static void ProblemEight()
        {
            StringBuilder hugeNumber = new StringBuilder();
            hugeNumber.Append("73167176531330624919225119674426574742355349194934");
            hugeNumber.Append("96983520312774506326239578318016984801869478851843");
            hugeNumber.Append("85861560789112949495459501737958331952853208805511");
            hugeNumber.Append("12540698747158523863050715693290963295227443043557");
            hugeNumber.Append("66896648950445244523161731856403098711121722383113");
            hugeNumber.Append("62229893423380308135336276614282806444486645238749");
            hugeNumber.Append("30358907296290491560440772390713810515859307960866");
            hugeNumber.Append("70172427121883998797908792274921901699720888093776");
            hugeNumber.Append("65727333001053367881220235421809751254540594752243");
            hugeNumber.Append("52584907711670556013604839586446706324415722155397");
            hugeNumber.Append("53697817977846174064955149290862569321978468622482");
            hugeNumber.Append("83972241375657056057490261407972968652414535100474");
            hugeNumber.Append("82166370484403199890008895243450658541227588666881");
            hugeNumber.Append("16427171479924442928230863465674813919123162824586");
            hugeNumber.Append("17866458359124566529476545682848912883142607690042");
            hugeNumber.Append("24219022671055626321111109370544217506941658960408");
            hugeNumber.Append("07198403850962455444362981230987879927244284909188");
            hugeNumber.Append("84580156166097919133875499200524063689912560717606");
            hugeNumber.Append("05886116467109405077541002256983155200055935729725");
            hugeNumber.Append("71636269561882670428252483600823257530420752963450");

            int largestProduct = 0, num1 = 0, num2 = 0, num3 = 0, num4 = 0, num5 = 0, product = 0;

            for (int i = 0; i < hugeNumber.Length-5; i++)
            {
                num1 = Int32.Parse(hugeNumber[i].ToString());
                num2 = Int32.Parse(hugeNumber[i+1].ToString());
                num3 = Int32.Parse(hugeNumber[i+2].ToString());
                num4 = Int32.Parse(hugeNumber[i+3].ToString());
                num5 = Int32.Parse(hugeNumber[i+4].ToString());

                product = num1 * num2 * num3 * num4 * num5;

                if (product > largestProduct)
                    largestProduct = product;
            }
            Console.WriteLine(" Find the greatest product of five consecutive digits in " +
                                                    "the 1000-digit number is " + largestProduct);
        }

        /*  *** PROBLEM SEVEN ***
         * By listing the first six prime numbers: 2, 3, 5, 7, 11, and 13, 
         * we can see that the 6th prime is 13.
         * 
         * What is the 10001st prime number?
         */
        static void ProblemSeven()
        {
            // start off with one prime
            int numPrimes = 1;
            int lastPrime = 2;
            int nextPrime = 3;

            while (numPrimes < 10001)
            {
                bool isPrime = true;

                // get next prime
                for (int j = 2; j < nextPrime; j++)
                {
                    if (nextPrime % j == 0)
                    {
                        isPrime = false;
                        nextPrime++;
                        break;
                    }
                }
                if (isPrime)
                {
                    numPrimes++;
                    lastPrime = nextPrime;
                    nextPrime++;
                    if (numPrimes < 10)
                    Console.WriteLine("Found prime " + lastPrime + ", total primes: " + numPrimes);
                }
            }
            Console.WriteLine("The 10001st prime number is " + lastPrime);
        }

        /*  *** PROBLEM SIX***
         * The sum of the squares of the first ten natural numbers is,
         * 1^2 + 2^2 + ... + 10^2 = 385
         * 
         * The square of the sum of the first ten natural numbers is,
         * (1 + 2 + ... + 10)^2 = 552 = 3025
         * 
         * Hence the difference between the sum of the squares of the first 
         * ten natural numbers and the square of the sum is 3025  385 = 2640.
         * 
         * Find the difference between the sum of the squares of the first one 
         * hundred natural numbers and the square of the sum.
         */
        static void ProblemSix()
        {
            long sumOfSquares = 0;
            long squareOfSum = 0;

            for (int i = 1; i <= 100; i++)
            {
                sumOfSquares += (i * i);
            }

            for (int i = 1; i <= 100; i++)
            {
                squareOfSum += i;
            }

            squareOfSum *= squareOfSum;

            Console.WriteLine("The difference between the sum of the squares of the first " +
                                    "one hundred natural numbers and the square of the sum is " + (squareOfSum - sumOfSquares));

        }

        /*  *** PROBELM FIVE ***
         * 
         * 2520 is the smallest number that can be divided by each of the numbers from 1 to 10 without any remainder. 
         * 
         * What is the smallest positive number that is evenly divisible by all of the numbers from 1 to 20?
         */
        static void ProblemFive()
        {
            int smallestNum = 1;
            bool foundNum = false;

            while (!foundNum)
            {
                foundNum = true;

                for (int i = 1; i <= 20; i++)
                {
                    if (smallestNum % i != 0)
                    {
                        foundNum = false;
                        smallestNum++;
                        break;
                    }
                }
            }
            Console.WriteLine("The smallest positive number that is evenly divisible "+
                                    " by all of the numbers from 1 to 20 is " + smallestNum); 
        }

        /* ** PROBLEM FOUR ***
         * 
         * A palindromic number reads the same both ways. The largest palindrome made
         * from the product of two 2-digit numbers is 9009 = 91 * 99.
         * 
         * Find the largest palindrome made from the product of two 3-digit numbers.
         * 
         * Worst case: 810,000 iterations?
         * 
         * o(n^2)
         */
        static void ProblemFour()
        {
            List<int> palindromes = new List<int>();

            for (int i = 999; i >= 100; i--)
            {
                for (int j = 999; j >= 100; j--)
                {
                    string product = (i * j).ToString();

                    if (product.Equals(reverseString(product)))
                    {
                        palindromes.Add(Int32.Parse(product));
                    }
                }
            }
            Console.WriteLine("Largest palindrome made from the product of two 3-digit numbers: " + palindromes.Max());
        }

        static string reverseString(string str)
        {
            char[] array = str.ToCharArray();
            Array.Reverse(array);
            return new string(array);
        }

        /* *** PROBLEM THREE ***
         * 
         * The prime factors of 13195 are 5, 7, 13 and 29.
         * 
         * What is the largest prime factor of the number 600851475143 ?
         * 
         * TODO: This is quite slow.
         */
        static void ProblemThree()
        {
            const long num = 600851475143;
            long largestPrime = -1;

            for (long i = num; i > 0; i--)
            {
                if (num % i == 0)
                {
                    bool isPrime = true;

                    for (int j = 2; j < i; j++)
                    {
                        if (i % j == 0)
                        {
                            isPrime = false;
                            break;
                        }
                    }
                    if (isPrime)
                    {
                        largestPrime = i;
                        break;
                    }
                }
            }
            Console.WriteLine("Largest prime is " + largestPrime);
        }

        /*  *** Problem 2 ***
         * Each new term in the Fibonacci sequence is generated by adding the previous two terms. 
         * 
         * By starting with 1 and 2, the first 10 terms will be:
         * 
         * 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, ...
         * 
         * Find the sum of all the even-valued terms in the sequence which do not exceed four million.
         */
        static void ProblemTwo()
        {
            const int upperLimit = 4000000;
            bool lessThanLimit = true;
            int nMinus1Term = 1;
            int nTerm = 2;
            int nextTerm = 0;

            // since we're really starting at index 3 of sequence, 
            // the initial sum is 2 (of even terms)
            int sum = 2;

            while (lessThanLimit)
            {
                nextTerm = nMinus1Term + nTerm;

                // only add even terms
                if (nextTerm % 2 == 0)
                {
                    if (nextTerm < upperLimit)
                    {
                        sum += nextTerm;
                    }
                    else
                    {
                        lessThanLimit = false;
                    }
                }

                // reset our n & n-1
                nMinus1Term = nTerm;
                nTerm = nextTerm;
            }

            Console.WriteLine("The sum of the of all the even-valued terms in " + 
                                "the fibonacci sequence which do not exceed four million is " + sum);
        }

        /*
         * * * PROBLEM 1 * * * 
         * 
         * If we list all the natural numbers below 10 that are multiples of 
         * 3 or 5, we get 3, 5, 6 and 9. The sum of these multiples is 23.
         * 
         * Find the sum of all the multiples of 3 or 5 below 1000.
         * 
         */
        static void ProblemOne()
        {
            long sum = 0;
            int i = 1;
            int nextMultiple3 = 0;
            int nextMultiple5 = 0;

            while (nextMultiple3 < 1000 || nextMultiple5 < 1000)
            {
                nextMultiple3 = 3 * i;
                nextMultiple5 = 5 * i;

                if (nextMultiple3 < 1000)
                {
                    sum += nextMultiple3;
                }

                // The requirement is 3 OR 5. Before we add the next
                // multiple of 5 we need to check if it happens to also
                // be a multiple of 3.
                if (nextMultiple5 % 3 != 0 && nextMultiple5 < 1000)
                {
                    sum += nextMultiple5;
                }
                i++;
            }

            Console.WriteLine("The sum of all the multiples of 3 or 5 below 1000 is " + sum);
        }
    }
}
