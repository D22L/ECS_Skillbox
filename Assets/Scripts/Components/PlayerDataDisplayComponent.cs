using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Entities;
using TMPro;
namespace ECS_Project
{
    public class PlayerDataDisplayComponent : MonoBehaviour, IConvertGameObjectToEntity
    {
        [SerializeField] private TextMeshProUGUI _nameField;
        [SerializeField] private TextMeshProUGUI _coinValueField;
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new PlayerDisplayData());
        }

        public void SetName(string nameValue) => _nameField.text = nameValue;
        public void SetCoin(int value) => _coinValueField.text = value.ToString();
    }

    public struct PlayerDisplayData :IComponentData{ }
}
