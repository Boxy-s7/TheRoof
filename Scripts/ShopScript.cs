using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopScript : MonoBehaviour
{
    public StoneLevelStats stoneStats;
    public NetLevelStats netStats;
    public HeroLevelStats heroStats;
    public int menuOpen;
    public GameObject GMenuStone;
    public GameObject GMenuNet;
    public GameObject GMenuHero;
    public GameObject GMenuInventory;

    public TMP_InputField textMeshSteinStats1;
    public TMP_InputField textMeshSteinStats2;
    public TMP_InputField textMeshSteinStats3;

    public Store store;
    public Register register;
    [Header("das sind die statpanels")]
    public GameObject StatPanelHero;
    public GameObject StatPanelNet;
    public GameObject StatPanelStone;
    public GameObject StatPanelInventory;
    
    public bool clickedStone = false;
    public bool clickedHero = false;
    public bool clickedNet = false;
    public TMP_InputField textMeshNetStats1;
    public TMP_InputField textMeshNetStats2;
    public TMP_InputField textMeshNetStats3;
    public TMP_InputField textMeshHeroStats1;
    public TMP_InputField textMeshHeroStats2;




    // Start is called before the first frame update
    void Start()
    {
        this.store = GameStore.Get();
        Debug.Log("shop start");
        this.StatPanelStone.SetActive(false);
        this.StatPanelHero.SetActive(false);
        this.StatPanelNet.SetActive(false);
        this.StatPanelInventory.SetActive(false);
        this.gameObject.SetActive(false);
        register = GameRegister.Get();
        this.register.shop = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (clickedStone && !this.GMenuStone.activeSelf)
        {
            clickedStone = false;
            this.StatPanelStone.SetActive(false);
        }
        if (clickedNet && !this.GMenuNet.activeSelf)
        {
            clickedNet = false;
            this.StatPanelNet.SetActive(false);
        }
        if (clickedHero && !this.GMenuHero.activeSelf)
        {
            clickedHero = false;
            this.StatPanelHero.SetActive(false);
        }
        
    }

    public void BuyStone(int level)
    {
        Debug.Log("BuyStone");
        if (this.stoneStats == null || this.stoneStats.level != level)
        {
            this.clickedStone = false;
            Debug.Log("BuyStone zurück gesätst");



        }
        this.stoneStats = LevelStats.stone.GetValueOrDefault(level, new StoneLevelStats(0, 0, 0));
        if (!clickedStone)
        {
            clickedStone = true;
            this.StatPanelStone.SetActive(true);
            textMeshSteinStats1.text = $"Level: {stoneStats.level}";
            textMeshSteinStats2.text = $"Price: {stoneStats.price}";
            textMeshSteinStats3.text = $"Value: {stoneStats.talerProStein}";
            Debug.Log("clicked war nd");
            return;

        }

        clickedStone = false;



        Debug.Log("buy level " + level);
        Stone stone = this.store.stone;
        int storeLevel = this.store.stone.level;
        if (level <= storeLevel)
        {
            Debug.Log("Stone Level already bought! Dismissed! " + level + " / " + storeLevel);
        }
        else if (level != storeLevel + 1)
        {
            Debug.Log("Stone Level too big! Dismissed! " + level + " / " + storeLevel);
        }
        else
        {

           
            if (stoneStats.price > this.store.hero.taler)
            {
                Debug.Log("zu wenig Geld! " + stoneStats.price + " / " + this.store.hero.taler);
            }
            else if (stoneStats.level > this.store.hero.level)
            {
                Debug.Log("zu wenig Hero level! " + stoneStats.level + " / " + this.store.hero.level);
            }



            else
            {
                this.store.stone.level = stoneStats.level;
                this.store.stone.talerProStein = stoneStats.talerProStein;
                this.store.hero.taler -= stoneStats.price;
                this.register.shopButtons.UpdateLevelStone(level);
                Debug.Log("stone level increased " + level);
            }
        }
    }
    public void BuyHero(int level)
    {
        Debug.Log("BuyStone");
        if (this.heroStats == null || this.heroStats.level != level)
        {
            this.clickedHero = false;
            Debug.Log("Buyhero zurück gesätst");



        }
        this.heroStats = LevelStats.hero.GetValueOrDefault(level, new HeroLevelStats(0, 0));
        if (!clickedHero)
        {
            clickedHero = true;
            this.StatPanelHero.SetActive(true);
            textMeshHeroStats1.text = $"task: {heroStats.level}/30";
            textMeshHeroStats2.text = $"Price: {heroStats.price}";
            //textMeshSteinStats3.text = $"Value: {heroStats.talerProStein}";
            Debug.Log("clicked war nd");
            return;

        }

        clickedHero = false;



        Debug.Log("buy level " + level);
        Hero hero = this.store.hero;
        int storeLevel = hero.level;
        if (level <= storeLevel)
        {
            Debug.Log("hero Level already bought! Dismissed! " + level + " / " + storeLevel);
        }
        else if (level != storeLevel + 1)
        {
            Debug.Log("hero Level too big! Dismissed! " + level + " / " + storeLevel);
        }
        else
        {


            if (heroStats.price > this.store.hero.taler)
            {
                Debug.Log("zu wenig Geld! " + heroStats.price + " / " + this.store.hero.taler);
            }

            else
            {

                this.store.hero.level = heroStats.level;
                this.store.hero.taler -= heroStats.price;
                this.register.shopButtons.UpdateLevelHero(level);
                Debug.Log("hero level increased " + level);
            }
        }
    }
    
        public void BuyNet(int level)
    {


        if (this.netStats == null || this.netStats.level != level)
        {
            this.clickedNet = false;




        }
        this.netStats = LevelStats.net.GetValueOrDefault(level, new NetLevelStats(0, 0, 0, "Stone"));
        if (!clickedNet)
        {
            clickedNet = true;
            this.StatPanelNet.SetActive(true);
            textMeshNetStats1.text = $"Level: {netStats.level}";
            textMeshNetStats2.text = $"price: {netStats.price}";
            textMeshNetStats3.text = $"max: {netStats.maxSteinmenge}";
            return;

        }

        clickedNet = false;



        Debug.Log("buy level " + level);
        Net net = this.store.net;
        int storeLevel = this.store.net.level;
        if (level <= storeLevel)
        {
            Debug.Log("net Level already bought! Dismissed! " + level + " / " + storeLevel);
        }
        else if (level != storeLevel + 1)
        {
            Debug.Log("Net Level too big! Dismissed! " + level + " / " + storeLevel);
        }
        else
        {


            if (netStats.price > this.store.hero.taler)
            {
                Debug.Log("zu wenig Geld! " + netStats.price + " / " + this.store.hero.taler);
            }
            else if (netStats.level > this.store.hero.level)
            {
                Debug.Log("zu wenig Hero level! " + netStats.level + " / " + this.store.hero.level);
            }

            else
            {
                this.store.net.catchedObject = LevelStats.net[store.net.level].catchedObject;
                this.store.net.level = netStats.level;
                this.store.net.maxSteinmenge = netStats.maxSteinmenge;
                this.store.hero.taler -= netStats.price;
                this.register.shopButtons.UpdateLevelNet(level);
                register.player.ChangeNet(level);
                Debug.Log("net level increased " + level);
                this.register.anzeige.CatchSymbolSwitch(LevelStats.net[store.net.level].catchedObject);
            }
        }
    }

    public void MenuStein()
    {
        menuOpen = 1;
        this.HandleMenu(menuOpen);


    }
    public void MenuNet()
    {
        menuOpen = 2;
        this.HandleMenu(menuOpen);


    }
    public void MenuHero()
    {
        menuOpen = 3;
        this.HandleMenu(menuOpen);


    }
    public void MenuInventory()
    {
        menuOpen = 4;
        this.HandleMenu(menuOpen);


    }
    public void MenuShopClose()
    {
        menuOpen = 0;
        this.HandleMenu(menuOpen);
    }
    private void HandleMenu(int menuOpen)
    {
        if (menuOpen == 0)
        {
            this.GMenuStone.SetActive(false);
            this.GMenuNet.SetActive(false);
            this.GMenuHero.SetActive(false);
            this.GMenuInventory.SetActive(false);




        }

        if (menuOpen == 1)
        {
            this.GMenuStone.SetActive(true);
            this.GMenuNet.SetActive(false);
            this.GMenuHero.SetActive(false);
            this.GMenuInventory.SetActive(false);





        }

        if (menuOpen == 2)
        {
            this.GMenuStone.SetActive(false);
            this.GMenuNet.SetActive(true);
            this.GMenuHero.SetActive(false);
            this.GMenuInventory.SetActive(false);




        }
        if (menuOpen == 3)
        {
            this.GMenuStone.SetActive(false);
            this.GMenuNet.SetActive(false);
            this.GMenuHero.SetActive(true);
            this.GMenuInventory.SetActive(false);




        }
        if (menuOpen == 4)
        {
            this.GMenuStone.SetActive(false);
            this.GMenuNet.SetActive(false);
            this.GMenuHero.SetActive(false);
            this.GMenuInventory.SetActive(true);


            
            
        }
    
    }
    
}