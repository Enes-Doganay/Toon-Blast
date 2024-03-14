using DG.Tweening;

public class LeftRocketAnimation : RocketAnimation
{
    private float xPosition = -6f;
    public override void ExecuteRocketAnimation()
    {
        CreateParticleEffects();
        EnableAnimationSprites();

        tween = animationSprite.transform.DOMoveX(xPosition, animationDuration).SetEase(Ease.Linear);
        tween.OnComplete(() =>
        {
            Destroy(animationSprite.gameObject);
            OnAnimationComplete?.Invoke();
        });
    }
}