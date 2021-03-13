using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WavesSequence : MonoBehaviour
{
    Text alertText; //Used to display the wave counter between waves as well as game over and win
    List<int> sequence = new List<int>();//list of wave IDs to be called
    Waves wavesScript;//Waves script component on gameObject
    int waveCounter = 0;

    public void startWaveSequence(int seqID)//called from GameControl to determine what order waves should appear in
    {
        alertText = GameObject.FindGameObjectWithTag("alert").GetComponent<Text>();
        wavesScript = gameObject.AddComponent<Waves>();
        switch (seqID)
        {
            case 0://Case 0 used for score attack, all else used for defined sequences of waves
                StartCoroutine(randomWaves()); 
                break;
            case 1:
                sequence.Add(4);
                StartCoroutine(waveSequence(true));
                break;
            case 2:
                for(int i = 1; i < wavesScript.wavesCount; i++)
                {
                    sequence.Add(i);
                }
                sequence.Add(0);
                StartCoroutine(waveSequence(true));
                break;
            default:
                print("Sequence not found");
                break;
        }
    }

    IEnumerator waveSequence(bool showText)//Used for defined waves
    {
        wavesScript.SpawnWave(sequence[waveCounter]);//first wave
        if (showText)
        {
            alertText.text = "Wave 1";
            StartCoroutine(clearAlertOnDelay(3));
        }
        waveCounter = 0;
        while (waveCounter <= sequence.Count && GameControl.alive == true)
        {
            if (wavesScript.CheckIfWaveComplete())
            {
                waveCounter++;
            }
            if (waveCounter == sequence.Count)//If the last wave ended
            {
                alertText.color = Color.green;
                alertText.text = "You Win!";//Displays victory message
                alertText.transform.position = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0); //Centers the win text
                StartCoroutine(endGameOnDelay(3));
                waveCounter++;//Ends coroutine and prevents infinite loop
            }
            else
            {
                if (wavesScript.CheckIfWaveComplete())
                {
                    StartCoroutine(nextWaveOnDelay(2));
                    if (showText)
                    {
                        alertText.text = "Wave " + (waveCounter + 1);
                        StartCoroutine(clearAlertOnDelay(2));
                    }
                    yield return new WaitForSeconds(3);
                }
                else
                {
                    yield return new WaitForSeconds(2);//Checks if the wave is complete every 4 seconds
                }
            }
        }
    }

    IEnumerator randomWaves()//use this for score attack, just generates random waves
    {
        while (true)
        {
            if (wavesScript.CheckIfWaveComplete())
            {
                wavesScript.SpawnWave(Random.Range(0,wavesScript.wavesCount));
            }
            yield return new WaitForSeconds(4);
        }
    }

    IEnumerator clearAlertOnDelay(float delay)//Hides the alert text after a delay
    {
        yield return new WaitForSeconds(delay);
        alertText.text = "";
    }

    IEnumerator endGameOnDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneController.ReturnToMenu();
    }

    IEnumerator nextWaveOnDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        wavesScript.SpawnWave(sequence[waveCounter]);//if the wave is over, spawns the next wave in the sequence
    }
}
