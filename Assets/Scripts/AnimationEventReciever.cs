using UnityEngine;

public class AnimationEventReciever : MonoBehaviour
{
    [SerializeField] private Weapon weapon;
    [SerializeField] private PlayerMovement playerMovement;

    public void FinishReload()
    {
        weapon.FinishReload();
    }

    public void EndReload()
    {
        weapon.EndReload();
    }

    public void DisableUpperBodyLayer()
    {
        weapon.DisableUpperBodyLayer();
    }

    public void PlayFootstep()
    {
        playerMovement.PlayFootstep();
    }

    public void PlayReloadSound()
    {
        weapon.PlayReloadSound();
    }
}