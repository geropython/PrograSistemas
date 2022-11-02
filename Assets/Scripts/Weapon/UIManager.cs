using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Actor player;
    public AutomaticGunScriptLPFP weapon;
    public HandgunScriptLPFP pistol;
    public ShotGunScript shotgun;
    public BossScript boss;
    public BossScript boss2;
    [SerializeField] GameObject victoryCanvas;
    [SerializeField] GameObject defeatCanvas;
    [SerializeField] Image lifebar;
    [SerializeField] Text ammoText;
    void Start()
    {
        Time.timeScale = 1f;
        player = GameObject.Find("Player").GetComponent<Actor>();
        weapon = GameObject.Find("Player").GetComponentInChildren<AutomaticGunScriptLPFP>();
        pistol = GameObject.Find("Player").GetComponentInChildren<HandgunScriptLPFP>();
        shotgun = GameObject.Find("Player").GetComponentInChildren<ShotGunScript>();
    }

    void Update()
    {
        lifebar.fillAmount = player.CurrentLife / 100f;
        if (weapon != null && weapon.gameObject.activeInHierarchy )
            ammoText.text = $"{weapon.CurrentAmmo}/{weapon.ammo}";
        if (pistol != null && pistol.gameObject.activeInHierarchy )
            ammoText.text = $"{pistol.CurrentAmmo}/{pistol.ammo}";
        else if (shotgun != null && shotgun.gameObject.activeInHierarchy)
            ammoText.text = $"{shotgun.CurrentAmmo}/{shotgun.ammo}";

        if (boss2 == null) {
            if (boss.finishedDeathAnim)
            {
                victoryCanvas.SetActive(true);
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
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
