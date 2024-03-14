using System;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    None = -1,
    Up = 0,
    UpRight = 1,
    Right = 2,
    DownRight = 3,
    Down = 4,
    DownLeft = 5,
    Left = 6,
    UpLeft = 7
}
public class Cell : MonoBehaviour
{
    public TextMesh LabelText;

    [HideInInspector] public int X;
    [HideInInspector] public int Y;

    public List<Cell> Neighbours { get; private set; }
    public List<Cell> AllArea { get; private set; }

    [HideInInspector] public Cell FirstCellBelow;
    [HideInInspector] public bool IsFillingCell;

    private Item _item;

    public Board Board { get; private set; }

    public Item Item
    {
        get
        {
            return _item;
        }
        set
        {
            if (_item == value) return;

            var oldItem = _item;
            _item = value;

            if (oldItem != null && Equals(oldItem.Cell, this))
            {
                oldItem.Cell = null;
            }
            if (value != null)
            {
                value.Cell = this;
            }
        }
    }

    public void Prepare(int x, int y,Board board)
    {
        Board = board;
        X = x;
        Y = y;
        transform.localPosition = new Vector3 (x, y);
        IsFillingCell = Y == Board.Rows - 1;        // Hücrenin Y kordinatý en üst satýra denk ise IsFillingCell'i true olarak ayarla
        
        UpdateLabel();
        UpdateNeighbours();
        UpdateAllArea();
    }

    private void UpdateLabel()
    {
        var cellName = X + " " + Y;
        LabelText.text = cellName;
        gameObject.name = "Cell " + cellName;
    }
    private void UpdateNeighbours()
    {
        Neighbours = new List<Cell>();

        var up = GetNeighbourWithDirection(Direction.Up);
        var down = GetNeighbourWithDirection(Direction.Down);
        var left = GetNeighbourWithDirection(Direction.Left);
        var right = GetNeighbourWithDirection(Direction.Right);

        if(up != null) Neighbours.Add(up);
        if (left != null) Neighbours.Add(left);
        if(right != null) Neighbours.Add(right);

        if (down != null)
        {
            Neighbours.Add(down);
            FirstCellBelow = down;
        }
    }
    private Cell GetNeighbourWithDirection(Direction direction)
    {
        var x = X;
        var y = Y;
        switch (direction)
        {
            case Direction.None:
                break;
            case Direction.Up:
                y += 1;
                break;
            case Direction.UpRight:
                y += 1;
                x += 1;
                break;
            case Direction.Right:
                x += 1;
                break;
            case Direction.DownRight:
                y -= 1;
                x += 1;
                break;
            case Direction.Down:
                y -= 1;
                break;
            case Direction.DownLeft:
                y -= 1;
                x -= 1;
                break;
            case Direction.Left:
                x -= 1;
                break;
            case Direction.UpLeft:
                y += 1;
                x -= 1;
                break;
            default:
                throw new ArgumentOutOfRangeException("direction", direction, null);
        }

        if (x >= Board.Cols || x < 0 || y >= Board.Rows || y < 0) return null;

        return Board.Cells[x, y];
    }
    public void UpdateAllArea()
    {
        AllArea = new List<Cell>();

        var up = GetNeighbourWithDirection(Direction.Up);
        var upRight = GetNeighbourWithDirection(Direction.UpRight);
        var right = GetNeighbourWithDirection(Direction.Right);
        var downRight = GetNeighbourWithDirection(Direction.DownRight);
        var down= GetNeighbourWithDirection(Direction.Down);
        var downLeft= GetNeighbourWithDirection(Direction.DownLeft);
        var left= GetNeighbourWithDirection(Direction.Left);
        var upLeft = GetNeighbourWithDirection(Direction.UpLeft);

        if (up != null) AllArea.Add(up);
        if (upRight != null) AllArea.Add(upRight);
        if (right != null) AllArea.Add(right);
        if (downRight != null) AllArea.Add(downRight);
        if (down != null) AllArea.Add(down);
        if (downLeft != null) AllArea.Add(downLeft);
        if (left != null) AllArea.Add(left);
        if (upLeft != null) AllArea.Add(upLeft);
    }
    public void CellTapped()
    {
        if (Item == null) return;

        SpecialTapSwitcher();
    }

    private void SpecialTapSwitcher()
    {
        switch (Item.GetMatchType())
        {
            case MatchType.Special:
                ComboManager.Instance.TryExecute(this);
            break;
                default: 
                MatchingManager.Instance.ExplodeMatchingCells(this);
                break;
        }
    }

    public Cell GetFallTarget()
    {
        var targetCell = this;
        if(targetCell.FirstCellBelow != null && targetCell.FirstCellBelow.Item == null)
        {
            targetCell = targetCell.FirstCellBelow;
        }
        return targetCell;
    }

    public List<Cell> GetColumnList()
    {
        var columnList = new List<Cell>();

        for (int i = 0; i < Board.Cols; i++)
        {
            columnList.Add(Board.Cells[i, Y]);
        }

        return columnList;
    }
    public List<Cell> GetRowList()
    {
        var rowList = new List<Cell>();

        for (int i = 0; i < Board.Rows; i++)
        {
            rowList.Add(Board.Cells[X, i]);
        }
        
        return rowList;
    }
}
