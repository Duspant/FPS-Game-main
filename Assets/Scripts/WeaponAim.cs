using UnityEngine;
using UnityEngine.UI;

public class WeaponAim : MonoBehaviour
{
    [Header("Camera + Crosshair")]
    public Camera fpsCam;
    public Image crosshair;

    [Header("Crosshair Settings")]
    public Color defaultColor = Color.white;
    public Color enemyColor = Color.red;
    public float detectionRange = 100f;

    void Update()
    {
        CrosshairDetection(); // Optional: for changing crosshair color
    }

    void CrosshairDetection()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, detectionRange))
        {
            if (hit.transform.CompareTag("Enemy"))
                crosshair.color = enemyColor;
            else
                crosshair.color = defaultColor;
        }
        else
        {
            crosshair.color = defaultColor;
        }
    }
}
