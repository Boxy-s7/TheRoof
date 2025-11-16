using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connect4Script : MonoBehaviour
{
    public Dictionary<int, GameObject> chips;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DropChip(int colum, int color)
    {
        if(chips[colum * 7 + 1].GetComponent<Connect4DisplayScript>().color == 1)
        {
            if(chips[colum * 7 + 2] == null)
        {
            if(chips[colum * 7 + 3] == null)
        {
            if(chips[colum * 7 + 4] == null)
        {
            if(chips[colum * 7 + 5] == null)
        {
            if(chips[colum * 7 + 6] == null)
        {
            if(chips[colum * 7 + 7] == null)
        {
            chips[colum * 7 + 7].GetComponent<Connect4DisplayScript>().ChangeColor(color);
        }
        }
        }
        }
        }
        }
        }
    }
}
