using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvenSumsApplication
{   // [20, 4, 20, 14, 5, 8, 2, 4, 18]
    // [1,2,3,4,5,6]
    class Solution4
    {
        public string solution(int[] sumsArray)
        {
            return Play(sumsArray, false, "");
        }

        string Play(int[] sumsArray, bool OppPlayer, String stack)
        {
            String result = "NO SOLUTION";

            List<String> WinningMoves = new List<String>();

            if (sumsArray.Count()==0)
            {
                return result;
            }

            Console.WriteLine(stack + "Player " + (OppPlayer ? 1 : 0) + ": " + ArrayToString(sumsArray));

            for (int PopCounter = sumsArray.Count(); PopCounter >=1 ; PopCounter--)
            {
                List<String> Moves = MakeMoves(sumsArray, PopCounter);

                if (Moves.Count > 0)
                {
                    // Try the move with the smallest index first
                    Moves.Sort();

                    foreach (String Move in Moves)
                    {
                        int indexX = int.Parse(Move.Split(',')[0]);
                        int indexY = int.Parse(Move.Split(',')[1]);

                        int[] nextMoveArray = TrimArray(sumsArray, indexX, indexY);

                        string nextMove = "";

                        if (nextMoveArray.Count() == 0)
                        {
                            Console.WriteLine(stack + "Player " + (!OppPlayer ? 1 : 0) + ": NO SOLUTION");
                            nextMove = "NO SOLUTION";
                        }
                        else
                            if (nextMoveArray.Count() == 1)
                            {
                                if (nextMoveArray[0] % 2 == 0)
                                {
                                    Console.WriteLine(stack + "Player " + (!OppPlayer ? 1 : 0) + ": SOLUTION=" + sumsArray[0].ToString());
                                    nextMove = sumsArray[0].ToString();
                                }
                                else
                                {
                                    Console.WriteLine(stack + "Player " + (!OppPlayer ? 1 : 0) + ": NO SOLUTION");
                                    nextMove = "NO SOLUTION";
                                }
                            }
                            else
                            {
                                Console.WriteLine(stack + "Player " + (OppPlayer ? 1 : 0) + ":" + Move + " ->> " + ArrayToString(nextMoveArray));

                                nextMove = Play(nextMoveArray, !OppPlayer, stack + "   ");
                            }

                        if ((nextMove == "NO SOLUTION"))
                        {
                            WinningMoves.Add(Move);
                        }

                        // Pick the first solution with the lowest starting index
                        if (WinningMoves.Count()>1)
                           break;
                    }

                    if (WinningMoves.Count > 1)
                        WinningMoves.Sort();

                    if (WinningMoves.Count >= 1)
                        return WinningMoves[0];
                }
            }

            return result;
        }

        List<List<String>> GenerateAllPossibleMoves(int[] sumsArray)
        {
            List<List<String>> AllMoves = new List<List<string>>();

            for(int PopCounter=1; PopCounter <= sumsArray.Count(); PopCounter++)
            {
                List<String> Moves = MakeMoves(sumsArray, PopCounter);

                if (Moves.Count()>0)
                    AllMoves.Add(Moves);
            }

            return AllMoves;
        }

        List<String> MakeMoves(int[] sumsArray, int PopCounter)
        {
            int startIndex = 0;
            List<String> Moves = new List<String>();

            while (startIndex<sumsArray.Count())
            {
                int evenSum = 0;
                int endIndex = startIndex + PopCounter - 1;

                if (endIndex<sumsArray.Count())
                { 
                    for (int Counter=startIndex;Counter<=endIndex; Counter++)
                        evenSum += sumsArray[Counter];

                    if ( (evenSum % 2) == 0)
                        Moves.Add(startIndex + "," + endIndex);
                }

                startIndex++;
            }

            return Moves;
        }

        int[] TrimArray(int[] sumsArray,int indexX, int indexY)
        {
            int ArraySize = 0;
                
            if (indexX==indexY)
                ArraySize = sumsArray.Length - 1;
            else
                ArraySize = sumsArray.Length - (indexY - indexX + 1);

            int[] result = new int[ArraySize];

            int iCounterForOld = 0;
            int iCounterForNew = 0;

            while(iCounterForOld<sumsArray.Length)
            {
                if (!( iCounterForOld >= indexX && iCounterForOld <= indexY ))
                { 
                    result[iCounterForNew] = sumsArray[iCounterForOld];
                    iCounterForNew++;
                }

                iCounterForOld++;
            }

            return result;
        }

        String ArrayToString(int[] sumsArray)
        {
            string result = "";

            foreach(int num in sumsArray.ToList())
            {
                result = result + num + ",";
            }

            return result.Substring(0, result.Length-1);
        }
    }
}

//bool isEven(int num)
//{
//    return ((num % 2) == 0);
//}

//bool isEven(int num1, int num2)
//{
//    return (((num1 + num2) % 2) == 0);
//}
//string Play(int[] sumsArray, bool OppPlayer, int check)
//{
//    String result = "NO SOLUTION";
//    int[,] PossibleMoves = new int[sumsArray.Length, 2];
//    int MovesCounter = 0;

//    Console.Out.WriteLine("");
//    Console.Out.WriteLine((OppPlayer ? 1 : 0) + ": " + ArrayToString(sumsArray));

//    if (sumsArray.Length == 1)
//    {
//        if (isEven(sumsArray[0]) && !OppPlayer)
//            return "0";
//        else
//            return result;
//    }
//    else
//    {
//        //for(int PopCounter=1; PopCounter <= sumsArray.Length; PopCounter++)
//        //{
//        //    int[] Evens = new int[sumsArray.Length];
//        //    int[] Remaining = new int[sumsArray.Length];

//        //    //SplitEvens(sumsArray, PopCounter, Evens, Remaining);

//        //    if (Evens.Length>0)
//        //    {
//        //        string value = Play(Remaining, !OppPlayer);

//        //        if ((value == "NO SOLUTION") && !OppPlayer)
//        //        {
//        //            //Good Move. Add to list
//        //        }
//        //    }
//        //}

//        // Evaluate Good Moves
//        // Return a Good Move

//        //for (int iCounterX = 0; iCounterX < sumsArray.Length; iCounterX++)
//        //{
//        //    for (int iCounterY = iCounterX+1; iCounterY < sumsArray.Length; iCounterY++)
//        //    {
//        //        if (isEven(sumsArray[iCounterX], sumsArray[iCounterY]))
//        //        {
//        //                Console.Out.WriteLine((OppPlayer ? 1 : 0) + ": index ->" + iCounterX + "," + iCounterY);

//        //            int[] nextMoveArray = TrimArray(sumsArray, iCounterX, iCounterY);

//        //            string nextMove = Play(nextMoveArray, !OppPlayer);

//        //            Console.Out.WriteLine(nextMove);

//        //            if ((nextMove == "NO SOLUTION") && !OppPlayer)
//        //            {
//        //                PossibleMoves[MovesCounter, 0] = iCounterX;
//        //                PossibleMoves[MovesCounter, 1] = iCounterY;
//        //                MovesCounter++;
//        //            }
//        //        }
//        //    }
//        //}
// //   }

////    return result;
////}