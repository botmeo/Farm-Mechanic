using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private ParticleSystem seedParticle;

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
}
