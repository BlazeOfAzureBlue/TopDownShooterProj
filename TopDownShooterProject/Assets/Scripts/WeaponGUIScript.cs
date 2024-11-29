using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGUIScript : MonoBehaviour
{

    public GameObject WeaponListGUI;
    public bool GUIActive = false;

    private bool debounce;
    private float Timer;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!debounce)
        {
            Timer += Time.deltaTime;
            if (Timer > 0.1)
            {
                debounce = true;
                Timer = 0;
            }
        }

        if (Input.GetKey(KeyCode.Escape) && debounce == true)
        {
            if(WeaponListGUI.activeInHierarchy == true)
            {
                debounce = false;
                WeaponListGUI.SetActive(false);
                GUIActive = false;
            }
            else if(WeaponListGUI.activeInHierarchy == false)
            {
                debounce = false;
                WeaponListGUI.SetActive(true);
                GUIActive = true;
            }
        }
    }
}
