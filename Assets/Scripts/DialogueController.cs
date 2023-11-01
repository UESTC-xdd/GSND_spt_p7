using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;
using UnityEngine.Events;

public class DialogueController : MonoBehaviour
{
    public InputAction NextDialogAction;
    public UnityAction OnFinishDialog;

    public Text nameText;
    public Text dialogueText;
    public Text CallerText;
    public GameObject TelephoneUI;
    public GameObject TelephoneIcon;
    //public GameObject mission1UI;
    //public GameObject mission2UI;
    //public GameObject mission3UI;
    public bool missionAccomplished = false;
    public bool dialEnd = false;

    public DialogueLine[] lines;
    private Queue<DialogueLine> dialogueQueue = new Queue<DialogueLine>();
    private bool dialogueStarted = false;

    void Start()
    {
        NextDialogAction.performed -= OnPressNextDialog;
        NextDialogAction.performed += OnPressNextDialog;
    }

    private void OnPressNextDialog(InputAction.CallbackContext obj)
    {
        if(dialogueStarted)
        {
            DisplayNextSentence();
        }
    }

    private void OnEnable()
    {
        NextDialogAction.Enable();
        NextDialogAction.performed -= OnPressNextDialog;
        NextDialogAction.performed += OnPressNextDialog;
    }

    private void OnDisable()
    {
        NextDialogAction.Disable();
    }

    public void StartDialogue()
    {
        dialogueStarted = true;
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (dialogueQueue.Count == 0)
        {
            Debug.Log("Dialog Finished");
            EndDialogue();
            return;
        }

        DialogueLine line = dialogueQueue.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(line));
    }

    public void StartDialogue(DialogueLine[] line, DialogType type)
    {
        StopAllCoroutines();
        dialogueStarted = true;
        TelephoneUI.SetActive(true);
        switch (type)
        {
            case DialogType.Telephone:
                {
                    TelephoneIcon.SetActive(true);
                    break;
                }
            case DialogType.Inspect:
                {
                    TelephoneIcon.SetActive(false);
                    break;
                }
            default:
                break;
        }
        dialogueQueue.Clear();

        for (int i = 0; i < line.Length; i++)
        {
            dialogueQueue.Enqueue(line[i]);
        }

        DisplayNextSentence();
    }

    public void StartDialogue(DialogueLine line,DialogType type)
    {
        StopAllCoroutines();
        dialogueStarted = true;
        TelephoneUI.SetActive(true);
        switch (type)
        {
            case DialogType.Telephone:
                {
                    TelephoneIcon.SetActive(true);
                    break;
                }
            case DialogType.Inspect:
                {
                    TelephoneIcon.SetActive(false);
                    break;
                }
            default:
                break;
        }
        dialogueQueue.Clear();
        dialogueQueue.Enqueue(line);
        DisplayNextSentence();
    }

    IEnumerator TypeSentence(DialogueLine line)
    {
        nameText.text = line.name;
        dialogueText.text = "";
        CallerText.text = line.name;
        //audioSource.clip = line.audioClip;
        //audioSource.Play();

        if (line.audioClip)
        {
            AudioMgr.Instance.Stop2DSE();
            AudioMgr.Instance.PlayOneShot2DSE(line.audioClip);
        }

        foreach (char letter in line.sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        StopAllCoroutines();
        dialogueStarted = false;
        StartCoroutine(WaitForCloseCommand());
    }

    public void StopDialog()
    {
        StopAllCoroutines();
        AudioMgr.Instance.Stop2DSE();
        dialogueStarted = false;
        TelephoneUI.SetActive(false);
    }

    IEnumerator WaitForCloseCommand()
    {
        while(!Input.GetKeyDown(KeyCode.Space))
        {
            yield return null;
        }
        //SceneManager.LoadScene("SecondFloor");
        TelephoneUI.SetActive(false);
        OnFinishDialog?.Invoke();
        //Mission1();
    }

    //void Mission1()
    //{
    //    mission1UI.SetActive(true);
    //}

    //void Mission2()
    //{
    //    mission1UI.SetActive(false);
    //    mission2UI.SetActive(true);
    //}

    //void Mission3()
    //{
    //    mission2UI.SetActive(false);
    //    mission3UI.SetActive(true);
    //}
}

[System.Serializable]
public class DialogueLine
{
    public string name;
    public string sentence;
    public AudioClip audioClip;

}

public enum DialogType
{
    Telephone,
    Inspect
}