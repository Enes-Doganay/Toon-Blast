using System.Collections.Generic;

public class LevelData_0 : LevelData
{
    public override ItemType GetNextFillItemType()
    {
        return GetRandomCubeItemType();
    }

    public override void Initialize()
    {
        GridData = new ItemType[Board.Rows, Board.Cols];

        for (var y = 0; y < Board.Rows; y++)
        {
            for (var x = 0; x < Board.Cols; x++)
            {
                GridData[x, y] = GetRandomCubeItemType();
            }
        }

        Goals = new List<LevelGoal>
        {
            new LevelGoal { ItemType = ItemType.GreenCube, Count = 20 },
            new LevelGoal { ItemType = ItemType.RedCube, Count = 20 }
        };

        Moves = 20;

    }
}