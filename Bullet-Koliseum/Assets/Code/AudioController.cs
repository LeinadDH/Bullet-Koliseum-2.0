using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class AudioController : MonoBehaviour
{
    const string musicVolume = "Music Volume";
    public AudioMixer audioMixer;
    [Space]
    public Slider musicSilder;
    public TextMeshProUGUI musicTXT;
    private void Start()
    {
        musicSilder.minValue = 0.001f;
        musicSilder.maxValue = 1;
        musicSilder.onValueChanged.AddListener(SetMusicVolume);

        musicSilder.value = PlayerPrefs.GetFloat(musicVolume, 1);
    }
    public void SetMusicVolume(float v)
    {
        audioMixer.SetFloat(musicVolume, Mathf.Log10(v) * 20);
        musicTXT?.SetText(Mathf.RoundToInt(v * 100).ToString() + " %");
        PlayerPrefs.SetFloat(musicVolume, v);
        PlayerPrefs.Save();
    }
}
