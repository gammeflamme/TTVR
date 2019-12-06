using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingSpaceScript : MonoBehaviour
{
    public Transform touchL;
    public Transform touchR;
    Vector3 trackingSpacePos;



    // Update is called once per frame
    void Update()
    {//detta script tar positionen och rotationen som OVRInput rapporterar och överför dem till controllrarna
        touchL.localPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
        touchL.localRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);
        touchR.localPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        touchR.localRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);


        //gör så att man kan röra sig i x och z axeln genom att använda thumbsticken på högra kontrollern
        trackingSpacePos.x = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick).x * 0.1f;
        trackingSpacePos.z = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick).y * 0.1f;
        gameObject.transform.Translate(trackingSpacePos);


    }
}
