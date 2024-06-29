using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Rotator : MonoBehaviour
{
    public GameObject rotatingObject;
    public Image rotatingArrow;
    public UnityEvent onRotatorEnd;


    public void Update(){
      // Assuming the rotation is around the Z axis
        float rotationAngle = rotatingObject.transform.eulerAngles.z;

        // Normalize the rotation angle to a fill amount (0 to 1)
        float fillAmount = rotationAngle / 360f;
        

        // Set the fill amount of the Image
        rotatingArrow.fillAmount = fillAmount;
        if(fillAmount == 1){

          onRotatorEnd.Invoke();
        }
    }
}
