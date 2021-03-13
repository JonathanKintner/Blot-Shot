  
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour
{
    Transform[] parts = new Transform[4];
    Vector3[] partBasePositions = { new Vector3(-1, 0, 1), new Vector3(1, 0, 1), new Vector3(-1, 0, -1), new Vector3(1, 0, -1) };
    float multiplier;
    bool shooting;
    void Start()
    {
        for(int i = 0; i < 4; i++)
        {
            parts[i] = transform.GetChild(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            multiplier = GameObject.FindGameObjectWithTag("Player").transform.GetChild(1).GetChild(0).GetComponent<gun>().isShooting ? 3f : 2.7f;
            for (int i = 0; i < 4; i++)
            {
                parts[i].localPosition = new Vector3(partBasePositions[i].x * multiplier, 0, partBasePositions[i].z * multiplier);
                //print(new Vector3(partBasePositions[i].x * multiplier, 0, partBasePositions[i].z * multiplier));
            }
        }
    }
}
