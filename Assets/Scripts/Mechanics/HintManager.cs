using System;
using System.Collections.Generic;
using UnityEngine;

public class HintManager : MonoBehaviour
{
    [SerializeField] private Board board;
    private void ShowHints()
    {
        var visitedCells = new List<Cell>();

        for (var y = 0; y < Board.Rows; y++)
        {
            for(var x = 0; x < Board.Cols; x++)
            {
                var cell = board.Cells[x, y];

                if(visitedCells.Contains(cell) || cell.Item == null) continue;

                var matchedCells = MatchingManager.Instance.FindMatches(cell, cell.Item.GetMatchType());
                var matchedCubeCount = MatchingManager.Instance.CountMatchedCubeItem(matchedCells);
                
                visitedCells.AddRange(matchedCells);

                for (int i = 0; i < matchedCubeCount; i++)
                {
                    var currentItem = matchedCells[i].Item;

                    CheckHintForCombo(currentItem, matchedCubeCount);
                    HintSpriteUpdate(currentItem, matchedCubeCount);
                }
            }
        }
    }
    private void CheckHintForCombo(Item item,int comboCount)
    {
        if(item.GetMatchType() == MatchType.Special && comboCount > 1)
        {
            if (item.Particle != null) return;

            var particle = ParticleManager.Instance.ComboHintParticle;
            var particleObj = Instantiate(particle, item.transform.position, Quaternion.identity, item.transform);
            item.Particle = particleObj;
        }
        else if(item.Particle != null)
        {
            Destroy(item.Particle.gameObject);
        }
    }
    private void HintSpriteUpdate(Item item, int matchedCount)
    {
        switch (matchedCount)
        {
            case < 5:
                item.HintUpdateToSprite(item.ItemType);
                break;
            case < 7:
                item.HintUpdateToSprite(ItemType.VerticalRocket);
                break;
            default:
                item.HintUpdateToSprite(ItemType.Bomb);
                break;
        }

    }
    private void Update()
    {
        ShowHints();
    }
}
