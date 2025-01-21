using Leap;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Optimization : MonoBehaviour
{
    [Header("Everything in the GameScene")]
    public GameObject[] t10;    
    public GameObject[] t20;
    public GameObject[] t30;
    public GameObject[] t40;
    public GameObject[] t50;
    public GameObject[] t60;
    public GameObject[] t70;
    public GameObject[] t80;
    public GameObject[] t90;
    public GameObject[] t100;
    public GameObject[] t110;

    [Header("Variables")]
    public float timeBeforeDestroy = 3f;

    void Start()
    {
        TurnOnThings(t10);
        TurnOnThings(t20);
        TurnOffThings(t30);
        TurnOffThings(t40);
        TurnOffThings(t50);
        TurnOffThings(t60);
        TurnOffThings(t70);
        TurnOffThings(t80);
        TurnOffThings(t90);
        TurnOffThings(t100);
        TurnOffThings(t110);
    }

    public void SelectObjectArray(int optCount)
    {
        if (optCount == 0)
        {
            TurnOnThings(t30);
            TurnOffThings(t10);
        }
        else if (optCount == 1)
        {
            TurnOnThings(t40);
            StartCoroutine(DestroyThings(t20));
        }
        else if (optCount == 2)
        {
            TurnOnThings(t50);
            StartCoroutine(DestroyThings(t30));
        }
        else if (optCount == 3)
        {
            TurnOnThings(t60);
            StartCoroutine(DestroyThings(t40));
        }
        else if (optCount == 4)
        {
            TurnOnThings(t70);
            StartCoroutine(DestroyThings(t50));
        }
        else if (optCount == 5)
        {
            TurnOnThings(t80);
            StartCoroutine(DestroyThings(t60));
        }
        else if (optCount == 6)
        {
            TurnOnThings(t90);
            StartCoroutine(DestroyThings(t70));
        }
        else if (optCount == 7)
        {
            TurnOnThings(t100);
            StartCoroutine(DestroyThings(t80));
        }
        else if (optCount == 8)
        {
            TurnOnThings(t110);
            StartCoroutine(DestroyThings(t90));
        }
    }

    public void TurnOnThings(GameObject[] objects)
    {
        for(int i = 0; i < objects.Length; i++)
        {
            if(objects[i] != null && !objects[i].activeInHierarchy)
            {
                objects[i].gameObject.SetActive(true);
            }
        }
    }

    public void TurnOffThings(GameObject[] objects)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i] != null && objects[i].activeInHierarchy)
            {
                objects[i].SetActive(false);
            }                
        }
    }
    IEnumerator DestroyThings(GameObject[] objects)
    {
        yield return new WaitForSeconds(timeBeforeDestroy);
        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i] != null && objects[i].activeInHierarchy)
            {
                Destroy(objects[i]);
            }
        }
    }
}
