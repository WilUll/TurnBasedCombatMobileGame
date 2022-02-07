using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 0.005f;
    public bool isMoving;
    Vector3 endPos;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void Move(Vector3 moveTo)
    {
        endPos = moveTo;
        isMoving = true;
    }

    private void Update()
    {
        if (isMoving)
        {
            if (Mathf.Abs(transform.position.x - endPos.x) != 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(endPos.x, transform.position.y), speed * Time.deltaTime);
                anim.SetFloat("DirectionX", -(transform.position.x - endPos.x));
            }
            else if(Mathf.Abs(transform.position.y - endPos.y) != 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, endPos, speed * Time.deltaTime);
                anim.SetFloat("DirectionY", -(transform.position.y - endPos.y));
            }
            else
            {
                isMoving = false;
            }

        }
    }
}
