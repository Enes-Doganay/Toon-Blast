using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BombBombCombo : ComboEffect
{
    private float growthTime = 0.3f;
    private float shakeDuration = 0.7f;
    private float shakeStrength = 0.2f;
    public override void ApplyEffect(Cell cell, List<Cell> matchedCells)
    {
        CreateMergeAnimationForMatchedCells(cell, matchedCells);

        mergeTween.OnComplete(() =>
        {
            ShakeAnimation.ApplyShakeAnimation(cell.Item.transform, shakeDuration, shakeStrength).OnComplete(() =>
            {
                CameraManager.Instance.TriggerShakeEffect();
                ExecuteItemsInAffectedCells(cell);
            });

        });
    }

    public override List<Cell> GetAffectedCells(Cell cell)
    {
        List<Cell> affectedCells = new List<Cell>();
        List<Cell> cellAllArea = cell.AllArea;

        affectedCells.AddRange(cellAllArea);

        for (int i = 0; i < 2; i++)
        {
            List<Cell> tempList = new List<Cell>(affectedCells);
            foreach (var c in tempList)
            {
                affectedCells.AddRange(c.AllArea);
            }
            affectedCells = affectedCells.Distinct().ToList();
        }
        return affectedCells;
    }

    protected override void CreateMergeAnimationForMatchedCells(Cell cell, List<Cell> matchedCells)
    {
        base.CreateMergeAnimationForMatchedCells(cell, matchedCells);

        var item = cell.Item;
        item.SpriteRenderer.sortingOrder += 10;
        mergeTween = item.transform.DOScale(cell.Item.transform.localScale * 3, growthTime);

    }
}