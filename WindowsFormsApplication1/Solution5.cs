using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvenSumsApplication
{
    class Solution5
    {
        public string solution(int[] sumsArray)
        {
            return Play(sumsArray, false, "");
        }

        string Play(int[] sumsArray, bool OppPlayer, String stack)
        {
            List<List<String>> AllMovesCombination = GenerateAllPossibleMoves(sumsArray);
            List<String> WinningMoves = new List<String>();

            if (AllMovesCombination.Count()==0)
            {
                Console.WriteLine(stack + "Player " + (OppPlayer ? 1 : 0) + ": NO SOLUTION");
                return "NO SOLUTION";
            }
            else
            { 
                foreach(List<String> CombinationMoves in AllMovesCombination)
                {
                    foreach (String Move in CombinationMoves)
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

                        if ((nextMove == "NO SOLUTION") )
                        {
                            WinningMoves.Add(Move);
                        }
                    }
                }

                if (WinningMoves.Count==0)
                {
                    return "NO SOLUTION";
                }

                if (WinningMoves.Count > 1)
                {
                    WinningMoves.Sort();
                }

                return WinningMoves[0];
            }
        }

        List<List<String>> GenerateAllPossibleMoves(int[] sumsArray)
        {
            List<List<String>> AllMoves = new List<List<string>>();

            for(int PopCounter=sumsArray.Count(); PopCounter >= 1; PopCounter--)
            {
                List<String> Moves = MakeMoves(sumsArray, PopCounter);

                if (Moves.Count()>0)
                {
                    Moves.Sort();
                    AllMoves.Add(Moves);
                }
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
