using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundClips : MonoBehaviour
{
    public static SoundClips instance;

    [SerializeField] public AudioClip[] _backgroundSound;
    [SerializeField] public AudioClip _fightingSound;
    [SerializeField] public AudioClip _StoreSound;

    [SerializeField] public AudioClip[] _effectSound;

    private AudioSource _audioSource;
    private bool _isBattle = true, _isStore = true;

    [SerializeField] public AudioSource _effectAudio;

    private void Awake()
    {
        instance = this;
    }

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
            _isBattle = false;
            _isStore = false;
        }
    }

    IEnumerator SetBattleSound()
    {
        yield return new WaitForSeconds(2f);

        _audioSource.Pause();
        _isBattle = true;
        _audioSource.clip = _fightingSound;
        _isStore = false;
        while (_isBattle)
        {
            _audioSource.Play();
            yield return new WaitForSeconds(_fightingSound.length);
            
        }
    }

    IEnumerator SetStoreSound()
    {
        _audioSource.Pause();
        _isStore = true;
        _audioSource.clip = _StoreSound;
        _isBattle = false;
        while (_isStore)
        {
            _audioSource.Play();
            yield return new WaitForSeconds(_StoreSound.length);
        }
    }

    public void EffectSound(int i)
    {
        _effectAudio.clip = _effectSound[i];

        _effectAudio.Play();
    }
}
