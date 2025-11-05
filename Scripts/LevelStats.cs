using System;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;


public class LevelStats
{
    public Store store;

    public static Dictionary<int, HeroLevelStats> hero;
    public static Dictionary<int, StoneLevelStats> stone;
    public static Dictionary<int, NetLevelStats> net;
    public static Dictionary<int, BrettLevelStats> brett;
    public static Dictionary<int, EimerLevelStats> eimer;
    public static Dictionary<int, ZombieLevelStats> zombie;
    public static Dictionary<int, DroneLevelStats> drone;
    public static Dictionary<int, NestLevelStats> nest;
    public static void Init()
    {

        hero = new Dictionary<int, HeroLevelStats>();
        for (int i = 0; i < 11; i++)
        {
            hero.Add(i, new HeroLevelStats(i, (int)Math.Pow(i, 4)));
        }

        stone = new Dictionary<int, StoneLevelStats>();
        for (int i = 0; i < 11; i++)
        {
            stone.Add(i, new StoneLevelStats(i, (int)Math.Pow(i, 4), (int)Math.Floor(Math.Pow(i, 2.8) * talerProSteinFactor[i]) + 5));
        }

        net = new Dictionary<int, NetLevelStats>();
        for (int i = 0; i < 11; i++)
        {
            net.Add(i, new NetLevelStats(i, (int)Math.Pow(i, 4), (int)(i * 5) + 3));
        }
        
        brett= new Dictionary<int, BrettLevelStats>();
        for (int i = 0; i < 11; i++)
        {
            brett.Add(i, new BrettLevelStats(i, (int)Math.Pow(i, 4) * 10));
        }

        eimer = new Dictionary<int, EimerLevelStats>();
        for (int i = 0; i < 11; i++)
        {
            eimer.Add(i, new EimerLevelStats(i, (int)Math.Pow(i, 4) * 20, eimerSizeTabelle[i], eimerCountTabelle[i]));
        }


        zombie = new Dictionary<int, ZombieLevelStats>();
        for (int i = 0; i < 11; i++)
        {
            zombie.Add(i, new ZombieLevelStats(i, (int)Math.Pow(i, 4) * 30, zombieSpeedFactor[i], zombieNetFactor[i]));
        }
        drone = new Dictionary<int, DroneLevelStats>();
        for (int i = 0; i < 9; i++)
        {
            drone.Add(i, new DroneLevelStats(i, (int)Math.Pow(i, 4) * 40, droneSpeedFactor[i], droneNetFactor[i], dronenSmartnisFactor[i]));
        }
        nest = new Dictionary<int, NestLevelStats>();
        for (int i = 0; i < 9; i++)
        {
            nest.Add(i, new NestLevelStats(i, (int)Math.Pow(i, 4) * 50, nestTasteFactor[i], nestScentFactor[i], i));
        }
    }

    static float[] talerProSteinFactor = new float[]
    {
         0f, 1.2f, 1.7f, 1.5f, 0.9f, 1.3f, 1f, 1.4f, 1.8f, 2.1f, 3.3f, 0
    };
    static float[] zombieNetFactor = new float[]
    {
        0f,1f, 1.2f, 1.2f, 1.3f, 1.3f, 1.4f, 1.4f, 1.6f, 1.6f, 2f, 0
    };
    static float[] zombieSpeedFactor = new float[]
    {
        0f,1f, 1f, 1.3f, 1.3f, 1.4f, 1.4f, 1.6f, 1.6f, 1.8f, 2f, 0
    };
    static int[] droneNetFactor = new int[]
    {
        0, 1, 1, 2, 2, 3, 5, 5, 0
    };
    static float[] droneSpeedFactor = new float[]
    {
        0f,1f, 1f, 1.3f, 1.3f, 1.6f, 1.6f, 2f, 0f
    };
    static int[] dronenSmartnisFactor = new int[]
    {
        0, 1, 1, 2, 2, 3, 3, 3, 0
    };
    static float[] eimerSizeTabelle = new float[]
    {
        0f, 1f, 1.2f, 1.4f, 1.4f, 1.6f, 1.8f, 1.8f, 2f, 2.2f, 2.4f, 0
    };
    static int[] eimerCountTabelle = new int[]
    {
        0, 1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 0
    };
    static int[] nestScentFactor = new int[]
    {
        0, 1, 1, 2, 2, 3, 3, 3, 0
    };
    static int[] nestTasteFactor = new int[]
    {
        0, 1, 1, 2, 2, 3, 3, 3, 0
    };
}

[System.Serializable]
public class HeroLevelStats
{
    public int level;
    public int price;
    public HeroLevelStats(int level, int price)
    {
        this.level = level;
        this.price = price;
    }
}

[System.Serializable]
public class StoneLevelStats
{
    public int level;
    public int price;
    public int talerProStein;
    public StoneLevelStats(int level, int price, int talerProStein)
    {
        this.talerProStein = talerProStein;
        this.level = level;
        this.price = price;
    }
}

[System.Serializable]
public class NetLevelStats
{
    public int level;
    public int price;
    public int maxSteinmenge;
    public NetLevelStats(int level, int price, int maxSteinmenge)
    {
        this.maxSteinmenge = maxSteinmenge;
        this.level = level;
        this.price = price;


    }
}
[System.Serializable]
public class BrettLevelStats
{
    public int level;
    public int price;
    
    public BrettLevelStats(int level, int price)
    {
        
        this.level = level;
        this.price = price;
        


    }
}
[System.Serializable]
public class EimerLevelStats
{
    public int level;
    public int price;
    public float size;
    public int count;
    public EimerLevelStats(int level, int price, float size, int count)
    {
        
        this.level = level;
        this.price = price;
        this.size = size;
        this.count = count;


    }
}

[System.Serializable]
public class ZombieLevelStats
{
    public int level;
    public int price;
    public float speed;
    public float zombieNet;
    public ZombieLevelStats(int level, int price, float speed, float zombieNet)
    {
        this.speed = speed;
        this.level = level;
        this.price = price;
        this.zombieNet = zombieNet;


    }

}

[System.Serializable]
public class DroneLevelStats
{
    public int level;
    public int price;
    public float speed;
    public int dronenNet;
    public int smartnis;
    public DroneLevelStats(int level, int price, float speed, int dronenNet, int smartnis)
    {
        this.speed = speed;
        this.level = level;
        this.price = price;
        this.dronenNet = dronenNet;
        this.smartnis = smartnis;

    }
}
[System.Serializable]
public class NestLevelStats
{
    public int level;
    public int price;
    public int taste;
    public int scent;
    public int bird;
    public NestLevelStats(int level, int price, int taste, int scent, int bird)
    {
        this.taste = taste;
        this.level = level;
        this.price = price;
        this.scent = scent;
        this.bird = bird;
    }
}