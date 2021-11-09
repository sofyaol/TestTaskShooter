using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

public class ColorGlow : MonoBehaviour
{
    [SerializeField] private List<Material> _materials;

    [SerializeField] private Color _newColor = Color.red;

    [SerializeField] private float _duration = 0.5f;

    private List<Color> _trueColors = new List<Color>();
   
    
    void Start()
    {
        foreach (var material in _materials)
        {
            _trueColors.Add(material.color);
        }
    }

    public void MakeColorGlare()
    {
        int i = 0;
        
        foreach (var material in _materials)
        {
            var i1 = i;
            material.DOColor(_newColor, _duration).
                OnComplete(() =>
            {
                 material.DOColor(_trueColors[i1], _duration);
            });
            i++;
        }

        
    }


}
