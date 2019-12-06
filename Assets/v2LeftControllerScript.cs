using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class v2LeftControllerScript : MonoBehaviour
{
    public GameObject grabbedObject;
    FixedJoint activeJoint;
    public bool isGrabbing;
    Vector3 prevpos1;
    Vector3 velocity;
    List<Vector3> velocityHistory = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        prevpos1 = gameObject.transform.position;

    }

    // Update is called once per frame
    void Update()
    {



        velocity = gameObject.transform.position - prevpos1;//använder positionen av handen på förra framen och subtraherar den med nuvarande framens position för att skapa en hastighetes vector
        velocityHistory.Insert(0, velocity);// velocityHistory är en lista över handens hastighet över tid. denna behövs då man brukar stanna sin hand när man släpper istället för att släppa innan man slutar röra på handen så jag måste ha en historik på hastigheterna innan man stannar handen
        velocityHistory.RemoveAt(3);//definierar hur myeket tidigare hastigheten man vill ta är i detta fall så är hastigheten 3 frames gammal

        if (OVRInput.Get(OVRInput.RawAxis1D.LHandTrigger) == 1)//använder OVRInput för att checka om spelaren använder vänstra grabb knappen
        {
            prevpos1 = gameObject.transform.position;//sparar positionen av kontollern på förra framen 
            if (!isGrabbing && grabbedObject.GetComponent<Grabbable>())//kollar så att spelaren inte har tagit tag i objectet redan så att man inte skapar flera hingejoints på samma object
            {
                activeJoint = grabbedObject.AddComponent<FixedJoint>();//skapar en fixed joint på objektet som spelaren tar tag i
                activeJoint.connectedBody = gameObject.GetComponent<Rigidbody>();//sätter connectedBody till spelarens kontoller så att objektet följer med spelarens hand

                isGrabbing = true;
            }
        }
        else//när spelaren släpper taget
        {

            if (isGrabbing)
            {

                isGrabbing = false;
                Destroy(activeJoint);//tar bort jointen så objectet lossnar från spelarens hand
                grabbedObject.GetComponent<Rigidbody>().velocity = velocityHistory[velocityHistory.Count - 1] * 50;// lägger till den uträknade hastigheten (för tre frames sedan) på handen på objectets rigidbody
                grabbedObject = null;//resettar grabbed object så spelaren kan ta tag i ett nytt object0

            }






        }



    }
    private void OnTriggerEnter(Collider other)
    {



        if (other.gameObject.GetComponent<Grabbable>() && !isGrabbing) //definierar objectet spelaren interagerar med och checkar om den är en grabbable och även om spelaren redan har tagit tag i ett object för att inte overwrite objektet spelaren har tagi tag i
        {
            grabbedObject = other.gameObject;
        }

    }





}
