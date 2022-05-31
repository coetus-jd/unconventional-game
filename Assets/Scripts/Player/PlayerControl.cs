using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    [SerializeField] private Animator anim;
    void Update()
    {
        movement();
    }

    void movement()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            anim.SetBool("Jumping", true);
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            anim.SetBool("Sliding", true);
        }
        else
        {
            anim.SetBool("Jumping", false);
            anim.SetBool("Sliding", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Destroy(this.gameObject);
    }
}
