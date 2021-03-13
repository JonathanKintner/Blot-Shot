using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{
    delegate void WavesMethod();//This allows the use of void in a list
    List<WavesMethod> wavesList = new List<WavesMethod>();//Should contain all waves

    List<GameObject> enemyTypes = new List<GameObject>();// [Rhino, Sprayer, ManOWar, Burster, Hive, Shark, Falcon, Wolf, Bee]

    List<GameObject> currentEnemies = new List<GameObject>();       //Important !!!! Add any instantiated enemies to this list in the wave functions or it will not be able to tell if the wave is complete or not !!!!

    List<float> enemyTimings = new List<float>(); //Used to make enemies appear with different timing after another during a wave
    int counter = 0;//Used for multi step waves
    int totalCount;//Used for multi step waves
    public int wavesCount;

    #region WaveStorage 
    public void SpawnWave(int waveID)//Use this public method to start a wave
    {
        if (CheckIfWaveComplete())//Prevents starting wave while another is in progress
        {
            currentEnemies.Clear(); //Clears the current enemies list before adding more 
            enemyTimings.Clear();//Clears timings list
            wavesList[waveID]();//calls the function based on the wave ID
        }
        else
        {
            print("Wave must be complete before beginning another");
        }
    }

    void DefineList()//Add waves to the list here
    {
        wavesList.Add(Wave0);
        wavesList.Add(Wave1);
        wavesList.Add(Wave2);
        wavesList.Add(Wave3);
        wavesList.Add(Wave4);
        wavesList.Add(Wave5);
        wavesList.Add(Wave6);
        wavesList.Add(Wave7);
        wavesList.Add(Wave8);
        wavesList.Add(Wave9);
        wavesList.Add(Wave10);
        wavesList.Add(Wave11);
        wavesList.Add(Wave12);
        wavesList.Add(Wave13);
        wavesList.Add(Wave14);
        wavesList.Add(Wave15);
        wavesList.Add(Wave16);
        wavesList.Add(Wave17);
        wavesList.Add(Wave18);
        wavesList.Add(Wave19);
        wavesList.Add(Wave20);
        wavesList.Add(Wave21);
        wavesList.Add(Wave22);
        wavesList.Add(Wave23);
        wavesList.Add(Wave24);
        wavesList.Add(Wave25);
        wavesList.Add(Wave26);
        wavesList.Add(Wave27);
        wavesList.Add(Wave28);
        wavesList.Add(Wave29);
        wavesCount = wavesList.Count;
    }

    void Wave0() // 2 vertical Man O Wars, 4 Bees from corners
    {
        GameObject enemy = Instantiate(enemyTypes[8], new Vector3(-20, 0, -10), Quaternion.identity);
        currentEnemies.Add(enemy);
        enemy = Instantiate(enemyTypes[8], new Vector3(-20, 0, 10), Quaternion.identity);
        currentEnemies.Add(enemy);
        enemy = Instantiate(enemyTypes[8], new Vector3(20, 0, -10), Quaternion.identity);
        currentEnemies.Add(enemy);
        enemy = Instantiate(enemyTypes[8], new Vector3(20, 0, 10), Quaternion.identity);
        currentEnemies.Add(enemy);
        enemy = Instantiate(enemyTypes[2], new Vector3(15, 0, 10), Quaternion.identity);
        enemy.GetComponent<ManOWar>().SetTargetLocation(new Vector3(15, 0, -10));
        currentEnemies.Add(enemy);
        enemy = Instantiate(enemyTypes[2], new Vector3(-15, 0, -10), Quaternion.identity);
        enemy.GetComponent<ManOWar>().SetTargetLocation(new Vector3(-15, 0, 10));
        currentEnemies.Add(enemy);
    }

    void Wave1() //Falcon Intro - 2 falcons spawned on a delay
    {
        GameObject enemy;
        enemy = Instantiate(enemyTypes[6], new Vector3(20, 0, 20), Quaternion.identity);
        enemy.GetComponent<Falcon>().InitializeSpawnPoint(0);
        currentEnemies.Add(enemy);
        enemyTimings.Add(.1f);
        enemy = Instantiate(enemyTypes[6], new Vector3(20, 0, 20), Quaternion.identity);
        enemy.GetComponent<Falcon>().InitializeSpawnPoint(1);
        currentEnemies.Add(enemy);
        enemyTimings.Add(3);
        StartCoroutine(MultiStepWave());
    }

    void Wave2()//Sprayer introduction - 2 sprayers
    {
        GameObject enemy;
        enemy = Instantiate(enemyTypes[1], new Vector3(0, 0, -12), Quaternion.identity);
        enemy.GetComponent<Sprayer>().targetDistance = 8;
        currentEnemies.Add(enemy);
        enemy = Instantiate(enemyTypes[1], new Vector3(0, 0, 12), Quaternion.identity);
        enemy.GetComponent<Sprayer>().targetDistance = 8;
        currentEnemies.Add(enemy);
    }

    void Wave3()// 2 Sprayers 2 Bees
    {
        GameObject enemy;
        enemy = Instantiate(enemyTypes[1], new Vector3(-20, 0, -12), Quaternion.identity);
        enemy.GetComponent<Sprayer>().targetDistance = 15;
        currentEnemies.Add(enemy);
        enemy = Instantiate(enemyTypes[1], new Vector3(20, 0, -12), Quaternion.identity);
        enemy.GetComponent<Sprayer>().targetDistance = 15;
        currentEnemies.Add(enemy);
        enemy = Instantiate(enemyTypes[8], new Vector3(-18, 0, 12), Quaternion.identity);
        currentEnemies.Add(enemy);
        enemy = Instantiate(enemyTypes[8], new Vector3(18, 0, 12), Quaternion.identity);
        currentEnemies.Add(enemy);
    }

    void Wave4()//Hive and 1 bee
    {
        GameObject enemy;
        enemy = Instantiate(enemyTypes[8], new Vector3(0, 0, -12), Quaternion.identity);
        currentEnemies.Add(enemy);
        enemy = Instantiate(enemyTypes[4], new Vector3(20, 0, 9), Quaternion.identity);
        currentEnemies.Add(enemy);
    }

    void Wave5()//Burst and Sprayer
    {
        GameObject enemy;
        enemy = Instantiate(enemyTypes[1], new Vector3(-20, 0, -12), Quaternion.identity);
        enemy.GetComponent<Sprayer>().targetDistance = 15;
        currentEnemies.Add(enemy);
        enemy = Instantiate(enemyTypes[3], new Vector3(20, 0, 12), Quaternion.identity);
        currentEnemies.Add(enemy);
    }

    void Wave6()//Man O War Intro - 2 Man O war
    {
        GameObject enemy;
        enemy = Instantiate(enemyTypes[2], new Vector3(11, 0, 10), Quaternion.identity);
        enemy.GetComponent<ManOWar>().SetTargetLocation(new Vector3(11, 0, -12));
        currentEnemies.Add(enemy);
        enemy = Instantiate(enemyTypes[2], new Vector3(-11, 0, -10), Quaternion.identity);
        enemy.GetComponent<ManOWar>().SetTargetLocation(new Vector3(-11, 0, 12));
        currentEnemies.Add(enemy);
    }

    void Wave7()//1 Burst 2 Bees
    {
        GameObject enemy;
        enemyTimings.Add(1);
        enemy = Instantiate(enemyTypes[8], new Vector3(-20, 0, 12), Quaternion.identity);
        currentEnemies.Add(enemy);
        enemyTimings.Add(0);
        enemy = Instantiate(enemyTypes[8], new Vector3(20, 0, 12), Quaternion.identity);
        currentEnemies.Add(enemy);
        enemyTimings.Add(2);
        enemy = Instantiate(enemyTypes[3], new Vector3(0, 0, -12), Quaternion.identity);
        currentEnemies.Add(enemy);
        StartCoroutine(MultiStepWave());
    }

    void Wave8()//Rhino Intro - 3 Rhinos
    {
        GameObject enemy;
        enemy = Instantiate(enemyTypes[0], new Vector3(18, 0, -12), Quaternion.identity);
        currentEnemies.Add(enemy);
        enemy = Instantiate(enemyTypes[0], new Vector3(-18, 0, -12), Quaternion.identity);
        currentEnemies.Add(enemy);
        enemy = Instantiate(enemyTypes[0], new Vector3(0, 0, 12), Quaternion.identity);
        currentEnemies.Add(enemy);

    }

    void Wave9()//3 Sharks
    {
        GameObject enemy;
        enemyTimings.Add(1);
        enemy = Instantiate(enemyTypes[5], new Vector3(18, 0, 12), Quaternion.identity);
        currentEnemies.Add(enemy);
        enemyTimings.Add(1);
        enemy = Instantiate(enemyTypes[5], new Vector3(18, 0, 12), Quaternion.identity);
        currentEnemies.Add(enemy);
        enemyTimings.Add(1);
        enemy = Instantiate(enemyTypes[5], new Vector3(18, 0, 12), Quaternion.identity);
        currentEnemies.Add(enemy);
        StartCoroutine(MultiStepWave());
    }

    void Wave10()//3 Wolves
    {
        GameObject enemy;
        enemy = Instantiate(enemyTypes[7], new Vector3(20, 0, 0), Quaternion.identity);
        currentEnemies.Add(enemy);
        enemy = Instantiate(enemyTypes[7], new Vector3(-20, 0, 12), Quaternion.identity);
        currentEnemies.Add(enemy);
        enemy = Instantiate(enemyTypes[7], new Vector3(-20, 0, -12), Quaternion.identity);
        currentEnemies.Add(enemy);
    }

    void Wave11()//1 Hive, 2 Rhino
    {
        GameObject enemy;
        enemyTimings.Add(1);
        enemy = Instantiate(enemyTypes[4], new Vector3(-20, 0, 8), Quaternion.identity);
        currentEnemies.Add(enemy);
        enemyTimings.Add(3);
        enemy = Instantiate(enemyTypes[0], new Vector3(-10, 0, -12), Quaternion.identity);
        currentEnemies.Add(enemy);
        enemyTimings.Add(0);
        enemy = Instantiate(enemyTypes[0], new Vector3(10, 0, -12), Quaternion.identity);
        currentEnemies.Add(enemy);
        StartCoroutine(MultiStepWave());
    }

    void Wave12() // 1 Sprayer 2 Falcons
    {
        GameObject enemy;
        enemy = Instantiate(enemyTypes[1], new Vector3(0, 0, 12), Quaternion.identity);
        enemy.GetComponent<Sprayer>().targetDistance = 8;
        currentEnemies.Add(enemy);
        enemy = Instantiate(enemyTypes[6], new Vector3(0, 0, 10), Quaternion.identity);
        enemy.GetComponent<Falcon>().InitializeSpawnPoint(2);
        currentEnemies.Add(enemy);
        enemy = Instantiate(enemyTypes[6], new Vector3(0, 0, 10), Quaternion.identity);
        enemy.GetComponent<Falcon>().InitializeSpawnPoint(3);
        currentEnemies.Add(enemy);
    }

    void Wave13() // 1 Sprayer 2 Sharks
    {
        GameObject enemy;
        enemyTimings.Add(1);
        enemy = Instantiate(enemyTypes[5], new Vector3(0, 0, -12), Quaternion.identity);
        currentEnemies.Add(enemy);
        enemyTimings.Add(.5f);
        enemy = Instantiate(enemyTypes[5], new Vector3(0, 0, -12), Quaternion.identity);
        currentEnemies.Add(enemy);
        enemyTimings.Add(1);
        enemy = Instantiate(enemyTypes[0], new Vector3(0, 0, 12), Quaternion.identity);
        currentEnemies.Add(enemy);
        StartCoroutine(MultiStepWave());
    }

    void Wave14()//2 Falcons 1 Wolf
    {
        GameObject enemy;
        enemy = Instantiate(enemyTypes[6], new Vector3(0, 0, 12), Quaternion.identity);
        enemy.GetComponent<Falcon>().InitializeSpawnPoint(1);
        currentEnemies.Add(enemy);
        enemy = Instantiate(enemyTypes[6], new Vector3(0, 0, 12), Quaternion.identity);
        enemy.GetComponent<Falcon>().InitializeSpawnPoint(0);
        currentEnemies.Add(enemy);
        enemy = Instantiate(enemyTypes[7], new Vector3(0, 0, -12), Quaternion.identity);
        currentEnemies.Add(enemy);
    }

    void Wave15()//4 Falcons - Cant get falcons to reliably show up at the right time on a delay, unity be like that sometimes
    {
        GameObject enemy;
        enemyTimings.Add(1);
        enemy = Instantiate(enemyTypes[6], new Vector3(0, 0, 12), Quaternion.identity);
        enemy.GetComponent<Falcon>().InitializeSpawnPoint(0);
        currentEnemies.Add(enemy);
        enemyTimings.Add(.1f);
        enemy = Instantiate(enemyTypes[6], new Vector3(0, 0, 12), Quaternion.identity);
        enemy.GetComponent<Falcon>().InitializeSpawnPoint(1);
        currentEnemies.Add(enemy);
        enemyTimings.Add(.1f);
        enemy = Instantiate(enemyTypes[6], new Vector3(0, 0, 12), Quaternion.identity);
        enemy.GetComponent<Falcon>().InitializeSpawnPoint(2);
        currentEnemies.Add(enemy);
        enemyTimings.Add(.1f);
        enemy = Instantiate(enemyTypes[6], new Vector3(0, 0, 12), Quaternion.identity);
        enemy.GetComponent<Falcon>().InitializeSpawnPoint(3);
        currentEnemies.Add(enemy);
    }

    void Wave16()//Shark, Burster, Wolf
    {
        GameObject enemy;
        enemy = Instantiate(enemyTypes[7], new Vector3(20, 0, 0), Quaternion.identity);
        currentEnemies.Add(enemy);
        enemy = Instantiate(enemyTypes[5], new Vector3(-20, 0, 0), Quaternion.identity);
        currentEnemies.Add(enemy);
        enemy = Instantiate(enemyTypes[3], new Vector3(0, 0, 12), Quaternion.identity);
        currentEnemies.Add(enemy);
    }

    void Wave17()//1 Bat 2 Hives
    {
        GameObject enemy;
        enemy = Instantiate(enemyTypes[4], new Vector3(20, 0, 8), Quaternion.identity);
        currentEnemies.Add(enemy);
        enemy = Instantiate(enemyTypes[4], new Vector3(-20, 0, 8), Quaternion.identity);
        currentEnemies.Add(enemy);
        enemy = Instantiate(enemyTypes[1], new Vector3(0, 0, -12), Quaternion.identity);
        enemy.GetComponent<Sprayer>().targetDistance = 8;
        currentEnemies.Add(enemy);
    }

    void Wave18()//2 Rhinos 2 falcons
    {
        GameObject enemy;
        enemy = Instantiate(enemyTypes[6], new Vector3(0, 0, 12), Quaternion.identity);
        enemy.GetComponent<Falcon>().InitializeSpawnPoint(0);
        currentEnemies.Add(enemy);
        enemy = Instantiate(enemyTypes[6], new Vector3(0, 0, 12), Quaternion.identity);
        enemy.GetComponent<Falcon>().InitializeSpawnPoint(3);
        currentEnemies.Add(enemy);
        enemy = Instantiate(enemyTypes[0], new Vector3(0, 0, 12), Quaternion.identity);
        currentEnemies.Add(enemy);
        enemy = Instantiate(enemyTypes[0], new Vector3(0, 0, -12), Quaternion.identity);
        currentEnemies.Add(enemy);
    }

    void Wave19() // 3 Bees 1 Shark
    {
        GameObject enemy;
        enemy = Instantiate(enemyTypes[8], new Vector3(0, 0, -12), Quaternion.identity);
        currentEnemies.Add(enemy);
        enemy = Instantiate(enemyTypes[8], new Vector3(20, 0, 0), Quaternion.identity);
        currentEnemies.Add(enemy);
        enemy = Instantiate(enemyTypes[8], new Vector3(-20, 0, 0), Quaternion.identity);
        currentEnemies.Add(enemy);
        enemy = Instantiate(enemyTypes[5], new Vector3(0, 0, 12), Quaternion.identity);
        currentEnemies.Add(enemy);
    }

    void Wave20()//2 Delayed Bees 1 Shark
    {
        GameObject enemy;
        enemyTimings.Add(0);
        enemy = Instantiate(enemyTypes[5], new Vector3(20, 0, -12), Quaternion.identity);
        currentEnemies.Add(enemy);
        enemyTimings.Add(1);
        enemy = Instantiate(enemyTypes[8], new Vector3(-15, 0, 12), Quaternion.identity);
        currentEnemies.Add(enemy);
        enemyTimings.Add(0);
        enemy = Instantiate(enemyTypes[8], new Vector3(-20, 0, 6), Quaternion.identity);
        currentEnemies.Add(enemy);
        StartCoroutine(MultiStepWave());
    }

    void Wave21()//2 Sprayers on left, 1 Rhino on right
    {
        GameObject enemy;
        enemy = Instantiate(enemyTypes[1], new Vector3(-20, 0, -8), Quaternion.identity);
        enemy.GetComponent<Sprayer>().targetDistance = 16;
        currentEnemies.Add(enemy);
        enemy = Instantiate(enemyTypes[1], new Vector3(-20, 0, 8), Quaternion.identity);
        enemy.GetComponent<Sprayer>().targetDistance = 16;
        currentEnemies.Add(enemy);
        enemy = Instantiate(enemyTypes[0], new Vector3(20, 0, 0), Quaternion.identity);
        currentEnemies.Add(enemy);
    }

    void Wave22()//2 horizontal Man O Wars, 1 Rhino on Right, delayed burster on left
    {
        GameObject enemy;
        enemyTimings.Add(1);
        enemy = Instantiate(enemyTypes[2], new Vector3(-20, 0, 8), Quaternion.identity);
        enemy.GetComponent<ManOWar>().SetTargetLocation(new Vector3(20, 0, 8));
        currentEnemies.Add(enemy);
        enemyTimings.Add(0);
        enemy = Instantiate(enemyTypes[2], new Vector3(-20, 0, -8), Quaternion.identity);
        enemy.GetComponent<ManOWar>().SetTargetLocation(new Vector3(20, 0, -8));
        currentEnemies.Add(enemy);
        enemyTimings.Add(0);
        enemy = Instantiate(enemyTypes[0], new Vector3(20, 0, 0), Quaternion.identity);
        currentEnemies.Add(enemy);
        enemyTimings.Add(4);
        enemy = Instantiate(enemyTypes[3], new Vector3(-20, 0, 0), Quaternion.identity);
        currentEnemies.Add(enemy);
        StartCoroutine(MultiStepWave());
    }

    void Wave23()//1 Hive, 2 Wolves
    {
        GameObject enemy;
        enemy = Instantiate(enemyTypes[4], new Vector3(0, 0, 12), Quaternion.identity);
        currentEnemies.Add(enemy);
        enemy = Instantiate(enemyTypes[7], new Vector3(-20, 0, -12), Quaternion.identity);
        currentEnemies.Add(enemy);
        enemy = Instantiate(enemyTypes[7], new Vector3(20, 0, -12), Quaternion.identity);
        currentEnemies.Add(enemy);
    }

    void Wave24()//1 Sprayer, 1 Rhino, 2 Bees
    {
        GameObject enemy;
        enemyTimings.Add(0);
        enemy = Instantiate(enemyTypes[1], new Vector3(-20, 0, 12), Quaternion.identity);
        enemy.GetComponent<Sprayer>().targetDistance = 16;
        currentEnemies.Add(enemy);
        enemyTimings.Add(0);
        enemy = Instantiate(enemyTypes[8], new Vector3(-17, 0, 12), Quaternion.identity);
        currentEnemies.Add(enemy);
        enemyTimings.Add(0);
        enemy = Instantiate(enemyTypes[0], new Vector3(0, 0, -12), Quaternion.identity);
        currentEnemies.Add(enemy);
        enemyTimings.Add(2);
        enemy = Instantiate(enemyTypes[8], new Vector3(-20, 0, 0), Quaternion.identity);
        currentEnemies.Add(enemy);
        StartCoroutine(MultiStepWave());
    }

    void Wave25()//Diagonal ManOWar, 2 Rhinos
    {
        GameObject enemy;
        enemy = Instantiate(enemyTypes[0], new Vector3(-20, 0, 12), Quaternion.identity);
        currentEnemies.Add(enemy);
        enemy = Instantiate(enemyTypes[0], new Vector3(20, 0, -12), Quaternion.identity);
        currentEnemies.Add(enemy);
        enemy = Instantiate(enemyTypes[2], new Vector3(20, 0, 12), Quaternion.identity);
        enemy.GetComponent<ManOWar>().SetTargetLocation(new Vector3(-20, 0, -12));
        currentEnemies.Add(enemy);
    }

    void Wave26()//1 Falcon 2 Bursters
    {
        GameObject enemy;
        enemy = Instantiate(enemyTypes[6], new Vector3(0, 0, 12), Quaternion.identity);
        enemy.GetComponent<Falcon>().InitializeSpawnPoint(2);
        currentEnemies.Add(enemy);
        enemy = Instantiate(enemyTypes[3], new Vector3(20, 0, 0), Quaternion.identity);
        currentEnemies.Add(enemy);
        enemy = Instantiate(enemyTypes[3], new Vector3(-20, 0, 0), Quaternion.identity);
        currentEnemies.Add(enemy);
    }

    void Wave27()//Sprayer above, 2 sharks from corners
    {
        GameObject enemy;
        enemy = Instantiate(enemyTypes[1], new Vector3(0, 0, 12), Quaternion.identity);
        enemy.GetComponent<Sprayer>().targetDistance = 8;
        currentEnemies.Add(enemy);
        enemy = Instantiate(enemyTypes[5], new Vector3(-20, 0, -12), Quaternion.identity);
        currentEnemies.Add(enemy);
        enemy = Instantiate(enemyTypes[5], new Vector3(20, 0, -12), Quaternion.identity);
        currentEnemies.Add(enemy);
    }

    void Wave28()//ManOWar, Rhino, Burster
    {
        GameObject enemy;
        enemy = Instantiate(enemyTypes[3], new Vector3(-20, 0, 12), Quaternion.identity);
        currentEnemies.Add(enemy);
        enemy = Instantiate(enemyTypes[0], new Vector3(-20, 0, -12), Quaternion.identity);
        currentEnemies.Add(enemy);
        enemy = Instantiate(enemyTypes[2], new Vector3(-20, 0, 0), Quaternion.identity);
        currentEnemies.Add(enemy);
    }

    void Wave29()//2 falcon, 2 rhinos
    {
        GameObject enemy;
        enemy = Instantiate(enemyTypes[0], new Vector3(-20, 0, -12), Quaternion.identity);
        currentEnemies.Add(enemy);
        enemy = Instantiate(enemyTypes[0], new Vector3(20, 0, -12), Quaternion.identity);
        currentEnemies.Add(enemy);
        enemy = Instantiate(enemyTypes[6], new Vector3(0, 0, 12), Quaternion.identity);
        enemy.GetComponent<Falcon>().InitializeSpawnPoint(0);
        currentEnemies.Add(enemy);
        enemy = Instantiate(enemyTypes[6], new Vector3(0, 0, 12), Quaternion.identity);
        enemy.GetComponent<Falcon>().InitializeSpawnPoint(1);
        currentEnemies.Add(enemy);
    }
    #endregion

    IEnumerator MultiStepWave()//Used for waves with multiple steps, ends on its own after the wave  
    {
        totalCount = 0;
        foreach (GameObject enemy in currentEnemies)//Disables all enemies to begin - this will not cause the wave to end as they are still in the list 
        {
            totalCount += 1;
            enemy.SetActive(false);
        }
        counter = 0;
        while (counter <= totalCount)
        {
            if(counter != 0)//The first enemy timing is for the time between the wave being called and the first enemy appearing, therefore nothing is spawned
            {
                if (currentEnemies[counter - 1] != null)
                {
                    currentEnemies[counter - 1].SetActive(true);//enables next enemy
                }
            }
            //print("WaveState: "+counter);
            if(counter == totalCount)//Prevents out of range error
            {
                counter++;
                yield return null;
                
            }
            else
            {
                yield return new WaitForSeconds(enemyTimings[counter]);
                counter++;
            }
           
        }
    }

    public bool CheckIfWaveComplete()//Checks whether or not the player has destroyed all enemies
    {
        if (currentEnemies.Count == 0)//if the list is empty, the wave is considered complete. When gameObjects are destroyed, they remain in the list as "null", but at the start of the game the list is empty, so this check prevents index out of range error
        {
            return (true);
        }
        else
        {
            bool listIsEmpty = true;//temporary variable used when it loops through the list
            for (int i = 0; i < currentEnemies.Count; i++)
            {
                if (currentEnemies[i] != null)//Destroyed gameObjects are null
                {
                    listIsEmpty = false;//Sets this to false if an enemy is still alive
                }
            }
            return (listIsEmpty);
        }
    }

    public void SkipWave()
    {
        foreach (GameObject enemy in currentEnemies)
        {
            Destroy(enemy);
        }
    }

    private void Awake()
    {
        DefineList();//Defines the list from the start
        enemyTypes = GetComponent<GameControl>().enemies;//Clones the list of enemies to save time defining waves
    }
}
