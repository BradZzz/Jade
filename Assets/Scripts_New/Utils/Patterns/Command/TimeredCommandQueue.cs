﻿using System.Collections;
using UnityEngine;

namespace Patterns
{
    public class TimeredCommandQueue<T, T1> : CommandQueue<T, T1> 
        where T : MonoBehaviour
        where T1 : Command
    {
        [Tooltip("Time until dequeue the next command.")]
        [SerializeField] [Range(0, 1f)] float dequeueTime = 0.5f;
        
        /// <summary>
        ///     Whether the component is operating or not.
        /// </summary>
        public bool IsActive { get; private set; }
        
        #region Corotines
        
        private Coroutine Enqueueing { get; set; }
        private Coroutine Dequeuing { get; set; }
        private Coroutine Priority { get; set; }
        
        #endregion

        //--------------------------------------------------------------------------------------------------------------

        #region Operations
        
        /// <summary>
        ///     Enqueue a command after a determined amount of time.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="timeToEnqueue"></param>
        public void EnqueueWithDelay(T1 command, float timeToEnqueue)
        {
            Enqueueing = StartCoroutine(TimeredEnqueue(command, timeToEnqueue));
        }
        
        
        /// <summary>
        ///     
        /// </summary>
        /// <param name="command"></param>
        public override void Enqueue(T1 command)
        {
            base.Enqueue(command);

            if (IsActive)
                return;
            
            UnQueueAll();
            IsActive = true;
        }

        public void UnQueueAll()
        {
            if (!IsEmpty)
                StartCoroutine(KeepDequeuing(0));
        }
        
        #endregion
        
        //--------------------------------------------------------------------------------------------------------------
        
        #region Corotines
        
        private IEnumerator KeepDequeuing(float delay)
        {
            yield return new WaitForSeconds(delay);

            Dequeue();
            if (!IsEmpty)
                StartCoroutine(KeepDequeuing(dequeueTime));
            else
                IsActive = false;
        }
        
        private IEnumerator TimeredEnqueue(T1 command, float time)
        {
            yield return new WaitForSeconds(time);
            Enqueue(command);
        }
        
        #endregion
        
        public void Reset()
        {
            if (Enqueueing != null)
                StopCoroutine(Enqueueing);
            if (Dequeuing != null)
                StopCoroutine(Dequeuing);
            if (Priority != null)
                StopCoroutine(Priority);

            Enqueueing = null;
            Dequeuing = null;
            Priority = null;

            Commands.Clear();
        }
    }
}