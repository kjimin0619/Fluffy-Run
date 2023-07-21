using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 4.5f;
    public float jumpSpeed = 7f;
    
    private float direction = 0f;
    private Rigidbody2D player;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    public bool isTouchingGround;

    private SpriteRenderer playerSprite;

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        direction = Input.GetAxis("Horizontal");
        player.velocity = new Vector2(direction * speed, player.velocity.y);

        if (Input.GetButtonDown("Jump") && isTouchingGround)
        {
            player.velocity = new Vector2(player.velocity.x, jumpSpeed);
        }

        // 플레이어가 왼쪽을 보고 있다면 스프라이트 뒤집기 (정지 시에도 유지)
        if (Math.Abs(direction) > 1e-6)
        {
            playerSprite.flipX = (direction < 0);
        }
    }

    // 동전 충돌 감지
    // 별, 버튼, 레버는 각자 구현
    // 가시는 TimeDecrease에서 (CollisionEnter로 확인)
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Coin"))
        {
            CoinCounter.Instance.AddCoin();
            col.gameObject.SetActive(false);
        }
    }
}
