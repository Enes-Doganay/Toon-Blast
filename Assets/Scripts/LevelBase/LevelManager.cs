using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Board board;
    [SerializeField] private FallAndFillManager fallAndFillManager;
    [SerializeField] private GoalManager goalManager;
    [SerializeField] private MovesManager movesManager;
    private LevelData levelData;

    private void Start()
    {
        PrepareLevel();
        InitFallAndFills();
        movesManager.Init(levelData.Moves);
        goalManager.Init(levelData.Goals);

    }

    private void PrepareLevel()
    {
        int currentLevel = PlayerPrefs.GetInt("Level", 1);
        levelData = LevelDataFactory.CreateLevelData(currentLevel);

        for (int y = 0; y < levelData.GridData.GetLength(0); y++)
        {
            for(int x = 0; x < levelData.GridData.GetLength(1); x++)
            {
                var cell = board.Cells[x, y];

                var itemType = levelData.GridData[x, y];
                var item = ItemFactory.Instance.CreateItem(itemType, board.ItemsParent);
                if (item == null) continue;

                cell.Item = item;
                item.transform.position = cell.transform.position;
            }
        }
    }

    private void InitFallAndFills()
    {
        FallAndFillManager.Instance.Init(board, levelData);
    }
}