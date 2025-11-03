using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    public AudioSource radio;
    public string mode = "home";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (mode)
        {
            case "home":
                radio.Play();
                break;
            


        }
    }
    
}
