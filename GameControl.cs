using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{

    float testSpeed = 7.5f;
    float testHealth = 100;
    GameObject player;

    private void Awake()
    {
        Cursor.visible = false;
        player = Instantiate(Resources.Load("Prefabs/ship0.5")) as GameObject;
        player.transform.position = Vector3.zero;
        player.AddComponent<Player>().initializeValues(testHealth, testSpeed);
        StartCoroutine(spawnEnemies());
    }

    public Vector3 GetPlayerLoc()
    {
        return (player.transform.position);
    }

    public float GetDistanceTo(Vector3 point1, Vector3 point2)
    {
        //finds distance between entity and point 
        Vector3 distVect = point1 - point2;
        //Finds hypotenuse 
        float dist = Mathf.Sqrt((distVect.x * distVect.x) + (distVect.z * distVect.z));
        return (dist);
    }

    IEnumerator spawnEnemies()
    {
        while (true)
        {
            GameObject enemy = GameObject.Instantiate(Resources.Load("Prefabs/test1")) as GameObject;
            yield return new WaitForSeconds(20);
        }
    }
}