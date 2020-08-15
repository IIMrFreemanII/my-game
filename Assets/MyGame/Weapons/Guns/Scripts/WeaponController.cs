using Extensions;
using MyGame.Weapons.Scripts;
using UnityEngine;

namespace MyGame.Weapons.Guns.Scripts
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private Transform weaponPosition = null;
        
        [SerializeField] private Weapon[] weaponSlots = new Weapon[3];
        [SerializeField] private Weapon currentWeapon = null;
        [SerializeField] private int _activeSlotIndex;
        
        [SerializeField] private LayerMask weaponLayer = default;
        [SerializeField] private float maxDistToGrabWeapon = 2f;

        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SwitchToSlot(0);
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SwitchToSlot(1);
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SwitchToSlot(2);
            }

            HandleFire();
            HandleGrabWeapon();
        }

        private void HandleFire()
        {
            if (currentWeapon)
            {
                currentWeapon.HandleFire();
            }
        }

        private void SwitchToSlot(int slotIndex)
        {
            Weapon tempWeapon = weaponSlots[slotIndex];
            _activeSlotIndex = slotIndex;
            
            if (!tempWeapon)
            {
                Debug.LogWarning("Empty slot");
                if (currentWeapon)
                {
                    currentWeapon.gameObject.SetActive(false);
                    currentWeapon = null;
                }
                
                return;
            }
            
            if (currentWeapon) currentWeapon.gameObject.SetActive(false);
            currentWeapon = tempWeapon;
            currentWeapon.gameObject.SetActive(true);
        }

        private void HandleGrabWeapon()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Vector3 origin = _camera.transform.position;
                Vector3 direction = _camera.transform.forward;

                if (Physics.Raycast(origin, direction, out RaycastHit hit, maxDistToGrabWeapon, weaponLayer))
                {
                    hit.transform.HandleComponent<Weapon>(weapon =>
                    {
                        int freeSlot = GetFreeSlotIndex();
                        
                        // swap weapon new weapon with current active when all slots are full
                        if (freeSlot == -1)
                        {
                            DropCurrentWeapon();
                            AddWeaponToSlot(weapon, _activeSlotIndex);
                            SwitchToSlot(_activeSlotIndex);
                            return;
                        }
                        
                        AddWeaponToSlot(weapon, freeSlot);

                        if (currentWeapon == null && freeSlot == _activeSlotIndex)
                        {
                            SwitchToSlot(_activeSlotIndex);
                        }
                    });
                } 
            }
        }

        private int GetFreeSlotIndex()
        {
            for (int i = 0; i < weaponSlots.Length; i++)
            {
                if (!weaponSlots[i])
                {
                    return i;
                }
            }

            return -1;
        }

        private void DropWeapon(int slotIndex)
        {
            Weapon weaponToDrop = weaponSlots[slotIndex];
            weaponToDrop.owner = null;
            
            weaponToDrop.transform.SetParent(null);
            ActivateWeaponPhysics(weaponToDrop);
            
            weaponSlots[slotIndex] = null;
        }

        private void DropCurrentWeapon()
        {
            DropWeapon(_activeSlotIndex);
            currentWeapon = null;
        }

        private void AddWeaponToSlot(Weapon weapon, int slotIndex)
        {
            Weapon weaponInSlot = weaponSlots[slotIndex];

            if (!weaponInSlot)
            {
                weapon.owner = gameObject;
                
                weapon.transform.SetParent(weaponPosition);
                weapon.gameObject.SetActive(false);
                weapon.transform.localPosition = Vector3.zero;
                weapon.transform.localRotation = Quaternion.identity;
                
                DeactivateWeaponPhysics(weapon);
                
                weaponSlots[slotIndex] = weapon;
            }
            else
            {
                Debug.LogWarning("Slot should be empty to add new weapon!");
            }
        }
        
        private void ActivateWeaponPhysics(Weapon weapon)
        {
            weapon.collider.enabled = true;
            weapon.rb.isKinematic = false;
        }
        
        private void DeactivateWeaponPhysics(Weapon weapon)
        {
            weapon.collider.enabled = false;
            weapon.rb.isKinematic = true;
        }
    }
}
