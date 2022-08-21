using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public static AudioSource BgmPlayer;
    public static AudioSource EffectPlayer;

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

    private void Start()
    {
        EffectSoundDictionary.Add("Click", Resources.Load<AudioClip>("Audio/CasualGameSounds/DM-CGS-21"));
        EffectSoundDictionary.Add("MoveScene", Resources.Load<AudioClip>("Audio/CasualGameSounds/DM-CGS-26"));
        EffectSoundDictionary.Add("MonsterHit", Resources.Load<AudioClip>("Audio/Shapeforms Audio Free Sound Effects/PUNCH_DESIGNED_HEAVY_23"));
        EffectSoundDictionary.Add("PlayerHit", Resources.Load<AudioClip>("Audio/Shapeforms Audio Free Sound Effects/PUNCH_INTENSE_HEAVY_03"));
        EffectSoundDictionary.Add("GetItem", Resources.Load<AudioClip>("Audio/CasualGameSounds/DM-CGS-45"));
    }

    public void PlayEffect(string name, float volume = 0.3f)
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

    public static void ChangeBgmVolume(float volume)
    {
        BgmPlayer.volume = volume;
    }

    public static void ChangeEffectVolume(float volume)
    {
        EffectPlayer.volume = volume;
    }
    

}