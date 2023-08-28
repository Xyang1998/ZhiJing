using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Displayable : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    public KeyCode keyCode;
    private UISystem uiSystem;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        
    }

    private void Start()
    {
        uiSystem = SystemMediator.Instance.uisystem;
        uiSystem.displayables.Add(this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            uiSystem.ShowUI(this);
        }
    }

    public void Show()
    {
        if (canvasGroup)
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
    }

    public void Hide()
    {
        if (canvasGroup)
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }
}
