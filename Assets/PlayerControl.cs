using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public float rotationSpeed=100f;
    public float jumpHeight=8f;
    private bool isFalling = false;
     Rigidbody rb;
    private Vector3 y;
     
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        rotation *= Time.deltaTime;
        rb.AddRelativeTorque(Vector3.back * rotation);

        if (Input.GetMouseButtonDown(0) && isFalling == false)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                y = rb.velocity;
                y.y = jumpHeight;

                isFalling = true;
                rb.velocity = y;
            }
            

        }
        
    }
    private void OnCollisionStay()
    {
        isFalling = false;
    }

}
