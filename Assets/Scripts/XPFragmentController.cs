using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class XPFragmentController : MonoBehaviour
{
    private bool canGoToPlayer;

    private void Start()
    {
        canGoToPlayer = false;
        Invoke("EnableXPToGoToPlayer", 0.4f);
    }

    private void Update()
    {
        if (!canGoToPlayer) return;

        transform.position = Vector2.MoveTowards(transform.position, PlayerStatusHandler.Instance.transform.position, 8 * Time.deltaTime);
        transform.position = new Vector2(transform.position.x, transform.position.y - 0.02f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerLevelUpController.Instance.AddXP();

            Destroy(this.gameObject);
        }
    }

    private void EnableXPToGoToPlayer()
    {
        canGoToPlayer = true;
    }
}
