using System.Collections.Generic;
public class ComboManager : Singleton<ComboManager>
{
    private Dictionary<ComboType, ComboEffect> comboEffects;
    private List<Cell> matchedCells;
    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    private void Init()
    {
        comboEffects = new Dictionary<ComboType, ComboEffect>
        {
            {ComboType.RocketRocket, new RocketRocketCombo() },
            {ComboType.RocketBomb, new RocketBombCombo() },
            {ComboType.BombBomb, new BombBombCombo() },
        };
    }

    public ComboType GetComboType(Cell cell)
    {
        matchedCells = MatchingManager.Instance.FindMatches(cell, MatchType.Special);
        if (matchedCells.Count <= 1) return ComboType.None;

        int bombCount = 0;
        int rocketCount = 0;

        foreach(var matchedCell in matchedCells)
        {
            var matchedItem = matchedCell.Item;
            if (matchedItem.ItemType == ItemType.Bomb)
            {
                bombCount++;
            }
            else if (matchedItem.ItemType == ItemType.HorizontalRocket || matchedItem.ItemType == ItemType.VerticalRocket)
            {
                rocketCount++;
            }
        }

        if (bombCount > 1) return ComboType.BombBomb;
        else if (rocketCount >= 1 && bombCount >= 1) return ComboType.RocketBomb;
        return ComboType.RocketRocket;
    }

    public void TryExecute(Cell cell)
    {
        ComboType comboType = GetComboType(cell);

        if(comboEffects.TryGetValue(comboType, out var comboEffect))
        {
            comboEffect.ApplyEffect(cell, matchedCells);
        }
        else
        {
            cell.Item.TryExecute();
        }

        MovesManager.Instance.DecraseMoves();
    }
}