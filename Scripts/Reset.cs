using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
public class Reset : MonoBehaviour{
    public int taler;
    public int level;
    public int talerProStein;
    public int steinmenge;


public TMP_InputField textMeshTaler;
public TMP_InputField textMeshStein;
public TMP_InputField textMeshLevel;


    void Start()
    {
        
    }

 
    public void Reseting()
    {   
        steinmenge = 0;
        talerProStein = 1;
        taler = 100;
        level = 1;

        textMeshTaler.text = taler.ToString();
        textMeshStein.text = steinmenge.ToString();
        textMeshLevel.text = level.ToString();
    }
    }

