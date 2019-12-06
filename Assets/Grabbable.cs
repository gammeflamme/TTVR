using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{


    public Vector3 velocity;

    // Start is called before the first frame update




    void Start()
    {
 }

    // Update is called once per frame
    void Update()
    {
        velocity = gameObject.GetComponent<Rigidbody>().velocity;
    }
    private void OnTriggerEnter(Collider other)
    {

    }
    private void OnTriggerExit(Collider other)
    {
    }

}