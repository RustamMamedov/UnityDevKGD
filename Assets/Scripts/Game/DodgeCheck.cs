using Events;
using Game;
using UnityEngine;

public class DodgeCheck : MonoBehaviour {

    [SerializeField]
    private EventDispatcher _carDodgeEventDispatcher;

    [SerializeField]
    private Cars _dodgedCars;

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("EnemyCar")) {
            _dodgedCars.carsList.Add(other.gameObject);
            _carDodgeEventDispatcher.Dispatch();
        }
    }
}
