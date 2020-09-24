using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphexItUp : MonoBehaviour
{
    public int sizeX, sizeY;
    public GameObject shroom, sphex;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                int offset = i % 2;
                
                GameObject go = Instantiate(sphex, new Vector3(i * 1.5f, j * 1.75f + offset * 0.875f, 0), Quaternion.identity);
                go.transform.Rotate(new Vector3(0, 0, 30));
            }
        }
    }
    // +-0.2, +-0.2, -0.5
    // Update is called once per frame
    void Update()
    {
        
    }
}
