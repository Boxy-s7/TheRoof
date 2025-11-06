using System;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;

public class Anzeige : MonoBehaviour
{
    private Store store;
    public Register register;
    public List<Sprite> test;
    public Dictionary<string, Sprite> catchSymbols;
    public GameObject catchSymbol;
    public TMP_InputField textMeshTaler;
    public TMP_InputField textMeshStein;
    public TMP_Text textMeshMission;

    void Start()
    {
        store = GameStore.Get();
        register = GameRegister.Get();
        textMeshMission.text = MissionFinder();
        register.anzeige = this;
        catchSymbols = new();
        catchSymbols.Add("Stone", test[0]);
        catchSymbols.Add("Egg", test[1]);
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
    public string MissionFinder()
    {
        Debug.Log(register.levelManager);
        LevelEntry entry = register.levelManager.solls[this.store.hero.level];
        return register.levelManager.discriptDict[entry.name].Replace("#", entry.soll.ToString()).Replace("§", this.store.hero.progress.ToString());

    }
    public void CatchSymbolSwitch(string key)
    {
        
        catchSymbol.GetComponent<SpriteRenderer>().sprite = catchSymbols.GetValueOrDefault(key);
        
    }
}