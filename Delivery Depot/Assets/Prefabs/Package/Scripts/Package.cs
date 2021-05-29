using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Package : PhysicsObject
{
    Customer[] customers;
    bool notDead = true;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        StartCoroutine(DespawnPackage());
        

        
    }

    private void Update()
    {
        customers = GameObject.FindObjectsOfType<Customer>();
        if (customers.Length >= 0 && notDead)
            foreach(Customer c in customers)
                if(DistCheck(c) <= 1f)
                {
                    notDead = false;
                    StartCoroutine(SecuredPackage(c));
                }
                    
    }

    float DistCheck(Customer c)
    {
        float dist;
        dist = (c.transform.position - this.transform.position).magnitude;
        return Mathf.Abs(dist);
    }
    IEnumerator SecuredPackage(Customer c)
    {
        Debug.Log("Secured Package");
        FindObjectOfType<ManageGame>().AddScore(10);
        yield return new WaitForSeconds(.2f);
        c.BeginDeath();
        Destroy(this.gameObject);
        
        
        
    }

    public IEnumerator DespawnPackage()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }
    
}
