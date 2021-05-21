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
        Text clueText = clueObj.GetComponentInChildren<Text>();

        if (currentObject == 0)
        {
            clueText.text = "HELP!";
        }
        else if (currentObject == 1)
        {
            clueText.text = "The first number is\nin a place to relax\nbut you must get there\nbefore the end of the wax";
        }
        else if (currentObject == 2)
        {
            clueText.text = "The second number is\nwhere you can have some fun\nhave some good laughts\nbut you son is being pointed a gun";
        }
        else if (currentObject == 3)
        {
            clueText.text = "The third number is\nin an object that creates light\nit will keep you safe\njust hope it works all night...";
        }
        else if (currentObject == 4)
        {
            //next line is for testing purposes
            GameObject.Find("DollHouse").GetComponent<Animator>().SetBool("mustOpen", true);
            clueText.text = "Once upon a time\nI lost my spouse\ntogether with my kids\nin my own... ";
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
