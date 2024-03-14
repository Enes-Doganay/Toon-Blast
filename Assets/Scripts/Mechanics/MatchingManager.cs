using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchingManager : Singleton<MatchingManager>
{
    [SerializeField] private Board board;

    private readonly bool[,] visitedCells = new bool[Board.Rows, Board.Cols];

    private int minimumMatchCount = 2;

    public List<Cell> FindMatches(Cell cell, MatchType matchType)
    {
        var matchedCells = new List<Cell>();
        ClearVisitedCells();
        FindMatches(cell, matchType, matchedCells);

        return matchedCells;
    }

    public void FindMatches(Cell cell, MatchType matchType, List<Cell> matchedCells)
    {
        if (cell == null) return;

        var x = cell.X; 
        var y = cell.Y;

        if (visitedCells[x,y]) return;

        if(cell.Item != null && cell.Item.GetMatchType() == matchType && cell.Item.GetMatchType() != MatchType.None)
        {
            visitedCells[x,y] = true;
            matchedCells.Add(cell);

            if (!cell.Item.Clickable) return;

            var neighbours = cell.Neighbours;

            if (neighbours.Count == 0) return;

            for (int i = 0; i < neighbours.Count; i++)
            {
                FindMatches(neighbours[i] , matchType, matchedCells);
            }
        }
    }

    private void ClearVisitedCells()
    {
        for(int x = 0; x < visitedCells.GetLength(0); x++)
        {
            for(int y = 0; y < visitedCells.GetLength(1); y++)
            {
                visitedCells[x, y] = false;
            }
        }
    }

    public int CountMatchedCubeItem(List<Cell> cells)
    {
        int count = 0;
        foreach(Cell cell in cells)
        {
            if(cell.Item.Clickable)
                count++;
        }
        return count;
    }

    public void ExplodeMatchingCells(Cell cell)
    {
        var previousCells = new List<Cell>();

        var matchedCells = FindMatches(cell, cell.Item.GetMatchType());
        var matchedCubeItemCount = CountMatchedCubeItem(matchedCells);

        if (matchedCubeItemCount < minimumMatchCount) return;

        for (int i = 0; i < matchedCells.Count; i++)
        {
            var explodedCell = matchedCells[i];

            ExplodeMatchingCellsInNeightbours(explodedCell, previousCells);

            var item = explodedCell.Item;
            item.TryExecute();
        }

        MovesManager.Instance.DecraseMoves();
        SpawnBonus(cell, matchedCubeItemCount);
    }
    private void SpawnBonus(Cell cell,int matchedCellCount)
    {
        switch (matchedCellCount)
        {
            case >= 7:
                cell.Item = ItemFactory.Instance.CreateItem(ItemType.Bomb, board.ItemsParent);
                break;
            case >= 5:
                var itemType = (Random.Range(0, 2) == 0) ? ItemType.VerticalRocket : ItemType.HorizontalRocket;
                cell.Item = ItemFactory.Instance.CreateItem(itemType, board.ItemsParent);
                break;
            default: 
                break;
        }

        if(cell.Item == null) return;
        cell.Item.transform.position = cell.transform.position;
    }
    private void ExplodeMatchingCellsInNeightbours(Cell cell, List<Cell> previousCells)
    {
        var explodedCellNeightbours = cell.Neighbours;

        for(int j = 0; j < explodedCellNeightbours.Count; j++)
        {
            var neighbourCell = explodedCellNeightbours[j];
            var neighbourCellItem = neighbourCell.Item;

            if(neighbourCellItem != null && !previousCells.Contains(neighbourCell))
            {
                previousCells.Add(neighbourCell);

                if (neighbourCellItem.InterectWithExplode)
                    neighbourCellItem.TryExecute();
            }
        }
    }
}
