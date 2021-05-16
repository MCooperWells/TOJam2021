using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerParticles : MonoBehaviour
{

    public GameObject ps;
    public Transform pos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player"){
            PlayPS(); 

        }

    }

    void PlayPS()
    {
        GameObject particle = Instantiate(ps, pos.position, Quaternion.identity);
        particle.GetComponent<ParticleSystem>().Play(); 
    }
}
