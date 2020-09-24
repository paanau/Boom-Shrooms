using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomScript : MonoBehaviour
{
    private float downtick;
    private bool tickdown;
    public ParticleSystem parSys;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BurstIntoTreats()
    {

        transform.localScale *= 0.1f;
        parSys.Play();
    }
}
