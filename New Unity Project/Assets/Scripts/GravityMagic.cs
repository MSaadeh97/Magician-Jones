using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GravityMagic : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float maxGrabDistance = 12f, throwForce = 20f, lerpSpeed = 10f;
    [SerializeField] Transform floatPoint;

    public GameObject particle;

    Rigidbody objectRB;

    public Boulder boulder;
    public Boulder lastBoulder;


    void Update()
    {
        if (objectRB)
        {
            objectRB.MovePosition(Vector3.Lerp(objectRB.position, floatPoint.transform.position, Time.deltaTime * lerpSpeed));
            particle.SetActive(true);
            if(Input.GetMouseButtonDown(0))
            {
                boulder.active = true;
                objectRB.isKinematic = false;
                objectRB.AddForce(cam.transform.forward * throwForce, ForceMode.VelocityChange);
                objectRB = null;
                particle.SetActive(false);
                lastBoulder = boulder;
                Invoke("DisableLastBoulder", 3);
            }
        }


        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (objectRB)
            {
                objectRB.isKinematic = false;
                objectRB = null;
            }
            else
            {
                RaycastHit hit;
                Ray ray = cam.ViewportPointToRay(new Vector3(.5f, .5f));
                if (Physics.Raycast(ray, out hit, maxGrabDistance))
                {
                    if(hit.collider.CompareTag("Boulder"))
                    {
                        boulder = hit.collider.gameObject.GetComponent<Boulder>();
                        objectRB = hit.collider.gameObject.GetComponent<Rigidbody>();
                    }
                    if (objectRB)
                    {
                        objectRB.isKinematic = true;
                    }
                }
            }
        }
    }

    void DisableLastBoulder()
    {
        lastBoulder.active = false;
    }
}
