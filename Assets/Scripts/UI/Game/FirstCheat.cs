using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstCheat : MonoBehaviour
{

    [SerializeField]
    private GameObject StandardOver;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerOver()
    {
        StandardOver.SetActive(true);
    }

    public void OnPointerExit()
    {
        StandardOver.SetActive(false);
    }
}
