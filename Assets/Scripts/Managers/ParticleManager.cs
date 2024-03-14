using UnityEngine;

public class ParticleManager : Singleton<ParticleManager>
{
    public ParticleSystem CubeParticleBlue;
    public ParticleSystem CubeParticleRed;
    public ParticleSystem CubeParticleYellow;
    public ParticleSystem CubeParticleGreen;
    public ParticleSystem ComboHintParticle;
    public ParticleSystem RocketTrailEffect;

    public void PlayParticle(Item item)
    {
        ParticleSystem particleSystemReference;
        switch(item.ItemType)
        {
            case ItemType.GreenCube:
                particleSystemReference = CubeParticleGreen;
                break;
            case ItemType.BlueCube:
                particleSystemReference = CubeParticleBlue;
                break;
            case ItemType.RedCube:
                particleSystemReference = CubeParticleRed;
                break;
            case ItemType.YellowCube:
                particleSystemReference = CubeParticleYellow;
                break;
            default:
                return;
        }
        var particle = Instantiate(particleSystemReference, item.transform.position, Quaternion.identity, item.Cell.Board.ParticlesParent);
        particle.Play();
    }

    public ParticleSystem PlayComboParticleOnItem(Item item)
    {
        var particle = Instantiate(ComboHintParticle, item.transform.position, Quaternion.identity,
            item.transform);
        particle.Play(true);
        return particle;
    }
    public void CurrentItemParticleDestroyer(Item item)
    {
        if (item.Particle != null)
        {
            Destroy(item.Particle.gameObject);
        }
    }

}
