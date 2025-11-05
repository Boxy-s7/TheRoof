using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    
    public Register register;
    public AudioSource radio;
    // Start is called before the first frame update
    void Start()
    {

        register = GameRegister.Get();
        register.radio = this;
        radio.Play();

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
