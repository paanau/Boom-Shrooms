using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MushroomSpawn : MonoBehaviour
{
    public AudioClip[] growSounds;

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
        startTimer = Random.Range(0.1f, 1.3f);
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
        StartCoroutine(PlaySound(.7f));

        Sequence verticalMove = DOTween.Sequence();

        verticalMove.Append(this.GetComponent<Transform>().DOLocalMoveY(transform.localPosition.y + .25f, 2.7f));

        Sequence widthSequence = DOTween.Sequence();
        Sequence heightSequence = DOTween.Sequence();

        // Delay the start a bit so that the particles can roam for a while
        heightSequence.AppendInterval(.7f);
        
                // Make the mushroom grow up
        heightSequence.Append(this.GetComponent<Transform>().DOScale(finalScale / 100, .1f));
        heightSequence.Append(this.GetComponent<Transform>().DOScale(finalScale / 4, .2f));

        heightSequence.Append(this.GetComponent<Transform>().DOScale(finalScale * Random.Range(1.2f, 1.4f), .6f));

        heightSequence.Append(this.GetComponent<Transform>().DOScale(finalScale / 1.5f, .4f));
        heightSequence.Append(this.GetComponent<Transform>().DOScale(finalScale, .35f));

        // Grow in a pulse-like manner, always heading towards the target scale
        float shrinkTime = Random.Range(.10f, .20f);
        float xGrowTime = Random.Range(.3f, .5f);
       
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

    private IEnumerator PlaySound(float delay)
    {
        yield return new WaitForSeconds(delay);
        // Play sound
        
        GetComponent<AudioSource>().clip = growSounds[Random.Range(0, growSounds.Length)];
        GetComponent<AudioSource>().Play();
    }
}
