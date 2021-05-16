using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnceMore : MonoBehaviour
{
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
    }

    private void OnEnable()
    {
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > 2f)
        {
            this.gameObject.SetActive(false);
        }    
    }
}
