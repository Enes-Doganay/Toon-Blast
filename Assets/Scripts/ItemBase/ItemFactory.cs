using System;
using UnityEngine;

public class ItemFactory : Singleton<ItemFactory>
{
    public ItemBase ItemBasePrefab;
    public Item CreateItem(ItemType itemType, Transform parent)
    {
        if(itemType == ItemType.None) return null;

        var itemBase = Instantiate(ItemBasePrefab, Vector3.zero, Quaternion.identity, parent);

        itemBase.ItemType = itemType;

        Item item = null;

        switch (itemType)
        {
            case ItemType.GreenCube:
                item = CreateCubeItem(itemBase, MatchType.Green);
                break;
            case ItemType.BlueCube:
                item = CreateCubeItem(itemBase, MatchType.Blue);
                break;
            case ItemType.RedCube:
                item = CreateCubeItem(itemBase, MatchType.Red);
                break;
            case ItemType.YellowCube:
                item = CreateCubeItem(itemBase, MatchType.Yellow);
                break;
            case ItemType.CrateLayer1:
            case ItemType.CrateLayer2:
                item = CreateCrateItem(itemBase);
                break;
            case ItemType.Balloon:
                item = CreateBalloonItem(itemBase);
                break;
            case ItemType.RedBalloon:
                item = CreateColorfulBalloonItem(itemBase, MatchType.Red);
                break;
            case ItemType.YellowBalloon:
                item = CreateColorfulBalloonItem(itemBase, MatchType.Yellow);
                break;
            case ItemType.BlueBalloon:
                item = CreateColorfulBalloonItem(itemBase, MatchType.Blue);
                break;
            case ItemType.GreenBalloon:
                item = CreateColorfulBalloonItem(itemBase, MatchType.Green);
                break;
            case ItemType.Bomb:
                item = CreateBombItem(itemBase);
                break;
            case ItemType.VerticalRocket:
            case ItemType.HorizontalRocket:
                item = CreateRocketItem(itemBase, itemType);
                break;
            default:
                Debug.LogWarning("Can not create item: " + itemType);
                break;

        }
        return item;
    }

    private Item CreateCubeItem(ItemBase itemBase, MatchType matchType)
    {
        var cubeItem = itemBase.gameObject.AddComponent<CubeItem>();
        cubeItem.PrepareCubeItem(itemBase, matchType);
        return cubeItem;
    }

    private Item CreateCrateItem(ItemBase itemBase)
    {
        var crateItem = itemBase.gameObject.AddComponent<CrateItem>();
        crateItem.PrepareCrateItem(itemBase);
        return crateItem;
    }
    private Item CreateBalloonItem(ItemBase itemBase)
    {
        var balloonItem = itemBase.gameObject.AddComponent<BalloonItem>();
        balloonItem.PrepareBallonItem(itemBase);
        return balloonItem;
    }
    private Item CreateColorfulBalloonItem(ItemBase itemBase, MatchType matchType)
    {
        var balloonItem = itemBase.gameObject.AddComponent<ColorfulBalloonItem>();
        balloonItem.PrepareColorfulBalloonItem(itemBase, matchType);
        return balloonItem;
    }

    private Item CreateBombItem(ItemBase itemBase)
    {
        var bombItem = itemBase.gameObject.AddComponent<BombItem>();
        bombItem.PrepareBombItem(itemBase);
        return bombItem;
    }

    private Item CreateRocketItem(ItemBase itemBase, ItemType itemType)
    {
        var rocketItem = itemBase.gameObject.AddComponent<RocketItem>();
        var direction = (itemType == ItemType.VerticalRocket) ? RocketDirection.Vertical : RocketDirection.Horizontal;

        rocketItem.PrepareRocketItem(itemBase, direction);
        return rocketItem;
    }
}
