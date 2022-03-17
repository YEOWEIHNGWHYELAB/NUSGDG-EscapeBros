using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerCombat : MonoBehaviour
{
    // Start is called before the first frame update
    private int _attackType = 1;
    private float _attackAnimDelayTime = 0.4f;
    private float _attackAnimDelayTimer;
    private float _attackAnimResetTime = 1f;
    private float _attackAnimResetTimer;

    private Animator _anim;
    PhotonView view;
    public float meleeAttackRange = 0.5f;
    public int meleeAttackDamage = 5;
    public Transform attackPoint;
    public LayerMask enemyLayer;

    void Start()
    {
        view = GetComponent<PhotonView>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            PlayerAttack();
        }
    }

    void PlayerAttack()
    {
        _attackAnimDelayTimer -= Time.deltaTime;
        _attackAnimResetTimer -= Time.deltaTime;
        if (Input.GetButtonDown("Fire3") && _attackAnimDelayTimer < 0f)
        {
            _anim.SetTrigger("Attack" + _attackType.ToString());
            _attackAnimDelayTimer = _attackAnimDelayTime;
            _attackAnimResetTimer = _attackAnimResetTime;
            if (_attackType < 3) _attackType++;
            else _attackType = 1;

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, meleeAttackRange, enemyLayer);

            foreach(Collider2D enemy in hitEnemies)
            {
                Debug.Log(enemy.gameObject.name);
                enemy.GetComponent<EnemyHealth>().TakeDamage(meleeAttackDamage);
            }
        }

        if (_attackAnimResetTimer < 0f)
        {
            _attackAnimResetTimer = _attackAnimResetTime;
            _attackType = 1;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, meleeAttackRange);
    }
}
