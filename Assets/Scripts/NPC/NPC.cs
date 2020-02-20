using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPC : Interactable
{
    //public NPCInfo info;

//    public GameManager gameManager;

    void Start()
    {
 //      gameManager = GameManager.instance;
    //    inventory = gM.inventory;
    //    textBox = info.greeting;
    }

        //    inventory = gM.inventory;
        //    textBox = info.greeting;
    

    public override void Interact()
    {
        //GameManager.instance.uIManager.OpenDialogue();
    }
    private void OnBecameVisible()
    {
        //GameManager.instance.topics.Add(info.characterName);
    }

    //void OnGUI()
    //{
    //    showQuest = gM.questDisplay.questPanel.activeSelf;
    //}
}
