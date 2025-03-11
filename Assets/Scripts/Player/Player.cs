using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //총괄 
    public PlayerController controller;
    public ItemData itemData;
    public Action additem;

    private void Awake()
    {
        CharaterManager.Instance.Player = this;
        controller= GetComponent<PlayerController>();
    }
}
