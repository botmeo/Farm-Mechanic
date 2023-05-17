using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private ParticleSystem seedParticle;
    [SerializeField] private ParticleSystem waterPartice;

    private void Start()
    {

    }

    private void Update()
    {

    }

    private void PlaySeedParticle()
    {
        seedParticle.Play();
    }

    private void PlayWaterParticle()
    {
        waterPartice.Play();
    }
}
