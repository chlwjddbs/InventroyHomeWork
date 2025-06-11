using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Player player;
    public ItemManager itemManager;
    public UIManager uiManager;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        player.Init();
        itemManager.Init();
        uiManager.Init(player);
    }
}
