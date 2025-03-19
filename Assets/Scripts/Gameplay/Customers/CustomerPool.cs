using System.Collections.Generic;
using UnityEngine;

public class CustomerPool : MonoBehaviour
{
    public static CustomerPool Instance;

    [SerializeField] private Customer _customerPrefab;
    
    private Queue<Customer> _customerPool = new Queue<Customer>();
    private StoreSpawnCustomers _storeSpawnCustomers; 

    private void Awake()
    {
        Instance = this;
    }

    public void Initialize(StoreSpawnCustomers spawner, int maxCustomers)
    {
        _storeSpawnCustomers = spawner;
        FillPool(maxCustomers);
    }

    private void FillPool(int maxCustomers)
    {
        for (int i = 0; i < maxCustomers; i++)
        {
            Customer customer = Instantiate(_customerPrefab);
            customer.gameObject.SetActive(false);
            _customerPool.Enqueue(customer);
        }
    }

    public Customer GetCustomer()
    {
        if (_customerPool.Count > 0)
        {
            Customer customer = _customerPool.Dequeue();
            customer.gameObject.SetActive(true);
            return customer;
        }
        return null;
    }

    public void ReturnCustomer(Customer customer)
    {
        if (!ReferenceEquals(customer, null))
        {
            customer.Agent.ResetPath();
            customer.IsMoving = false;

            customer.gameObject.SetActive(false);
            _customerPool.Enqueue(customer);

            _storeSpawnCustomers.DecreaseCustomerCount(); 
        }
    }
}