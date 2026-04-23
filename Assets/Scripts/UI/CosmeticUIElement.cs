using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CosmeticUIElement : MonoBehaviour
{
    [SerializeField] private RawImage cosmeticImage;
    [SerializeField] private TMP_Text costText;
    [SerializeField] private GameObject equippedIcon;
    public Button Button;

    public CosmeticData CosmeticData {  get; private set; }

    public void Init(CosmeticData cosmeticData)
    {
        this.CosmeticData = cosmeticData;
        costText.text = cosmeticData.Cost.ToString();
        cosmeticImage.texture = cosmeticData.Image;
        equippedIcon.SetActive(false);
    }

    public void SetOwned(bool value)
    {
        costText.gameObject.transform.parent.gameObject.SetActive(!value);
        //costText.gameObject.SetActive(!value);
    }

    public void SetEquipped(bool value)
    {
        equippedIcon?.SetActive(value);
    }

}
