using System;
using UnityEngine;

namespace Sfx
{
    public enum SoundType
    {
        AmethystPopUp,
        Sparkles,
        Menu,
        Walk,
        Talk
    }

    public class soundManager : MonoBehaviour
    {
        public static soundManager Instance { get; private set; }

        [SerializeField] private SoundList[] soundList;

        public AudioSource footstepSource;
    
        public AudioSource sfxSource;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

         
            //sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.playOnAwake = false;
            sfxSource.spatialBlend = 0;

           // footstepSource = gameObject.AddComponent<AudioSource>();
            footstepSource.loop = true;
            footstepSource.playOnAwake = false;
            footstepSource.spatialBlend = 0;
        }

        public static void PlaySound(SoundType type, float volume = 1f)
        {
            var list = Instance.soundList[(int)type].sounds;
            if (list == null || list.Length == 0) return;

            var clip = list[UnityEngine.Random.Range(0, list.Length)];
            Instance.sfxSource.PlayOneShot(clip, volume);
        }


        public static void PlayFootstep(SoundType type, float volume = 1f)
        {
            var list = Instance.soundList[(int)type].sounds;
            if (list == null || list.Length == 0) return;

            AudioClip clip = list[UnityEngine.Random.Range(0, list.Length)];

            Instance.footstepSource.pitch = UnityEngine.Random.Range(0.9f, 1.05f);
            Instance.footstepSource.clip = clip;
            Instance.footstepSource.volume = volume;
            Instance.footstepSource.Play();
        }

        public static void StopFootstep()
        {
            if (Instance.footstepSource.isPlaying)
                Instance.footstepSource.Stop();
        }

    }

    [Serializable]
    public struct SoundList
    {
        public string name;
        public AudioClip[] sounds;
    }
}