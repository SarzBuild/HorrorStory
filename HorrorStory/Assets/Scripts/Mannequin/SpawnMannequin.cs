using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMannequin : MonoBehaviour
{
    public Transform spawnLocation;
    public GameObject mannequinPrefab;
    //public GameObject MannequinShell; 
    private GameObject spawnedMannequin;
    private Mannequin mannequin; 

    public void SpeedUpMannequin()
    {
        mannequin.SpeedUp();


    }

    public void Die()
    {
        Destroy(mannequin.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            spawnedMannequin = Instantiate(mannequinPrefab) as GameObject;
            spawnedMannequin.transform.rotation = spawnLocation.rotation;
            spawnedMannequin.transform.position = spawnLocation.position;
            mannequin = spawnedMannequin.GetComponent<Mannequin>();
            //MannequinShell.transform.parent = spawnedMannequin.transform;
            Destroy(gameObject);
        }
    }
}
