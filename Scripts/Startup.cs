using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;


public class Startup : MonoBehaviour
{
    public Store store;
    public Register register;

    public GameObject BrettPrefab;
    public GameObject EimerPrefab;
    public GameObject ZombiePrefab;
    public GameObject DronenPrefab;
    public GameObject shop;




    // Start is called before the first frame update
    public GameObject movedItem;
    void Start()
    {
        this.register = GameRegister.Get();
        
        if (!this.register.gameStoreManager.storeLoaded)
        {
            this.register.gameStoreManager.Reset();
        }
        this.store = GameStore.Get();
        Debug.Log("startup start " + this.store.inventory.bretters.Count);
        this.store.inventory.bretters.ForEach((Brett brett) =>
        {
            
            GameObject brettObj = Instantiate(BrettPrefab, new Vector3(brett.positionX, brett.positionY, 0), Quaternion.Euler(0, 0, brett.rotationZ));


        });
        this.store.inventory.eimers.eimers.ForEach((Eimer eimer) =>
        {
            Debug.Log("eimer create");
            GameObject eimerObj = Instantiate(EimerPrefab, new Vector3(eimer.positionX, eimer.positionY, 0), Quaternion.identity);
            

        });
        if (this.store.inventory.zombie.zombieLevel >= 1)
        {
            Instantiate(ZombiePrefab, new Vector3(9, -3.5f, 0), Quaternion.identity);


        }
        if (this.store.inventory.drone.dronenLevel >= 1)
        {
            Instantiate(DronenPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            this.store.inventory.drone.speed = LevelStats.drone[this.store.inventory.drone.dronenLevel].speed;
            this.store.inventory.drone.maxSteinmenge = LevelStats.drone[this.store.inventory.drone.dronenLevel].dronenNet;

        }


    }
}