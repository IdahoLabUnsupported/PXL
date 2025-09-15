using MixedReality.Toolkit.Input;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HandTracker : MonoBehaviour
{
    public static PokeInteractor LeftController;
    public static PokeInteractor RightController;
   
    static bool searched = false;
    private List<PokeInteractor> hands;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!searched)
        {
            this.hands = gameObject.GetComponentsInChildren<PokeInteractor>().ToList();
        
            RightController = hands[0];
            LeftController = hands[1];

            searched = true;
        }

    }

    public Vector3 GetPosition()
    {
        ///this has to distinguish between left and right hand
        if (RightController.enabled)
        {
            return RightController.transform.position;
        }
        else if (LeftController.enabled)
        {
            return LeftController.transform.position;
        }
        return Vector3.zero;
    }


}
