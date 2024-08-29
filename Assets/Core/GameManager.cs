using System;
using System.Collections;
using Core.Character;
using Unity.Mathematics;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core
{
    public class GameManager : SingletonBase<GameManager>
    {
       

      

        public void Start()
        {
            

        }

       


        public void FreezeTime(float duration)
        {
            Time.timeScale = 0.1f;
            StartCoroutine(UnfreezeTime(duration));
        }
        
        private IEnumerator UnfreezeTime(float duration)
        {
            yield return new WaitForSeconds(duration);
            Time.timeScale = 1.0f;
        }

       
        
    }
}