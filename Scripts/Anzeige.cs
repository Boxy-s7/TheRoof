using System;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Anzeige : MonoBehaviour
{
    private Store store;
    public Register register;
    
    
    public TMP_InputField textMeshTaler;
    public TMP_InputField textMeshStein;
    public TMP_Text textMeshMission;

    void Start()
    {
        store = GameStore.Get();
        register = GameRegister.Get();
        textMeshMission.text = MissionFinder();
        register.anzeige = this;
    }
    public void missionUpdate()
    {
        textMeshMission.text = "Mission to Level Up:" + MissionFinder();
    }
    void Update()
    {
        if (store == null) return;
        if (textMeshStein != null) textMeshStein.text = store.net.steinmenge.ToString();
        
        if (textMeshTaler != null) textMeshTaler.text = store.hero.taler.ToString();
    }

    public void Sell()
    {
        if (store == null) return;
        store.hero.taler += store.stone.talerProStein * store.net.steinmenge;
        store.net.steinmenge = 0;
    }

    public void Work()
    {
        if (store == null) return;
        store.net.steinmenge += 1;
    }

    public void Buy()
    {
        if (store == null) return;
        int kosten = (int)Math.Pow(store.hero.level, 3);
        if (store.hero.taler >= kosten)
        {
            store.hero.level += 1;
            store.hero.taler -= kosten;
        }
    }
    string[] missionTabelle = new string[]
    {
        " ",
        "Collect 30 stones with your net",
        "Let 10 stones bounce on your plank",
        "Collect 10 stones with your buckets",
        "Collect 30 stones with your zombie",
        "Collect 100 stones with your drone",
        " ",
        " ",
        " ",
        " ",
        " "
    };
    public string MissionFinder()
    {
        Debug.Log(register.levelManager);
        LevelEntry entry = register.levelManager.solls[this.store.hero.level];
        return register.levelManager.discriptDict[entry.name].Replace("#", entry.soll.ToString()).Replace("§", this.store.hero.progress.ToString());

    }
}