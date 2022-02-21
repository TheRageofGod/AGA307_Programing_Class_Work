using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireing_Point : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 1000f;
    public Transform firingPoint;
    public LayerMask layerMask;
    public LineRenderer lazer;
  
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            GameObject projectileInstance;
            projectileInstance = Instantiate(projectilePrefab, firingPoint.position, firingPoint.rotation);
            projectileInstance.GetComponent<Rigidbody>().AddForce(firingPoint.forward * projectileSpeed);
        }
    }

    private void FixedUpdate()
    {
        Vector3 pos = new Vector3(firingPoint.position.x, firingPoint.position.y, firingPoint.position.z);
        if (Input.GetButton("Fire2"))
        {
            Vector3 rayEnd = firingPoint.TransformDirection(Vector3.forward * 100);
            lazer.SetPosition(0, pos);
            lazer.SetPosition(1, rayEnd);

            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, layerMask))
            {
                Destroy(hit.collider.gameObject);
            }
            if (Input.GetButtonUp("Fire2"))
            {
                lazer.SetPosition(0, pos);
                lazer.SetPosition(1, pos);
            }
            Debug.DrawRay(transform.position, transform.forward, Color.blue);
        }
       
    }
}
