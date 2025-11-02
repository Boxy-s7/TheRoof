using System;
using UnityEngine;
using TMPro;

public class Anzeige : MonoBehaviour
{
    private Store store;
    
    public TMP_InputField textMeshTaler;
    public TMP_InputField textMeshStein;

    void Start()
    {
        store = GameStore.Get();
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
}