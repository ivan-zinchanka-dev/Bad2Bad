using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Controllers
{
    public class HealthBody : MonoBehaviour
    {
        [SerializeField] private int _maxHealth = 100;
        [SerializeField] private UIBehaviour _view;
        [SerializeField] private UnityEvent<int> _onHealthUpdate;
        [SerializeField] private UnityEvent _onDeath;
        
        private int _health;
        
        public int Health => _health;
        public UnityEvent<int> OnHealthUpdate => _onHealthUpdate;
        public UnityEvent OnDeath => _onDeath;

        private void Awake()
        {
            RestoreHealth();
        }

        public void RestoreHealth()
        {
            _health = _maxHealth;
        }
        
        public void MakeDamage(int damage)
        {
            _health = Mathf.Clamp(_health - damage, 0, _maxHealth);
            
            OnHealthValueChanged();
        }

        private void OnHealthValueChanged()
        {
            UpdateView();
            
            _onHealthUpdate?.Invoke(_health);
            
            if (_health <= 0)
            {
                _onDeath?.Invoke();
                
                _onHealthUpdate?.RemoveAllListeners();
                _onDeath?.RemoveAllListeners();
            }
        }
        
        public void SetView(UIBehaviour view)
        {
            _view = view;
        }

        private void UpdateView()
        {
            if (_view == null)
            {
                return;
            }

            if (_view is Slider slider)
            {
                slider.maxValue = _maxHealth;
                slider.value = _health;
            }
            else if (_view is Image image && image.type == Image.Type.Filled)
            {
                image.fillAmount = _health / (float)_maxHealth;
            }
            else if (_view is TextMeshProUGUI textMesh)
            {
                textMesh.text = string.Format("{0:d}/{1:d}", _health, _maxHealth);
            }
        }

    }
}