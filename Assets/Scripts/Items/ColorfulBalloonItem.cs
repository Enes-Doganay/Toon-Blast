using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorfulBalloonItem : Item
{
    public MatchType matchType;

    public void PrepareColorfulBalloonItem(ItemBase itemBase, MatchType matchType)
    {
        SoundID = SoundID.Balloon;
        this.matchType = matchType;
        itemBase.Clickable = false;
        itemBase.InterectWithExplode = false;
        Prepare(itemBase, GetSpritesForMatchType());
    }

    public Sprite GetSpritesForMatchType()
    {
        var imageLibrary = ItemImageLibrary.Instance;

        switch (matchType)
        {
            case MatchType.Green:
                return imageLibrary.GreenBalloonSprite;
            case MatchType.Red:
                return imageLibrary.RedBalloonSprite;
            case MatchType.Blue:
                return imageLibrary.BlueBalloonSprite;
            case MatchType.Yellow:
                return imageLibrary.YellowBalloonSprite;
            default:
                return null;
        }
    }

    public override MatchType GetMatchType()
    {
        return matchType;
    }
    public override void TryExecute()
    {
        AudioManager.Instance.PlayEffect(SoundID);
        base.TryExecute();
    }
}
