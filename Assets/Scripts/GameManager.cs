using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float scrollingSpeed;

    private void Awake() {
        Instance = this;
        DontDestroyOnLoad(this);
    }
}
