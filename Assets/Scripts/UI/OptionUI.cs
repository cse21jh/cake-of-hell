using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : BaseUI
{
    public static GameObject OptionUIs;

    private Slider BGMVolume;
    private Slider effectVolume;

    // Start is called before the first frame update
    void Start()
    {
        BGMVolume = gameObject.GetComponent<Slider>();
        effectVolume = gameObject.GetComponent<Slider>();
        BGMVolume.value = SoundManager.BgmPlayer.volume;
        effectVolume.value = SoundManager.EffectPlayer.volume;
    }

    // Update is called once per frame
    void Update()
    {
        SoundManager.ChangeBgmVolume(BGMVolume.value);
        SoundManager.ChangeEffectVolume(effectVolume.value);
    }

    public override void Open()
    {
        gameObject.SetActive(true);
        Debug.Log("Option UI Opened!");
    }

    public override void Close()
    {
        gameObject.SetActive(false);
        Debug.Log("Option UI Closed!");
    }
}
