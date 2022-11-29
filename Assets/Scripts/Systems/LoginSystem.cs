using System.Text;
using Unity.Entities;
using UnityEngine;
using System.Threading.Tasks;
using UnityGoogleDrive.Data;

namespace ECS_Project
{
    public class LoginSystem : ComponentSystem
    {
        private EntityQuery _entityQueryLoginData;
        private EntityQuery _entityQueryPlayerStatsData;

        private SavedPlayerStatsData _savedPlayerStatsData = new SavedPlayerStatsData();
        private PlayerDatabase _playerDatabase = new PlayerDatabase();
        private File _file;
        private bool _result;
        private readonly string fileID = "1AQBETlFTKMfFV4ZxXb9HB_sLZZUeJFKb"; 
        protected override async void OnStartRunning()
        {
            _entityQueryLoginData = GetEntityQuery(ComponentType.ReadOnly<LoginData>());
            _entityQueryPlayerStatsData = GetEntityQuery(ComponentType.ReadOnly<PlayerStatsData>());

            _result = await CheckDeviceID();            

            Entities.With(_entityQueryLoginData).ForEach((Entity entity, LoginButtonComponent button) =>
            {
                if (!_result) button.OnLogin += Button_OnLogin;
                else button.LoginingDone();
            });

            if (_result)
                UpdateStatsData();            
        }
        private void UpdateStatsData()
        {
            Entities.With(_entityQueryPlayerStatsData).ForEach((Entity entity, PlayerStatsComponent statsComponent) =>
            {
                statsComponent.PlayerName = _savedPlayerStatsData.PlayerName;
                statsComponent.playerStatsData.CoinValue = _savedPlayerStatsData.CoinValue;
            });
        }
        protected override void OnDestroy()
        {
            if (_savedPlayerStatsData.PlayerName.Equals(string.Empty)) return;

            Entities.With(_entityQueryPlayerStatsData).ForEach((Entity entity, PlayerStatsComponent statsComponent) =>
            {
                _savedPlayerStatsData.CoinValue = statsComponent.playerStatsData.CoinValue;
                _playerDatabase.UpdatePlayerData(_savedPlayerStatsData);
                UploadPlayerDatabase(_playerDatabase);
            });
        }

        private async Task<bool> CheckDeviceID()
        {
            _file = await GoogleDriveTools.Download(fileID);
            if (_file.Content != null)
            {
                var jsonStr = Encoding.ASCII.GetString(_file.Content);
                _playerDatabase = JsonUtility.FromJson<PlayerDatabase>(jsonStr);
                if (_playerDatabase.HavePlayer(SystemInfo.deviceUniqueIdentifier))
                {                    
                    _savedPlayerStatsData = _playerDatabase.GetPlayerStats(SystemInfo.deviceUniqueIdentifier);
                    return true;
                }
                return false;
            }
            return false;
        }

        private void Button_OnLogin(string userName)
        {                        
            if (_file.Content == null)
            {
                _savedPlayerStatsData = AddNewPlayer(ref _playerDatabase, userName);
                UploadPlayerDatabase(_playerDatabase);
            }
            else
            {
                var jsonStr = Encoding.ASCII.GetString(_file.Content);
                _playerDatabase = JsonUtility.FromJson<PlayerDatabase>(jsonStr);
                if (!_playerDatabase.HavePlayer(SystemInfo.deviceUniqueIdentifier))
                {
                    _playerDatabase = JsonUtility.FromJson<PlayerDatabase>(jsonStr);
                    _savedPlayerStatsData = AddNewPlayer(ref _playerDatabase, userName);
                    UploadPlayerDatabase(_playerDatabase);
                }
                else
                {
                    _savedPlayerStatsData = _playerDatabase.GetPlayerStats(userName);
                }
            }

            Entities.With(_entityQueryLoginData).ForEach((Entity entity, LoginButtonComponent button) =>
            {
                button.LoginingDone();
            });

            UpdateStatsData();
        }

        private async void UploadPlayerDatabase(PlayerDatabase playerDatabase)
        {
            var jsonValue = JsonUtility.ToJson(playerDatabase);
            _file = await GoogleDriveTools.UploadFileData(_file, jsonValue);
            OnDoneUpload();
        }

        private SavedPlayerStatsData AddNewPlayer(ref PlayerDatabase playerDatabase, string name)
        {
            SavedPlayerStatsData savedPlayerStatsData = new SavedPlayerStatsData();
            savedPlayerStatsData.PlayerName = name;
            savedPlayerStatsData.CoinValue = 0;
            savedPlayerStatsData.DevaceID = SystemInfo.deviceUniqueIdentifier;
            playerDatabase.AddNewUser(savedPlayerStatsData);
            return savedPlayerStatsData;
        }
        private void OnDoneUpload() => Debug.Log("Data saved");


        protected override void OnUpdate()
        {

        }
    }
}
