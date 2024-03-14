using DG.Tweening;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RocketBombCombo : ComboEffect
{
    private RocketAnimation lastRocketAnimation;
    public override void ApplyEffect(Cell cell, List<Cell> matchedCells)
    {
        PrepareCellForAnimation(cell);
        CreateMergeAnimationForMatchedCells(cell, matchedCells);
        FallAndFillManager.Instance.StopFall();

        mergeTween.OnComplete(() =>
        {

            UpdateCellAreaSprite(cell);
            
            RemoveItemFromMatchedCells(matchedCells);

            ExecuteItemsInAffectedCells(cell);

            AudioManager.Instance.PlayEffect(SoundID.Rocket);

            lastRocketAnimation.OnAnimationComplete = () =>
            {
                FallAndFillManager.Instance.StartFall();
            };
        });
    }

    public override List<Cell> GetAffectedCells(Cell cell)
    {
        List<Cell> affectedCells = new List<Cell>();
        List<Cell> allAreaCells = cell.AllArea;

        foreach (Cell area in allAreaCells)
        {
            var columnCells = area.GetColumnList();
            var rowCells = area.GetRowList();

            affectedCells.AddRange(columnCells);
            affectedCells.AddRange(rowCells);
        }

        return affectedCells;
    }

    private void UpdateCellAreaSprite(Cell cell)
    {
        List<Cell> cellAllArea = cell.AllArea;
        foreach (var areaCell in cellAllArea)
        {
            if (areaCell == null) continue;

            SetSpriteByDirection(cell, areaCell);
        }
    }
    private void SetSpriteByDirection(Cell cell, Cell areaCell)
    {
        var imageLibrary = ItemImageLibrary.Instance;

        if(areaCell.Item == null)
        {
            var item = ItemFactory.Instance.CreateItem(ItemType.HorizontalRocket, areaCell.Board.ItemsParent);
            areaCell.Item = item;
            item.transform.position = areaCell.transform.position;
        }

        areaCell.Item.SpriteRenderer.enabled = false;

        if (areaCell.Y > cell.Y)
        {
            CreateRocketAnimation<UpRocketAnimation>(areaCell, imageLibrary.RocketUp);
        }
        else if (areaCell.Y < cell.Y)
        {
            CreateRocketAnimation<DownRocketAnimation>(areaCell, imageLibrary.RocketDown);
        }
        if (areaCell.X > cell.X)
        {
            CreateRocketAnimation<RightRocketAnimation>(areaCell, imageLibrary.RocketRight);
        }
        else if (areaCell.X < cell.X)
        {
            CreateRocketAnimation<LeftRocketAnimation>(areaCell, imageLibrary.RocketLeft);
        }
    }

    private void CreateRocketAnimation<T>(Cell cell, Sprite sprite) where T : RocketAnimation
    {
        SpriteRenderer spriteRenderer = cell.Item.AddSprite(sprite);
        T animation = cell.Item.AddComponent<T>();
        lastRocketAnimation = animation;

        animation.PrepareAnimationSprite(spriteRenderer);
        animation.ExecuteRocketAnimation();
    }
}