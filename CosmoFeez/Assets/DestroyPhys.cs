using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPhys : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("block");
        GetComponent<Rigidbody>().isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            if(collision.gameObject.layer == LayerMask.NameToLayer("AntiBlock"))
            {
                if (collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude > 1)
                {
                    Debug.Log(gameObject.GetComponent<Rigidbody>().velocity.magnitude);
                    GetComponent<Rigidbody>().isKinematic = false;
                    gameObject.layer = LayerMask.NameToLayer("AntiBlock");
                }
            }
            else
            {
                if (collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude > 2)
                {
                    GetComponent<Rigidbody>().isKinematic = false;
                    gameObject.layer = LayerMask.NameToLayer("AntiBlock");
                }
            }
            
            
        }
        
    }
    private void OnCollisionExit(Collision collision)
    {

    }
}
