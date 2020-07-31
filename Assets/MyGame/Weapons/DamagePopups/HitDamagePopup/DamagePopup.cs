using Extensions; 
using TMPro;
using UnityEngine;

namespace MyGame.Weapons.DamagePopups.HitDamagePopup
{
    public class DamagePopup : MonoBehaviour
    {
        private TextMeshPro _textMesh;
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
            _textMesh = GetComponent<TextMeshPro>();
        }

        public void Setup(float damage)
        {
            _textMesh.text = damage.ToString();
        }

        private void LateUpdate()
        {
            Vector3 dirToCamera = transform.DirectionTo(_camera.transform.position);
            transform.forward = -dirToCamera;
        }
    }
}
