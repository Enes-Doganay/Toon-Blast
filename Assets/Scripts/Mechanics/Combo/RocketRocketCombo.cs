using DG.Tweening;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class RocketRocketCombo : ComboEffect
{
    private SpriteRenderer leftRocketRenderer;
    private SpriteRenderer rightRocketRenderer;
    private SpriteRenderer upRocketRenderer;
    private SpriteRenderer downRocketRenderer;
    private RocketAnimation lastRocketAnimation;
    private Item item;
    public override void ApplyEffect(Cell cell, List<Cell> matchedCells)
    {
        item = cell.Item;
        RocketDirection rocketDirection = DetermineRocketDirection();
        FallAndFillManager.Instance.StopFall();//SetIsFallable(false);
        
        PrepareCellForAnimation(cell);
        CreateMergeAnimationForMatchedCells(cell, matchedCells);

        mergeTween.OnComplete(() =>
        {
            PrepareAnimationSprites(rocketDirection);
            ExecuteRocketAnimation(cell);

            RemoveItemFromMatchedCells(matchedCells);

            ExecuteItemsInAffectedCells(cell);
            
            lastRocketAnimation.OnAnimationComplete = () =>
            {
                FallAndFillManager.Instance.StartFall();//SetIsFallable(true);
            };
        });
    }
    public override List<Cell> GetAffectedCells(Cell cell)
    {
        List<Cell> affectedCells = new List<Cell>();
        
        affectedCells.AddRange(cell.GetColumnList());
        affectedCells.AddRange(cell.GetRowList());

        return affectedCells;
    }
    private RocketDirection DetermineRocketDirection()
    {
        return (item.ItemType == ItemType.VerticalRocket) ? RocketDirection.Horizontal : RocketDirection.Vertical;
    }
    private void PrepareAnimationSprites(RocketDirection rocketDirection)
    {
        var imageLibrary = ItemImageLibrary.Instance;

        leftRocketRenderer = item.AddSprite(imageLibrary.RocketLeft);
        rightRocketRenderer = item.AddSprite(imageLibrary.RocketRight);

        upRocketRenderer = item.AddSprite(imageLibrary.RocketUp);
        downRocketRenderer = item.AddSprite(imageLibrary.RocketDown);

        AudioManager.Instance.PlayEffect(SoundID.Rocket);
    }
    private void ExecuteRocketAnimation(Cell cell)
    {
        CreateRocketAnimation<LeftRocketAnimation>(cell, leftRocketRenderer);
        CreateRocketAnimation<RightRocketAnimation>(cell, rightRocketRenderer);
        CreateRocketAnimation<UpRocketAnimation>(cell, upRocketRenderer);
        CreateRocketAnimation<DownRocketAnimation>(cell, downRocketRenderer);
    }


    private void CreateRocketAnimation<T>(Cell cell, SpriteRenderer spriteRenderer) where T : RocketAnimation
    {
        T animation = cell.Item.AddComponent<T>();
        lastRocketAnimation = animation;

        animation.PrepareAnimationSprite(spriteRenderer);
        animation.ExecuteRocketAnimation();
    }
}