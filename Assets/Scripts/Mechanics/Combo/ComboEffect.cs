using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
public abstract class ComboEffect : MonoBehaviour
{
    public abstract void ApplyEffect(Cell cell, List<Cell> matchedCells);
    public abstract List<Cell> GetAffectedCells(Cell cell);

    public float mergeAnimationTime = 0.3f;
    public Tween mergeTween;

    protected virtual void CreateMergeAnimationForMatchedCells(Cell cell, List<Cell> matchedCells)
    {
        cell.Item.SpriteRenderer.sortingOrder += 10;

        foreach (var matchedCell in matchedCells)
        {
            if (matchedCell.Item == cell) return;

            mergeTween = matchedCell.Item.transform.DOMove(cell.transform.position, mergeAnimationTime);
        }
    }
    protected virtual void ExecuteItemsInAffectedCells(Cell cell)
    {
        List<Cell> affectedCells = GetAffectedCells(cell);
        foreach (Cell affectedCell in affectedCells)
        {
            if (affectedCell.Item == null) continue;

            affectedCell.Item.TryExecute();
        }
    }
    protected virtual void RemoveItemFromMatchedCells(List<Cell> matchedCells)
    {
        foreach (var matchedCell in matchedCells)
        {
            if (matchedCell.Item == null) continue;
            matchedCell.Item.RemoveItem();
        }
    }
    protected void PrepareCellForAnimation(Cell cell)
    {
        cell.Item.IsFallable = false;
        cell.Item.SpriteRenderer.enabled = false;
    }
}