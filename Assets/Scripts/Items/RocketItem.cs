using Unity.VisualScripting;
using UnityEngine;

public class RocketItem : Item
{
    private MatchType matchType = MatchType.Special;
    private RocketDirection direction;

    private SpriteRenderer firstRoketPiece; //left or down
    private SpriteRenderer secondRoketPiece; //right or up

    private RocketAnimation firstPieceRocketAnimation;
    private RocketAnimation secondPieceRocketAnimation;
    private bool isExecuting = false;

    public void PrepareRocketItem(ItemBase itemBase, RocketDirection rocketDirection)
    {
        SoundID = SoundID.Rocket;
        var imageLibrary = ItemImageLibrary.Instance;
        direction = rocketDirection;
        Sprite rocketSprite;

        if (direction == RocketDirection.Horizontal)
        {
            firstPieceRocketAnimation = transform.AddComponent<LeftRocketAnimation>();
            secondPieceRocketAnimation = transform.AddComponent<RightRocketAnimation>();
            rocketSprite = imageLibrary.RocketHorizontal;
            firstRoketPiece = AddSprite(imageLibrary.RocketLeft);
            secondRoketPiece = AddSprite(imageLibrary.RocketRight);
        }
        else
        {
            firstPieceRocketAnimation = transform.AddComponent<DownRocketAnimation>();
            secondPieceRocketAnimation = transform.AddComponent<UpRocketAnimation>();
            rocketSprite = imageLibrary.RocketVertical;
            firstRoketPiece = AddSprite(imageLibrary.RocketDown);
            secondRoketPiece = AddSprite(imageLibrary.RocketUp);
        }

        firstRoketPiece.enabled = false;
        secondRoketPiece.enabled = false;

        Prepare(itemBase, rocketSprite);
    }
    public override MatchType GetMatchType()
    {
        return matchType;
    }
    public RocketDirection GetDirection()
    {
        return direction;
    }

    public override void TryExecute()
    {

        if(isExecuting) return;

        AudioManager.Instance.PlayEffect(SoundID);
        SpriteRenderer.enabled = false;
        isExecuting = true;
        FallAndFillManager.Instance.StopFall();

        var cellList = (direction == RocketDirection.Vertical) ? Cell.GetRowList() : Cell.GetColumnList();

        firstPieceRocketAnimation.OnAnimationComplete = () =>
        {
            isExecuting = false;
            FallAndFillManager.Instance.StartFall();
            base.TryExecute();
        };

        firstPieceRocketAnimation.PrepareAnimationSprite(firstRoketPiece);
        secondPieceRocketAnimation.PrepareAnimationSprite(secondRoketPiece);

        firstPieceRocketAnimation.ExecuteRocketAnimation();
        secondPieceRocketAnimation.ExecuteRocketAnimation();

        for(int i = 0; i < cellList.Count; i++)
        {
            var cell = cellList[i];
            if (cell.Item == null || cell.Item == this) continue;

            cell.Item.TryExecute();
        }
    }
}