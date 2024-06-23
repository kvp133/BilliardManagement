using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilliardManagement.Models
{
    public class DataProvider
    {
        private static DataProvider instance;

        public static DataProvider Instance
        {
            get
            {
                if (instance == null)
                    instance = new DataProvider();
                return instance;
            }
            private set { instance = value; }
        }
        public BilliardManagementContext DB { get; set; } 
        private DataProvider() { 
            DB = new BilliardManagementContext();

        }

        
    }
}
