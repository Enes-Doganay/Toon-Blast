using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private const int BaseSortingOrder = 10;
    private int childSpriteOrder;
    public SpriteRenderer SpriteRenderer;

    public ItemType ItemType;
    public bool Clickable;
    public bool InterectWithExplode;
    public bool IsFallable;
    public int Health;

    public FallAnimation FallAnimation;
    public ParticleSystem Particle;
    private Cell cell;
    public SoundID SoundID = SoundID.None;
    public Cell Cell
    {
        get { return cell; }
        set
        {
            if(cell == value) return;

            var oldCell = cell;
            cell = value;

            if (oldCell != null && oldCell.Item == this)
            {
                oldCell.Item = null;
            }
            if(value != null)
            {
                value.Item = this;
                gameObject.name = cell.gameObject.name + " " + GetType().Name;
            }
        }
    }


    public void Prepare(ItemBase itemBase, Sprite sprite)
    {
        SpriteRenderer = AddSprite(sprite);

        ItemType = itemBase.ItemType;
        Clickable = itemBase.Clickable;
        InterectWithExplode = itemBase.InterectWithExplode;
        IsFallable = itemBase.IsFallable;
        FallAnimation = itemBase.FallAnimation;
        Health = itemBase.Health;
        FallAnimation.Item = this;
    }

    public SpriteRenderer AddSprite(Sprite sprite)
    {
        var spriteRenderer = new GameObject("Sprite_" + childSpriteOrder).AddComponent<SpriteRenderer>();
        spriteRenderer.transform.SetParent(transform);
        spriteRenderer.transform.localPosition = Vector3.zero;
        spriteRenderer.sprite = sprite;
        spriteRenderer.sortingLayerID = SortingLayer.NameToID("Item");
        //spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        spriteRenderer.sortingOrder = BaseSortingOrder + childSpriteOrder++;

        return spriteRenderer;
    }

    public virtual MatchType GetMatchType() { return MatchType.None; }

    public virtual void TryExecute()
    {
        GoalManager.Instance.UpdateLevelGoal(ItemType);
        RemoveItem();
    }
    public void RemoveItem()
    {
        Cell.Item = null;
        Cell = null;
        Destroy(gameObject);
    }

    public void UpdateSprite(Sprite sprite)
    {
        var spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = sprite; 
    }

    public virtual void HintUpdateToSprite(ItemType itemType)
    {
        return;
    }
    public void Fall()
    {
        if (!this.IsFallable) return;

        FallAnimation.FallTo(cell.GetFallTarget());
    }
}
