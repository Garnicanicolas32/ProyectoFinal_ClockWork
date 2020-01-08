using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Detect : MonoBehaviour {
    /*
    private GraphicRaycaster gr;
    private PointerEventData pointerEventData = new PointerEventData(null);

    public Transform currentCharacter;


    void Update()
    {

        //CONFIRM
        if (Input.GetKeyDown(KeyCode.Z))
        {
        }          

        pointerEventData.position = transform.position;
        List<RaycastResult> results = new List<RaycastResult>();
        gr.Raycast(pointerEventData, results);

        if (hasToken)
        {
            if (results.Count > 0)
            {
                Transform raycastCharacter = results[0].gameObject.transform;

                if (raycastCharacter != currentCharacter)
                {
                    if (currentCharacter != null)
                    {
                    }
                    SetCurrentCharacter(raycastCharacter);
                }
            }
        }

    }

    void SetCurrentCharacter(Transform t)
    {

        if (t != null)
        {
            t.Find("selectedBorder").GetComponent<Image>().color = Color.white;
            t.Find("selectedBorder").GetComponent<Image>().DOColor(Color.red, .7f).SetLoops(-1);
        }

        currentCharacter = t;

        if (t != null)
        {
            int index = t.GetSiblingIndex();
            Character character = SmashCSS.instance.characters[index];
            SmashCSS.instance.ShowCharacterInSlot(0, character);
        }
        else
        {
            SmashCSS.instance.ShowCharacterInSlot(0, null);
        }
    }

    void TokenFollow(bool trigger)
    {
        hasToken = trigger;
    }*/
}
