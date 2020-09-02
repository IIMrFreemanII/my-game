using System.Collections;
using UnityEngine;

namespace MyGame.Weapons.Projectiles.Scripts
{
    public class ParticleHandler : MonoBehaviour
    {
        [SerializeField] private ParticleSystem particle;
        [SerializeField] private float timeToDestroy = 5f;
    
        private ParticleSystem.MainModule _mainModule;
        private Coroutine _coroutine;

        private void Awake()
        {
            particle = GetComponent<ParticleSystem>();
            _mainModule = particle.main;
            _mainModule.playOnAwake = false;
        
            particle.Stop(true);
        }

        public void Play()
        {
            particle.Play(true);
        }

        private void OnEnable()
        {
            if (_coroutine == null)
            {
                _coroutine = StartCoroutine(SelfDestroyWithDelay());
            }
        }

        private IEnumerator SelfDestroyWithDelay()
        {
            yield return new WaitForSeconds(timeToDestroy);
            SelfDestroy();
        }

        private void SelfDestroy()
        {
            Destroy(gameObject);
        }
    }
}
