using UnityEngine;
using UnityEngine.UI;
using System;
using Unity.Entities;
using TMPro;
using UnityEngine.Events;

namespace ECS_Project
{
    [RequireComponent(typeof(Button))]
    public class LoginButtonComponent : MonoBehaviour, IConvertGameObjectToEntity
    {
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private UnityEvent onDoneAction;
        private Button _button;        
        public event Action<string> OnLogin;
        public event Action OnAuthorizated;
        private string _username;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(TryLoad);            
        }

        private void OnEnable()
        {
            _inputField.onValueChanged.AddListener(InputValueChanged);
        }
        private void OnDisable()
        {
            _inputField.onValueChanged.RemoveListener(InputValueChanged);
        }

        private void TryLoad()
        {
            OnLogin?.Invoke(_username);
        }

        public void LoginingDone()
        {
            onDoneAction?.Invoke();
            OnAuthorizated?.Invoke();
        }

        private void InputValueChanged(string value)
        {
            _username = value;
        }

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new LoginData());
        }
    }

    public struct LoginData : IComponentData
    {
     
    }
}
