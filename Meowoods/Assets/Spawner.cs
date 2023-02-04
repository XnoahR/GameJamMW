using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Ghost;
    public bool CanSpawn;
    void Start()
    {
        CanSpawn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanSpawn)
        {
            StartCoroutine(Spawn());
        }
    }

    IEnumerator Spawn()
    {
        Instantiate(Ghost, new Vector3(Random.Range(-7, 8), 0.5f, transform.position.z + Random.Range(-3, 1)), transform.rotation);
        CanSpawn = false;
        yield return new WaitForSeconds(2f);
        CanSpawn = true;
    }
}
