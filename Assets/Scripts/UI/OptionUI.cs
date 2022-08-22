using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : BaseUI, ISingleOpenUI
{
    private Slider BGMVolume;
    private Slider effectVolume;

    void Start()
    {
        BGMVolume = gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Slider>();
        effectVolume = gameObject.transform.GetChild(0).transform.GetChild(1).GetComponent<Slider>();
        BGMVolume.value = SoundManager.Instance.BGMVolume;
        effectVolume.value = SoundManager.Instance.EffectVolume;
    }

    void Update()
    {
        UpdateBGMVolume();
        UpdateEffectVolume();
    }

    public void UpdateBGMVolume()
    {
        SoundManager.Instance.BGMVolume = BGMVolume.value;
        SoundManager.Instance.BgmPlayer.volume = BGMVolume.value;
    }

    public void UpdateEffectVolume()
    {
        SoundManager.Instance.EffectVolume = effectVolume.value;
        SoundManager.Instance.EffectPlayer.volume = effectVolume.value;
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
