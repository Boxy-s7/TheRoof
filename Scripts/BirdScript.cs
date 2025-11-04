using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public int spawnSite;
    public Store store;
    public Register register;
    public String mode = "Find food";
    public Vector3 target;
    public float speedFactor = 1;
    // Start is called before the first frame update
    void Start()
    {
        this.store = GameStore.Get();
        register = GameRegister.Get();
        Debug.Log(register.nest.nestPos);
        this.register.bird = this;
        target = register.nest.nestPos;
        if (gameObject.transform.position.x > 0)
        {
            spawnSite = -1;
        }
        else
        {
            spawnSite = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (mode == "Find food")
        {
            UpdateFindfood();
        }
        else
        {
            transform.position = Vector3.MoveTowards(
                    transform.position,
                    target,
                    speedFactor * Time.deltaTime);
            if (gameObject.transform.position == target)
            {
                DestroyImmediate(gameObject);
            }
        }
    }
    private void UpdateFindfood()
    {
        transform.position = Vector3.MoveTowards(
                    transform.position,
                    target,
                    speedFactor * Time.deltaTime);
        if (gameObject.transform.position == target)
        {
            mode = "Fly home";
            target = new Vector3(10 * spawnSite, UnityEngine.Random.Range(5f, 0f));
        }
    }
}
