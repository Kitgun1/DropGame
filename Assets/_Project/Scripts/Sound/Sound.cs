using UnityEngine;

namespace _Project.Sound
{
    public class Sound : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _audio;

        public void Click() // Test
        {
            _audioSource.clip = _audio;
            _audioSource.Play();
        }
    }
}