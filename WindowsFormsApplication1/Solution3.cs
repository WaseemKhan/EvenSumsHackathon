using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvenSumsApplication
{
    class Solution3
    {
        class Move
        {
            public int StartIndex = -1;
            public int EndIndex = -1;

            public Move(int sIndex, int eIndex)
            {
                StartIndex = sIndex;
                EndIndex = eIndex;
            }
        }

        public string solution(int[] sumsArray)
        {
            return Play(sumsArray, false, "");
        }

        string Play(int[] sumsArray, bool OppPlayer, String stack)
        {
            List<Move> WinningMoves = new List<Move>();

            for (int PopCounter = sumsArray.Count()-1; PopCounter>=0 ; PopCounter--)
            {
                List<Move> Moves = MakeMoves(sumsArray, PopCounter);

                if (Moves.Count() > 0)
                    foreach (Move M in Moves)
                    {
                        int indexX = M.StartIndex;
                        int indexY = M.EndIndex;

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
                                Console.WriteLine(stack + "Player " + (OppPlayer ? 1 : 0) + ":(" + M.StartIndex + "," + M.EndIndex + ") ->> " + ArrayToString(nextMoveArray));
                                nextMove = Play(nextMoveArray, !OppPlayer, stack + "   ");
                            }

                        if ((nextMove == "NO SOLUTION") )
                        {
                            WinningMoves.Add(M);
                        }
                    }
                }

                if (WinningMoves.Count==0)
                {
                    return "NO SOLUTION";
                }

                if (WinningMoves.Count > 1)
                {
                    WinningMoves.OrderBy(m => m.StartIndex);
                }

                return (WinningMoves[0].StartIndex + "," + WinningMoves[0].EndIndex);
        }

        List<List<Move>> GenerateAllPossibleMoves(int[] sumsArray)
        {
            List<List<Move>> AllMoves = new List<List<Move>>();

            for(int PopCounter=1; PopCounter <= sumsArray.Count(); PopCounter++)
            {
                List<Move> Moves = MakeMoves(sumsArray, PopCounter);

                if (Moves.Count()>0)
                    AllMoves.Add(Moves);
            }

            return AllMoves;
        }

        List<Move> MakeMoves(int[] sumsArray, int PopCounter)
        {
            int startIndex = 0;
            List<Move> Moves = new List<Move>();

            while (startIndex<sumsArray.Count())
            {
                int evenSum = 0;
                int endIndex = startIndex + PopCounter - 1;

                if (endIndex<sumsArray.Count())
                { 
                    for (int Counter=startIndex;Counter<=endIndex; Counter++)
                        evenSum += sumsArray[Counter];

                    if ( (evenSum % 2) == 0)
                        Moves.Add( new Move(startIndex, endIndex));
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

            if (sumsArray.Count() > 0)
            {
                foreach (int num in sumsArray.ToList())
                {
                    result = result + num + ",";
                }

                return result.Substring(0, result.Length - 1);
            }
            else
                return result;
        }
    }
}

