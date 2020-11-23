using Game;
using Sirenix.OdinInspector;
using UnityEngine;

public class CarLight : MonoBehaviour {

    [FoldoutGroup("Car")]
    [SerializeField]
    private Car _car;

    [FoldoutGroup("Light settings")]
    [BoxGroup("Light settings/Color")]
    [SerializeField]
    private Color _color = Color.white;

    [FoldoutGroup("Light settings")]
    [BoxGroup("Light settings/Light")]
    [SerializeField]
    private Light _spotLight;

    private void OnEnable() {
        _spotLight.range = _car.CarSettings.lightRange;
        _spotLight.spotAngle = _car.CarSettings.lightAngle;
    }
}
