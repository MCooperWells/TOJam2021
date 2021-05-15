using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour


{

    public float speed, rotSpeed, height;
    Vector3 pos;


    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {

        float newY = Mathf.Sin(Time.time * speed) *height +pos.y;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        transform.Rotate(0, rotSpeed, 0);
    }
}
