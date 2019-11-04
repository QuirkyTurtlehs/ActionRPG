using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainVector : MonoBehaviour
{
    // Start is called before the first frame update

    PlayerInput playerInput;

        

    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void Slice()
    {
        playerInput = transform.parent.gameObject.GetComponent<PlayerInput>();

        List<SliceVectors> sliceList = playerInput.sliceVectorList;

        for (int i = 0; i < sliceList.Count; i++)
        {

        }
        
    }
}
