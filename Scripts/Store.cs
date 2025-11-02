using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.Collections.LowLevel.Unsafe;

[System.Serializable]
public class Store
{
    public Hero hero;
    public Market market;
    public Stone stone;
    public Net net;

    public Inventory inventory;

    // Weitere Felder nach Bedarf
    public Store()
    {
        this.hero = new Hero();
        this.net = new Net();
        this.stone = new Stone();
        this.inventory = new Inventory();
        this.inventory.bretters = new List<Brett>();
        this.inventory.eimers = new Eimers(0, 0, 0);
        this.inventory.zombie = new Zombie();
        this.inventory.drone = new Drone();
    }
}


[System.Serializable]
public class Hero
{
    
    public int taler;
    public int level;
    public int progress;



}



[System.Serializable]
public class Stone
{
    public int talerProStein;
    public int level;



}


[System.Serializable]
public class Market
{
    




}


[System.Serializable]
public class Net
{
    public int steinmenge;
    public int maxSteinmenge;
    public int level;



}

[System.Serializable]
public class Inventory
{
    public List<Brett> bretters;
    public Eimers eimers;
    public Zombie zombie;
    public Drone drone;



}

[System.Serializable]
public class Brett
{
    public float positionX;
    public float positionY;
    public float rotationZ;

    public Brett(float positionX, float positionY, float rotationZ)
    {
        Debug.Log("new brett" + positionX + " " + positionY + " " + rotationZ);

        this.positionX = positionX;
        this.positionY = positionY;
        this.rotationZ = rotationZ;
    }

}


[System.Serializable]
public class Eimers
{
    public int level;
    public int count;
    public float size;
    public List<Eimer> eimers;
    public Eimers(int count, float size, int level)
    {
        this.level = level;
        this.count = count;
        this.size = size;
        this.eimers = new List<Eimer>();
    }
}
[System.Serializable]
public class Eimer
{
    public float positionX;
    public float positionY;
    



    public Eimer(float positionX, float positionY)
    {
        Debug.Log("new eimer");

        this.positionX = positionX;
        this.positionY = positionY;


    }

    

}

[System.Serializable]
public class Zombie
{
    public int zombieLevel;
}

[System.Serializable]
public class Drone
{
    public int dronenLevel;
    public int maxSteinmenge;
    public float speed;
    public int smartnis;
}
