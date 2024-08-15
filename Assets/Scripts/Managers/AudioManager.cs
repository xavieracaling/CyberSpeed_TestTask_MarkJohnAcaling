using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource GameWin;
    public AudioClip PageFlip;
    public AudioClip Matched;
    public AudioClip NotMatched;
    public static AudioManager Instance;
    private void Awake() {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    
}
