using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDelay : MonoBehaviour
{
    // References
    private AudioSource sound;
    private Vector3 player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera").transform.position;
        sound = GetComponent<AudioSource>();
        AudioLowPassFilter LPF = GetComponent<AudioLowPassFilter>();
        LPF.cutoffFrequency = 5000 * (1500 / Vector3.Distance(transform.position, player));
        sound.PlayDelayed(Vector3.Distance(gameObject.transform.position, player) / 343f);
    }
}