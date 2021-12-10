using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{

    public GameObject Win;
    private bool isLeft = false;
    public float timer = 0;
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer>5)
        {
            if (isLeft==false)
            {
                timer = 0;
                isLeft = true;
            }
            else
            {
                timer = 0;

                isLeft = false;
            }
        }
        if (isLeft)
        {
            transform.Translate(Vector3.left * Time.deltaTime * 3);
        }
        else
        {
            transform.Translate(Vector3.right * Time.deltaTime * 3);

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Red"))
        {
            Destroy(other.gameObject);
            if (transform.localScale==new Vector3(1,1,1))
            {
                Win.SetActive(true);

                return;
            }
            transform.localScale = transform.localScale - Vector3.one;
        }
        if (other.CompareTag("Green"))
        {
            Destroy(other.gameObject);
            if (transform.localScale == new Vector3(1, 1, 1))
            {
                Win.SetActive(true);
                return;
            }
            transform.localScale = transform.localScale + Vector3.one;
        }
    }

}
