using System;
using System.Collections;
using System.Collections.Generic;
using New_version;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class BulletRecoil : MonoBehaviour
{
     [SerializeField] private float _bulletForce = 800f;
     [SerializeField] private BulletData _bulletData;

    
     private void OnTriggerEnter(Collider other)
     {

         if (other.gameObject.layer == 8) // enemy 
         {
             _bulletData.Rb = other.gameObject.GetComponent<Rigidbody>();
             _bulletData.Direction = transform.forward;
             
         }
     }
}
