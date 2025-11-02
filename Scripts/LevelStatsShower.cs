using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStatsShower : MonoBehaviour
{
    public List<HeroLevelStats> hero = new List<HeroLevelStats>();
    public List<StoneLevelStats> stone = new List<StoneLevelStats>();
    public List<NetLevelStats> net = new List<NetLevelStats>();


    public string test;
    // Start is called before the first frame update
    void Start()
    {
        this.hero = new List<HeroLevelStats>(LevelStats.hero.Values);
        this.stone = new List<StoneLevelStats>(LevelStats.stone.Values);
        this.net = new List<NetLevelStats>(LevelStats.net.Values);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
