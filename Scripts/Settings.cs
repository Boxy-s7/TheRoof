using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using System;
using UnityEngine.UI;
using JetBrains.Annotations;
public class Settings : MonoBehaviour
{
    public Store store;
    public Register register;
    public GameObject effectSliderSettings;
    public GameObject musicSliderSettings;
    public Slider effectSlider;
    public Slider musicSlider;
    public GameObject settingsPanel;

    public GameObject pauseMenu;
    public float musicSliderVar;
    public float effectSliderVar;





    void Start()
    {
        
        this.store = GameStore.Get();
        register = GameRegister.Get();
        register.settings = this;
        musicSlider = musicSliderSettings.GetComponent<Slider>();
        effectSlider = effectSliderSettings.GetComponent<Slider>();
        

    }


    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        pauseMenu.SetActive(true);
    }
    public void OpenSettings()
    {
        musicSlider.value = this.store.storeSettings.musicVolume;
        effectSlider.value = this.store.storeSettings.effectVolume;
        settingsPanel.SetActive(true);
        pauseMenu.SetActive(false);
    }
    public void MusicVolumeChange(float changedValue)
    {
        musicSliderVar = musicSlider.value;
        register.radio.VolumenChanged();
        this.store.storeSettings.musicVolume = musicSliderVar;
    }
    public void EffectVolumeChange(float changedValue)
    {
        effectSliderVar = effectSlider.value;
        this.store.storeSettings.effectVolume = effectSliderVar;
    }
    
}


