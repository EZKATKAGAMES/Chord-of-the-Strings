using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeybindMenu : MonoBehaviour
{

    // TODO: Improve the functionality of this script, it is very primitive, following a tutorial because this script was blocking progress, so I rushed it.

    Transform menuPanel;

    Event keyEvent;

    Text buttonText;

    KeyCode newKey;

    bool waitingAssignation;

    // Use this for initialization
    void Start()
    {
        menuPanel = transform.Find("Panel");

        menuPanel.gameObject.SetActive(false);

        for (int i = 0; i < menuPanel.childCount; i++) // Make sure each button is displaying the correct key assigned.
        {
            if(menuPanel.GetChild(i).name == ("ForwardKey"))
            {
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.Up.ToString();
            }
            else if(menuPanel.GetChild(i).name == ("LeftKey"))
            {
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.Left.ToString();
            }
            else if(menuPanel.GetChild(i).name == ("BackwardKey"))
            {
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.Down.ToString();
            }
            else if (menuPanel.GetChild(i).name == ("RightKey"))
            {
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.Right.ToString();
            }
           
        }

    }

    // Update is called once per frame
    void Update()
    {
        // Temporary of course

        if (Input.GetKeyDown(KeyCode.Escape) && !menuPanel.gameObject.activeSelf)
        {
            menuPanel.gameObject.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && menuPanel.gameObject.activeSelf)
        {
            menuPanel.gameObject.SetActive(false);
        }

    }


    private void OnGUI()
    {
        keyEvent = Event.current;

        if(keyEvent.isKey && waitingAssignation)
        {
            newKey = keyEvent.keyCode; // new key assigned
            waitingAssignation = false;
        }



    }

    public void StartAssignment(string keyName)
    {
        if (!waitingAssignation)
        {
            StartCoroutine(AssignKey(keyName));
        }
    }

    public void SendText(Text text)
    {
        buttonText = text;
    }

    IEnumerator WaitForInput()
    {
        while (!keyEvent.isKey)
        {
            yield return null;
        }
    }

    IEnumerator AssignKey(string keyName)
    {
        waitingAssignation = true;

        yield return new WaitForSeconds(0);

        switch (keyName)
        {
            case "up":
                GameManager.GM.Up = newKey; // new keycode for this action
                buttonText.text = GameManager.GM.Up.ToString(); // Text for new button
                PlayerPrefs.SetString("upkey", GameManager.GM.Up.ToString());
                break;
            case "left":
                GameManager.GM.Left = newKey;
                buttonText.text = GameManager.GM.Left.ToString();
                PlayerPrefs.SetString("leftkey", GameManager.GM.Left.ToString());
                break;
            case "down":
                GameManager.GM.Down = newKey;
                buttonText.text = GameManager.GM.Down.ToString();
                PlayerPrefs.SetString("downkey", GameManager.GM.Down.ToString());
                break;
            case "right":
                GameManager.GM.Right = newKey;
                buttonText.text = GameManager.GM.Right.ToString();
                PlayerPrefs.SetString("rightkey", GameManager.GM.Right.ToString());
                break;
        }

        yield return null;
    }
}
