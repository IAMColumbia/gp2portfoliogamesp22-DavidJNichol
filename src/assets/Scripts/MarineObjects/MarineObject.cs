using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarineObject : MonoBehaviour, IMarineObject
{
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
    protected float moveSpeed;
    public bool CanMove { get { return canMove; } set { canMove = value; } }
    protected bool canMove;
    public bool CanCollideWithHook { get { return canCollideWithHook; } set { canCollideWithHook = value; } }
    protected bool canCollideWithHook;
    public float SpawnOffsetY { get { return spawnOffsetY; }}
    protected float spawnOffsetY;
    public  bool IsOnHook { get { return isOnHook; } set { isOnHook = value; } }
    protected bool isOnHook;
    public Rigidbody2D Rigidbody { get { return rb; } set { rb = value; } }
    public float SpawnProbability { get { return spawnProbability; } set { spawnProbability = value; } }
    protected float spawnProbability;

    protected Rigidbody2D rb;

    protected Collider2D hookCollider;

    public delegate void OnDeactivateHandler();
    public event OnDeactivateHandler OnDeactivate; // for spawning new fish
    public delegate void OnHookCollisionHandler(MarineObject marineObject);
    public event OnHookCollisionHandler OnHookCollision; // for QTE

    protected virtual void Start()
    {
        moveSpeed = 0;
        canMove = true;
        canCollideWithHook = true;
        rb = GetComponent<Rigidbody2D>();
    }

    protected void Update()
    {
        if (isOnHook)
            FollowHookPosition();
        else
            Move();
    }
    public void Move()
    {
        if (canMove)
            this.transform.position = new Vector3(transform.position.x + moveSpeed, transform.position.y, transform.position.z);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);

        if (OnDeactivate != null)
        {
            OnDeactivate();
        }
    }

    public void ResetValuesOnSpawn()
    {
        gameObject.SetActive(true);
        canMove = true;
        canCollideWithHook = true;
    }

    private void FollowHookPosition()
    {
        if (hookCollider)
            this.transform.position = new Vector3(this.transform.position.x, hookCollider.transform.position.y, this.transform.position.z);
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Hook"))
        {
            if (canCollideWithHook)
            {
                OnHookCollision(this); // player qte event fire
                canMove = false;
                isOnHook = true;
                hookCollider = col;
            }
        }
        else if (col.CompareTag("RightBound"))
        {
            Deactivate(); // spawn new delegate fire
        }
    }
}
