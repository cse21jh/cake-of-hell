using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : BaseUI, ISingleOpenUI
{
    public GameObject OptionUIs;

    public Slider BGMVolume;
    public Slider effectVolume;

    private float BGMVolumeValue;
    private float effectVolumeValue;

    // Start is called before the first frame update
    void Start()
    {
        BGMVolume = GameObject.Find("MusicSlider").GetComponent<Slider>();
        effectVolume = GameObject.Find("SoundSlider").GetComponent<Slider>();
        BGMVolume.value = SoundManager.Instance.BGMVolume;
        BGMVolumeValue = SoundManager.Instance.BGMVolume;
        effectVolume.value = SoundManager.Instance.EffectVolume;
        effectVolumeValue = SoundManager.Instance.EffectVolume;
    }

    // Update is called once per frame
    void Update()
    {
        BGMVolume.value = SoundManager.Instance.BGMVolume;
        effectVolume.value = SoundManager.Instance.EffectVolume;
        UpdateBGMVolume();
        UpdateEffectVolume();
    }

    public void UpdateBGMVolume()
    {
        SoundManager.Instance.BGMVolume = BGMVolume.value;
    }

    public void UpdateEffectVolume()
    {
        SoundManager.Instance.EffectVolume = effectVolume.value;
    }

    public override void Open()
    {
        OptionUIs.SetActive(true);
        Debug.Log("Option UI Opened!");
    }

    public override void Close()
    {
        OptionUIs.SetActive(false);
        Debug.Log("Option UI Closed!");
    }
}
