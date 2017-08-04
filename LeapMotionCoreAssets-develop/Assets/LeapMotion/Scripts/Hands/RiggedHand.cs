/******************************************************************************\
* Copyright (C) Leap Motion, Inc. 2011-2014.                                   *
* Leap Motion proprietary. Licensed under Apache 2.0                           *
* Available at http://www.apache.org/licenses/LICENSE-2.0.html                 *
\******************************************************************************/

using UnityEngine;
using System.Collections;
using Leap;

// Class to setup a rigged hand based on a model.
public class RiggedHand : HandModel {
	
  public Vector3 modelFingerPointing = Vector3.forward;
  public Vector3 modelPalmFacing = -Vector3.up;
    private GameObject centerObj;

  public override void InitHand() {
        print("1");
    UpdateHand();
  }

  public Quaternion Reorientation() {
    return Quaternion.Inverse(Quaternion.LookRotation(modelFingerPointing, -modelPalmFacing));
  }

  public override void UpdateHand() {
    if (palm != null) {
      palm.position = GetPalmPosition();
      palm.rotation = GetPalmRotation() * Reorientation();
    }

    if (forearm != null)
      forearm.rotation = GetArmRotation() * Reorientation();

    for (int i = 0; i < fingers.Length; ++i) {
      if (fingers[i] != null) {
				fingers[i].fingerType = (Finger.FingerType)i;
        fingers[i].UpdateFinger();
			}
		}
        
  }
    private void Start()
    {
        print("2");
        centerObj = GameObject.Find("Handle");
        centerObj.transform.position = new Vector3(centerObj.transform.position.x, centerObj.transform.position.y, centerObj.transform.position.z);
    }
    private void Update()
    {
        print("3");
        if (palm != null)
        {
            palm.position = GetPalmPosition();
            palm.rotation = GetPalmRotation() * Reorientation();
        }
        Vector3 pencilposition = GetPalmPosition();
        print(pencilposition.x);
        centerObj.transform.position = new Vector3(pencilposition.x, pencilposition.y, pencilposition.z);
    }

}
