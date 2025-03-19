using System.Collections;
using UnityEngine;

public class StoreSpawnCustomers : MonoBehaviour
{
    [SerializeField] private int _maximumCustomersOnShop;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float _spawnInterval = 5f;
    [SerializeField] private float _stayTime = 2f;
    [Header("Target waypoints")]
    [Header("[0-2]:")]
    [Header("0 - intermediate. First way point")]
    [Header("1 - Seller point")]
    [Header("2 - Exit point")]
    [SerializeField] private Transform[] _wayPoints;

    private int _currentCustomers = 0;
    

    private void Start()
    {
        CustomerPool.Instance.Initialize(this, _maximumCustomersOnShop);
        StartCoroutine(SpawnCustomers());
    }

    private IEnumerator SpawnCustomers()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnInterval);

            if (_currentCustomers < _maximumCustomersOnShop)
            {
                Customer customer = CustomerPool.Instance.GetCustomer();
                if (!ReferenceEquals(customer, null))
                {
                    customer.transform.position = spawnPoint.position;
                    customer.ResetCustomer(spawnPoint.position);
                    customer.SetWaypoints(_wayPoints, _stayTime);
                    _currentCustomers++;
                }
            }
        }
    }

    public void DecreaseCustomerCount()
    {
        _currentCustomers--;
    }
}