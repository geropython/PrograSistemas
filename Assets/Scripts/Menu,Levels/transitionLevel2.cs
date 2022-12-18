using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class transitionLevel2 : MonoBehaviour
{
    public Animator anim;
    public int levelToFade;
    
    //Tutorial Level2
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FadeToLevel(2); 
        }
    }

    private void FadeToLevel(int levelIndex)
    {
        anim.SetTrigger("FadeOut");
        Invoke(nameof(OnFadeComplete), 2);
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToFade);
    }
}
