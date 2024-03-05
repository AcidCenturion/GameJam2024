using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    [SerializeField] private AudioSource intro;
    [SerializeField] private AudioSource explor;
    [SerializeField] private AudioSource unintended;
    [SerializeField] private AudioSource unintendedFin;
    [SerializeField] private AudioSource fin;
    [SerializeField] private AudioSource altFin;

    bool hasPlayedIntro = false;
    bool hasPlayedExplor = false;
    bool hasPlayedUnin = false;
    bool hasPlayedFin = false;
    bool hasPlayedAltFin = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Intro" && !hasPlayedIntro)
        {
            intro.Play();
            hasPlayedIntro = true;
        }
        if(collision.gameObject.tag == "Exploration" && !hasPlayedExplor)
        {
            explor.Play();
            hasPlayedExplor = true;
        }
        if(collision.gameObject.tag == "UninPath" && !hasPlayedUnin)
        {
            unintended.Play();
            hasPlayedUnin = true;
        }
        // end reached, not played vo yet, reached unintended path
        if(collision.gameObject.tag == "End" && !hasPlayedFin && hasPlayedUnin)
        {
            unintendedFin.Play();
            hasPlayedFin = true;
        }
        // end reached, not played vo yet, took intended path
        if(collision.gameObject.tag == "End" && !hasPlayedFin && !hasPlayedUnin)
        {
            fin.Play();
            hasPlayedFin = true;
        }
        if(collision.gameObject.tag == "OtherEnd" && !hasPlayedAltFin)
        {
            altFin.Play();
            hasPlayedAltFin = true;
        }
    }
}
