using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpForceMode : MonoBehaviour
{
    public float _JumpPower =20f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.rigidbody.AddForce(Vector2.up * _JumpPower, ForceMode.Impulse);
        }
    }
}
