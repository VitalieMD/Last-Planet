using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float controlSpeed = 30f;
    [SerializeField] float xRange = 10f;
    [SerializeField] float yRange = 10f;

    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -10;
    [SerializeField] float positionYawFactor = 2f;
    [SerializeField] float controRollFactor = -15;

    [SerializeField] GameObject[] lasers;
    float xThrow, yThrow;

    void Start()
    {
        
    }

    void Update()
    {
        PlayerMovement();
        PlayerRotation();
        Shooting();
    }

    void PlayerRotation()
    {
        float pitch = transform.localPosition.y * positionPitchFactor + yThrow * controlPitchFactor;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void PlayerMovement()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void Shooting()
    {
        if (Input.GetButton("Fire1"))
        {
            SetActivateLasers(true);
        }
        else
        {
            SetActivateLasers(false);
        }
    }

    void SetActivateLasers(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionComponent = laser.GetComponent<ParticleSystem>().emission;
            emissionComponent.enabled = isActive  ;
        }
    }
}