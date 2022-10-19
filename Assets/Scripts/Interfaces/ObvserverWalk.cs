using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObvserverWalk : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake() => _audioSource = GetComponent<AudioSource>();

    private void OnEnable() => WallRunTutorial.OnJump += PlayJumpAudio;
    private void OnDisable() => WallRunTutorial.OnJump -= PlayJumpAudio;

    private void PlayJumpAudio() => _audioSource.Play();
}
