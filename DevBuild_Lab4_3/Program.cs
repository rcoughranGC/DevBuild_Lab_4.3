using System;
using System.Collections.Generic;

namespace DevBuild_Lab4_3
{

    class Customer
    {
        private string ContactName;
        private string ContactEmail;
        private string Phone;
        private string Company;

        public void SetName(string name)
        {
            ContactName = name;
        }
        public string GetName()
        {
            return ContactName;
        }
        public void SetEmail(string email)
        {
            ContactEmail = email;
        }
        public string GetEmail()
        {
            return ContactEmail;
        }
        public void SetPhone(string phone)  //I thought it would be important to do some formatting here but decided to focus on the main ideas first.
        {
            if (string.IsNullOrEmpty(phone) || string.IsNullOrWhiteSpace(phone))
            {
                phone = "NO PHONE";
            }
            Phone = phone;
        }
        public string GetPhone()
        {
            return Phone;
        }
        public void SetCompany(string company)
        {
            Company = company;
        }
        public string GetCompany()
        {
            return Company;
        }

        public Customer(string company, string name, string email, string phone)
        {
            SetCompany(company);
            SetName(name);
            SetEmail(email);
            SetPhone(phone);
        }
        public override string ToString()
        {
            return $"{ContactName} - {Company} - {ContactEmail} - {Phone}";
        }
    }

    class Program
    {
        
        static void PrintCustomers(List<Customer> customers)
        {
            Console.WriteLine("Customer List:");
            foreach (Customer entry in customers)
            {
                Console.WriteLine($"{entry.GetName()}\n  - {entry.GetCompany()}\n  - {entry.GetEmail()}\n  - {entry.GetPhone()}");
            }
        }

        static Customer FindCustomer(List<Customer> customers, string query)
        {
            foreach (Customer entry in customers)
            {
                // Exact name match, forgiving caps differences
                // Allow for StartsWith in company search, but at least 3 characters needed to yield a result
                if (query.Length < 3)
                {
                    Console.WriteLine("Not enough characters entered, please try again");
                    return null;
                }
                else if(entry.GetName().ToLower() ==query.ToLower() || entry.GetCompany().ToLower().StartsWith(query.ToLower()))
                {
                    Console.WriteLine("\nCustomer Found!");
                    return entry;
                }
            }
            Console.WriteLine($"\n{query} not found");
            return null;
        }
        static Customer FindByPhone(List<Customer> customers, string query)
        {
            foreach (Customer entry in customers)
            {
                if (entry.GetPhone() == query)
                {
                    Console.WriteLine("\nCustomer Found!");
                    return entry;
                }
            }
            Console.WriteLine($"\n{query} not found");
            return null;
        }
        static void Main(string[] args)
        {

            // Create List of Customers

            List<Customer> customers = new List<Customer>();
            Customer cust = new Customer("Planet Express", "Phillip J. Fry", "fry@planetexpress.com", "000-000-0000"); 
            customers.Add(cust);
            cust = new Customer("Springfield Nuclear Power Plant", "Montgomery Burns", "mrburns@snpp.com", "800-252-2525");
            customers.Add(cust);
            cust = new Customer("Dunder Mifflin Paper Company", "Michael Scott", "mscott123@aol.com", "800-484-4848");
            customers.Add(cust);
            cust = new Customer("Pawnee Parks Dept", "Ron Swanson", "donotemail@ever.com", "");
            customers.Add(cust);
            cust = new Customer("Burton Trucking Co", "Jack Burton", "allinthereflexes@porkchopexpress.net", "555-550-5500");
            customers.Add(cust);
            
            //Print list of customers
            PrintCustomers(customers);

            ////Search tests - for a company
            //Console.WriteLine(FindCustomer(customers, "Planet Express"));
            //Console.WriteLine(FindCustomer(customers, "Delorean Motor Company")); //Null test

            // User input search
            bool keepGoing = true;
            while (keepGoing)
            {
                Console.WriteLine("Please enter a customer name or company to look up.");
                Console.Write("Or enter 0 to search by phone: ");
                string userInput = Console.ReadLine();
                if (userInput == "0") //Option to search by phone
                {
                    Console.Write("Enter a number in this format ###-###-####: ");
                    userInput = Console.ReadLine();
                    Console.WriteLine(FindByPhone(customers, userInput)); //Expect to see the ToString override result of a Customer, or a "not found"

                }
                else
                Console.WriteLine(FindCustomer(customers, userInput));  //Expect to see the ToString override result of a Customer, or a "not found"

                
                Console.Write("\nContinue? y/n: ");  // This was added mostly for repeated testing functionality,
                if (Console.ReadLine() != "y")       // not for user experience. Would create a full method for user.
                    keepGoing = false;
            }
        }
    }
}
