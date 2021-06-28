using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageByte : MonoBehaviour
{

    public static byte[] _imageByte;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public byte[] GetBytes()
    {
        return _imageByte;
    }

    public void SetBytes(byte[] byties)
    {
        _imageByte = byties;
    }
}
