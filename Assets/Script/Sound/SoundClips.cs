using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundClips : MonoBehaviour
{
    [SerializeField] public AudioClip[] _backgroundSound;
    [SerializeField] public AudioClip _fightingSound;

    [SerializeField] public AudioClip[] _effectSound;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        StartCoroutine("StartSound");
    }

    IEnumerator StartSound()
    {
        _audioSource.Pause();

        while (true)
        {
            int _random = Random.Range(0, _backgroundSound.Length - 1);
            _audioSource.clip = _backgroundSound[_random];
            _audioSource.Play();
            yield return new WaitForSeconds(_backgroundSound[_random].length + 20);
        }
    }

    IEnumerator SetBattleSound()
    {
        _audioSource.Pause();

        _audioSource.clip = _fightingSound;

        while (true)
        {
            _audioSource.Play();
            yield return new WaitForSeconds(_fightingSound.length);
        }
    }
}
