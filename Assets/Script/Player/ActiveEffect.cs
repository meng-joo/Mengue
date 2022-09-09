using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveEffect : MonoBehaviour
{
    public GameObject _Onparticle;
    public Material _playerMat;
    public GameObject _disParticle;

    private void OnEnable()
    {
        GameObject pop = _Onparticle;
        Instantiate(pop, transform.position, Quaternion.identity);
        transform.rotation = Quaternion.Euler(0, 180, 0);
        transform.position = Vector3.zero;

        StartCoroutine(PlayerEffect());
    }

    IEnumerator PlayerEffect()
    {
        Color _color = Color.white;
        _color.a = 1;

        for (int i = 0; i < 20; i++)
        {
            _playerMat.SetColor("_EmissionColor", _color);
            _color.r -= 0.05f;
            _color.g -= 0.05f;
            _color.b -= 0.05f;

            yield return new WaitForSecondsRealtime(0.01f);
        }
    }

    private void OnDisable()
    {
        GameObject pop = _disParticle;
        Instantiate(pop, transform.position, Quaternion.identity);
    }
}
