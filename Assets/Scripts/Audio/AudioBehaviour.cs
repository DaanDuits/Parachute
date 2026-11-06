using UnityEngine;

namespace FoGD
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioBehaviour : MonoBehaviour
    {
        private AudioSource _source;

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            if (_source.isPlaying)
                return;

            Destroy(gameObject);
        }
    }
}
