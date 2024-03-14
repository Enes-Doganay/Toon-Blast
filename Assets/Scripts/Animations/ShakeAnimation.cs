using UnityEngine;
using DG.Tweening;
public static class ShakeAnimation
{
    public static Tween ApplyShakeAnimation(Transform transform, float duration, float strength)
    {
        return transform.DOShakePosition(duration, strength);
    }
}