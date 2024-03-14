using UnityEngine;
using DG.Tweening;
public class FallAnimation : MonoBehaviour
{
    public Item Item;
    [HideInInspector] public Cell TargetCell;

    [SerializeField] private float animationDuration = 0.35f;
    private Vector3 targetPosition;
    public void FallTo(Cell targetCell)
    {
        if (TargetCell != null && targetCell.Y >= TargetCell.Y) return;

        TargetCell = targetCell;
        Item.Cell = TargetCell;
        targetPosition = TargetCell.transform.position;

        Item.transform.DOMoveY(targetPosition.y, animationDuration).SetEase(Ease.InCubic).OnComplete(()=>
        {
            TargetCell = null;
        });
    }
}