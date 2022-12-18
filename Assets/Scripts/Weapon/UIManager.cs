using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Actor player;
    public AutomaticGunScriptLPFP weapon;
    public HandgunScriptLPFP pistol;
    public BossScript boss;
    public BossScript boss2;
    [SerializeField] GameObject victoryCanvas;
    [SerializeField] GameObject defeatCanvas;
    [SerializeField] Image lifebar;
    [SerializeField] Image lavaBar;
    [SerializeField] CheckDistance checkDistance;
    [SerializeField] Text ammoText;
    void Start()
    {
        Time.timeScale = 1f;
        player = GameObject.Find("Player").GetComponent<Actor>();
        weapon = GameObject.Find("Player").GetComponentInChildren<AutomaticGunScriptLPFP>();

        //pistol = GameObject.Find("Player").GetComponentInChildren<HandgunScriptLPFP>();
    }

    // Update is called once per frame
    void Update()
    {
        lifebar.fillAmount = player.CurrentLife / 100f;
        lavaBar.fillAmount = 1 - (checkDistance._distance / 100);
        if (weapon != null && weapon.gameObject.activeInHierarchy )
            ammoText.text = $"{weapon.CurrentAmmo}/{weapon.ammo}";
        else if (pistol != null && pistol.gameObject.activeInHierarchy )
            ammoText.text = $"{pistol.CurrentAmmo}/{pistol.ammo}";

        if (SceneManager.GetActiveScene().name == "boss")
        if (boss2 == null) {
            if (boss == null) return;
            if (boss.finishedDeathAnim)
            {
                SceneManager.LoadScene("Escape");
                //Time.timeScale = 0f;
                //Cursor.lockState = CursorLockMode.None;
            }
           
        }else if (boss2 != null)
        {
            if (boss.finishedDeathAnim && boss2.finishedDeathAnim)
            {
                victoryCanvas.SetActive(true);
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
            }
        }
         if (player.CurrentLife <= 0f)
        {
            defeatCanvas.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
