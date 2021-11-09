using System.Collections.Generic;
using New_version;
using UnityEngine;


[RequireComponent(typeof(Animator), typeof(ICharacter))]
public class RagdollCharacter : MonoBehaviour
{
    [SerializeField] private Rigidbody _mainRigidbody;
    [SerializeField] private BulletData _bulletData;
    
    private List<Rigidbody> _ragdollRigidbodies = new List<Rigidbody>();
    private ICharacter _character;
    private Animator _animator;
    
    

    void Start()
    {
        _character = GetComponent<ICharacter>();
        _animator = GetComponent<Animator>();
        
        if (_mainRigidbody == null)
        {
            _mainRigidbody = GetComponent<Rigidbody>();
        }

        foreach (var rb in GetComponentsInChildren<Rigidbody>())
            {
                _ragdollRigidbodies.Add(rb);
                rb.isKinematic = true;
            }

        _character.OnCharacterDie += OnCharacterDie;
    }

    private void OnCharacterDie()
    {
        _animator.enabled = false;
        
        foreach (var rb in _ragdollRigidbodies)
        {
            rb.isKinematic = false;
        }
        
        _mainRigidbody.isKinematic = true; 
       // bulletData.Rb?.AddForce(bulletData.Direction * 90f, ForceMode.Impulse);
    }
    
}

