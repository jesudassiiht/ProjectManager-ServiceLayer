using System;

namespace PrjManager.Infrastructure.Logging
{
    public interface ILogger
    {
        void Info(string message);
        void InfoFormat(string format, params object[] values);
        Guid Error(string message);
        Guid Error(Exception ex);
    }
}
