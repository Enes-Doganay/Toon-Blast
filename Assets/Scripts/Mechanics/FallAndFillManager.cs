using System.Collections.Generic;

public class FallAndFillManager : Singleton<FallAndFillManager>
{
    private bool isActive;
    private Board board;
    private LevelData levelData;
    private Cell[] fillingCells;
    public void Init(Board board, LevelData levelData)
    {
        this.board = board;
        this.levelData = levelData;
        
        FindFillingCells();
        StartFall();
    }

    public void FindFillingCells()
    {
        var cellList = new List<Cell>();

        for(var y = 0; y < Board.Rows; y++)
        {
            for(var x = 0; x < Board.Cols; x++)
            {
                var cell = board.Cells[x, y];

                if(cell != null && cell.IsFillingCell)
                {
                    cellList.Add(cell);
                }

            }
        }
        fillingCells = cellList.ToArray();
    }

    public void DoFalls()
    {
        for(int y = 0; y < Board.Rows; y++)
        {
            for (int x = 0; x < Board.Cols; x++)
            {
                var cell = board.Cells[x, y];

                if (cell.Item != null && cell.FirstCellBelow != null && cell.FirstCellBelow.Item == null)
                {
                    cell.Item.Fall();
                }
            }
        }
    }

    public void DoFills()
    {
        for (int i = 0; i < fillingCells.Length; i++)
        {
            var cell = fillingCells[i];

            if(cell.Item == null)
            {
                cell.Item = ItemFactory.Instance.CreateItem(levelData.GetNextFillItemType(), board.ItemsParent);

                var offsetY = 0.0f;
                var targetCellBelow = cell.GetFallTarget().FirstCellBelow;

                if(targetCellBelow != null)
                {
                    if(targetCellBelow.Item != null)
                    {
                        offsetY = targetCellBelow.Item.transform.position.y + 1;
                    }
                }

                var pos = cell.transform.position;
                pos.y += 2;
                pos.y = pos.y > offsetY ? pos.y : offsetY;

                if (cell.Item == null) continue;

                cell.Item.transform.position = pos;
                cell.Item.Fall();
            }
        }
    }
    public void StartFall() { isActive = true; }
    public void StopFall() { isActive = false; }

    private void Update()
    {
        if (!isActive) return;

        DoFalls();
        DoFills();
    }
}
