using DotNetty.Common.Utilities;
using Lucene.Net.Util;
using NetTopologySuite.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Test22;

namespace Test22
{
    internal class Node
    {
        public static int N;
        public int[,] puzz;
        public int size = N;
        public int level = 0;
        public int H, M;
        public string hashp;
        public Node parent;
        public static Queue<Node> Final_queue = new Queue<Node>();
        public int Total_F;
        public int Total_F_manhaten;

        //this function detects which is the next node to generate children from
        public Node Next_puzzle(int choice)
        {
            Node smallest_puzzle = new Node();
            while (Program.p.Count > 0)
            {

                smallest_puzzle = Program.p.Dequeue();
                Program.puzzle_list.Add(smallest_puzzle.hashp);

                
                if (choice == 1)
                {
                    //Base Case
                    if (smallest_puzzle.H == 0)
                    {
                        return smallest_puzzle;
                    }
                    else
                    {
                            generate_child(smallest_puzzle,choice);

                    }
                }
                else if(choice==21)
                {
                    //Base Case
                    if (smallest_puzzle.M == 0)
                    {
                        return smallest_puzzle;
                    }
                    else
                    {
                        generate_child(smallest_puzzle, choice);

                    }
                }
                
            }
                return smallest_puzzle;
        }

        //this function generate children from the smallest heuristc value node
        public void generate_child(Node smallest_puzzle,int choice)
        {
            int row = blank_postion(smallest_puzzle)[0];
            int column = blank_postion(smallest_puzzle)[1];
            //left
            if (row >= 0 && column - 1 >= 0)
            {

                //adding child information
                Node left = new Node();
                left.puzz = (int[,])smallest_puzzle.puzz.Clone();
                left.level = smallest_puzzle.level;
                left.parent = smallest_puzzle;
                left.level++;

                //retrive the next swaped num after zero position in adjacent array 
                int[] adjacent = new int[2];
                adjacent[0] = row;
                adjacent[1] = column - 1;
                int newData;
                int[,] puzzleCopy;
                puzzleCopy = (int[,])left.puzz.Clone();

                //holding the next swaped num in newData
                newData = puzzleCopy[adjacent[0], adjacent[1]];
                puzzleCopy[adjacent[0], adjacent[1]] = puzzleCopy[row, column];
                puzzleCopy[row, column] = newData;
                left.puzz = puzzleCopy;
                
               //creates the puzzle string 
                left.hashp = puzzle_string(left);
                if (choice == 1)
                {
                    //calculate the hamming for the puzzle
                    int hamm = Hamming(left);
                    left.H = hamm;
                    left.Total_F = hamm + left.level;
                    if(!Program.puzzle_list.Contains(left.hashp))
                        Program.p.Enqueue(left, left.Total_F);
                }
                else if (choice == 21)
                {
                    //calculate the Manhattan for the puzzle
                    int man = ManHattan(left);
                    left.M = man;
                    left.Total_F_manhaten = man + left.level;
                    if (!Program.puzzle_list.Contains(left.hashp))
                        Program.p.Enqueue(left, left.Total_F_manhaten);
                }
              
                
                

            }

            
            //right
            if (row >= 0 && column + 1 != N)
            {
                //adding child information
                Node right = new Node();
                right.puzz = (int[,])smallest_puzzle.puzz.Clone(); ;
                right.level = smallest_puzzle.level;
                right.parent = smallest_puzzle; 
                right.level++;

                //retrive the next swaped num after zero position in adjacent array 
                int[] adjacent = new int[2];
                adjacent[0] = row;
                adjacent[1] = column + 1;

                //swap variabels
                int newData;
                int[,] puzzleCopy;

                //add the source puzzle in puzzlecopy
                puzzleCopy = (int[,])right.puzz.Clone();

                //holding the next swaped num in newData
                newData = puzzleCopy[adjacent[0], adjacent[1]];
                puzzleCopy[adjacent[0], adjacent[1]] = puzzleCopy[row, column];
                puzzleCopy[row, column] = newData;
                right.puzz = puzzleCopy;

                //creates the puzzle string  
                right.hashp = puzzle_string(right);
                if (choice == 1)
                {
                    //calculate the hamming for the puzzle
                    int hamm = Hamming(right);
                    right.H = hamm;
                    right.Total_F = hamm + right.level;
                    if (!Program.puzzle_list.Contains(right.hashp))
                        Program.p.Enqueue(right, right.Total_F);
                }
                else if (choice == 21)
                {
                    //calculate the Manhattan for the puzzle
                    int man = ManHattan(right);
                    right.M = man;
                    right.Total_F_manhaten = man + right.level;
                    if (!Program.puzzle_list.Contains(right.hashp))
                        Program.p.Enqueue(right, right.Total_F_manhaten);
                }
       
               
            }
        
            //up
            if (row - 1 >= 0 && column >= 0)
            {
                //adding child information
                Node up = new Node();
                up.puzz = (int[,])smallest_puzzle.puzz.Clone(); ;
                up.level = smallest_puzzle.level;
                up.parent = smallest_puzzle;
                up.level++;

                //retrive the next swaped num after zero position in adjacent array 
                int[] adjacent = new int[2];
                adjacent[0] = row - 1;
                adjacent[1] = column;

                //swap variabels
                int newData;
                int[,] puzzleCopy;

                //add the source puzzle in puzzlecopy
                puzzleCopy = (int[,])up.puzz.Clone();

                //holding the next swaped num in newData
                newData = puzzleCopy[adjacent[0], adjacent[1]];
                puzzleCopy[adjacent[0], adjacent[1]] = puzzleCopy[row, column];
                puzzleCopy[row, column] = newData;
                up.puzz = puzzleCopy;

                //creates the puzzle string  
                up.hashp = puzzle_string(up);
                if (choice == 1)
                {
                    //calculate the hamming for the puzzle
                    int hamm = Hamming(up);
                    up.H = hamm;
                    up.Total_F = hamm + up.level;
                    if (!Program.puzzle_list.Contains(up.hashp))
                        Program.p.Enqueue(up, up.Total_F);
                }
                else if (choice == 21)
                {
                    //calculate the Manhattan for the puzzle
                    int man = ManHattan(up);
                    up.M = man;
                    up.Total_F_manhaten = man + up.level;
                    if (!Program.puzzle_list.Contains(up.hashp))
                        Program.p.Enqueue(up, up.Total_F_manhaten);
                }
           

            }
            //down
            if (row + 1 != N && column >= 0)
            {
                //adding child information
                Node down = new Node();
                down.puzz = (int[,])smallest_puzzle.puzz.Clone(); ;
                down.level = smallest_puzzle.level;
                down.parent = smallest_puzzle;
                down.level++;

                //retrive the next swaped num after zero position in adjacent array 
                int[] adjacent = new int[2];
                adjacent[0] = row + 1; 
                adjacent[1] = column;
                //swap variables
                int newData;
                int[,] puzzleCopy;
                //add the source puzzle in puzzlecopy
                puzzleCopy = (int[,])down.puzz.Clone();

                //holding the next swaped num in newData
                newData = puzzleCopy[adjacent[0], adjacent[1]];
                puzzleCopy[adjacent[0], adjacent[1]] = puzzleCopy[row, column];
                puzzleCopy[row, column] = newData;
                down.puzz = puzzleCopy;

                //creating the puzzle string
                down.hashp = puzzle_string(down);
                if (choice == 1)
                {
                    //calculate the hamming for the puzzle
                    int hamm = Hamming(down);
                    down.H = hamm;
                    down.Total_F = hamm + down.level;
                    if (!Program.puzzle_list.Contains(down.hashp))
                        Program.p.Enqueue(down, down.Total_F);
                }
                else if (choice == 21)
                {
                    //calculate the Manhattan for the puzzle
                    int man = ManHattan(down);
                    down.M = man;
                    down.Total_F_manhaten = man + down.level;
                    if (!Program.puzzle_list.Contains(down.hashp))
                        Program.p.Enqueue(down, down.Total_F_manhaten);
                }
             
            }
           

        }

