using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private new Camera camera = null;
    [SerializeField] private GameObject currentWeapon = null;

    private void Awake()
    {
        camera = camera != null ? camera : Camera.main;
    }
    
    private void OnDisable()
    {
        // ResetWeaponParent();
        // ResetCameraParent();
    }

    private void ResetCameraParent()
    {
        camera.transform.SetParent(null);
    }

    private void ResetWeaponParent()
    {
        currentWeapon.transform.SetParent(transform);
    }
}
