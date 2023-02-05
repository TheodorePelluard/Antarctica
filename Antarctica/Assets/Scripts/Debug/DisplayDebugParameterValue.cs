using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DisplayDebugParameterValue : MonoBehaviour
{
    [SerializeField] InputActionReference ToggleDebugMenuAction;
    [SerializeField] bool _toggleDebugMenu = false;

    public List<BoolGameParameter> BoolParameters;
    public List<FloatGameParameter> FloatParameters;
    [Space]
    [SerializeField] Transform _content;
    [SerializeField] TextMeshProUGUI _debugCanvasValue;
    List<TextMeshProUGUI> _debugBoolValues;
    List<TextMeshProUGUI> _debugFloatValues;

    private void Start()
    {
        _content.gameObject.SetActive(_toggleDebugMenu);

        _debugBoolValues = new List<TextMeshProUGUI>();
        _debugFloatValues = new List<TextMeshProUGUI>();

        for (int i = 0; i < BoolParameters.Count; i++)
        {
            TextMeshProUGUI debugValue = Instantiate(_debugCanvasValue, _content);
            _debugBoolValues.Add(debugValue);
        }

        for (int i = 0; i < FloatParameters.Count; i++)
        {
            TextMeshProUGUI debugValue = Instantiate(_debugCanvasValue, _content);
            _debugFloatValues.Add(debugValue);
        }

        ToggleDebugMenuAction.action.performed += ToggleDebugMenu; 
    }


    private void Update()
    {
        if (!_toggleDebugMenu)
            return;

        for (int i = 0; i < BoolParameters.Count; i++)
        {
            _debugBoolValues[i].text = BoolParameters[i].name + ": " + BoolParameters[i].Get();
        }

        for (int i = 0; i < FloatParameters.Count; i++)
        {
            _debugFloatValues[i].text = FloatParameters[i].name + ": " + FloatParameters[i].Get();
        }
    }

    void ToggleDebugMenu(InputAction.CallbackContext context)
    {
        _toggleDebugMenu = !_toggleDebugMenu;
        _content.gameObject.SetActive(_toggleDebugMenu);
    }
}
