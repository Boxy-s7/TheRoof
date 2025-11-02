using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DronenDetectorScript : ItemScript
{
    public float droneFactor;

    public Boolean ready;
    
    
    // Start is called before the first frame update
    
    // Update is called once per frame
    void Update()
    {

    }
    public void OnTriggerStay2D(UnityEngine.Collider2D collision)
    {
        
        if (ready == true && collision.gameObject.CompareTag("Stone") && IsReachable(collision.gameObject.transform.position))
        {
            collision.gameObject.GetComponent<SteinScript>().Warning();
            register.drone.drone.Hunt(collision.gameObject);


            ready = false;
        }
    }

    private bool IsReachable(Vector3 positionStein)
    {
        Vector3 positionDrone = transform.position;
        float x = Mathf.Abs(positionDrone.x - positionStein.x);;
        float y = positionStein.y - positionDrone.y;
        if (y < 0)
        {
            return false;
        }

        bool isReachable = (x * droneFactor / LevelStats.drone[this.store.inventory.drone.dronenLevel].speed <= y);
        if (isReachable)
        {
            Debug.Log("IsReachable");

        }
        else
        {
            Debug.Log("is not reacheble " + x + " " + y + " " + droneFactor);

        }
        return isReachable;
        }

    public void Next()
    {
        ready = true;
    }

    public override string ItemType()
    {
        return "DronenDetector";
    }

    public override void Register()
    {
        register.drone.dronenDetector = this;
    }
    public void Scan()
    {
        ready = true;
        Debug.Log("calling Scan");

    }
}
