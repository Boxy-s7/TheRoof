using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SteinScript : MonoBehaviour
{
    
    private static int nextId = 0;
    private int id = SteinScript.nextId++;
    public bool isDroneTarget;

    void Start()
    {
        // Hole die Anzeige-Komponente vom GameObject "main"

       
    }

    void Update()
    {

    }

    public void Warning()
    {
        isDroneTarget = true;
    }

    public void Catched()
    {
        isDroneTarget = false;
    }
    
    void OnDestroy()
    {
        if (isDroneTarget)
        {
            GameRegister.Get().drone.drone.TargetDestroyed();
        }
    }

}
