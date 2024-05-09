using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : EnemyAlbility
{
    // Start is called before the first frame update
    public float Speed;
    private Transform Parent;
    public Transform PathParent;
    private List<Vector3> ListPaths = new();

    public bool isWASD;

    private int indexPath;

    private EnemyData enemyData;

    protected override void Initialization()
    {
        base.Initialization();
        enemyData = this.GetComponent<EnemyData>();
        foreach (Transform t in PathParent)
        {
            //Debug.Log(t.name);
            ListPaths.Add(t.position);
        }
        this.transform.position = ListPaths[0];
        indexPath = 1;
        Speed = _enemyData.stats.Speed;
        
    }

    void Start()
    {
        Parent = this.transform.parent;
        _enemyAnimController.PlayAnim(State.Run);
    }

    // Update is called once per frame
    void Update()
    {
        if(isWASD)
        {
            this.WASD_Movement();
        }
        else
        {
            this.MovePaths();
        }
    }

    private void MovePaths()
    {
        if (enemyData.IsDead) return;
        if (this.transform.position != ListPaths[indexPath])
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, ListPaths[indexPath], Speed * Time.deltaTime);
        }
        else
        {
            if (indexPath != ListPaths.Count - 1)
            {
                indexPath++;
            }
            else
            {
                indexPath = 0;
                this.transform.position = ListPaths[0];
            }
        }
    }

    private void WASD_Movement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.position += Parent.forward.normalized * Speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.position -= Parent.forward.normalized * Speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.position += Parent.right.normalized * Speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.position -= Parent.right.normalized * Speed * Time.deltaTime;
        }
    }
}
