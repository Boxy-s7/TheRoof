using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class DestroyAfterTime : MonoBehaviour
{
    public int time;
    // Start is called before the first frame update
    void Start()
    {
        StartTimer();
    }
    public async void StartTimer()
    {
        await Task.Delay(time * 1000);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
