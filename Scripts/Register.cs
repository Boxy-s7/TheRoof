using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine.UIElements;

[System.Serializable]
public class Register
{
    public MusicScript radio;
    public Settings settings;
    public Anzeige anzeige;
    public MarketScript market;
    public NetScript net;
    public playerScript player;
    public ShopButtonHandler shopButtons;
    public ShopScript shop;
    public InventoryScript inventory;
    public GameStoreManager gameStoreManager;
    public ZombieScript zombie;
    public ZombieNetScript ZombieNet;
    public LevelManager levelManager;

    public List<BrettScript> bretters;
    public List<EimerScript> eimers;
    public RegisterDrone drone;
    public BirdScript bird;
    public NestScript nest;
    public Register()
    {
        this.bretters = new List<BrettScript>();
        this.eimers = new List<EimerScript>();
        this.drone = new RegisterDrone();
    }


}
[System.Serializable]
public class RegisterDrone
{
    public DronenScript drone;
    public DronenBodyScript dronenBody;
    public DronenDetectorScript dronenDetector;

   
}