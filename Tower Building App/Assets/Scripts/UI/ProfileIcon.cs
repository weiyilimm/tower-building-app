using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProfileIcon : MonoBehaviour
{
    public TextMeshProUGUI ProfileIconXP;
    public TextMeshProUGUI ProfileIconUsername;
    void Start()
    {
        ProfileIconXP.text = User_Data.data.global_xp.ToString();
        ProfileIconUsername.text = User_Data.data.Username;
    }
}
