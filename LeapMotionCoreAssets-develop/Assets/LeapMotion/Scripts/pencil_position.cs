using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;



public class pencil_position : HandModel
{
    private GameObject centerObj;
    public Vector3 modelFingerPointing = Vector3.forward;
    public Vector3 modelPalmFacing = -Vector3.up;

    public override void InitHand()
    {
        print ("1pencil");
        this.UpdateHand();
    }
    public Quaternion Reorientation()
    {
        return Quaternion.Inverse(Quaternion.LookRotation(modelFingerPointing, -modelPalmFacing));
    }


    public override void UpdateHand()
    {
        print("4");
        if (palm != null)
        {
            palm.position = GetPalmPosition();
            palm.rotation = GetPalmRotation() * Reorientation();
        }

        if (forearm != null)
            forearm.rotation = GetArmRotation() * Reorientation();

        for (int i = 0; i < fingers.Length; ++i)
        {
            if (fingers[i] != null)
            {
                fingers[i].fingerType = (Finger.FingerType)i;
                fingers[i].UpdateFinger();
            }
        }
    }
    private void Start()
    {
        print("2");
        centerObj = GameObject.Find("Handle");
        this.transform.position = new Vector3(centerObj.transform.position.x, centerObj.transform.position.y, centerObj.transform.position.z);
    }
    private void Update()
    {
        print("3");
        if (palm != null)
        {
            palm.position = GetPalmPosition();
            palm.rotation = GetPalmRotation() * Reorientation();
        }
        Vector3 pencilposition =  GetPalmPosition();
        print(pencilposition.x);
        this.transform.position = new Vector3(pencilposition.x, pencilposition.y, pencilposition.z);
    }
}