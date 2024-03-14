using DG.Tweening;
using System;
using UnityEngine;

public abstract class RocketAnimation : MonoBehaviour
{
    public Action OnAnimationComplete;
    protected float animationDuration = 0.5f;
    protected SpriteRenderer animationSprite;
    protected Tween tween;
    public abstract void ExecuteRocketAnimation();
    public virtual void PrepareAnimationSprite(SpriteRenderer spriteRenderer)
    {
        Transform parent = GameObject.Find("Animations").transform;
        animationSprite = spriteRenderer;
        animationSprite.transform.SetParent(parent);
    }
    protected void EnableAnimationSprites()
    {
        animationSprite.enabled = true;
    }

    protected void CreateParticleEffects()
    {
        var particleManager = ParticleManager.Instance;

        var effect = Instantiate(particleManager.RocketTrailEffect, animationSprite.transform);
        effect.transform.localPosition = Vector3.zero;
    }
}
