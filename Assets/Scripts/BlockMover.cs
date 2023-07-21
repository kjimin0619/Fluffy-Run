using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMover : MonoBehaviour
{

    private Rigidbody2D Block;

    // Start is called before the first frame update
    void Start()
    {
        Block = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        Block.velocity = new Vector2(0, -5);

    }
}
