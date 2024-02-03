using System.Globalization;
using System.Text.RegularExpressions;

namespace TicTacToe
{

    public class TicTacToeGrid
    {
        public char[,] grid;   
        public int turn; 
        public TicTacToeGrid(int size)
        {
            this.grid = new char[size,size];
            for ( int i = 0; i < size; i++ )
            {
                for ( int j = 0; j < size; j++)
                {
                   grid[i,j]=' ';
                }
            }
            this.turn = 0;
          
        }

        public void PlaceCharacter(char x,int row, int column)
        {
            row--;
            column--;
            while (row < 0 || column < 0 || row >= this.grid.GetLength(0) || column >= this.grid.GetLength(1))
            {
                Console.WriteLine("Re Enter row: ");
                row = Convert.ToInt32(Console.ReadLine())-1;
                Console.WriteLine("Re Enter column: ");
                column = Convert.ToInt32(Console.ReadLine())-1;
            }
            // while (x != 'X' || x != 'O' )
            // {
            //     Console.WriteLine("Re Enter  X or O :");
            //     ConsoleKeyInfo keyInfo = Console.ReadKey();
            //     x = keyInfo.KeyChar ;
            //     char.ToUpper(x);
            // }
            this.grid[row,column] = x;
            this.turn++;
        }

        public void PrintGrid()
        {
            for (int i = 0; i< this.grid.GetLength(0); i++ )
            {
                for ( int j = 0; j < this.grid.GetLength(1); j++)
                {
                    Console.Write("|"+grid[i,j]);
                }
                Console.Write("|");
                Console.WriteLine();
            }
        }
        
        private char CheckRows(){
            for (int i = 0; i < this.grid.GetLength(0); i++)
            {
                char symbol = grid[i,0];
                bool match = true;
                for  ( int j = 0; j < this.grid.GetLength(1); j++)
                {
                    
                    if (this.grid[i,j] != symbol )
                    {
                        match = false;
                        break;
                    }
                }
                if(match)
                {
                    return symbol;
                }
            }
            return ' ';
        }
        private char CheckColumn(){
            for (int i = 0; i < this.grid.GetLength(1); i++)
            {
                char symbol = this.grid[0,i];
                bool match = true;
                for  ( int j = 0; j < this.grid.GetLength(0); j++)
                {
                    
                    if (this.grid[j,i] != symbol )
                    {
                        match = false;
                        break;
                    }
                }
                if(match)
                {
                    return symbol;
                }
            }
            return ' ';
        }
        private char CheckDiagonals(){
            
            char mainDiagonal = this.grid [0,0];
            bool match = true;
            for ( int i = 0; i < this.grid.GetLength(1); i++)
            {
                if ( this.grid[i,i] !=  mainDiagonal )
                {
                    match = false;
                    break;
                }
            }
            if(match)
            {
                return mainDiagonal;
            }

            char antiDiagonal = this.grid[0,this.grid.GetLength(0)-1];
            bool antiMatch = true;
            for ( int i = 0; i < this.grid.GetLength(0); i++)
            {
                if ( this.grid[i,this.grid.GetLength(0)-1-i] != antiDiagonal)
                {
                    antiMatch = false;
                    break;
                }
            }
            if(antiMatch)
            {
                return antiDiagonal;
            }
            return ' ';
        }


        public String CheckGrid(){
            char diagonal = CheckDiagonals();
            if (diagonal != ' ')
            {
                return diagonal + " wins";
            }
            char row = CheckRows();
            if (row != ' ')
            {
                return row + " wins";
            }
            char column = CheckColumn();
            if (column != ' ')
            {
                return column + " wins";
            }

            if (IsGridFull())
            {
                return "Tie";
            }
            return " ";
        }
        public bool IsGridFull()
        {
            if (this.turn >= this.grid.GetLength(0) * this.grid.GetLength(0))
            {
                return true;
            }
            return false;
        }
    }

    public class TicTacToeGame
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter size of grid");
            int size = Convert.ToInt32(Console.ReadLine());

            TicTacToeGrid grid = new TicTacToeGrid(size);
            while(grid.CheckGrid() == " ")
            {
                Console.WriteLine("Enter which row to play");
                int row = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter which column to play");
                int column = Convert.ToInt32(Console.ReadLine());
                char character = ' ';
                if(IsEven(grid.turn))
                {
                    character = 'X';
                }
                else{
                    character = 'O';
                }
                grid.PlaceCharacter(character,row,column);
                grid.PrintGrid();

            }

            Console.WriteLine(grid.CheckGrid());


            

            // String winner = grid.CheckGrid();
            

        }
        static bool IsEven(int number)
    {
        return number % 2 == 0;
    }
    }
}