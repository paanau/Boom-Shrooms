using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphexyScript : MonoBehaviour
{
    public int shroomCount = 0, myShroomCap, sporeCount = 0;
    public GameObject shroomObject, myHex;
    public ParticleSystem mySpores;
    private List<GameObject> myShrooms, myNeighbours;
    private Color myMatColor;
    private Renderer myRend;
    private bool fade, spored;
    private float materialTicker, fadeMulti, sporeRange = 2f;

    // Start is called before the first frame update
    void Start()
    {
        myRend = myHex.GetComponent<Renderer>();
        myMatColor = myRend.material.color;
        myShrooms = new List<GameObject>();
        RangeCheck();
        materialTicker = 0.01f;
    }

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fade)
        {
            Fade();
            if (materialTicker <= 0)
            {
                fadeMulti = 1f;
                materialTicker = 0;
                fade = false;
            }
        }
    }

    private void OnMouseDown()
    {
        materialTicker += 0.1f;
        GrowShproom();
    }

    private void OnMouseDrag()
    {
        GrowShproom();
    }

    private void OnMouseUp()
    {
        ReleaseEnergy();
    }

    private void GrowShproom()
    {
        fade = true;

        if (materialTicker > 0.6f)
        {
            NewShroom();
            ReleaseEnergy();
        }
    }

    private void ReleaseEnergy()
    {
        fadeMulti = -2f;
        fade = true;
    }

    private void Fade()
    {
        materialTicker += fadeMulti * Time.deltaTime;
        myRend.material.color = new Color(myMatColor.r, myMatColor.g, myMatColor.b, (1f - materialTicker));
    }

    private void RangeCheck()
    {
        myNeighbours = new List<GameObject>();
        GameObject[] otherSphexes = GameObject.FindGameObjectsWithTag("Sphex");
        foreach (GameObject otherSphex in otherSphexes)
        {
            if (Vector3.Distance(transform.position, otherSphex.transform.position) > 0 & Vector3.Distance(transform.position, otherSphex.transform.position) < sporeRange)
            {
                myNeighbours.Add(otherSphex);
            }
        }

        myShroomCap = myNeighbours.Count;
    }

    private void NewShroom()
    {
        GameObject go = Instantiate(shroomObject, new Vector3(transform.position.x + Random.Range(-0.2f, 0.2f), transform.position.y + Random.Range(-0.2f, 0.2f), -0.5f), Quaternion.identity);
        myShrooms.Add(go);
        shroomCount++;
        if (shroomCount >= myShroomCap)
        {
            StartCoroutine(WaitASec(0.2f));
        }

    }

    public void NewShroom(GameObject tranShroom, int t)
    {
        myShrooms.Add(tranShroom);
        shroomCount++;
        if (shroomCount >= myShroomCap)
        {
            //StartCoroutine(WaitASec(1f * t));
            MakeSomeSpores();
        }
        
    }

    private void SporePower()
    {

    }

    private IEnumerator WaitASec(float t)
    {
        yield return new WaitForSeconds(t);
        MakeSomeSpores();
    }

    private void MakeSomeSpores()
    {
        int shrooming = 0;
        
        foreach (GameObject goo in myShrooms)
        {
            //mySpores.Play();
            Debug.Log(myNeighbours.Count);
            goo.transform.position = myNeighbours[shrooming].transform.position + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), -0.5f);
            myNeighbours[shrooming].GetComponent<SphexyScript>().NewShroom(goo, shrooming);
            shrooming++;
            if (shrooming == myNeighbours.Count)
            {
                break;
            }
            
        }
        shroomCount -= shrooming;
        myShrooms.RemoveRange(0, shrooming);
        spored = true;
        if (shroomCount >= myShroomCap)
        {
            MakeSomeSpores();
        }
    }
}
