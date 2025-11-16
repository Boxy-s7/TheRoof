using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EggScript : MonoBehaviour
{
    public GameObject BrokenEggPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            return;
        }
        if (collision.gameObject.GetComponent<Bounceble>() == null || !collision.gameObject.GetComponent<Bounceble>().brettEggOk)
        {
            Debug.Log(collision.gameObject.tag);
            Instantiate(BrokenEggPrefab, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
            

        }
            
           
        
    }
    void Update()
    {
        
    }
}
