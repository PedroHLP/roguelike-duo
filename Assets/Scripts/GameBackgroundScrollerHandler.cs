using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBackgroundScrollerHandler : MonoBehaviour
{
    public float resetPositionY = -22.5f;

    private void Update()
    {
        transform.Translate(Vector2.down * GameManager.Instance.scrollingSpeed * Time.deltaTime);

        if (transform.position.y <= resetPositionY)
        {
            RepositionBackground();
        }
    }

    private void RepositionBackground()
    {
        float highestY = float.MinValue;

        foreach (Transform child in transform)
        {
            if (child.position.y > highestY)
            {
                highestY = child.position.y;
            }
        }
        
        float backgroundHeight = GetComponentInChildren<SpriteRenderer>().bounds.size.y;
        transform.position = new Vector2(transform.position.x, highestY + backgroundHeight);
    }
}
