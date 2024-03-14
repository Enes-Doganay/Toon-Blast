using DG.Tweening;

public class UpRocketAnimation : RocketAnimation
{
    private float yPosition = 10f;
    public override void ExecuteRocketAnimation()
    {
        CreateParticleEffects();
        EnableAnimationSprites();

        tween = animationSprite.transform.DOMoveY(yPosition, animationDuration).SetEase(Ease.Linear);
        tween.OnComplete(() =>
        {
            Destroy(animationSprite.gameObject);
            OnAnimationComplete?.Invoke();
        });
    }
}