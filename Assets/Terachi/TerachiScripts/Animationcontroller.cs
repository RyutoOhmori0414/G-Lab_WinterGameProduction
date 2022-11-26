using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animationcontroller : MonoBehaviour
{
    [SerializeField] Animator animatorObject;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animatorObject.SetTrigger("ThrowTrigger");
        }

        if(Input.GetAxis("Horizontal") > 0f || Input.GetAxis("Horizontal") < 0f)
        {
            animatorObject.SetTrigger("WalkTrigger");
        }
        else
        {
            animatorObject.SetTrigger("EmptyTrigger");
        }


        
    }
}
