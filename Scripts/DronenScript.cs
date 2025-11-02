using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DronenScript : ItemScript
{
    public String mode = "home";
    public Vector3 target;
    public Vector3 home;
    public Vector3 shop;

    public GameObject huntedStein;
    public float huntingThresholdY = -1;
    public DroneLevelStats levelStats;


    
    public float speedFactor = 1;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        switch (mode)
        {
            case "home":
                UpdateHome();
                break;
            case "scan":
                UpdateScan();
                break;
            case "hunt":
                UpdateHunt();
                break;
            case "full":
                UpdateFull();
                break;


        }
    }

    private void UpdateHome()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            store.inventory.drone.speed * speedFactor * 0.5f * Time.deltaTime
        );
        if (register.drone.dronenBody.steinmenge >= store.inventory.drone.maxSteinmenge)
        {
            Full();

        }
        else if (transform.position.x == home.x && transform.position.y == home.y)
        {
            mode = "scan";
            register.drone.dronenDetector.Scan();
        }

    }
    
    private void UpdateScan() { }
    
    public void Hunt(GameObject stein)
    {
        Vector3 position = stein.transform.position;
        target = new Vector3(position.x, transform.position.y, 0);
        huntedStein = stein;
        mode = "hunt";
    }

    private void UpdateFull()
    {
        transform.position = Vector3.MoveTowards(
        transform.position,
        target,
        store.inventory.drone.speed * speedFactor * 0.5f * Time.deltaTime
        );
        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            register.drone.dronenBody.Sell();
            Home();
        }
    }

    private void UpdateHunt()
    {
        if (huntedStein.transform.position.y > huntingThresholdY)
        {
            if(gameObject.transform.position.x == target.x)
            {
                target.y = huntedStein.transform.position.y + 0.1f;
            }
            transform.position = Vector3.MoveTowards(
                transform.position,
                target,
                store.inventory.drone.speed * speedFactor * Time.deltaTime);
                
        }
        else
        {
            Home();
        }
    }

    public void Full()
    {
        
        mode = "full";
        this.huntedStein = null;
        if (store.inventory.drone.smartnis > 1)
        {
            target = FindFullTarget();
        }
        else
        {
            target = shop;
        }
    }

    public void Home()
    {
        this.huntedStein = null;
        mode = "home";
        target = new Vector3(home.x, home.y, home.z);
    }
    


    public override string ItemType()
    {
        return "drone";
    }

    public override void Register()
    {
        this.register.drone.drone = this;
    }
    private Vector3 FindFullTarget()
    {
        Vector3 target = new Vector3(0, 0, 0);
        var droneX = transform.position.x;
        float smallX = 100;
        store.inventory.eimers.eimers.ForEach(eimer =>
        {
            var diff = Mathf.Abs(eimer.positionX - droneX);
            if (diff < smallX)
            {
                smallX = diff;
                target.x = eimer.positionX;
                target.y = eimer.positionY;

            }
        });
        var diff = Mathf.Abs(shop.x - droneX);
        if (diff < smallX)
        {
            return shop;
        }
        return target;
        
            
        

    }
    
    
}