using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spin : MonoBehaviour
{
    // Start is called before the first frame update
    public float spinSpeed;
    public bool shouldSpin = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldSpin)
        {
            transform.Rotate(0, spinSpeed * Time.deltaTime, 0);
        }
    }
}