        //this function creats the goal puzzle
        public static int[,] Goal_puzzle(Node intial)
        {
            int z = 0;
            int N = intial.size;
            int[,] goal = new int[N+1, N+1];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    z++;
                    goal[i, j] = z;
                }
            }
            goal[N - 1, N - 1] = 0;
            return goal;
        }

        //this function calculates hamming distance
        public static int Hamming(Node intial)
        {

            int N = intial.size;
            int[,] goal = new int[N, N];
            goal = Goal_puzzle(intial);

            int h = 0;
            for (int i = 0; i < N; i++)
            {

                for (int j = 0; j < N; j++)
                {
                    if (intial.puzz[i, j] != goal[i, j])
                    {
                        if (intial.puzz[i, j] != 0)
                        {
                            h++;
                        }

                    }
                }
            }
            intial.H = h;
            return intial.H;
        }

        //this function calculates Manhattan distance
        public static int ManHattan(Node intial)
        {

            int value = 0;
            int sum = 0;

            int targetRow, targetCol, dx, dy;
            int N = intial.size;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    value = intial.puzz[i, j];
                    if (value != 0)
                    {

                        targetRow = (value - 1) / N;
                        targetCol = (value - 1) % N;
                        dx = i - targetRow;
                        dy = j - targetCol;
                        sum += Math.Abs(dx) + Math.Abs(dy);
                    }
                }
            }
            intial.M = sum;
            return intial.M;
        }

        //this function detects the balnk postion
        public static int[] blank_postion(Node intial)
        {
            int[] pos = new int[2];
            for (int i = 0; i < intial.size; i++)
            {
                for (int j = 0; j < intial.size; j++)
                {
                    if (intial.puzz[i, j] == 0)
                    {
                        pos[0] = i;
                        pos[1] = j;
                        break;
                    }
                }
            }
            return pos;
        }
        //this function converts the puzzle To string to add it in the HashSet
        public string puzzle_string(Node puzzle)
        {
            string sb;
            string total = "";
            int z = 0;
            for (var i = 0; i < puzzle.size; i++)
            {
                for (var j = 0; j < puzzle.size; j++)
                {
                    sb = puzzle.puzz[i, j].ToString();
                    total += sb;
                    z++;
                }
            }
            return total;
        }
    }
}

