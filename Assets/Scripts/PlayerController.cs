using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("General Setup Settings")]
    [Tooltip("How fast the ship moves up/down/left/right")]
    [SerializeField] float movementSpeed = 2f;
    [Tooltip("Left bounds for ship movement")]
    [SerializeField] float minXRange = 10f;
    [Tooltip("Right bounds for ship movement")]
    [SerializeField] float maxXRange = 10f;
    [Tooltip("Lower bounds for ship movement")]
    [SerializeField] float minYRange = 10f;
    [Tooltip("Upper bounds for ship movement")]
    [SerializeField] float maxYRange = 10f;
    [Tooltip("Weapons array, used to script firing controls")]
    [SerializeField] GameObject[] lasers;

    [Header("Ship Rotation Settings")]
    [Tooltip("How much the ship pitches up/down when pressing input keys")]
    [SerializeField] float pitchIntensity = -10f;
    [Tooltip("How much the ship pitches when moving up/down")]
    [SerializeField] float positionPitchFactor = -2f;
    [Tooltip("How much the ship yaws when moving left/right")]
    [SerializeField] float positionYawFactor = 2f;
    [Tooltip("How much the ship rolls when moving left/right")]
    [SerializeField] float positionRollFactor = -20f;    

    float xInput, yInput;
    // Update is called once per frame
    void Update()
    {
        ShipMovement();
        ShipRotation();
        ShipWeapons();

        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadLevel();
        }
    }

    void ShipMovement()
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

    void ShipRotation()
    {
        transform.localRotation = Quaternion.Euler(-30f, 30f, 0f);
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yInput * pitchIntensity;
        
        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xInput * positionRollFactor;
        
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);

    }

    void ShipWeapons()
    {
        if (Input.GetButton("Fire1") || Input.GetKey(KeyCode.Space)) 
        {
            FiringLasers(true);
        }
        else
        {
            FiringLasers(false);
        }
    }

    public void FiringLasers(bool emissionState)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = emissionState;
        }
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
