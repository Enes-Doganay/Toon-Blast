using UnityEngine;

public class Board : MonoBehaviour
{
    public Transform CellsParent;
    public Transform ItemsParent;
    public Transform ParticlesParent;
    [SerializeField] private Cell cellPrefab;
    
    public const int Rows = 9;
    public const int Cols = 9;

    public readonly Cell[,] Cells = new Cell[Cols, Rows];
    private void Awake()
    {
        Prepare();
    }
    public void Prepare()
    {
        CreateCells();
        PrepareCells();
    }
    private void CreateCells()
    {
        for(int y = 0; y < Rows; y++)
        {
            for (int x = 0; x < Cols; x++)
            {
                var cell = Instantiate(cellPrefab, Vector3.zero, Quaternion.identity, CellsParent);
                Cells[x, y] = cell;
            }
        }
    }

    private void PrepareCells()
    {
        for (int y = 0; y < Rows; y++)
        {
            for(int x= 0; x < Cols; x++)
            {
                Cells[x, y].Prepare(x, y, this);
            }
        }
    }
    /*
    private void CreateRandomRocket()
    {
        int rocketsToCreate = MovesManager.Instance.Moves;
        int rocketsCreated = 0;

        while (rocketsCreated < rocketsToCreate)
        {
            Cell cell = GetRandomCell();

            if (cell != null && cell.Item != null && cell.Item.GetMatchType() != MatchType.Special)
            {
                cell.Item.RemoveItem();

                var itemType = (Random.Range(0, 2) == 0) ? ItemType.VerticalRocket : ItemType.HorizontalRocket;
                var item = ItemFactory.Instance.CreateItem(itemType, ItemsParent);

                cell.Item = item;
                item.Cell = cell;
                item.transform.position = cell.transform.position;

                rocketsCreated++;
            }
        }
    }
    */

    public Cell GetRandomCell()
    {
        int randomRow = Random.Range(0, Rows);
        int randomCol = Random.Range(0, Cols);

        return Cells[randomRow, randomCol];
    }
}
