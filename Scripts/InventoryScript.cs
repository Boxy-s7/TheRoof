using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    public Store store;
    public Register register;
    public GameObject BrettPrefab;
    public GameObject EimerPrefab;
    public GameObject ZombiePrefab;
    public GameObject DronenPrefab;
    public GameObject NestPrefab;
    public GameObject PfanenPrefab;

    public float floorY;
    public int clicked;
    public TMP_InputField textMeshInventoryStats1;
    public TMP_InputField textMeshInventoryStats2;
    public TMP_InputField textMeshInventoryStats3;
    public GameObject statsPanelInventory;
    public ZombieLevelStats zombieLevelStats;
    public DroneLevelStats droneLevelStats;
    public NestLevelStats nestLevelStats;
    // Start is called before the first frame update
    public GameObject movedItem;
    
    
    void Start()
    {
        this.store = GameStore.Get();
        this.register = GameRegister.Get();
        this.register.inventory = this;
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void BuyBrett()
    {
        var anzahlBrett = this.store.inventory.bretters.Count;
        BrettLevelStats current = LevelStats.brett[anzahlBrett];
        BrettLevelStats next = LevelStats.brett[anzahlBrett + 1];

        if (clicked == 1)
        {
            clicked = 0;
            statsPanelInventory.SetActive(false);



            if (next.price > this.store.hero.taler)
            {
                Debug.Log("zu wenig Geld! ");
            }
            else if (anzahlBrett >= this.store.hero.level)
            {
                Debug.Log("zu wenig Hero level! ");
            }


            else
            {

                movedItem = Instantiate(BrettPrefab, GetPosition(), Quaternion.identity);
                this.register.bretters.ForEach(brett =>
                {
                    brett.LevelUp();
                });
                this.register.market.BuySthStart();
                this.store.hero.taler -= next.price;
                movedItem.GetComponent<BrettScript>().Move();
                
            } }
        else
        {
            clicked = 1;
            statsPanelInventory.SetActive(true);
            textMeshInventoryStats1.text = $"Description: A plank that you can place in the air to make the stones bounce in the right direction";
            textMeshInventoryStats2.text = $"next level price: {next.price}";
            textMeshInventoryStats3.text = $"current level: {current.level}";
        }



    }
    public void BuyEimer()
    {
        var anzahlEimerS = this.store.inventory.eimers.count;
        var anzahlEimerI = this.store.inventory.eimers.eimers.Count;
        EimerLevelStats current = LevelStats.eimer[this.store.inventory.eimers.level];
        EimerLevelStats next = LevelStats.eimer[current.level + 1];
        if (clicked == 2)
        {
            clicked = 0;
            statsPanelInventory.SetActive(false);
            

            if (next.price> this.store.hero.taler)
            {
                Debug.Log("zu wenig Geld! ");
            }
            else if (current.level >= this.store.hero.level)
            {
                Debug.Log("zu wenig Hero level! ");
            }


            else
            {
                if (current.count != next.count)
                {
                    movedItem = Instantiate(EimerPrefab, GetFloorPosition(), Quaternion.identity);
                    this.register.market.BuySthStart();
                    this.store.hero.taler -= next.price;
                    movedItem.GetComponent<EimerScript>().Move();

                }
                this.store.inventory.eimers.level++;
                store.inventory.eimers.size = next.size;
                store.inventory.eimers.count = next.count;
                register.eimers.ForEach((EimerScript eimer)  => eimer.UpgradeEimer());
            }
        }
        else
        {
            clicked = 2;
            statsPanelInventory.SetActive(true);
            textMeshInventoryStats1.text = $"Description: A Bucket that you can placedown to automaticly collect your stones";
            textMeshInventoryStats2.text = $"next level price: {next.price}";
            textMeshInventoryStats3.text = $"current level: {current.level}";
        }


    }
    public void BuyZombie()
    {
        ZombieLevelStats current = LevelStats.zombie[this.store.inventory.zombie.zombieLevel];
        ZombieLevelStats next = LevelStats.zombie[this.store.inventory.zombie.zombieLevel + 1];
        if (clicked == 3)
        {
            clicked = 0;
            statsPanelInventory.SetActive(false);
            if (LevelStats.zombie[this.store.inventory.zombie.zombieLevel].price > this.store.hero.taler)
            {
                Debug.Log("zu wenig Geld! ");
            }
            else if (this.store.inventory.zombie.zombieLevel >= this.store.hero.level - 2)
            {
                Debug.Log("zu wenig Hero level! ");
            }


            else
            {
                this.store.inventory.zombie.zombieLevel++;
                if (this.store.inventory.zombie.zombieLevel == 1)
                {
                    Instantiate(ZombiePrefab, new Vector3(9, -3.5f, 0), Quaternion.identity);
                    this.store.hero.taler -= next.price;
                }
                else
                {
                    UpgradeZombie();



                }
            }
        }
        else
        {
            clicked = 3;
            statsPanelInventory.SetActive(true);
            textMeshInventoryStats1.text = $"Description: A zombie with a net that runs from one side to the other each upgrade makes the net bigger or the zombie faster";
            textMeshInventoryStats2.text = $"next level price: {next.price}";
            textMeshInventoryStats3.text = $"current level: {current.level}";
        }


    }
    public void UpgradeZombie()
    {
        register.zombie.UpgradeZombie();
        register.ZombieNet.UpgrandeZombieNet();
        
    }
    public void BuyDrone()
    {
        DroneLevelStats current = LevelStats.drone[this.store.inventory.drone.dronenLevel];
        DroneLevelStats next = LevelStats.drone[this.store.inventory.drone.dronenLevel + 1];
        if (clicked == 4)
        {
            clicked = 0;
            statsPanelInventory.SetActive(false);
            if (LevelStats.drone[this.store.inventory.drone.dronenLevel].price > this.store.hero.taler)
            {
                Debug.Log("zu wenig Geld! ");
            }
            else if (this.store.inventory.drone.dronenLevel >= this.store.hero.level - 3)
            {
                Debug.Log("zu wenig Hero level! ");
            }


            else
            {
                this.store.inventory.drone.dronenLevel++;
                this.store.hero.taler -= next.price;
                this.store.inventory.drone.speed = next.speed;
                this.store.inventory.drone.maxSteinmenge = next.dronenNet;
                this.store.inventory.drone.smartnis = next.smartnis;
                if (this.store.inventory.drone.dronenLevel == 1)
                {
                    var d = Instantiate(DronenPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                    

                    
                }
                
            }
        }
        else
        {
            clicked = 4;
            statsPanelInventory.SetActive(true);
            textMeshInventoryStats1.text = $"Description: A drone that flyes to stones and gives them to the shop each upgrade makes it smarter faster and can make it hold more";
            textMeshInventoryStats2.text = $"next level price: {next.price}";
            textMeshInventoryStats3.text = $"current level: {current.level}";
        }


    }
    public void BuyNest()
    {
        NestLevelStats current = LevelStats.nest[this.store.inventory.nest.nestLevel];
        NestLevelStats next = LevelStats.nest[this.store.inventory.nest.nestLevel + 1];
        if (clicked == 5)
        {
            clicked = 0;
            statsPanelInventory.SetActive(false);
            if (LevelStats.nest[this.store.inventory.nest.nestLevel].price > this.store.hero.taler)
            {
                Debug.Log("zu wenig Geld! ");
            }
            else if (this.store.inventory.nest.nestLevel >= this.store.hero.level - 4)
            {
                Debug.Log("zu wenig Hero level! ");
            }


            else
            {
                this.store.inventory.nest.nestLevel++;
                this.store.hero.taler -= next.price;
                this.store.inventory.nest.scent = next.scent;
                this.store.inventory.nest.taste = next.taste;
                this.store.inventory.nest.bird = next.bird - 1;
                this.store.inventory.nest.scent = next.scent;
                if (this.store.inventory.nest.nestLevel == 1)
                {
                    movedItem = Instantiate(NestPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                    movedItem.GetComponent<NestScript>().Move();
                    this.register.market.BuySthStart();
                    
                }
                
            }
        }
        else
        {
            clicked = 5;
            statsPanelInventory.SetActive(true);
            textMeshInventoryStats1.text = $"A nest that attracts birds that if hit drop eggs";
            textMeshInventoryStats2.text = $"next level price: {next.price}";
            textMeshInventoryStats3.text = $"current level: {current.level}";
        }


    }
    public void BuyPfane()
    {
        var anzahlPfanneS = this.store.inventory.pfanens.count;
        var anzahlPfanneI = this.store.inventory.pfanens.pfanen.Count;
        EimerLevelStats current = LevelStats.eimer[this.store.inventory.eimers.level];
        EimerLevelStats next = LevelStats.eimer[current.level + 1];
        if (clicked == 6)
        {
            clicked = 0;
            statsPanelInventory.SetActive(false);
            

            if (next.price> this.store.hero.taler)
            {
                Debug.Log("zu wenig Geld! ");
            }
            else if (current.level >= this.store.hero.level)
            {
                Debug.Log("zu wenig Hero level! ");
            }


            else
            {
               
                if (current.count != next.count)
                {
                    movedItem = Instantiate(PfanenPrefab, GetFloorPosition(), Quaternion.identity);
                    this.register.market.BuySthStart();
                    this.store.hero.taler -= next.price;
                    movedItem.GetComponent<PfanenScript>().Move();
                    
                }
                this.store.inventory.eimers.level++;
                store.inventory.pfanens.size = next.size;
                store.inventory.pfanens.count = next.count;
                register.pfanen.ForEach((PfanenScript pfanen)  => pfanen.UpgradePfane());
                
            }
        }
        else
        {
            clicked = 6;
            statsPanelInventory.SetActive(true);
            textMeshInventoryStats1.text = $"Description: A Bucket that you can placedown to automaticly collect your stones";
            textMeshInventoryStats2.text = $"next level price: {next.price}";
            textMeshInventoryStats3.text = $"current level: {current.level}";
        }


    }



    private Vector3 GetPosition()
    {
        // Mausposition von Bildschirm in Welt umrechnen
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Z-Koordinate anpassen, weil ScreenToWorldPoint sonst - je nach Kamera – Blödsinn liefert
        mousePos.z = 0f;

        // Objekt-Position setzen
        return mousePos;
    }
    
    private Vector3 GetFloorPosition()
    {
        Vector3 mousePos = GetPosition();
        mousePos.y = floorY;
        Debug.Log("Pos Y = " + mousePos.y);
        return mousePos;
    }
}