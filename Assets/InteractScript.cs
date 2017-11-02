using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractScript : MonoBehaviour
{

    public string interactButton = "Fire1";
    public float interactDistance = 5f;

    void Update()
    {
        if (Input.GetButtonDown(interactButton))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, interactDistance))
            {
                if (hit.collider.CompareTag("Door"))
                {
                    //hit.collider.transform.parent.parent.GetComponent<DoorScript>().ChangeDoorState();
                }
            }

        }
    }
}
