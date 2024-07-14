using log4net;
using log4net.Config;

namespace TestProject1.Core;
     
public static class MyLogger
{    
    private static ILog? logger;
    public static ILog Logger
    {
        get 
        {
            if (logger == null)
                XmlConfigurator.Configure(new FileInfo("Log.config"));
                logger = LogManager.GetLogger("my_log");
            return logger;
        }
    }

} 