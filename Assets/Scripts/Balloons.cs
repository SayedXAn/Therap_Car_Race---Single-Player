using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloons : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody body;
    public float speed = 5;
    private void Update()
    {
        if (transform.position.y > 200f)
        {
            transform.position = new Vector3(transform.position.x, 10f, transform.position.z);
        }
        else
        {
            body.velocity = Vector3.up * Time.deltaTime * speed;
        }
            
    }

}
