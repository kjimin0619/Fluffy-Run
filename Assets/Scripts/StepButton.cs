using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class StepButton : MonoBehaviour
{
    public Sprite buttonOff, buttonOn;
    public GameObject groundObj;
    public Vector3 offPos, onPos, diff;
    public float movingSpeed = 1.5f;

    private bool touching = false;
    private IEnumerator goingOnCoroutine, goingOffCoroutine;
    
    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        _spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        groundObj = this.transform.Find("StepGround").gameObject;
        
        offPos = groundObj.transform.position;
        onPos = offPos + diff;
    }

    void Update()
    {
        if (touching)
        {
            _spriteRenderer.sprite = buttonOn;
            
            groundObj.transform.position = Vector3.MoveTowards(
                groundObj.transform.position, onPos, Time.deltaTime * movingSpeed);

            if (Vector3.Distance(groundObj.transform.position, onPos) < 0.01f)
            {
                groundObj.transform.position = onPos;
            }
        }
        else
        {
            _spriteRenderer.sprite = buttonOff;
            
            groundObj.transform.position = Vector3.MoveTowards(
                groundObj.transform.position, offPos, Time.deltaTime * movingSpeed);

            if (Vector3.Distance(groundObj.transform.position, offPos) < 0.01f)
            {
                groundObj.transform.position = offPos;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Block"))
        {
            touching = true;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Block"))
        {
            touching = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Block"))
        {
            touching = false;
        }
    }
}
