using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{

    [SerializeField] private PlayerMovement player;
    [SerializeField] private int HP;
    public Image healthbar;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        HP = player._health;
        healthbar.fillAmount = HP / 100f;
    }


}
