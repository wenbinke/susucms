using System;
using SusuCMS.Data.Enums;
using SusuCMS.Data;


namespace SusuCMS.Helpers
{
    public static class LogHelper
    {
        public static void AddSystemLog(LogType logType, string message)
        {
            using (var dataContext = new DataContext())
            {
                dataContext.SystemLogs.Add(new SystemLog
                {
                    LogType = (int)logType,
                    Message = message,
                    CreationTime = DateTime.Now
                });
                dataContext.SaveChanges();
            }
        }

        public static void AddSystemErrorLog(string message)
        {
            AddSystemLog(LogType.Error, message);
        }
    }
}
