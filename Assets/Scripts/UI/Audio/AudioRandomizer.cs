using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI.Audio
{
    public class AudioRandomizer : MonoBehaviour
    {
        [SerializeField]
        private AudioClip[] audioClips = default;

        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = audioClips[0];
        }

        public void PlayRandomClip()
        {
            if (audioClips.Length > 1)
            {
                AudioClip prevClip = audioSource.clip;

                do
                {
                    audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
                } while (audioSource.clip == prevClip);
            }

            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.Play();
        }
    }
}