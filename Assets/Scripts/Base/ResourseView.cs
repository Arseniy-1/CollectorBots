using TMPro;
using UnityEngine;

public class ResourseView : MonoBehaviour
{
    [SerializeField] private Base _base;
    [SerializeField] private TextMeshProUGUI _textView;

    private void OnEnable()
    {
        _base.ResourseCountChanged += Show;
    }

    private void OnDisable()
    {
        _base.ResourseCountChanged -= Show;
    }

    private void Show(int amount)
    {
        _textView.text = amount.ToString();
    }
}
