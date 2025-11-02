using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;



public class CameraScript : MonoBehaviour
{
    public GameObject player;
   
    public GameObject camara1;
    public GameObject camara2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = player.transform.position;

        if (playerPosition.x < -10)
        {
            this.camara2.SetActive(true);
            this.camara1.SetActive(false);
            camara2.tag = "MainCamera";
            camara1.tag = "Untagged";

        }
        else
        {
            this.camara2.SetActive(false);
            this.camara1.SetActive(true);
            camara2.tag = "Untagged";
            camara1.tag = "MainCamera";
        }
    }
}
