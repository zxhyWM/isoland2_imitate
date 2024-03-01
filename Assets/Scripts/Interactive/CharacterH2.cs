using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogueController))]
public class CharacterH2 : Interactive
{
    private DialogueController _dialogueController;

    private void Awake()
    {
        _dialogueController = GetComponent<DialogueController>();
    }

    public override void EmptyClicked()
    {
        if(isDone)
            _dialogueController.ShowDialogueFinish();
        //对话内容a
        else
            _dialogueController.ShowDialogueEmpty();
    }

    protected override void OnClickedAction()
    {
        //对话内容b
        _dialogueController.ShowDialogueFinish();
    }
}
