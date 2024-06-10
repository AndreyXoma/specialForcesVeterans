using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace specialForcesVeterans.model
{
    public class Zakaz
    {
        private int id;
        private string name;
        private string fullnameClient;
        private string phone;
        private string time;
        private string typeObject;
        private string adress;
        private string typeService;
        private string email;
        private string description;

        public Zakaz(string name, string fullnameClient, string phone, string time, string typeObject, string adress, string typeService, string email, string description)
        {
            this.name = name;
            this.fullnameClient = fullnameClient;
            this.phone = phone;
            this.time = time;
            this.typeObject = typeObject;
            this.adress = adress;
            this.typeService = typeService;
            this.email = email;
            this.description = description;
        }
        public int getId() { return id; }
        public string getName() { return name; }
        public string getFullNameClient() { return fullnameClient; }
        public string getPhone() { return phone; }
        public string getTypeObject() { return typeObject;}
        public string getAdress() { return adress; }
        public string getTypeService() { return typeService;}
        public string getEmail() { return email; }
        public string getDescription() { return description; }
        public string getTime()
        {
            return time;
        }

        public void setTime(string time)
        {
            this.time = time;
        }
        public void setId(int id) { this.id = id;}
        public void setName(string name) { this.name = name;}
        public void setFullNameClient(string fullNameClient) { this.fullnameClient=fullNameClient;}
        public void setPhone(string phone) { this.phone = phone;}
        public void setTypeObject(string typeObject) { this.typeObject = typeObject; }
        public void setAdress(string adress) { this.adress = adress; }
        public void setTypeService(string typeService) { this.typeService=typeService; }
        public void setEmail(string email) { this.email= email;}
        public void setDescription(string description) { this.description= description;}
        
    }
}
