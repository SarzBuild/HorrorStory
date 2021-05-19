using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{

    public float speed = 1.0f;
    private bool isRising = false;
    private bool isDraining = false;

    // Start is called before the first frame update
    void Start()
    {
        isRising = false;
        isDraining = false;
    }

    public void Rise()
    {
        isRising = true;
        isDraining = false;
        Debug.Log("Rising");
    }

    public void Drain()
    {
        isRising = false;
        isDraining = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            if (isRising)
            {
                GameActions.playerDrown();
            }
            else
            {
                GameActions.BathroomFinished();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (isRising) transform.Translate(Vector3.up * speed * Time.deltaTime, Space.World);

        if (isDraining) transform.Translate(-Vector3.up * speed * 3 * Time.deltaTime, Space.World);

    }
}
