using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    public class ZombiesData : MonoBehaviour
    {
        [SerializeField] private List<ZombieSO> zombies;

        private void Awake()
        {
            GlobalVariables.Instance.zombies = zombies;
        }
    }
}