using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AsteroidCountUpdater : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_text != null)
        {
            _text.text = "Asteroid Count: " + AsteroidSpawner.AsteroidCount;
        }
    }
}
