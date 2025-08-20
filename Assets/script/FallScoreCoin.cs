using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FallScoreCoin : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Counter"))
        {
            if(GManager.instance != null)
            {
                GManager.instance.Possession++;
                GManager.instance.Score += 15f;
            }

            Destroy(gameObject);
        }
    }
}
