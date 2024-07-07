using log4net;

namespace TestProject1.Core;
     
public class MyLogger
{    
    public ILog Log
    {
        get { return LogManager.GetLogger(GetType()); }
    }

}    