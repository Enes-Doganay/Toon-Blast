using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    public ItemType ItemType;
    public bool Clickable = true;
    public bool IsFallable = true;
    public bool InterectWithExplode = false;
    public int Health = 1;
    public FallAnimation FallAnimation;
}
