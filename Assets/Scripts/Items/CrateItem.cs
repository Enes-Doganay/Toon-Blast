public class CrateItem : Item
{
    public void PrepareCrateItem(ItemBase itemBase)
    {
        SoundID = SoundID.Crate;
        var crateLayer2Sprite = ItemImageLibrary.Instance.GetSpriteForItemType(itemBase.ItemType);
        itemBase.IsFallable = false;
        itemBase.Health = GetCrateHealth(itemBase.ItemType);
        itemBase.InterectWithExplode = true;
        itemBase.Clickable = false;
        Prepare(itemBase, crateLayer2Sprite);
    }

    private int GetCrateHealth(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.CrateLayer1:
                return 1;
            case ItemType.CrateLayer2:
                return 2;
            default: 
                return 0;
        }
    }

    public override void TryExecute()
    {
        AudioManager.Instance.PlayEffect(SoundID);
        Health--;
        if(Health < 1)
        {
            base.TryExecute();
        }
        else
        {
            var crateLayer1Sprite = ItemImageLibrary.Instance.CrateLayer1Sprite;
            UpdateSprite(crateLayer1Sprite);
        }
    }
}
