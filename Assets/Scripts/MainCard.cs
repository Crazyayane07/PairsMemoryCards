using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCard : MonoBehaviour {

    [SerializeField]
    private SceneController controller;
    [SerializeField]
    private GameObject Back;
    public int id { get; set; }

    public void OnMouseDown()
    {
        if (Back.activeSelf && controller.canWork())
        {
            setFaceCard();
            controller.showed(this);
        }
    }

    public void setFaceAndId(int id, Sprite Face)
    {
        this.id = id;
        GetComponent<SpriteRenderer>().sprite = Face;
    }

    public void setBackCard()
    {
        Back.SetActive(true);
    }

    public void setFaceCard()
    {
        Back.SetActive(false);
    }
}
