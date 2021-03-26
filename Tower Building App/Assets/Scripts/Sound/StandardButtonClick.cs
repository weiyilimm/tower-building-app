using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StandardButtonClick : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(StandardButtonClickSound);
    }
    public void StandardButtonClickSound()
    {
        FindObjectOfType<SoundManager>().Play("standard button click");
    }
}
