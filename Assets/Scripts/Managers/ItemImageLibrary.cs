using UnityEngine;

public class ItemImageLibrary : Singleton<ItemImageLibrary>
{
    [Header("Cubes")]
    public Sprite GreenCubeSprite;
    public Sprite GreenCubeRocketHintSprite;
    public Sprite GreenCubeBombHintSprite;

    public Sprite YellowCubeSprite;
    public Sprite YellowCubeRocketHintSprite;
    public Sprite YellowCubeBombHintSprite;

    public Sprite BlueCubeSprite;
    public Sprite BlueCubeRocketHintSprite;
    public Sprite BlueCubeBombHintSprite;

    public Sprite RedCubeSprite;
    public Sprite RedCubeRocketHintSprite;
    public Sprite RedCubeBombHintSprite;

    [Header("Balloon")]
    public Sprite BalloonSprite;

    public Sprite GreenBalloonSprite;
    public Sprite YellowBalloonSprite;
    public Sprite BlueBalloonSprite;
    public Sprite RedBalloonSprite;

    [Header("Crate")]
    public Sprite CrateLayer1Sprite;
    public Sprite CrateLayer2Sprite;

    [Header("Rocket")]
    public Sprite RocketVertical;
    public Sprite RocketHorizontal;
    public Sprite RocketUp;
    public Sprite RocketRight;
    public Sprite RocketDown;
    public Sprite RocketLeft;

    [Header("Bomb")]
    public Sprite BombSprite;

    public Sprite GetSpriteForItemType(ItemType itemType)
    {
        switch(itemType)
        {
            case ItemType.GreenCube: return GreenCubeSprite;
            case ItemType.YellowCube: return YellowCubeSprite;
            case ItemType.BlueCube: return BlueCubeSprite;
            case ItemType.RedCube: return RedCubeSprite;
            case ItemType.CrateLayer1: return CrateLayer1Sprite;
            case ItemType.Balloon: return BalloonSprite;
            case ItemType.GreenBalloon: return GreenBalloonSprite;
            case ItemType.YellowBalloon: return YellowBalloonSprite;
            case ItemType.RedBalloon: return RedBalloonSprite;
            case ItemType.BlueBalloon: return BlueBalloonSprite;
            case ItemType.Bomb: return BombSprite;
            case ItemType.VerticalRocket: return RocketVertical;
            case ItemType.HorizontalRocket: return RocketHorizontal;
            case ItemType.CrateLayer2: return CrateLayer2Sprite;
            default: return null;
        }
    }
}
