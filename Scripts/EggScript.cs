using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EggScript : MonoBehaviour
{
    public Store store;
    public GameObject BrokenEggPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bird")
        {
            return;
        }
        if (collision.gameObject.tag == "Player" && store.net.catchedObject == "Egg")
        {
            return;
        }
        
        if (collision.gameObject.GetComponent<Bounceble>() == null || !collision.gameObject.GetComponent<Bounceble>().brettEggOk)
        {
            
            Instantiate(BrokenEggPrefab, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
            

        }
            
           
        
    }
    void Update()
    {
        
    }
}
