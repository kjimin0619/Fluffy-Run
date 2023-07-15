using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClearCheck : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Character")
        {
            
            Debug.Log("Clear");
            
            // 클리어 시의 로직은 여기에 처리합니다.
            
            // 클리어 처리 이후 객체 파괴
            Destroy(this.gameObject);
        }
    }
}
