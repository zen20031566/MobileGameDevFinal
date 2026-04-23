using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CosmeticUIElement : MonoBehaviour
{
    [SerializeField] private RawImage cosmeticImage;
    [SerializeField] private TMP_Text costText;
    [SerializeField] private GameObject equippedIcon;

    private CosmeticData cosmeticData; 

    public void Init(CosmeticData cosmeticData)
    {
        this.cosmeticData = cosmeticData;
        costText.text = cosmeticData.Cost.ToString();
    }


}
