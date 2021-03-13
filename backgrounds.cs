
  
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backgrounds : MonoBehaviour
{
    List<GameObject> backs = new List<GameObject>();
    List<float> scrollAmounts = new List<float>();
    List<float> bgheights = new List<float>();
    List<GameObject> possibleProps = new List<GameObject>();
    List<GameObject> props = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(ScrollBackground());
        StartCoroutine(SpawnProps());
    }

    public void SetBackground(string setting)
    {

       switch (setting)
       {
            case "space":
                GameObject spaceBack = new GameObject();
                spaceBack.name = "Space_BG";
                spaceBack.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/Backgrounds/Asset_BG_Nebula_Top");
                spaceBack.transform.localRotation *= Quaternion.Euler(90, 0, 0);
                spaceBack.GetComponent<SpriteRenderer>().rendererPriority = 0;
                createNewBackgrounds(spaceBack, Instantiate(spaceBack),spaceBack.GetComponent<SpriteRenderer>().bounds.size.z,4,-1f);
                spaceBack = new GameObject();
                spaceBack.name = "Space_BG";
                spaceBack.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/Backgrounds/Asset_BG_Nebula_Bottom");
                spaceBack.transform.localRotation *= Quaternion.Euler(90, 0, 0);
                spaceBack.GetComponent<SpriteRenderer>().rendererPriority = 1;
                createNewBackgrounds(spaceBack, Instantiate(spaceBack), spaceBack.GetComponent<SpriteRenderer>().bounds.size.z, 1.1f, -3);
                spaceBack = new GameObject();
                spaceBack.name = "Space_BG";
                spaceBack.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/Backgrounds/Asset_BG_Stars_Top");
                spaceBack.transform.localRotation *= Quaternion.Euler(90, 0, 0);
                spaceBack.transform.localScale = new Vector3(.8f, .8f, .8f);
                spaceBack.GetComponent<SpriteRenderer>().rendererPriority = 2;
                createNewBackgrounds(spaceBack, Instantiate(spaceBack), spaceBack.GetComponent<SpriteRenderer>().bounds.size.z, 3, -1f);
                spaceBack = new GameObject();
                spaceBack.name = "Space_BG";
                spaceBack.transform.localScale = new Vector3(1f, .8f, .8f);
                spaceBack.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/Backgrounds/Asset_BG_Stars_Bottom");
                spaceBack.transform.localRotation *= Quaternion.Euler(90, 0, 0);
                spaceBack.GetComponent<SpriteRenderer>().rendererPriority = 3;
                createNewBackgrounds(spaceBack, Instantiate(spaceBack), spaceBack.GetComponent<SpriteRenderer>().bounds.size.z, .8f, -4);
                possibleProps.Clear();
                possibleProps.Add(Resources.Load("Prefabs/planet") as GameObject);
                break;
            default:
                print("Invalid background setting.");
                break;
       }
    }

    void createNewBackgrounds(GameObject b1, GameObject b2, float newBgHeight, float newScrollAmount, float depth)//Automatically creates 2 to enable looping 
    {
        b1.transform.position = new Vector3(0, depth, 0);
        b2.transform.position = new Vector3(0, depth, newBgHeight);
        backs.Add(b1);
        scrollAmounts.Add(newScrollAmount);
        bgheights.Add(newBgHeight);
        backs.Add(b2);
        bgheights.Add(newBgHeight);
        scrollAmounts.Add(newScrollAmount);
    }

    IEnumerator ScrollBackground()
    {
        while (true)
        {
            for(int i = 0; i < backs.Count; i++)
            {
                if(backs[i] == null)
                {
                    backs.Remove(backs[i]);
                    scrollAmounts.Remove(scrollAmounts[i]);
                    bgheights.Remove(bgheights[i]);
                }
                else
                {

                    if (backs[i].transform.position.z <= -bgheights[i])
                    {
                        backs[i].transform.position += new Vector3(0, 0, bgheights[i] - -bgheights[i]);
                        //print("ok");
                    }
                    backs[i].transform.position += new Vector3(0, 0, -scrollAmounts[i] * Time.deltaTime);
                }
            }
            for(int i = 0; i < props.Count; i++)
            {
                props[i].transform.Translate(0, -.6f*Time.deltaTime, 0);
            }
            yield return null;
        }
    }

    IEnumerator SpawnProps()
    {
        while (true)
        {
            float level = Random.Range(-3, -1);
            GameObject newProp = GameObject.Instantiate(possibleProps[Random.Range(0, possibleProps.Count)]);
            newProp.transform.position = new Vector3(Random.Range(-6,6), level, bgheights[0] + 10);
            props.Add(newProp);
            yield return new WaitForSeconds(30);
        }
    }
}
