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
    [BoxGroup("Light settings/Width and height")]
    [SerializeField]
    [Range(0, 100)]
    private float _fov;

    [FoldoutGroup("Light settings")]
    [BoxGroup("Light settings/Width and height")]
    [SerializeField]
    [Range(0, 1)]
    private float _aspect;

    private void OnEnable() {

    }
}
