using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActorController : MonoBehaviour //,IInteractable
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
    

    public void Interact()
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
