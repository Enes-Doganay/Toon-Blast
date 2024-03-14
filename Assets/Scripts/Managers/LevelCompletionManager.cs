using System.Collections;
using UnityEngine;

public class LevelCompletionManager : MonoBehaviour
{
    [SerializeField] private Board board;

    private float rocketCreationDelay = 0.15f;
    private float specialItemExecutionDelay = 0.1f;

    private void OnEnable()
    {
        GoalManager.Instance.OnGoalsCompleted += HandleGoalsCompleted;
    }

    private void OnDisable()
    {
        GoalManager.Instance.OnGoalsCompleted -= HandleGoalsCompleted;
    }

    private void HandleGoalsCompleted()
    {
        StartCoroutine(HandleGoalsCompletedCoroutine());
    }

    private IEnumerator HandleGoalsCompletedCoroutine()
    {
        yield return new WaitForSeconds(1f);

        StartCoroutine(CreateRocketsCoroutine());
    }

    private IEnumerator CreateRocketsCoroutine()
    {
        int rocketsToCreate = MovesManager.Instance.Moves;
        int rocketsCreated = 0;

        while (rocketsCreated < rocketsToCreate)
        {
            if(CreateRocket())
                rocketsCreated++;

            yield return new WaitForSeconds(rocketCreationDelay);
        }

        ExecuteAllSpecialItems();
    }

    private bool CreateRocket()
    {
        Cell cell = board.GetRandomCell();

        if (cell != null && cell.Item != null && cell.Item.GetMatchType() != MatchType.Special)
        {
            CreateRocket(cell);
            MovesManager.Instance.DecraseMoves();
            return true;
        }
        return false;
    }

    private void CreateRocket(Cell cell)
    {
        var itemType = (Random.Range(0, 2) == 0) ? ItemType.VerticalRocket : ItemType.HorizontalRocket;
        var item = ItemFactory.Instance.CreateItem(itemType, board.ItemsParent);

        cell.Item.RemoveItem();
        cell.Item = item;
        item.Cell = cell;
        item.transform.position = cell.transform.position;
    }

    private void ExecuteAllSpecialItems()
    {
        StartCoroutine(ExecuteAllSpecialItemsCoroutine());
    }

    private IEnumerator ExecuteAllSpecialItemsCoroutine()
    {
        yield return new WaitForSeconds(1f);

        for (int y = 0; y < Board.Rows; y++)
        {
            for (int x = 0; x < Board.Cols; x++)
            {
                Cell cell = board.Cells[x, y];
                if (cell.Item != null && cell.Item.GetMatchType() == MatchType.Special)
                {
                    cell.Item.TryExecute();
                    yield return new WaitForSeconds(specialItemExecutionDelay);
                }
            }
        }

        yield return new WaitForSeconds(1f);
        UIManager.Instance.SetLevelCompletedPanel();
    }
}