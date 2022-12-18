using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class transitionLevel : MonoBehaviour
{
    public Animator anim;
    public int levelToFade;

    //Tutorial Level1
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FadeToLevel(1);
        }
    }

    private void FadeToLevel(int levelIndex)
    {
        anim.SetTrigger("FadeOut");
        levelToFade = levelIndex;
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToFade);
    }
}
