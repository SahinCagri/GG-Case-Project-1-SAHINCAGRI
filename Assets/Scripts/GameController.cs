using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Threading;
using UnityEditor.Events;

public class GameController : MonoBehaviour
{
    [SerializeField]  Button[] skinButtons;
    [SerializeField]  GameObject buttonsParent;
    [SerializeField] Sprite[] sprites;
    
    Color prevColor;
    Color colorOfMechanicFrame;

    bool isCheck = false;
    int currentButtonIndex = 0;

    private void Awake()
    {
        colorOfMechanicFrame = skinButtons[0].GetComponent<Image>().color;
        prevColor= skinButtons[5].GetComponent<Image>().color;
    }

    private void Update()
    {
        if (isCheck)
        {
            MakeButtonInteractable();
            isCheck = false;
        }
    }

    void MakeButtonInteractable()
    {
        skinButtons[currentButtonIndex].interactable = true;
        skinButtons[currentButtonIndex].GetComponent<Image>().color = prevColor;
        skinButtons[currentButtonIndex].GetComponent<Image>().sprite = sprites[currentButtonIndex];
        skinButtons[currentButtonIndex].onClick.AddListener(() => OpenButtonToSee());
    }

    //THIS FUNCTION WILL BE USED FROM UNLOCK BUTTON
    public void RotateButtons()
    {
       
        CleanButtons();

        buttonsParent.transform.DORotate(new Vector3(0, 0, 360), 2f, RotateMode.FastBeyond360).SetEase(Ease.OutExpo)
         .OnComplete(() => StartCoroutine(RandomPositionForTheGreenFrame()));
         
    }

    IEnumerator RandomPositionForTheGreenFrame()
    {
        for (int i = 0; i < 30; i++)
        {
            skinButtons[currentButtonIndex].GetComponent<Image>().color = prevColor;
            currentButtonIndex = Random.Range(0, 9);
            skinButtons[currentButtonIndex].GetComponent<Image>().color = colorOfMechanicFrame;
            yield return new WaitForSeconds(0.1f);
        }
        isCheck = true;
    }

   
    void CleanButtons()
    {
        for (int i = 0; i < skinButtons.Length; i++)
        {
            skinButtons[i].interactable = false;
            skinButtons[i].GetComponent<Image>().color = prevColor;
            skinButtons[i].GetComponent<Image>().sprite = null;
        }
        UnityEventTools.RemovePersistentListener(skinButtons[currentButtonIndex].onClick, OpenButtonToSee);
        skinButtons[currentButtonIndex].transform.GetChild(0).gameObject.SetActive(true);
        skinButtons[currentButtonIndex].transform.GetChild(1).gameObject.SetActive(true);
    }

     void OpenButtonToSee()
    {
        skinButtons[currentButtonIndex].transform.GetChild(0).gameObject.SetActive(false);
        skinButtons[currentButtonIndex].transform.GetChild(1).gameObject.SetActive(false);
    }
}
