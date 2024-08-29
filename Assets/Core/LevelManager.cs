using System;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using Core.Character;
using Core.Combat;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace Core
{
    public class LevelManager : MonoBehaviour
    {
        public Transform cameraBorders;
        public Transform PlayerSpawnPosition;
        public Transform BossSpawnPosition;
        public PlayerController PlayerPrefab;
        public GameObject BossPrefab;
        public Collider2D Area;

        private PlayerController player;
        private GameObject boss;
        public GameObject button;
        private EventBingding<GameEndEvent> gameEndEventBingding;
        private void Start()
        {
            Init();
            button.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 250);
            CameraController.Instance.SetBorders(cameraBorders);
            gameEndEventBingding = new EventBingding<GameEndEvent>(GameEnd);
            EventBus<GameEndEvent>.Register(gameEndEventBingding);
        }
        public void Init()
        {
            player =Object.Instantiate<PlayerController>(PlayerPrefab, PlayerSpawnPosition.position, quaternion.identity);
            boss =Instantiate(BossPrefab, BossSpawnPosition.position, quaternion.identity);
            boss.GetComponent<Destructable>().Area = Area;
            CameraController.Instance.player = player;
        }
        public void ReStartGame()
        {
            button.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0,250), 2);
            if(player)
                Object.Destroy(player.gameObject);
            if(boss)
                Object.Destroy(boss);
            Init();
        }

        public void GameEnd()
        {
            button.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0,0), 2);
        }

        public void OnDestroy()
        {
            EventBus<GameEndEvent>.DeRegister(gameEndEventBingding);
        }
    }

    public struct GameEndEvent : IEvent
    {
        
    }
}