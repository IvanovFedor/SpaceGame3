using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    float xRot;
    float zRot;
    float yRot;
    public GameObject playerGameObject;
    public float sensivity = 0.0001f;
    [SerializeField]float speed = 0f;
    float maxSpeed = 5f;
    float minSpeed = -1f;
    bool TimeTurbo = true;

    float ShipTurboTimeOver = 5;
    private float _up;
    private float _right;
    private float _forward;

    public Rigidbody _rigPlayer;

    public AudioClip ErroreTurbo;
    public AudioClip ActivateTurbo;

    public AudioSource ShipMusic;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _rigPlayer.maxAngularVelocity = 0.9f;
    }

    void Update()
    {
        ShipRotMove();
    }

    void ShipRotMove()
    {
        if (Input.GetKey(KeyCode.R))
        {
            _up = 1f;
        }
        else if (Input.GetKey(KeyCode.F))
        {
            _up = -1f;
        }
        else if (!Input.GetKey(KeyCode.F) && !Input.GetKey(KeyCode.R))
        {
            _up = 0f;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            zRot = 0.1f;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            zRot = -0.1f;
        }
        else if (!Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.E))
        {
            zRot = 0f;
        }

        _right = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.W))
        {
            speed = speed + 0.02f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            speed = speed - 0.02f;
        }
        else if (Input.GetKey(KeyCode.X))
        {
            speed = 0f;
        }
        else if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            speed = speed - 0f;
        }

        if (speed >= maxSpeed)
        {
            speed = maxSpeed;
        }
        else if (speed <= minSpeed)
        {
            speed = minSpeed;
        }
        if (Input.GetKey(KeyCode.Tab) && TimeTurbo)
        {
            StartCoroutine(TurboSpeed());
        }
        else if (!TimeTurbo && Input.GetKey(KeyCode.Tab))
        {
            ShipMusic.PlayOneShot(ErroreTurbo);
        }

        if (xRot >= 7)
        {
            xRot = 7;
        }
        else if(xRot <= -7)
        {
            xRot = -7;
        }
        else if (xRot <= 1.5 && xRot >= -1.5)
        {
            xRot = 0;
        }
        if (yRot >= 7)
        {
            yRot = 7;
        }
        else if (yRot <= -7)
        {
            yRot = -7;
        }
        else if (yRot <= 1.5 && yRot >= -1.5)
        {
            yRot = 0;
        }
        xRot += Input.GetAxis("Mouse X");
        yRot += Input.GetAxis("Mouse Y");

        _rigPlayer.AddRelativeTorque(-yRot, -zRot, -xRot);
        _rigPlayer.AddRelativeForce(_right, _up, speed);
    }

    IEnumerator TurboSpeed()
    {
        ShipMusic.PlayOneShot(ActivateTurbo);
        yield return new WaitForSeconds(0.3f);
        TimeTurbo = false;
        speed = 8f;
        _rigPlayer.maxAngularVelocity = 2f;
        yield return new WaitForSeconds(2f);
        speed = maxSpeed;
        _rigPlayer.maxAngularVelocity = 0.9f;
        yield return new WaitForSeconds(ShipTurboTimeOver);
        TimeTurbo = true;
        StopCoroutine(TurboSpeed());
    }
}
