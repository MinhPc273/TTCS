using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : EnemyAlbility
{
    // Start is called before the first frame update
    [SerializeField] Transform Model;

    public float Speed;
    private Transform Parent;
    public Transform PathParent;
    private List<Vector3> ListPaths = new();

    private bool canMove;
    public bool CanMove
    {
        set { canMove = value; }
    }

    [SerializeField] GameObject fxSlow;

    public bool isWASD;

    private int indexPath;

    private EnemyData enemyData;

    private Coroutine enumerator_run;

    protected override void Initialization()
    {
        base.Initialization();
        enemyData = this.GetComponent<EnemyData>();
        foreach (Transform t in PathParent)
        {
            //Debug.Log(t.name);
            ListPaths.Add(t.position);
        }
    }

    void Start()
    {
        Parent = this.transform.parent;
    }

    private void OnEnable()
    {
        this.transform.position = ListPaths[0];
        indexPath = 1;

        fxSlow.SetActive(false);
        Speed = _enemyData.stats.Speed;
        _enemyAnimController.PlayAnim(StateAnim.Run);

        Vector3 vector = Model.transform.localScale;
        if(vector.x < 0)
        {
            vector.x *= -1;
        }
        Model.transform.localScale = vector;
        canMove = true;
    }

    public void Attack()
    {
        canMove = false;
        _enemyAnimController.PlayAnim(StateAnim.Attack);
    }

    public void Run_Slow()
    {
        _enemyAnimController.PlayAnim(StateAnim.Run , 0.6f);
        Speed = _enemyData.stats.Speed * 0.6f;

        if(enumerator_run != null)
        {
            StopCoroutine(enumerator_run);
        }
        enumerator_run = StartCoroutine(reloadSpeed());


        fxSlow.SetActive(true);
    }

    IEnumerator reloadSpeed()
    {
        yield return new WaitForSeconds(1.5f);
        Speed = _enemyData.stats.Speed;
        _enemyAnimController.PlayAnim(StateAnim.Run);
        fxSlow.SetActive(false);
        enumerator_run = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(!canMove) return;
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
                if(indexPath == 2 || indexPath == 3 || indexPath == 6 || indexPath == 7)
                {
                    Vector3 vector3 = Model.localScale;
                    vector3.x *= -1;
                    Model.localScale = vector3;
                }
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
