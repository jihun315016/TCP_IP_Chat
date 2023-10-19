using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Loggings
{
    public class UnifiedLogger
    {
        private readonly ILogger<Worker> _logger;
        private readonly Logging _log4net;  

        public UnifiedLogger(ILogger<Worker> logger, Logging log4net)
        {
            _logger = logger;
            _log4net = log4net;
        }

        #region 디버그 정보 쓰기
        public void WriteDebug(string message)
        {
            _logger.LogDebug(message);
            _log4net.WirteDebug(message);
        }       

        public void WriteDebug(Exception ex)
        {
            string message = $"[Error] {ex.Message}{Environment.NewLine}{ex.StackTrace}";
            _logger.LogDebug(message);
            _log4net.WirteDebug(message);
        }
        #endregion

        #region 실행 정보 쓰기
        public void WriteInfo(string message)
        {
            _logger.LogInformation(message);
            _log4net.WirteInfo(message);
        }

        public void WriteInfo(Exception ex)
        {
            string message = $"[Error] {ex.Message}{Environment.NewLine}{ex.StackTrace}";
            _logger.LogInformation(message);
            _log4net.WirteInfo(message);
        }
        #endregion

        #region 경고 로그 쓰기
        public void WirteWarn(string message)
        {
            _logger.LogWarning(message);
            _log4net.WirteWarn(message);
        }

        public void WirteWarn(Exception ex)
        {
            string message = $"[Error] {ex.Message}{Environment.NewLine}{ex.StackTrace}";
            _logger.LogWarning(message);
            _log4net.WirteWarn(message);
        }
        #endregion

        #region 오류 로그 쓰기
        public void WriteError(string message)
        {
            _logger.LogError(message);
            _log4net.WirteError(message);
        }

        public void WriteError(Exception ex)
        {
            string message = $"[Error] {ex.Message}{Environment.NewLine}{ex.StackTrace}";
            _logger.LogError(message);
            _log4net.WirteError(message);
        }
        #endregion

        #region 치명적인 오류 쓰기
        public void WirteFatal(string message)
        {
            _logger.LogCritical(message);
            _log4net.WirteFatal(message);
        }

        public void WirteFatal(Exception ex)
        {
            string message = $"[Error] {ex.Message}{Environment.NewLine}{ex.StackTrace}";
            _logger.LogCritical(message);
            _log4net.WirteFatal(message);
        }
        #endregion
    }
}
