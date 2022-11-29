using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animationcontroller : MonoBehaviour
{
    [SerializeField] Animator animatorObject;

    [SerializeField] string _horizontal = "Horizontal";
    [SerializeField] string _vertical = "Vertical";

    private void Update()
    {
        float h = Input.GetAxisRaw(_horizontal);
        float v = Input.GetAxisRaw(_vertical);
        animatorObject.SetFloat("WalkFloat", h * 5 + v * 5);


        if (Input.GetKeyDown(KeyCode.Space))
        {
            animatorObject.SetTrigger("ThrowTrigger");
        }

        if(Input.GetAxis("Horizontal") > 0f || Input.GetAxis("Horizontal") < 0f)
        {
            //animatorObject.SetFloat("WalkFloat", input);
        }
        else
        {
            animatorObject.SetTrigger("EmptyTrigger");
        }


        
    }
}
