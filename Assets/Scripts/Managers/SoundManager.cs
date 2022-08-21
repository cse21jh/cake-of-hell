using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : Singleton<SoundManager>
{

    public AudioSource BgmPlayer;
    public AudioSource EffectPlayer;

    public float BGMVolume { get; set; } = 1f;
    public float EffectVolume { get; set; } = 0.3f;
    

    [SerializeField] private AudioClip[] EffectAudioClips;

    private Dictionary<string, AudioClip> EffectSoundDictionary = new Dictionary<string, AudioClip>();

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        GameObject EffectTempObject = new GameObject("Effect");
        EffectTempObject.transform.SetParent(gameObject.transform);
        EffectPlayer = EffectTempObject.AddComponent<AudioSource>();

        GameObject BgmTempObject = new GameObject("Bgm");
        BgmTempObject.transform.SetParent(gameObject.transform);
        BgmPlayer = BgmTempObject.AddComponent<AudioSource>();

        foreach (AudioClip audioclip in EffectAudioClips)
        {
            EffectSoundDictionary.Add(audioclip.name, audioclip);
        }
    }

    void Start()
    {
        EffectSoundDictionary.Add("Click", Resources.Load<AudioClip>("Audio/CasualGameSounds/DM-CGS-21"));
        EffectSoundDictionary.Add("MoveScene", Resources.Load<AudioClip>("Audio/CasualGameSounds/DM-CGS-26"));
        EffectSoundDictionary.Add("MonsterHit", Resources.Load<AudioClip>("Audio/Shapeforms Audio Free Sound Effects/PUNCH_DESIGNED_HEAVY_23"));
        EffectSoundDictionary.Add("PlayerHit", Resources.Load<AudioClip>("Audio/Shapeforms Audio Free Sound Effects/PUNCH_INTENSE_HEAVY_03"));
        EffectSoundDictionary.Add("GetItem", Resources.Load<AudioClip>("Audio/CasualGameSounds/DM-CGS-45"));
    }

    public void PlayEffect(string name)
    {
        EffectPlayer.PlayOneShot(EffectSoundDictionary[name], EffectVolume);
    }

    public void PlayBgm(AudioClip clip)
    {
        BgmPlayer.loop = true;
        BgmPlayer.volume = BGMVolume;

        BgmPlayer.clip = clip;
        BgmPlayer.Play();
    }

    public void StopBgm()
    {
        BgmPlayer.clip = null;
        BgmPlayer.Stop();
    }

}