using UnityEngine;
using System.Collections;

public class ReptileSpawn : MonoBehaviour
{

    public GameObject reptile;
    private float timer;
    private float reduction = 0.0f;
    private float minTime = 60.0f;
    private float passedTime = 0.0f;
    // Use this for initialization
    void Start()
    {
        timer = 60 * Random.value;
        Invoke("Spawn", timer + minTime - reduction);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Reset()
    {
        CancelInvoke("Spawn");
        timer = 60 * Random.value;
        Invoke("Spawn", timer + minTime - reduction);
    }
    void Spawn()
    {
        passedTime += timer;
        if (passedTime > 300.0f)
        {
            if (reduction < minTime)
                reduction += 0.5f;
            passedTime = 0;
        }
        timer = 60 * Random.value;
        GameObject go = (GameObject)Instantiate(reptile, transform.position, transform.rotation);
        Invoke("Spawn", timer + minTime - reduction);
    }
}
