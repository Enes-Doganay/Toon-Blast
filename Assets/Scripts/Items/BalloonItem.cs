public class BalloonItem : Item
{
    public void PrepareBallonItem(ItemBase itemBase)
    {
        SoundID = SoundID.Balloon;
        var ballonSprite = ItemImageLibrary.Instance.BalloonSprite;
        itemBase.Clickable = false;
        itemBase.InterectWithExplode = true;
        Prepare(itemBase, ballonSprite);
    }
    public override void TryExecute()
    {
        AudioManager.Instance.PlayEffect(SoundID);
        base.TryExecute();
    }
}