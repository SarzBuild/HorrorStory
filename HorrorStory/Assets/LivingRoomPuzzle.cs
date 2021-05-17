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
            puzzleObjects[currentObject].GetComponentInChildren<SpriteRenderer>().enabled = true;
        }
    }
    void HandleClue()
    {
        Text clueText = clueObj.GetComponentInChildren<Text>();

        if (currentObject == 0)
        {
            clueText.text = "Mom! Help! It's so dark in here!";
        }
        else if (currentObject == 1)
        {
            clueText.text = "With light we grow";
        }
        else if (currentObject == 2)
        {
            clueText.text = "I turn into paper to create it";
        }
        else if (currentObject == 3)
        {
            clueText.text = "Once upon a time\nI lost my spouse\ntogether with my kids\nin my own... ";
        }
        else if (currentObject == 4)
        {
            Debug.Log("test");
            roomDoor.GetComponentInChildren<Door>().canBeOpened = true;
        }
        StartCoroutine("ShowClue");

    }
    IEnumerator ShowClue()
    {
        if (currentObject < 4)
        {
            clueObj.SetActive(true);
            yield return new WaitForSeconds(2);
            clueObj.SetActive(false);
        }
        else
        {
            yield return new WaitForSeconds(2);
            //subtitleText.SetActive(false);
        }
    }
}
