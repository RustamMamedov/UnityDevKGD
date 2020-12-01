using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Events;
using Game;
using Sirenix.OdinInspector;

namespace UI {

    public class SattingsScreen : MonoBehaviour {

        [SerializeField]
         private EventListeners _updateEventListeners;

        [SerializeField]
        private ScriptableFloatValue _valueSounds;

        [SerializeField]
        private ScriptableIntValue _difficultyValue;

        [SerializeField]
        private ScriptableIntValue _lightValue;

        [SerializeField]
        private Button _buttonOK;

        [SerializeField]
        private Button _buttonCanel;

        [SerializeField]
        private EventDispatcher _SaveEventDispatcher;

        [BoxGroup("Sliders")]
        [SerializeField]
        private Slider _soundSlider;

        [BoxGroup("Sliders")]
        [SerializeField]
        private Slider _difficultySlider;

        [BoxGroup("Sliders")]
        [SerializeField]
        private Slider _lightSlider;

        private struct CanelParametrs {
            public int difficult;
            public int light;
            public float sound;
            public Vector2 positionSoundFlag,
                positionDifficultFlag,
                positionLightFlag;
        }
        private CanelParametrs _canelParametrs;

        private void Awake() {
            _canelParametrs = new CanelParametrs();
            ButtonsClick();
        }

        private void UpdateBehavour() {
            _lightValue.Value = (int) _lightSlider.value;
            _difficultyValue.Value = (int)_difficultySlider.value;
            _valueSounds.Value = _soundSlider.value;
        }

        private void OnEnable() {
            LoadParametrs();
            _updateEventListeners.OnEventHappened += UpdateBehavour;
            CanelParametrsOnEnable();
        }

        private void OnDisable() {
            _updateEventListeners.OnEventHappened -= UpdateBehavour;
        }

        private void ButtonsClick() {
            _buttonOK.onClick.AddListener(ClikOK);
            _buttonCanel.onClick.AddListener(ClicCanel);

        }

        private void ClikOK() {
            _SaveEventDispatcher.Dispatch();
            StartCoroutine(OnSattingsButtonClickCoroutine());
        }

        private void ClicCanel() {
            _difficultySlider.value = _canelParametrs.difficult;
            _soundSlider.value = _canelParametrs.sound;
            _lightSlider.value = _canelParametrs.light;
            UpdateBehavour();
            StartCoroutine(OnSattingsButtonClickCoroutine());
        }

        private IEnumerator OnSattingsButtonClickCoroutine() {
            yield return new WaitForSeconds(0.2f);
            UIManager.Instance.ShowMenuScreen();
        }

        private void CanelParametrsOnEnable() {
            _canelParametrs.difficult = (int)_difficultySlider.value;
            _canelParametrs.sound = _soundSlider.value;
            _canelParametrs.light = (int) _lightSlider.value;
        }

        private void LoadParametrs() {
            _difficultySlider.value = _difficultyValue.Value;
            _soundSlider.value = _valueSounds.Value;
            _lightSlider.value = _lightValue.Value;
        }

        //        #region My_Version

        //        [SerializeField]
        //        private Image _lenhthSoundSettings;

        //        [SerializeField]
        //        private Image _flagSound;

        //        [SerializeField]
        //        private Image _flagDifficulty;

        //        [SerializeField]
        //        private Image _flagLight;

        //        [SerializeField]
        //        private Image _lengthDifficulty;

        //        [SerializeField]
        //        private Image _lengthLight;

        //        [SerializeField]
        //        private ScriptableFloatValue _valueSounds;

        //        [SerializeField]
        //        private ScriptableIntValue _difficultyValue;

        //        [SerializeField]
        //        private ScriptableIntValue _lightValue;

        //        [SerializeField]
        //        private Button _buttonOK;

        //        [SerializeField]
        //        private Button _buttonCanel;

        //        private class Cursor {
        //            public float x = 0f;
        //            public float y = 0f;
        //        }
        //        private Cursor _cursor;

        //        private bool _check;

        //        private void Awake() {
        //            _cursor = new Cursor();
        //            _canelParametrs = new CanelParametrs();
        //        }

        //        private void OnEnable() {
        //            SubscribeEvents();
        //            _check = false;
        //            CanelParametrsFromEnable();
        //            EnableButton();
        //        }

        //        private void OnDisable() {
        //            UnSubscribeEvents();
        //        }

        //        private void SubscribeEvents() {
        //            _updateEventListeners.OnEventHappened += UpdateBehavour;
        //        }

        //        private void UnSubscribeEvents() {
        //            _updateEventListeners.OnEventHappened -= UpdateBehavour;
        //        }

        //        private void UpdateBehavour() {
        //            OnTouchPosition();
        //            SliderSounds();
        //            ChoiseInVariable( _lengthDifficulty, _flagDifficulty, _difficultyValue);
        //            ChoiseInVariable( _lengthLight, _flagLight, _lightValue);
        //        }

