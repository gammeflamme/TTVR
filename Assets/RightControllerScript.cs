using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightControllerScript : MonoBehaviour
{
    public GameObject grabbedObject;
    FixedJoint activeJoint;
    public bool isGrabbing;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.RawButton.RHandTrigger))//använder OVRInput för att checka om spelaren använder grabb knappen

        {

            if (!isGrabbing && grabbedObject.GetComponent<Grabbable>())//kollar så att spelaren inte har tagit tag i objectet redan så att man inte skapar flera hingejoints på samma object
            {
                activeJoint = grabbedObject.AddComponent<FixedJoint>();//skapar en fixed joint på objektet som spelaren tar tag i
                activeJoint.connectedBody = gameObject.GetComponent<Rigidbody>();//sätter connectedBody till spelarens kontoller så att objektet följer med spelarens hand

                isGrabbing = true;
            }
        }
        else//när spelaren släpper taget
        {

            isGrabbing = false;
            Destroy(activeJoint);//tar bort jointen så objectet lossnar från spelarens hand



        }
    }
    private void OnTriggerEnter(Collider other)
    {

        

        if (other.gameObject.GetComponent<Grabbable>() && !isGrabbing) //definierar objectet spelaren interagerar med och checkar om den är en grabbable och även om spelaren redan har tagit tag i ett object för att inte overwrite objektet spelaren har tagi tag i
        {
            grabbedObject = other.gameObject; 
        }

    }



    //anledningen till att jag använde en fixed joint istället för att göra objectet till  en child av controllern är på grund av att jag ville att objektet skulle behålla sin hastighet när man släpper men av någon anledning så får objectets rigid body ingen velocity nör man svingar runt det
    private void OnTriggerExit(Collider other)
    {
        grabbedObject = null;//resettar grabbed object så spelaren kan ta tag i ett nytt object0
    }

}


