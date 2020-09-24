using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MushroomSpawn : MonoBehaviour
{
    float originalScale;
    float startTimer = 0f;
    public Transform[] spawnEffects;
    bool spawned = false;
    // Start is called before the first frame update
    void Start()
    {
        originalScale = this.GetComponent<Transform>().localScale.x;

        this.GetComponent<Transform>().localScale = new Vector3(0.01f, 0.01f, 0.01f);

        GetComponent<SpriteRenderer>().enabled = false;
        startTimer = Random.Range(0.1f, 1f);
        spawnEffects[0].GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = this.GetComponent<Renderer>().sortingOrder + 1;  // Front effect
        spawnEffects[1].GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = this.GetComponent<Renderer>().sortingOrder - 1;  // Back effect
    }

    // Update is called once per frame
    void Update()
    {

            startTimer -= Time.deltaTime;
        
        if (startTimer < 0 && !spawned)
        {
            spawned = true;
            GetComponent<SpriteRenderer>().enabled = true;

            SpawnMushroom(Random.Range(0.8f, 1.4f));


        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            SpawnMushroom(Random.Range(0.8f, 1.4f));
        }
    }

    public void SpawnMushroom(float finalScale = 1.0f)
    {
        StartCoroutine(PlayParticles(.1f));

        Sequence verticalMove = DOTween.Sequence();

        verticalMove.Append(this.GetComponent<Transform>().DOLocalMoveY(transform.localPosition.y - 1f, 0.01f));
        verticalMove.Append(this.GetComponent<Transform>().DOLocalMoveY(transform.localPosition.y + .5f, 2.2f));

        Sequence widthSequence = DOTween.Sequence();
        Sequence heightSequence = DOTween.Sequence();
        // Assume that the size that the 
        float originalScale = this.GetComponent<Transform>().localScale.x;
        // Make sure it starts small
        // Random time before the mushroom starts growing 
        //  widthSequence.Append(this.GetComponent<Transform>().DOScaleX(0.01f, Random.Range(0.1f, .2f)));


        // Make the mushroom grow up
        heightSequence.Append(this.GetComponent<Transform>().DOScale(finalScale / 100, .1f));

        heightSequence.Append(this.GetComponent<Transform>().DOScale(finalScale / 4, .3f));

        heightSequence.Append(this.GetComponent<Transform>().DOScale(finalScale * Random.Range(1.2f, 1.4f), .6f));
        heightSequence.Append(this.GetComponent<Transform>().DOScale(finalScale / 3, .4f));
        //heightSequence.Append(this.GetComponent<Transform>().DOScale(finalScale / 4, .4f));

        heightSequence.Append(this.GetComponent<Transform>().DOScale(finalScale, .5f));

        // Grow in a pulse-like manner, always heading towards the target scale
        float shrinkTime = Random.Range(.10f, .20f);
        float xGrowTime = Random.Range(.3f, .5f);

        //widthSequence.Append(this.GetComponent<Transform>().DOScaleX(originalScale / 4, .1f));

        //widthSequence.Append(this.GetComponent<Transform>().DOScaleX(originalScale * .8f, xGrowTime));
        //widthSequence.Append(this.GetComponent<Transform>().DOScaleX(originalScale / 4, shrinkTime));

        //widthSequence.Append(this.GetComponent<Transform>().DOScaleX(originalScale * 1.2f, xGrowTime));
        //widthSequence.Append(this.GetComponent<Transform>().DOScaleX(originalScale / 2, shrinkTime));

        //widthSequence.Append(this.GetComponent<Transform>().DOScaleX(originalScale * Random.Range(1.5f, 2f), xGrowTime + .2f));

        //// Final Scale
        //widthSequence.Append(this.GetComponent<Transform>().DOScaleX(finalScale, shrinkTime));
        
    }

    private IEnumerator PlayParticles(float delay)
    {
        yield return new WaitForSeconds(delay);
        // Play dust effects
        foreach (Transform effect in spawnEffects)
        {
            effect.GetComponent<ParticleSystem>().Play();
        }
    }
}
