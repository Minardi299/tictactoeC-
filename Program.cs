using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

namespace TicTacToe
{
    public enum TicTacToeMark
    {
        X,
        O,
        Empty
    }
    public class TicTacToePosition
    {
        private TicTacToeMark _mark;
        public TicTacToeMark Mark
        {
            get => this._mark;
            set
            {
                if (value != TicTacToeMark.X && value != TicTacToeMark.O && value != TicTacToeMark.Empty)
                {
                    throw new ArgumentException("Invalid mark value.");
                }
                if ((this._mark == TicTacToeMark.X || this._mark == TicTacToeMark.O) && value != this._mark)
                {
                    throw new ArgumentException("Position already contains X or O.");
                }
                this._mark = value;
            }
        }
        public override string ToString()
        {
            switch (_mark)
            {
                case TicTacToeMark.X:
                    return "X";
                case TicTacToeMark.O:
                    return "O";
                case TicTacToeMark.Empty:
                default:
                    return " ";
            }
        }
        public TicTacToePosition()
        {
            this._mark = TicTacToeMark.Empty;
        }
    }
    public class TicTacToeGrid
    {
        public TicTacToePosition[,] grid;   
        public int turn; 
        public string Winner
        {
            get
            {
                string winner = CheckGrid();
                if (winner == "X wins" || winner == "O wins")
                {
                    return winner.Substring(0, 1);
                }
                else if (winner == "Tie")
                {
                    return "Tie";
                }
                else
                {
                    return "Ongoing";
                }
            }
        }
//Call 3 method to check row, column and diagonal to determine if the game is won or not
        public String CheckGrid(){
            TicTacToeMark diagonal = CheckDiagonals();
            if (diagonal != TicTacToeMark.Empty )
            {
                return diagonal.ToString() + " wins";
            }
            TicTacToeMark row = CheckRows();
            if (row != TicTacToeMark.Empty)
            {
                return row.ToString() + " wins";
            }
            TicTacToeMark column = CheckColumn();
            if (column != TicTacToeMark.Empty)
            {
                return column.ToString() + " wins";
            }

            if (IsGridFull())
            {
                return "Tie";
            }
            return " ";
        }
        public TicTacToeGrid(int size)
        {
            this.grid = new TicTacToePosition[size,size];
            for ( int i = 0; i < size; i++ )
            {
                for ( int j = 0; j < size; j++)
                {
                   grid[i,j]=new TicTacToePosition();
                }
            }
            this.turn = 0;
          
        }
//Place a character down in the grid  at position row and column
        public void PlaceCharacter(TicTacToePosition x,int row, int column)
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
//Loop through the grid and print out all the characters
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
        
        private TicTacToeMark CheckRows(){
            for (int i = 0; i < this.grid.GetLength(0); i++)
            {
                TicTacToeMark symbol = this.grid[i,0].Mark;
                bool match = true;
                for  ( int j = 0; j < this.grid.GetLength(1); j++)
                {
                    
                    if (this.grid[i,j].Mark != symbol )
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
            return TicTacToeMark.Empty;
        }
        private TicTacToeMark CheckColumn(){
            for (int i = 0; i < this.grid.GetLength(1); i++)
            {
                TicTacToeMark symbol = this.grid[0,i].Mark;
                bool match = true;
                for  ( int j = 0; j < this.grid.GetLength(0); j++)
                {
                    
                    if (this.grid[j,i].Mark != symbol )
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
            return TicTacToeMark.Empty;
        }
        private TicTacToeMark CheckDiagonals(){
            
            TicTacToeMark mainDiagonal = this.grid [0,0].Mark;
            bool match = true;
            for ( int i = 0; i < this.grid.GetLength(1); i++)
            {
                if ( this.grid[i,i].Mark !=  mainDiagonal )
                {
                    match = false;
                    break;
                }
            }
            if(match)
            {
                return mainDiagonal;
            }

            TicTacToeMark antiDiagonal = this.grid[0,this.grid.GetLength(0)-1].Mark;
            bool antiMatch = true;
            for ( int i = 0; i < this.grid.GetLength(0); i++)
            {
                if ( this.grid[i,this.grid.GetLength(0)-1-i].Mark != antiDiagonal)
                {
                    antiMatch = false;
                    break;
                }
            }
            if(antiMatch)
            {
                return antiDiagonal;
            }
            return TicTacToeMark.Empty;
        }
//Check if the grid is full and end the game if it is
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
                TicTacToePosition character = new();
                if(IsEven(grid.turn))
                {
                    character.Mark = TicTacToeMark.X;
                }
                else{
                    character.Mark = TicTacToeMark.O;
                }
                grid.PlaceCharacter(character,row,column);
                grid.PrintGrid();
                Console.WriteLine(grid.CheckGrid());
            }
        }
        static bool IsEven(int number)
    {
        return number % 2 == 0;
    }
    }
}