using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCoin : MonoBehaviour
{
    public GameObject PrefabCoin;
    public Transform CoinParent;

    // Start is called before the first frame update
    void Start()
    {
        for (float i=0; i < 125; i++)
        {
            float x = Random.Range(-3.5f, 3.5f);
            float z = Random.Range(1f, 10f);
            Vector3 pos = new Vector3(x, 6, z);

            GameObject coin = Instantiate(PrefabCoin, pos, Quaternion.identity);
            coin.transform.SetParent(CoinParent);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
