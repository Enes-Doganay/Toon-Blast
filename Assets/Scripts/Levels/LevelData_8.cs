using System.Collections.Generic;

public class LevelData_8 : LevelData
{
    public override ItemType GetNextFillItemType()
    {
        return GetRandomCubeItemType();
    }

    public override void Initialize()
    {
        GridData = new ItemType[Board.Rows, Board.Cols];

        GridData[1, 1] = ItemType.CrateLayer2;
        GridData[1, 7] = ItemType.CrateLayer2;

        GridData[7, 1] = ItemType.CrateLayer2;
        GridData[7, 7] = ItemType.CrateLayer2;

        GridData[3, 3] = ItemType.CrateLayer2;
        GridData[3, 4] = ItemType.CrateLayer2;
        GridData[3, 5] = ItemType.CrateLayer2;

        GridData[4, 3] = ItemType.CrateLayer2;
        GridData[4, 5] = ItemType.CrateLayer2;
            
        GridData[5, 3] = ItemType.CrateLayer2;
        GridData[5, 4] = ItemType.CrateLayer2;
        GridData[5, 5] = ItemType.CrateLayer2;

        for (var y = 0; y < Board.Rows; y++)
        {
            for (var x = 0; x < Board.Cols; x++)
            {
                if (GridData[x, y] != ItemType.None) continue;
                GridData[x, y] = GetRandomCubeItemType();
            }
        }


        Goals = new List<LevelGoal>
        {
            new LevelGoal { ItemType = ItemType.CrateLayer2, Count = 12 }
        };

        Moves = 30;
    }
}
