using TMPro;
using UnityEngine;

public class ResourseView : MonoBehaviour
{
    [SerializeField] private Base _base;
    [SerializeField] private TextMeshProUGUI _textView;

    private void OnEnable()
    {
        _base.OnResourseCountChanged += Show;
    }

    private void OnDisable()
    {
        _base.OnResourseCountChanged -= Show;
    }

    private void Show(int amount)
    {
        _textView.text = amount.ToString();
    }
}
