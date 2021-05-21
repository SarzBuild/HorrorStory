using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LivingRoomPuzzle : MonoBehaviour
{
    [SerializeField] GameObject subtitleText;
    [SerializeField] GameObject clueObj;
    [SerializeField] GameObject roomDoor;
    public List<LivingRoomPuzzleObjects> puzzleObjects;
    public int currentObject;
    // Start is called before the first frame update
    void Start()
    {
        currentObject = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void HandleInteraction()
    {
        EnableInterationWithObject();
    }
    void EnableInterationWithObject()
    {
        HandleClue();
        if (currentObject >= 0 && currentObject < puzzleObjects.Count -1)
        {
            roomDoor.GetComponentInChildren<Door>().canBeOpened = false;
        }        
        puzzleObjects[currentObject].canInteract = false;
        currentObject++;
        if (currentObject < puzzleObjects.Count)
        {
            puzzleObjects[currentObject].canInteract = true;
            //puzzleObjects[currentObject].GetComponentInChildren<SpriteRenderer>().enabled = true;
        }
    }
    void HandleClue()
    {
        TMPro.TextMeshProUGUI clueText = clueObj.GetComponentInChildren<TMPro.TextMeshProUGUI>();

        if (currentObject == 0)
        {
            clueText.text = "Mom, help! It's so dark in here";
        }
        else if (currentObject == 1)
        {
            clueText.text = "Just.... Seek knowledge";
        }
        else if (currentObject == 2)
        {
            clueText.text = "This living room looks so small";
        }
        else if (currentObject == 3)
        {
            clueText.text = "Flush down your fears";
        }
        else if (currentObject == 4)
        {
            //next line is for testing purposes
            //GameObject.Find("DollHouse").GetComponent<Animator>().SetBool("mustOpen", true);
            clueText.text = "Scare away the darkness of the end";
        }
        else if (currentObject == 5)
        {
            Debug.Log("test");
            roomDoor.GetComponentInChildren<Door>().canBeOpened = true;
        }
        StartCoroutine("ShowClue");

    }
    IEnumerator ShowClue()
    {
        if (currentObject < 5)
        {
            clueObj.SetActive(true);
            yield return new WaitForSeconds(3);
            clueObj.SetActive(false);
        }
        else
        {
            yield return new WaitForSeconds(2);
            //subtitleText.SetActive(false);
        }
    }
}
