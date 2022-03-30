using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class EnemyController : MonoBehaviour
{
    public GameObject[] playersInGame = new GameObject[2];
    public GameObject spawnPoint;
    PhotonView view;

    private float distancePlayer1;
    private float distancePlayer2;
    private Vector2 targetX;
    private Vector2 targetPatrol;
    private float currentDamageTimeDelta = 0.0F;
    private bool isMovingLeft = false;

    [SerializeField]
    private Transform referencePos;
    private Transform leftMax;
    private Transform rightMax;

    public float enemyAttackDistance = 3F;
    public float damagePeriod = 1.0F;
    public float speed = 1.0F;

    public void GetSpawn(Transform leftRange, Transform rightRange, Transform spawnPos)
    {
        referencePos = spawnPos;
        leftMax = leftRange;
        rightMax = rightRange;
    }

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();

        playersInGame[0] = GameObject.Find("Player1(Clone)");
        playersInGame[1] = GameObject.Find("Player2(Clone)");
    }
    
    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            distancePlayer1 = Vector2.Distance(transform.position, playersInGame[0].GetComponent<Transform>().position);
            distancePlayer2 = Vector2.Distance(transform.position, playersInGame[1].GetComponent<Transform>().position);

            if (distancePlayer1 <= enemyAttackDistance || distancePlayer2 <= enemyAttackDistance)
            {
                if (distancePlayer1 <= distancePlayer2)
                {
                    if (distancePlayer1 <= 1F)
                    {
                        if (currentDamageTimeDelta <= 0)
                        {
                            playersInGame[0].GetComponent<PlayerHealth>().HealthControl(true, true, 20);
                            currentDamageTimeDelta = damagePeriod;
                        }
                        else
                        {
                            currentDamageTimeDelta -= Time.deltaTime;
                        }
                        
                    }
                    else
                    {
                        currentDamageTimeDelta = damagePeriod;
                    }

                    targetX.x = playersInGame[0].GetComponent<Transform>().position.x;
                    targetX.y = transform.position.y;
                    transform.position = Vector2.MoveTowards(transform.position, targetX, speed * Time.deltaTime);
                }
                else
                {
                    if (distancePlayer2 <= 1F)
                    {
                        if (currentDamageTimeDelta <= 0)
                        {
                            playersInGame[1].GetComponent<PlayerHealth>().HealthControl(true, false, 20);
                            currentDamageTimeDelta = damagePeriod;
                        }
                        else
                        {
                            currentDamageTimeDelta -= Time.deltaTime;
                        }
                    }
                    else
                    {
                        currentDamageTimeDelta = damagePeriod;
                    }

                    targetX.x = playersInGame[1].GetComponent<Transform>().position.x;
                    targetX.y = transform.position.y;
                    transform.position = Vector2.MoveTowards(transform.position, targetX, speed * Time.deltaTime);
                }
            } 
            else
            {
                
                if (isMovingLeft)
                {
                    targetPatrol.x = leftMax.position.x;
                    targetPatrol.y = referencePos.position.y;
                }
                else
                {
                    targetPatrol.x = rightMax.position.x;
                    targetPatrol.y = referencePos.position.y;
                }

                transform.position = Vector2.MoveTowards(transform.position, targetPatrol, speed * Time.deltaTime);
                
                if (isMovingLeft && transform.position.x <= targetPatrol.x + 0.5)
                {
                    isMovingLeft = false;
                }
                else if (!isMovingLeft && transform.position.x >= targetPatrol.x - 0.5)
                {
                    isMovingLeft = true;
                }
            }
        }
    }
}
