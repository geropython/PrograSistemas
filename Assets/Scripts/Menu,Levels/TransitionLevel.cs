using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionLevel : MonoBehaviour
{
    public Animator anim;
    public int levelToFade;

    //Tutorial Level2

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colisiono");

            FadeToLevel(2);
        
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
