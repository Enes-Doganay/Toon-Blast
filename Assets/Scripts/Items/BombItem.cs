using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombItem : Item
{
    private readonly MatchType matchType = MatchType.Special;

    public void PrepareBombItem(ItemBase itemBase)
    {
        SoundID = SoundID.Bomb;
        var bombSprite = ItemImageLibrary.Instance.BombSprite;
        Prepare(itemBase, bombSprite);
    }

    public override MatchType GetMatchType()
    {
        return matchType;
    }

    public override void TryExecute()
    {
        var explodeCellArea = Cell.AllArea;

        AudioManager.Instance.PlayEffect(SoundID);
        base.TryExecute();

        for(int i = 0; i < explodeCellArea.Count; i++)
        {
            var currentCell = explodeCellArea[i];
            if(currentCell.Item == null) continue;
            var item = currentCell.Item;
            item.TryExecute();
        }
    }
}
