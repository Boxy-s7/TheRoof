using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;


public class GameStoreManager : MonoBehaviour
{
    public string pfadToStore = "store.json"; // Relativ zu Application.persistentDataPath
    public Store store;
    public bool storeLoaded = false;
    public Register register;
    
    
    private string FullPath => Path.Combine(Application.persistentDataPath, pfadToStore);

    private void Awake()
    {

        LoadStore();

    }
    void Start()
    {
        this.register = GameRegister.Get();
        this.register.gameStoreManager = this;
        Debug.Log("GameStoreManager Awake");


    }

    private void OnApplicationQuit()
    {
        SaveStore();
        this.store = GameStore.Get();
    }

    public void LoadStore()
    {
        try
        {
            if (File.Exists(FullPath))
            {

                string json = File.ReadAllText(FullPath);
                Store loadedStore = JsonUtility.FromJson<Store>(json);
                GameStore.Set(loadedStore);
                Debug.Log("GameStore geladen: " + json.ToString());
                Debug.Log("GameStore check" + loadedStore.inventory.bretters.Count);
                this.storeLoaded = true;
            }
            else
            {
                Store defaultStore = new Store(); // Standarddaten, z.B. score = 0

                GameStore.Set(defaultStore);
                Debug.Log("Keine Store-Datei gefunden. Neuer Store erstellt.");
            }
        }
        catch (Exception e)
        {
            Store defaultStore = new Store(); // Standarddaten, z.B. score = 0

            GameStore.Set(defaultStore);
            Debug.Log("Store war kaput");
            Console.WriteLine(e.Message);
        }

    }

    private void SaveStore()
    {
        Store store = GameStore.Get();
        string json = JsonUtility.ToJson(store, true);
        File.WriteAllText(FullPath, json);
        Debug.Log("GameStore gespeichert: " + FullPath);
        Debug.Log("GameStore gespeichert: " + json);
    }
    public void Reset()
    {
        Debug.Log("reset anfang");
        Register register = GameRegister.Get();
        Store store = GameStore.Init();
        register.player.ChangeNet(store.net.level);
        register.net.ClearNet();
        if (register.zombie != null)
        {
            register.zombie.Destroy();
            register.zombie = null;
        }
        if (register.drone.drone != null)
        {
            register.drone.drone.Destroy();
            register.drone = new RegisterDrone();
        }
        register.anzeige.missionUpdate();
        register.bretters.ForEach(brett => { brett.Destroy(); });
        register.bretters.Clear();
        register.eimers.ForEach(eimer => { eimer.Destroy(); });
        register.eimers.Clear();
        Destroy(register.nest.gameObject);
        register.eimers.ForEach(Pfanen => { Pfanen.Destroy(); });
        register.pfanen.Clear();
        this.storeLoaded = true;
        Debug.Log("reset ende");
    }
}
