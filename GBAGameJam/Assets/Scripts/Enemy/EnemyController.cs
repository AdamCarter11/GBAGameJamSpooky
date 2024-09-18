using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Animator animatorRef;
    private float distanceToPlayer;
    private bool isFollowing;
    private bool alerted;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetDistanceToPlayer();
        
    }



    private void GetDistanceToPlayer()
    {
        distanceToPlayer = Vector2.Distance(player.transform.position,transform.position);
    }

    private void Alert()
    {
        animatorRef.SetTrigger("Alert");
    }

    private void EnemyLogic()
    {
        if (distanceToPlayer < 10 && !alerted)
        {
            Alert();
            //isFollowing = true;
        }
        if (distanceToPlayer < 8 && alerted && !isFollowing)
        {
            isFollowing = true;
            animatorRef.SetBool("Following", true);
        }
        if(distanceToPlayer < 2)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        animatorRef.SetBool("Attack", true);
        yield return new WaitForSeconds(1);
        animatorRef.SetBool("Attack", false);
    }
}
