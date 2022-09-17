using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roulette : MonoBehaviour
{

    [SerializeField]
    GameObject ball;
    [SerializeField]
    Rigidbody ballRi;

    [SerializeField]
    Camera fCam;


    [SerializeField]
    public float speed = 100;
    float temSpeed = 100;

    bool open;

    public void rot()
    {
        if (!open)
        {

            temSpeed = speed;
            ball.transform.position = new Vector3(2.5f, 1, 1.5f);
            ballRi.velocity = Vector3.left * Random.Range(1, 5);
            ballRi.velocity = Vector3.back * Random.Range(1, 5);
            open = true;
        }

    }

    private void FixedUpdate()
    {
        fCam.transform.position = new Vector3(ball.transform.position.x, ball.transform.position.y + 1.5f, ball.transform.position.z);

        if (open)
        {
            gameObject.transform.rotation *= Quaternion.Euler(new Vector3(0, speed, 0));
            speed = speed * 0.99f;
        }

        if (speed <= 0.1f)
        {
            open = false;
            speed = temSpeed;
        }
    }

}
