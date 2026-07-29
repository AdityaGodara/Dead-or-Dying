using TMPro;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    [SerializeField] private TMP_Text ammoText;

    public void UpdateAmmo(int currentAmmo, int magazineSize)
    {
        ammoText.text = $"{currentAmmo} / {magazineSize}";
    }
}