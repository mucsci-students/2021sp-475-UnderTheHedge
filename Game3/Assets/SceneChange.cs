﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
        void OnTriggerEnter (Collider other)
        {
            if (other.gameObject.CompareTag ("Player"))
            {
                SceneManager.LoadScene (1);
                Debug.Log ("Aye LMAO");
            }
        }
}
