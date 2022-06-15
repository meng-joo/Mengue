using UnityEngine;
using System.Collections;

public class csDestroyEffect : MonoBehaviour {
	
	void Start () {
        Invoke("EffectDestroy", 2);
    }

    private void EffectDestroy()
    {
        Destroy(gameObject);
    }
}
