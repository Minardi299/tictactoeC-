namespace TicTacToe
{

    public class TicTacToeGrid
    {
        public char[,] grid;    
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
          
        }

        public void PlaceCharacter(char x,int row, int column)
        {
            row = row - 1;
            column = column - 1;
            while (row > this.grid.GetLength(0) && column > this.grid.GetLength(1))
            {
                Console.WriteLine("Re Enter row: ");
                row = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Re Enter column: ");
                column = Convert.ToInt32(Console.ReadLine());
            }
            this.grid[row,column] = x;
            
        }

        public void PrintGrid()
        {
            for (int i = 0; i< this.grid.GetLength(0); i++ )
            {
                for ( int j = 0; j < this.grid.GetLength(1); j++)
                {
                    Console.Write("|"+grid[i,j]);
                }
                Console.WriteLine();
            }
        }
        
        private String CheckRows(){
            return "The winner";
        }
        private String CheckColumn(){
            return "The winner";
        }
        private String CheckDiagonals(){
            return "The winner";
        }

        public String CheckGrid(){
            CheckDiagonals();
            CheckRows();
            CheckColumn();

            return "The winner";
        }
    }

    public class TicTacToeGame
    {
        static void Main(string[] args)
        {
            TicTacToeGrid grid = new TicTacToeGrid(4);
            Console.WriteLine("the main method");
            grid.PlaceCharacter('H',4,1);
            grid.PlaceCharacter('G',1,4);
            grid.PrintGrid();
            // String winner = grid.CheckGrid();
            

        }
    }
}