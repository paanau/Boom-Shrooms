using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SporeCloudScript : MonoBehaviour
{
    private float lifetime;
    // Start is called before the first frame update
    void Start()
    {
        lifetime = GetComponent<ParticleSystem>().time;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
