using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundmusic : MonoBehaviour
{
    private static backgroundmusic _backgroundmusic;
    void Awake()
    {
        if (_backgroundmusic == null)
        {
            _backgroundmusic = this;
            DontDestroyOnLoad(_backgroundmusic);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
}
