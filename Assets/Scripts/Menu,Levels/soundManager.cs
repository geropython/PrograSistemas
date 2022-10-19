using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour
{
    public static AudioClip FireSound, ReloadSound, BoomSound, rpgSound, piuSound, tutorialSound1, tutorialSound2, tutorialSound3, dashSound;
    static AudioSource audioSrc;
    void Start()
    {
        FireSound = Resources.Load <AudioClip> ("fire");
        ReloadSound = Resources.Load<AudioClip> ("reload");
        BoomSound = Resources.Load<AudioClip>("boom");
        rpgSound = Resources.Load<AudioClip>("kick");
        piuSound = Resources.Load<AudioClip>("piu");
        tutorialSound1 = Resources.Load<AudioClip>("tutorial1");
        tutorialSound2 = Resources.Load<AudioClip>("tutorial2");
        tutorialSound3 = Resources.Load<AudioClip>("tutorial3");
        dashSound = Resources.Load<AudioClip>("dash");

        audioSrc = GetComponent<AudioSource>();
    }

    public static void PlaySound (string clip)
    {
        switch (clip) {
            case "fire":
                audioSrc.PlayOneShot(FireSound);
                break;
            case "reload":
                audioSrc.PlayOneShot(ReloadSound);
                break;
            case "boom":
                audioSrc.PlayOneShot(BoomSound);
                break;
            case "kick":
                audioSrc.PlayOneShot(rpgSound);
                break;
            case "piu":
                audioSrc.PlayOneShot(piuSound);
                break;
            case "tutorial1":
                audioSrc.PlayOneShot(tutorialSound1);
                break;
            case "tutorial2":
                audioSrc.PlayOneShot(tutorialSound2);
                break;
            case "tutorial3":
                audioSrc.PlayOneShot(tutorialSound3);
                break;
            case "dash":
                audioSrc.PlayOneShot(dashSound);
                break;
        }
    }
}
