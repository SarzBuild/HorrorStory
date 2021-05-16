using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMannequin : MonoBehaviour
{
    public Transform spawnLocation;
    public GameObject mannequinPrefab;
    public GameObject MannequinShell; 


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject spawnedMannequin = Instantiate(mannequinPrefab) as GameObject;
            spawnedMannequin.transform.rotation = spawnLocation.rotation;
            spawnedMannequin.transform.position = spawnLocation.position;
            MannequinShell.transform.parent = spawnedMannequin.transform;
            Destroy(gameObject);
        }
    }
}
