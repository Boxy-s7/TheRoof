using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.UIElements;

public class ZombieScript : MonoBehaviour
{
    public float richtung;
    public float xLeft;
    public float xRight;
    public float speed;
    public float xRightNetFactor;
    private Store store;
    public Register register;
    public ZombieLevelStats levelStats;
    
    // Start is called before the first frame update
    void Start()
    {
        this.store = GameStore.Get();
        this.register = GameRegister.Get();
        this.register.zombie = this;
        UpgradeZombie();
    }

    // Update is called once per frame
    void Update()
    {
        var pos = gameObject.transform.position;
        if (richtung == -1f)
        {
            if (gameObject.transform.position.x <= xLeft)
            {
                richtung = 1f;
                transform.Rotate(new Vector3(0, -180, 0));

            }


        }


        else if (richtung == 1f)
        {
            if (pos.x >= xRight - xRightNetFactor)
            {
                richtung = -1f;
                this.register.ZombieNet.ClearZombieNet();
                transform.Rotate(new Vector3(0, 180, 0));

            }


        }

        var x = Time.deltaTime * speed * richtung * levelStats.speed;
        gameObject.transform.position = new Vector3(pos.x + x, pos.y, pos.z);
        
    }
    public void Destroy()
    {
        
        DestroyImmediate(gameObject);



    }

    public void UpgradeZombie()
    {
        this.levelStats = LevelStats.zombie.GetValueOrDefault(this.store.inventory.zombie.zombieLevel);
        xRightNetFactor = levelStats.zombieNet * 2;
    }
}
