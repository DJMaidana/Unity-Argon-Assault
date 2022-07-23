using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float movementSpeed = 2f;
    [SerializeField] float minXRange = 10f;
    [SerializeField] float maxXRange = 10f;
    [SerializeField] float minYRange = 10f;
    [SerializeField] float maxYRange = 10f;

    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float positionYawFactor = 2f;
    [SerializeField] float positionRollFactor = -20f;

    [SerializeField] float pitchIntensity = -10f;
    

    float xInput, yInput;
    // Update is called once per frame
    void Update()
    {
        MoveShip();
        RotateShip();
    }

    void MoveShip()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

        Vector3 localOffset = new Vector3(xInput, yInput, 0) * Time.deltaTime * movementSpeed;

        Vector3 newLocalPos = transform.localPosition + localOffset; 

        float clampedXPos = Mathf.Clamp(newLocalPos.x, minXRange, maxXRange);   // Clamps values to prevent going off screen
        float clampedYPos = Mathf.Clamp(newLocalPos.y, minYRange, maxYRange);

        Vector3 clampedLocalPos = new Vector3(clampedXPos, clampedYPos, newLocalPos.z);

        transform.localPosition = clampedLocalPos;
    }

    void RotateShip()
    {
        transform.localRotation = Quaternion.Euler(-30f, 30f, 0f);
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yInput * pitchIntensity;
        
        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xInput * positionRollFactor;
        
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);

    }
}
