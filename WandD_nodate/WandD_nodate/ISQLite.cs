using System;
using System.Collections.Generic;
using System.Text;

namespace WandD_nodate
{
    public interface ISQLite
    {
        string GetDatabasePath(string filename);
    }
}
