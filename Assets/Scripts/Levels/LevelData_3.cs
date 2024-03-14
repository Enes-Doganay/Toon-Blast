using System.Collections.Generic;

public class LevelData_3 : LevelData
{
    private static readonly ItemType[] ColoredBalloonArray = new[]
    {
            ItemType.GreenBalloon,
            ItemType.YellowBalloon,
            ItemType.BlueBalloon,
            ItemType.RedBalloon
        };

    public override ItemType GetNextFillItemType()
    {
        return GetRandomCubeItemType();
    }

    public override void Initialize()
    {
        GridData = new ItemType[Board.Rows, Board.Cols];

        Goals = new List<LevelGoal>
        {
            new LevelGoal { ItemType = ItemType.RedBalloon, Count = 0 },
            new LevelGoal { ItemType = ItemType.GreenBalloon, Count = 0 },
            new LevelGoal { ItemType = ItemType.BlueBalloon, Count = 0 },
            new LevelGoal { ItemType = ItemType.YellowBalloon, Count = 0 }
        };

        for (var y = 0; y < Board.Rows; y++)
        {
            ItemType item;

            item = GetRandomColoredBalloonItemType();
            GridData[0, y] = item;
            Goals.Find(g => g.ItemType == item).Count++;
            

            item = GetRandomColoredBalloonItemType();
            GridData[Board.Cols - 1, y] = item;
            Goals.Find(g => g.ItemType == item).Count++;

        }

        for (var y = 0; y < Board.Rows; y++)
        {
            for (var x = 1; x < Board.Cols - 1; x++)
            {
                if (GridData[x, y] != ItemType.None) continue;
                GridData[x, y] = GetRandomCubeItemType();
            }
        }



        Moves = 20;
    }

    private ItemType GetRandomColoredBalloonItemType()
    {
        return GetRandomItemTypeFromArray(ColoredBalloonArray);
    }
}