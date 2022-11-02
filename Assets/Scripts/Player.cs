using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10;
    public Joystick joyStick;
    public Transform firePoint;
    public GameObject bulletPrefab;

    private CharacterController controller;
    private GameObject focusEnemy;


    void Start()
    {
        controller = GetComponent<CharacterController>();

        //corutine-shooting
       StartCoroutine(KeepShooting());
    }

    void Update()
    {
        // find target
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");

        float miniDist = 9999;
        foreach (GameObject enemy in enemys)
        {
            float d = Vector3.Distance(transform.position, enemy.transform.position);

            if (d < miniDist)
            {
                miniDist = d;
                focusEnemy = enemy;
            }
        }

        // 取得方向鍵輸入
        // float h = Input.GetAxis("Horizontal");
        // float v = Input.GetAxis("Vertical");

        // 取得虛擬搖桿輸入
        float h = joyStick.Horizontal;
        float v = joyStick.Vertical;

        // 合成方向向量
        Vector3 dir = new Vector3(h, 0, v);

        // 調整角色面對方向
        // 判斷方向向量長度是否大於 0.1（代表有輸入）
        if (dir.magnitude > 0.1f)
        {
            // 將方向向量轉為角度
            float faceAngle = Mathf.Atan2(h, v) * Mathf.Rad2Deg;

            // 使用 Lerp 漸漸轉向
            Quaternion targetRotation = Quaternion.Euler(0, faceAngle, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.3f);
        }

        //focus enemy
        else
        {
            if (focusEnemy)
            {
                var targetRotation = Quaternion.LookRotation(focusEnemy.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 20 * Time.deltaTime);
            }
        }

        // 地心引力 (y)
        if (!controller.isGrounded)
        {
            dir.y = -9.8f * 30 * Time.deltaTime;
        }

        // 移動角色位置
        controller.Move(dir * speed * Time.deltaTime);

        // 射擊
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }

    void Fire()
    {
        // 產生出子彈
        Instantiate(bulletPrefab, firePoint.transform.position, transform.rotation);
    }

    IEnumerator KeepShooting()
    {
        while(true)
        {
            Fire();
            yield return new WaitForSeconds(0.5f);
        }
    }
    

}
