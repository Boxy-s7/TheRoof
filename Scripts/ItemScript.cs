using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


public abstract class ItemScript : MonoBehaviour
{

    public abstract String ItemType();
    public Store store;
    public Register register;
    
    void Start()
    {
        Debug.Log(ItemType() + " Start");
        this.store = GameStore.Get();
        this.register = GameRegister.Get();
        this.Register();

    }

    public abstract void Register();

    public void Destroy()
    {
        Debug.Log(ItemType() + " Destroy");
        DestroyImmediate(gameObject);
    }

}
