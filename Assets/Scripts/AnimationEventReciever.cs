using UnityEngine;

public class AnimationEventReciever : MonoBehaviour
{
    [SerializeField] private Weapon weapon;

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
}