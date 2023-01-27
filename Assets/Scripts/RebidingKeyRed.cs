using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class RebidingKeyRed : MonoBehaviour
{
    public TMP_Text input;
    public GameObject TextObject;
    public static KeyCode inputRed = KeyCode.UpArrow;
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
                if (kcode == RebidingKeyBlue.inputBlue) { }
                else
                {
                    Debug.Log("KeyCode down: " + kcode);
                    inputRed = kcode;
                    input.text = ("" + kcode);
                    TextObject.SetActive(true);
                    OtherObject.SetActive(false);
                }
            }
        }
    }
}
