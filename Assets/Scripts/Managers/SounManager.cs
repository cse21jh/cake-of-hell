using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SoundManager>();
            }

            return instance;
        }
    }

    private AudioSource BgmPlayer;
    private AudioSource EffectPlayer;

    [SerializeField] private AudioClip[] EffectAudioClips;

    Dictionary<string, AudioClip> EffectSoundDictionary = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        GameObject EffectTempObject = new GameObject("Effect");
        EffectTempObject.transform.SetParent(transform);
        EffectPlayer = EffectTempObject.AddComponent<AudioSource>();

        GameObject BgmTempObject = new GameObject("Bgm");
        BgmTempObject.transform.SetParent(transform);
        BgmPlayer = BgmTempObject.AddComponent<AudioSource>();

        foreach (AudioClip audioclip in EffectAudioClips)
        {
            EffectSoundDictionary.Add(audioclip.name, audioclip);
        }
    }

    public void PlayEffect(string name, float volume = 1f)
    {
        EffectPlayer.PlayOneShot(EffectSoundDictionary[name], volume);
    }

    public void PlayBgm(AudioClip clip, float volume = 1f)
    {
        BgmPlayer.loop = true;
        BgmPlayer.volume = volume;

        BgmPlayer.clip = clip;
        BgmPlayer.Play();
    }

    public void StopBgm()
    {
        BgmPlayer.clip = null;
        BgmPlayer.Stop();
    }

    public void ChangeBgmVolume(float volume)
    {
        BgmPlayer.volume = volume;
    }
    

}