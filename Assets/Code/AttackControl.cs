using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackControl : MonoBehaviour
{
    private Animator anim;
    private string horizontalInput = "Horizontal";
    private string verticallInput = "Vertical";
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Input.GetAxis(horizontalInput) != 0 || Input.GetAxis(verticallInput) != 0)
            {
                anim.SetTrigger("SlashMask");
            }
            else
            {
                anim.SetTrigger("Slash");
            }
        }
    }
}
