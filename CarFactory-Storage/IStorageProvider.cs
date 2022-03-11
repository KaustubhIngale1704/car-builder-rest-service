using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFactory_Factory
{
    public interface IStorageProvider
    {
        public SQLiteConnection GetConnection();
    }
}
