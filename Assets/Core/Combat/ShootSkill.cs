//
// using System.Collections.Generic;
// using MainScript;
// using Unity.VisualScripting;
// using UnityEngine;
// using UnityEngine.Serialization;
// using UnrealM;
//
// public class ShootSkill : MonoBehaviour
// {
//     private float timer = 0;
//     public float rate = 10;//子弹发射速率
//     public Transform OrginAimTarget;
//     public Transform[] bulletPoints;//子弹发射点
//     public RectTransform Crosshair;//准星
//     public Transform CurrentAimTarget;
//     public float offset = 0.5f;
//     private EntityData entityData;
//     public GameObject bulletPrefab;
//     private AimAssist _aimAssist;
//     [HideInInspector]
//     public Vector3 ShootTarget;
//     public Transform MissilePoint;
//     [HideInInspector]
//     public Transform[] MissilePoints;
//     public GameObject MissilePrefab;
//     public GameObject SidePlanePrefab;
//     [HideInInspector]
//     public Vector3 target;
//
//     private Ray Aimray;
//
//     public Transform[] SidePlanePoints;
//
//     private void Start()
//     {
//       entityData = GetComponent<EntityData>();
//       _aimAssist = GetComponent<AimAssist>();
//       MissilePoints = MissilePoint.GetComponentsInChildren<Transform>(); 
//     }
//
//
//
//     private void Update()
//     {
//         if(!entityData.CanInput)
//             return;
//         Shoot(bulletPoints);
//         if (Input.GetKeyDown(KeyCode.Q))
//         {
//             MissileShoot(MissilePoints);
//         }
//
//         if (Input.GetKeyDown(KeyCode.E))
//         {
//             MissileShoot(MissilePoints);
//         }
//     }
//     
//     public void Shoot(Transform[] Points)
//     {
//         var pos = ShootTarget;
//         Aimray = new Ray(entityData.GamingCam.transform.position,
//             pos - entityData.GamingCam.transform.position);
//         if (_aimAssist.Target)
//         {
//             target = _aimAssist.Target.position;
//         }
//         else
//         {
//             target = Aimray.GetPoint(200);
//         }
//         if (Input.GetKey(KeyCode.Space))//按下空格
//         {
//             timer += Time.deltaTime;//计时器计时
//             if (timer > 1f / rate)//如果计时大于子弹的发射速率（rate每秒几颗子弹）
//             {
//               BulletShoot(Points,target);
//                 timer = 0;//时间清零
//                  
//             }
//         }
//     }
//
//     public void BulletShoot(Transform[] Points,Vector3 target)
//     {
//         foreach (var shootpoint in Points)
//         {
//             var bullet =
//                 ObjectPoolManager.Instance.SpawnObject<bullet>(bulletPrefab,shootpoint.position,shootpoint.rotation, ObjectPoolManager.PoolType.Gameobject);
//                    
//                     
//             bullet.transform.position = shootpoint.position;
//                     
//             bullet.transform.LookAt(target); //子弹的Z轴朝向目标
//             Quaternion offsetRotation = Quaternion.Euler(0, offset, 0); // 创建偏移旋转
//             if (shootpoint.localPosition.x < 0)
//             {
//                 bullet.transform.rotation *= Quaternion.Inverse(offsetRotation);
//             }
//             else
//             {
//                 bullet.transform.rotation *= offsetRotation;
//             }
//             bullet.OnSpawned();
//         }
//     }
//
//     public void MissileShoot(Transform[] MissilePoints)
//     {
//         var Targets = Physics.OverlapBox(CurrentAimTarget.position+CurrentAimTarget.forward*100, new Vector3(300, 300, 300), Quaternion.identity, 1 << LayerManager.AimTarget);
//         if (MissilePoints != null && MissilePoints.Length>0)
//         {
//             for (int i = 1; i < MissilePoints.Length; i++)
//             {
//                 var missile =  ObjectPoolManager.Instance.SpawnObject<GuidedMissile>(MissilePrefab,MissilePoints[i].position,MissilePoints[i].rotation, ObjectPoolManager.PoolType.Gameobject);
//                 if (Targets.Length == 0)
//                 {
//                     missile.OnSpawned(null);
//                 }
//                 else if (i-1 <  Targets.Length)
//                 {
//                     missile.OnSpawned(Targets[i-1].transform);
//                 }
//                 else
//                 {
//                     missile.OnSpawned(Targets[Random.Range(0,Targets.Length)].transform);
//                 }
//                 
//             }
//         }
//     }
//
//     // public void GeneraSidePlane()
//     // {
//     //     foreach (Transform sidePlanePoint in SidePlanePoints)
//     //     {
//     //         var sideplane = ObjectPoolManager.Instance.SpawnObject<SidePlaneShoot>(SidePlanePrefab,sidePlanePoint.position,
//     //             Quaternion.identity, ObjectPoolManager.PoolType.Gameobject);
//     //         sideplane.Init(this,sidePlanePoint);
//     //     }
//     // }
//     //
//     // public void SidePlaneQuit()
//     // {
//     //     foreach (Transform sidePlanePoint in SidePlanePoints)
//     //     {
//     //         var sideplane = sidePlanePoint.GetComponentInChildren<SidePlaneShoot>();
//     //        if (sideplane != null)
//     //        {
//     //            sideplane.Quit();
//     //        }
//     //     }
//     //}
//     
//
// }
