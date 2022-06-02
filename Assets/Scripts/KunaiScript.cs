using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunaiScript : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;

    void Start()
    {
        rb.velocity = new Vector2(-speed,0);
        transform.rotation = Quaternion.Euler(0,0,90);
    }

    private void Update() {
        Destroy(this.gameObject, 3);
    }

}
