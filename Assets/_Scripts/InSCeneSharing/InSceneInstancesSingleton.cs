using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InSceneInstancesSingleton : MonoBehaviour
{
    public static InSceneInstancesSingleton Instance;
    [SerializeField] private PlayerController playerController;
    public PlayerController PlayerControllerRef { get { return playerController; } }

    private void Awake()
    {
        Instance = this;
    }
}
