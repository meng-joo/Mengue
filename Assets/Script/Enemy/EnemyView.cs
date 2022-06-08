using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    private void OnTriggerEnter(Collider collison)
    {
        if (collison.tag == "Player")
        {
            transform.parent.SendMessage("StartBattle", collison.gameObject);
        }
    }
}
