using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [HideInInspector]
    public static UIManager instance = null;

    [Header("Panel References")]
    public GameObject pausePanel;
    public GameObject hudPanel;
    public GameObject dialoguePanel;
    public GameObject inventoryPanel;

    [Header("Inventory Variables")]
    public GameObject slotPrefab;
    public GameObject slotParent;

    private void Awake()
    {
        instance = this;
    }

    #region Show Panels
    public void ShowPause(bool show)
    {
        pausePanel.SetActive(show);
    }
    public void ShowHud(bool show)
    {
        hudPanel.SetActive(show);
    }
    public void ShowDialogue(bool show)
    {
        dialoguePanel.SetActive(show);
    }
    public void ShowInventory(bool show)
    {
        inventoryPanel.SetActive(show);
    }
    #endregion


 
}