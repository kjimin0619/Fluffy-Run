using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public Sprite leverLeft, leverRight;
    public GameObject objLeft, objRight;
    public bool isRight = false;

    private bool touching = false;
    private bool canSwitch = true;
    private float waitingTime = 0.5f;

    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        _spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();

        isRight = false;
        objLeft.SetActive(true);
        objRight.SetActive(false);
    }

    void Update()
    {
        if (touching && Input.GetKey(KeyCode.UpArrow) && canSwitch)
        {
            StartCoroutine(SwitchLever());
        }
    }

    private IEnumerator SwitchLever()
    {
        canSwitch = false;
        isRight = !isRight;
        _spriteRenderer.sprite = isRight ? leverRight : leverLeft;
        objLeft.SetActive(!isRight);
        objRight.SetActive(isRight);

        yield return new WaitForSecondsRealtime(waitingTime);
        canSwitch = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            touching = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        touching = false;
    }
}
