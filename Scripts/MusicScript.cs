using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    public Store store;
    public Register register;
    public AudioSource radio;
    // Start is called before the first frame update
    void Start()
    {
        store = GameStore.Get();
        register = GameRegister.Get();
        register.radio = this;
        radio.Play();
        radio.volume = this.store.storeSettings.musicVolume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void VolumenChanged()
    {
        radio.volume = register.settings.musicSliderVar / 100;
    }
}
