using System.Collections;
using Extensions;
using MyGame.Entities.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace MyGame.UI.HealthBar
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private bool rotateToCameraView = true;
        [SerializeField] private bool hideOnStart = true;

        [SerializeField] private Image healthImage = null;
        [SerializeField] private float updateInSeconds = 0;
        [SerializeField] private Health health = null;
        [SerializeField] private GameObject background = null;

        [SerializeField] private new Camera camera = null;

        private void Awake()
        {
            camera = camera != null ? camera : Camera.main;
        }

        private void OnEnable()
        {
            health.OnHealthPercentChanged += HandleHealthChanged;
            
            if (hideOnStart) background.SetActive(false);
        }

        private void OnDisable()
        {
            health.OnHealthPercentChanged -= HandleHealthChanged;
        }

        private void HandleHealthChanged(float percent)
        {
            if (!background.activeInHierarchy) background.SetActive(true);
            
            StartCoroutine(ChangeToPercent(percent));
        }

        private IEnumerator ChangeToPercent(float percent)
        {
            float preChangePercent = healthImage.fillAmount;
            float elapsed = 0f;

            while (elapsed < updateInSeconds)
            {
                elapsed += Time.deltaTime;
                healthImage.fillAmount = Mathf.Lerp(preChangePercent, percent, elapsed / updateInSeconds);
                yield return null;
            }

            healthImage.fillAmount = percent;
        }

        private void LateUpdate()
        {
            if (!rotateToCameraView) return;

            Vector3 dirToCamera = transform.DirectionTo(camera.transform.position);
            transform.forward = dirToCamera;
        }
    }
}