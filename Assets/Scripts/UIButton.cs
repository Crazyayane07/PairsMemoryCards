using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButton : MonoBehaviour {

    [SerializeField]
    private GameObject controler;
    [SerializeField]
    private string function;

    public void OnMouseUp()
    {
            controler.SendMessage(function);
    }
}
