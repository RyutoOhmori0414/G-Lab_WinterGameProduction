using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaneController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // プレイヤーに当たった際
        if (other.gameObject.CompareTag("Player"))
        {
            // プレイヤーについていく
            // もしくは、取られたらいったんActiveをFalseにしてそのキャラが破壊されたときにそこにPositionを映す

        }
    }
}
