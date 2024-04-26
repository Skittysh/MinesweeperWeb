public class Board
{
    public int height;
    public int width;
    public int mines;
    private Cell[,] cells;
    private bool isFirstReveal;
    private int fieldsToReveal;

    public int FieldsToReveal => fieldsToReveal;


    public Board(int height, int width, int mines)
    {
        this.height = height;
        this.width = width;
        this.mines = mines;
        cells = new Cell[height, width];
        fieldsToReveal = height * width - mines;
        isFirstReveal = false;

        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                cells[row, col] = new Cell();
            }
        }
    }

    private void GenerateMines(int row, int col)
    {
        Random szansa = new Random();
        for (int minesStart = 0; minesStart < mines; minesStart++)
        {
            int tempHeight, tempWidth;
            do
            {
                tempHeight = szansa.Next(0, height);
                tempWidth = szansa.Next(0, width);
            } while ((tempHeight == row && tempWidth == col) ||
                     (tempHeight == (row - 1) && tempWidth == col) ||
                     (tempHeight == (row + 1) && tempWidth == col) ||
                     (tempHeight == (row) && tempWidth == (col - 1)) ||
                     (tempHeight == (row) && tempWidth == (col + 1)) ||
                     (tempHeight == (row - 1) && tempWidth == (col + 1)) ||
                     (tempHeight == (row + 1) && tempWidth == (col + 1)) ||
                     (tempHeight == (row - 1) && tempWidth == (col - 1)) ||
                     (tempHeight == (row + 1) && tempWidth == (col - 1)) ||
                     hasMine(tempHeight, tempWidth));

            setMine(tempHeight, tempWidth);
        }
    }
    
    public void RevealCell(int row, int col)
    {
            
        if (isFirstReveal == false)
        {
            GenerateMines(row, col);
            isFirstReveal = true;
        }

        if (row >= 0 && row < height && col >= 0 && col < width)
        {
            if (!cells[row, col].hasMine && !cells[row, col].isRevealed)
            {
                cells[row, col].isRevealed = true;
                fieldsToReveal--;
                int mineCounter = GetMinesAround(row, col);
                if (mineCounter == 0)
                {
                    RevealCell(row - 1, col);
                    RevealCell(row + 1, col);
                    RevealCell(row, col - 1);
                    RevealCell(row, col + 1);
                    RevealCell(row - 1, col - 1);
                    RevealCell(row + 1, col + 1);
                    RevealCell(row - 1, col + 1);
                    RevealCell(row + 1, col - 1);
                }
            }
        }
    }

    public void ToggleFlag(int row, int col)
    {
        if (cells[row, col].hasFlag)
        {
            cells[row, col].hasFlag = false;
        }
        else
            cells[row, col].hasFlag = true;
    }

    private void setMine(int row, int col)
    {
        cells[row, col].hasMine = true;
    }

    public bool hasFlag(int row, int col)
    {
        return cells[row, col].hasFlag;
    }

    public bool hasMine(int row, int col)
    {
        return cells[row, col].hasMine;
    }

    public bool isRevealed(int row, int col)
    {
        return cells[row, col].isRevealed;
    }
    
    public int GetMinesAround(int row, int col)
    {
        int minesNext = 0;
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                int nx = row + dx;
                int ny = col + dy;
                if (nx >= 0 && nx < height && ny >= 0 && ny < width && hasMine(nx, ny))
                {
                    minesNext++;
                }
            }
        }

        return minesNext;
    }
}

public class Cell
{
    public Cell()
    {
        hasMine = false;
        hasFlag = false;
        isRevealed = false;
    }

    public bool hasMine = false;
    public bool hasFlag = false;
    public bool isRevealed = false;
}