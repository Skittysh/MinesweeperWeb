public class Game
{
    public Board board;

    public bool EmptyCell(int row, int col)
    {
        if (board.isRevealed(row, col))
            return true;
        return false;
    }

    public int getMines(int row, int col)
    {
        return board.GetMinesAround(row, col);
    }

    public Game(int height, int width, int mines)
    {
        board = new Board(height, width, mines);
    }

    public void ExecuteReveal(int row, int col)
    {
        board.RevealCell(row, col);
    }
}