        //        #region TouchPosition
        //        private void OnTouchPosition() {
        //#if UNITY_EDITOR
        //            if (!Input.GetMouseButton(0)) {
        //                _check = false;
        //                return;
        //            }
        //            _check = true;
        //            var touchPosition = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        //#else
        //            if (Input.touchCount < 1) {
        //                return;
        //            }
        //            touchPosition = Camera.main.ScreenToWorldPoint(new Vector2(Input.touches[0].position.x,Input.touches[0].position.y));
        //#endif
        //            _cursor.x = touchPosition.x;
        //            _cursor.y = touchPosition.y;
        //        }
        //        #endregion TouchPosition

        //        #region SoundSettings

        //        private Vector2 SizeInWorldCoord(Image image) {
        //            var value = Camera.main.ScreenToWorldPoint(image.rectTransform.rect.size + new Vector2(Screen.width / 2, Screen.height / 2)) / 2;
        //            return new Vector2 (value.x,value.y);
        //        }
        //        private void SliderSounds() {
        //            var size = SizeInWorldCoord(_flagSound);
        //            if (_check&&_cursor.x < _flagSound.transform.position.x+size.x && _cursor.x > _flagSound.transform.position.x-size.x) {
        //                if (_cursor.y < _flagSound.transform.position.y + size.y && _cursor.y > _flagSound.transform.position.y- size.y) {
        //                    StartCoroutine(TouchCoroutine());
        //                }
        //            }
        //        }

        //        private IEnumerator TouchCoroutine() {
        //            var size = SizeInWorldCoord(_lenhthSoundSettings);
        //            while (_check) {
        //                var higher = _cursor.x <= _lenhthSoundSettings.transform.position.x + size.x;
        //                var unHigher = _cursor.x >= _lenhthSoundSettings.transform.position.x - size.x;
        //                if (higher && unHigher) { 
        //                    _flagSound.transform.position = new Vector2(_cursor.x, _flagSound.transform.position.y);
        //                }
        //                else {
        //                    if (!higher) {
        //                        _flagSound.transform.position = new Vector2(_lenhthSoundSettings.transform.position.x + size.x, _flagSound.transform.position.y);
        //                    }
        //                    else {
        //                        _flagSound.transform.position = new Vector2(_lenhthSoundSettings.transform.position.x - size.x, _flagSound.transform.position.y);
        //                    }
        //                }
        //                _valueSounds.Value =(_flagSound.transform.position.x+size.x) / (size.x*2+_lenhthSoundSettings.transform.position.x);
        //                if (_valueSounds.Value < .07f) _valueSounds.Value = 0;
        //                yield return null;
        //            }
        //        }
        //        #endregion

        //        #region ChoiseInVariable 

        //        private void ChoiseInVariable(Image lenght, Image flag, ScriptableIntValue value) {
        //            var size = SizeInWorldCoord(lenght);
        //            if (_check&&_cursor.x < lenght.transform.position.x + size.x && _cursor.x > lenght.transform.position.x - size.x) {
        //                if (_cursor.y < lenght.transform.position.y + size.y && _cursor.y > lenght.transform.position.y - size.y) {
        //                    TouchChoiseValue(lenght, flag, value);
        //                }
        //            }
        //        }

        //        private void TouchChoiseValue(Image lenght, Image flag, ScriptableIntValue value) {
        //            if (_cursor.x < 0) {
        //                if (flag.transform.position.x > 0) {
        //                    flag.transform.position = new Vector2(-flag.transform.position.x+lenght.transform.position.x, flag.transform.position.y);
        //                }
        //                value.Value = 0;
        //            }
        //            else {
        //                value.Value = 1;
        //                if (flag.transform.position.x < 0) {
        //                    flag.transform.position = new Vector2(-flag.transform.position.x+lenght.transform.position.x, flag.transform.position.y);
        //                }
        //            }
        //        }

        //        #endregion ChoiseInVariable

        //        #region Button

        //        private void EnableButton (){
        //            _buttonOK.onClick.AddListener(OnButtonOK);
        //            _buttonCanel.onClick.AddListener(OnButtonCanel);
        //        }

        //        private void OnButtonOK() {
        //            _SaveEventDispatcher.Dispatch();
        //            UIManager.Instance.ShowMenuScreen();
        //        }

        //        private void OnButtonCanel() {
        //            _difficultyValue.Value = _canelParametrs.difficult;
        //            _valueSounds.Value = _canelParametrs.sound;
        //            _lightValue.Value = _canelParametrs.light;
        //            _flagDifficulty.transform.position = _canelParametrs.positionDifficultFlag;
        //            _flagSound.transform.position = _canelParametrs.positionSoundFlag;
        //            _flagLight.transform.position = _canelParametrs.positionLightFlag;
        //            UIManager.Instance.ShowMenuScreen();
        //        }

        //        private void CanelParametrsFromEnable() {
        //            _canelParametrs.difficult = _difficultyValue.Value;
        //            _canelParametrs.sound = _valueSounds.Value;
        //            _canelParametrs.light = _lightValue.Value;
        //            _canelParametrs.positionSoundFlag = _flagSound.transform.position;
        //            _canelParametrs.positionDifficultFlag = _flagDifficulty.transform.position;
        //            _canelParametrs.positionLightFlag = _flagLight.transform.position;
        //        }

        //        #endregion Button

        //    

        //    #endregion My_Version
    }
}
