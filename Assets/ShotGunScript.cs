using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShotGunScript : MonoBehaviour
{
	public RaycastHit rayHit;
	public int damage;
	public float spread, range, reloadTime, timeBetweenShots;
	public LayerMask whatIsEnemy;
	public GameObject crosshair;
	public float cooldownTime = 2;
	Animator anim;

	[Header("Gun Camera")]
	public Camera gunCamera;

	[Header("Gun Camera Options")]
	//How fast the camera field of view changes when aiming 
	[Tooltip("How fast the camera field of view changes when aiming.")]
	public float fovSpeed = 15.0f;
	//Default camera field of view
	[Tooltip("Default value for camera field of view (40 is recommended).")]
	public float defaultFov = 40.0f;
	public float aimFov = 15.0f;

	[Header("UI Weapon Name")]
	[Tooltip("Name of the current weapon, shown in the game UI.")]
	public string weaponName;
	private string storedWeaponName;

	[Header("Weapon Sway")]
	[Tooltip("Toggle weapon sway.")]
	public bool weaponSway;
	public float swayAmount = 0.02f;
	public float maxSwayAmount = 0.06f;
	public float swaySmoothValue = 4.0f;
	private Vector3 initialSwayPosition;
	[Header("Weapon Settings")]
	public float sliderBackTimer = 1.58f;
	private bool hasStartedSliderBack;

	[Tooltip("Enables auto reloading when out of ammo.")]
	public bool autoReload;
	public float autoReloadDelay;
	private bool isReloading;
	private bool hasBeenHolstered = false;
	private bool holstered;
	private bool isRunning;
	private bool isAiming;
	private bool isWalking;
	private bool isInspecting;
	private int currentAmmo;

	public int CurrentAmmo => currentAmmo;
	[Tooltip("How much ammo the weapon should have.")]
	public int ammo;
	private bool outOfAmmo;

	[Header("Bullet Settings")]
	[Tooltip("How much force is applied to the bullet when shooting.")]
	public float bulletForce = 400;
	[Tooltip("How long after reloading that the bullet model becomes visible " +
		"again, only used for out of ammo reload aniamtions.")]
	public float showBulletInMagDelay = 0.6f;
	[Tooltip("The bullet model inside the mag, not used for all weapons.")]
	public SkinnedMeshRenderer bulletInMagRenderer;

	[Header("Grenade Settings")]
	public float grenadeSpawnDelay = 0.35f;
	[Header("Muzzleflash Settings")]
	public spawnpoints Spawnpoints;

	[Range(2, 25)]
	public int maxRandomValue = 5;
	public ParticleSystem muzzleParticles;
	public bool enableSparks = true;
	public ParticleSystem sparkParticles;
	public int minSparkEmission = 1;
	public int maxSparkEmission = 7;

	[Header("Muzzleflash Light Settings")]
	public float lightDuration = 0.02f;
	[Header("Audio Source")]
	public AudioSource mainAudioSource;
	public AudioSource shootAudioSource;
	[Header("UI Components")]
	public Text timescaleText;
	public Text currentWeaponText;
	public Text currentAmmoText;
	public Text totalAmmoText;

	[System.Serializable]
	public class prefabs
	{
		[Header("Prefabs")]
		public Transform bulletPrefab;
		public Transform casingPrefab;
		public Transform grenadePrefab;
	}
	public prefabs Prefabs;
	[System.Serializable]
	public class spawnpoints
	{
		[Header("Spawnpoints")]
		public Transform casingSpawnPoint;
		public Transform bulletSpawnPoint;
		public Transform grenadeSpawnPoint;
	}

	private void Awake()
	{
		anim = GetComponent<Animator>();
		currentAmmo = ammo;
	}

	private void Start()
	{
		storedWeaponName = weaponName;
		currentWeaponText.text = weaponName;
		totalAmmoText.text = ammo.ToString();
		initialSwayPosition = transform.localPosition;
	}

	private void LateUpdate()
	{
		//Weapon sway
		if (weaponSway == true)
		{
			float movementX = -Input.GetAxis("Mouse X") * swayAmount;
			float movementY = -Input.GetAxis("Mouse Y") * swayAmount;
			//Clamp movement to min and max values
			movementX = Mathf.Clamp
				(movementX, -maxSwayAmount, maxSwayAmount);
			movementY = Mathf.Clamp
				(movementY, -maxSwayAmount, maxSwayAmount);
			//Lerp local pos
			Vector3 finalSwayPosition = new Vector3
				(movementX, movementY, 0);
			transform.localPosition = Vector3.Lerp
				(transform.localPosition, finalSwayPosition +
				initialSwayPosition, Time.deltaTime * swaySmoothValue);
		}
	}

	private void Update()
	{
		if (Input.GetButton("Fire2") && !isReloading && !isRunning && !isInspecting)
		{
			gunCamera.fieldOfView = Mathf.Lerp(gunCamera.fieldOfView,
				aimFov, fovSpeed * Time.deltaTime);

			isAiming = true;
			anim.SetBool("Aim", true);
		}
		else
		{
			//When right click is released
			gunCamera.fieldOfView = Mathf.Lerp(gunCamera.fieldOfView,
				defaultFov, fovSpeed * Time.deltaTime);
			isAiming = false;
			anim.SetBool("Aim", false);
		}

		if (Input.GetKeyDown(KeyCode.ScrollLock))
		{
			Time.timeScale = 1.0f;
			timescaleText.text = "1.0";
		}
		if (Input.GetKeyDown(KeyCode.ScrollLock))
		{
			Time.timeScale = 0.5f;
			timescaleText.text = "0.5";
		}

		if (Input.GetKeyDown(KeyCode.ScrollLock))
		{
			Time.timeScale = 0.25f;
			timescaleText.text = "0.25";
		}

		if (Input.GetKeyDown(KeyCode.ScrollLock))
		{
			Time.timeScale = 0.1f;
			timescaleText.text = "0.1";
		}
		//Pause game when 5 key is pressed
		if (Input.GetKeyDown(KeyCode.ScrollLock))
		{
			Time.timeScale = 0.0f;
			timescaleText.text = "0.0";
		}

		currentAmmoText.text = currentAmmo.ToString();
		AnimationCheck();

		//Play knife attack 1 animation when Q key is pressed
		if (Input.GetKeyDown(KeyCode.Q) && !isInspecting)
		{
			anim.Play("Knife Attack 1", 0, 0f);
		}
		//Play knife attack 2 animation when F key is pressed
		if (Input.GetKeyDown(KeyCode.F) && !isInspecting)
		{
			anim.Play("Knife Attack 2", 0, 0f);
		}

		//Throw grenade when pressing G key
		if (Input.GetKeyDown(KeyCode.G) && !isInspecting)
		{
			StartCoroutine(GrenadeSpawnDelay());
			//Play grenade throw animation
			anim.Play("GrenadeThrow", 0, 0.0f);
		}

		//If out of ammo
		if (currentAmmo <= 0)
		{
			currentWeaponText.text = "OUT OF AMMO";
			outOfAmmo = true;
			if (autoReload == true && !isReloading)
			{
				StartCoroutine(AutoReload());
			}
			anim.SetBool("Out Of Ammo Slider", true);
			anim.SetLayerWeight(1, 1.0f);
		}
		else
		{
			//When ammo is full, show weapon name again
			currentWeaponText.text = storedWeaponName.ToString();
			outOfAmmo = false;
			anim.SetLayerWeight(1, 0.0f);
		}

		//Shooting 
		if (Input.GetMouseButtonDown(0) && !outOfAmmo && !isReloading && !isInspecting && !isRunning)
		{
			anim.Play("Fire", 0, 0f);

			muzzleParticles.Emit(1);

			//Remove 1 bullet from ammo
			currentAmmo -= 1;
			shootAudioSource.Play();

			if (!isAiming) //if not aiming
			{
				anim.Play("Fire", 0, 0f);

				if (enableSparks == true)
				{
					sparkParticles.Emit(Random.Range(1, 6));
				}
			}
			else //if aiming
			{
				anim.Play("Aim Fire", 0, 0f);
			}
			//shoot spread
			float x = Random.Range(-spread, spread);
			float y = Random.Range(-spread, spread);

			//DIRECTION SPREAD
			Vector3 direction = gunCamera.transform.forward + new Vector3(0, x, y);

			if (Physics.Raycast(gunCamera.transform.position, direction, out rayHit, range, whatIsEnemy))
			{
				Debug.Log(rayHit.collider.name);

				if (rayHit.collider.CompareTag("Enemy"))
					rayHit.collider.GetComponent<Bot>().TakeDamage(damage);
				else if (rayHit.collider.CompareTag("Boss"))
					rayHit.collider.gameObject.GetComponent<BossScript>().HP_Min -= damage;
				else if (rayHit.collider.CompareTag("Boss2"))
					rayHit.collider.gameObject.GetComponent<BossScript2>().HP_Min -= damage;
			}
		}

		if (Input.GetMouseButtonDown(0) && outOfAmmo && !isReloading && !isInspecting && !isRunning)
		{
			Reload();
		}

		//Inspect weapon when pressing T key
		if (Input.GetKeyDown(KeyCode.T))
		{
			anim.SetTrigger("Inspect");
		}

		//Toggle weapon holster when pressing E key
		if (Input.GetKeyDown(KeyCode.E) && !hasBeenHolstered)
		{
			holstered = true;
			mainAudioSource.Play();
			hasBeenHolstered = true;
		}
		else if (Input.GetKeyDown(KeyCode.E) && hasBeenHolstered)
		{
			holstered = false;
			mainAudioSource.Play();
			hasBeenHolstered = false;
		}

		//Holster anim toggle
		if (holstered == true)
		{
			anim.SetBool("Holster", true);
		}
		else
		{
			anim.SetBool("Holster", false);
		}

		//Reload 
		if (Input.GetKeyDown(KeyCode.R) && !isReloading && !isInspecting)
		{
			Reload();

			if (!hasStartedSliderBack)
			{
				hasStartedSliderBack = true;
				StartCoroutine(HandgunSliderBackDelay());
			}
		}

		//Walking when pressing down WASD keys
		if (Input.GetKey(KeyCode.W) && !isRunning ||
			Input.GetKey(KeyCode.A) && !isRunning ||
			Input.GetKey(KeyCode.S) && !isRunning ||
			Input.GetKey(KeyCode.D) && !isRunning)
		{
			anim.SetBool("Walk", true);
		}
		else
		{
			anim.SetBool("Walk", false);
		}

		//Running when pressing down W and Left Shift key
		if ((Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift)))
		{
			isRunning = true;
		}
		else
		{
			isRunning = false;
		}

		//Run anim toggle
		if (isRunning == true)
		{
			anim.SetBool("Run", true);
		}
		else
		{
			anim.SetBool("Run", false);
		}
	}
	private IEnumerator HandgunSliderBackDelay()
	{
		//Wait set amount of time
		yield return new WaitForSeconds(sliderBackTimer);
		//Set slider back
		anim.SetBool("Out Of Ammo Slider", false);
		//Increase layer weight for blending to slider back pose
		anim.SetLayerWeight(1, 0.0f);
		hasStartedSliderBack = false;
	}

	private IEnumerator GrenadeSpawnDelay()
	{
		//Wait for set amount of time before spawning grenade
		yield return new WaitForSeconds(grenadeSpawnDelay);
		//Spawn grenade prefab at spawnpoint
		Instantiate(Prefabs.grenadePrefab,
			Spawnpoints.grenadeSpawnPoint.transform.position,
			Spawnpoints.grenadeSpawnPoint.transform.rotation);
	}

	private IEnumerator AutoReload()
	{
		if (!hasStartedSliderBack)
		{
			hasStartedSliderBack = true;
			StartCoroutine(HandgunSliderBackDelay());
		}
		//Wait for set amount of time
		yield return new WaitForSeconds(autoReloadDelay);

		if (outOfAmmo == true)
		{
			anim.Play("Reload Out Of Ammo", 0, 0f);
			mainAudioSource.Play();

			if (bulletInMagRenderer != null)
			{
				bulletInMagRenderer.GetComponent
				<SkinnedMeshRenderer>().enabled = false;
				StartCoroutine(ShowBulletInMag());
			}
		}
		currentAmmo = ammo;
		outOfAmmo = false;
	}

	private void Reload()
	{
		if (currentAmmo == ammo) return;
		isReloading = true;
		if (outOfAmmo == true)
		{
			anim.Play("Reload Out Of Ammo", 0, 0f);
			mainAudioSource.Play();

			if (bulletInMagRenderer != null)
			{
				bulletInMagRenderer.GetComponent
				<SkinnedMeshRenderer>().enabled = false;
				StartCoroutine(ShowBulletInMag());
			}
		}
		else
		{
			anim.Play("Reload Ammo Left", 0, 0f);
			mainAudioSource.Play();

			if (bulletInMagRenderer != null)
			{
				bulletInMagRenderer.GetComponent
				<SkinnedMeshRenderer>().enabled = true;
			}
		}
		currentAmmo = ammo;
		outOfAmmo = false;
	}

	private IEnumerator ShowBulletInMag()
	{
		yield return new WaitForSeconds(showBulletInMagDelay);
		bulletInMagRenderer.GetComponent<SkinnedMeshRenderer>().enabled = true;
	}

	private void AnimationCheck()
	{
		if (anim.GetCurrentAnimatorStateInfo(0).IsName("Reload Out Of Ammo") || anim.GetCurrentAnimatorStateInfo(0).IsName("Reload Ammo Left"))
		{
			isReloading = true;
		}
		else
		{
			isReloading = false;
		}

		//Check if inspecting weapon
		if (anim.GetCurrentAnimatorStateInfo(0).IsName("Inspect"))
		{
			isInspecting = true;
		}
		else
		{
			isInspecting = false;
		}
	}
}
