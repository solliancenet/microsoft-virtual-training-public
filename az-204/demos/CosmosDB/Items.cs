using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosDB
{
    public class Item
    {
        public string id;
        public string PartitionKey;
    }

    public class TodoItem : Item
    {
        public string Task;
    }

    public class Family : Item
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string LastName { get; set; }
        public Parent[] Parents { get; set; }
        public Child[] Children { get; set; }
        public Address Address { get; set; }
        public bool IsRegistered { get; set; }
        // The ToString() method is used to format the output, it's used for demo purpose only. It's not required by Azure Cosmos DB
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class Parent : Item
    {
        public string FamilyName { get; set; }
        public string FirstName { get; set; }
    }

    public class Child : Item
    {
        public string FamilyName { get; set; }
        public string FirstName { get; set; }
        public string Gender { get; set; }
        public int Grade { get; set; }
        public Pet[] Pets { get; set; }
    }

    public class Pet : Item
    {
        public string GivenName { get; set; }
    }

    public class Address : Item
    {
        public string State { get; set; }
        public string County { get; set; }
        public string City { get; set; }
    }
}
