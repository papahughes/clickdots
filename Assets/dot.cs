using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dot : MonoBehaviour
{
    public Color[] colors;
    private SpriteRenderer spriteRenderer;

    //sizes
    private int size = 0;

    //point values
    [HideInInspector] public int pointValue;

    //scale
    private Vector3 scale = Vector3.zero;

    //life
    private float timeBeforeShrink = 0.0f;

    //audio
    [HideInInspector] public AudioSource audioSource;

    void Start()
    {
        //audio
        audioSource = GetComponent<AudioSource>();

        //set colors
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = colors[Random.Range(0, colors.Length - 1)];

        //size setting
        size = Random.Range(0, 3);

        switch(size)
        {
            case 0: //small
                scale = transform.localScale *= 0.7f;
                pointValue = 20;
                timeBeforeShrink = 1f;
                audioSource.pitch = Random.Range(1.3f, 1.5f);
                break;

            case 1: //medium
                scale = transform.localScale *= 1.4f;
                pointValue = 10;
                timeBeforeShrink = 1.5f;
                audioSource.pitch = Random.Range(0.9f, 1.1f);
                break;

            case 2: //large
                scale = transform.localScale *= 2.1f;
                pointValue = 5;
                timeBeforeShrink = 2f;
                audioSource.pitch = Random.Range(0.5f, 0.7f);
                break;
        }

        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        //grow/shrink animation
        transform.localScale = Vector3.MoveTowards(transform.localScale, scale, 3.5f * Time.deltaTime);

        //shrinking
        timeBeforeShrink -= Time.deltaTime;
        if(timeBeforeShrink <= 0)
        {
            scale = Vector3.zero;
        }

        //destroy once shrunk
        if(transform.localScale == Vector3.zero)
        {
            Destroy(this.gameObject);
        }
    }
}
