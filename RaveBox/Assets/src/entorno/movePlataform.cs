using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlataform : MonoBehaviour
{
    // Start is called before the first frame update
    float xIini;
    void Start()
    {
        xIini = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
          transform.position = new Vector3( xIini+ Mathf.PingPong(Time.time*10, 30), transform.position.y, transform.position.z);
    }

}
