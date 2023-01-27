using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class RebidingKeyBlue : MonoBehaviour
{
    public TMP_Text input;
    public static KeyCode inputBlue = KeyCode.DownArrow;
    public GameObject TextObject;
    public GameObject OtherObject;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKey(kcode))
            {
                if (kcode == RebidingKeyRed.inputRed) { }
                else
                {
                    input.text = ("" + kcode);
                    inputBlue = kcode;
                    TextObject.SetActive(true);
                    OtherObject.SetActive(false);
                }
            }
        }
    }
}
