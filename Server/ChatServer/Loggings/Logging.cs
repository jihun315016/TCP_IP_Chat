using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Repository;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Loggings
{
    // ex)
    // Logging logging = new Logging("logs", DateTime.Now.ToString(), Level.All, 30);
    public class Logging
    {
        ILog log;
        private string logFolder;
        private string logFileName;
        private int logDurationDays;
        private RollingFileAppender roller;
        private bool runAsConsole = false;

        public Logging(string logFolder,  string logFileName, Level logLevel, int logDurationDays)
        {
            this.logFolder = logFolder;
            this.logFileName = logFileName + ".log";
            this.logDurationDays = logDurationDays;
            SetupLog4net(logLevel, logFileName);
        }

        private void SetupLog4net(Level logLevel, string logFileName)
        {
            ILoggerRepository repository;
            Hierarchy hierarchy;
            Logger logger;

            // log 파일이 저장될 폴더 세팅
            CheckAndCreateDirectory();

            // logFileName 이름에 대한 Logger 인스턴스 생성
            repository = GetRepository();

            // RollingDateAppender 타입 roller 초기화
            SetRollingDateAppender(logFolder);
            
            hierarchy = (Hierarchy)repository;
            hierarchy.Threshold = logLevel;

            logger = hierarchy.LoggerFactory.CreateLogger((ILoggerRepository)hierarchy, logFileName);
            logger.Hierarchy = hierarchy;
            logger.Repository.Configured = true;
            logger.AddAppender(roller);
            logger.Level = logLevel;

            log = new LogImpl(logger);   
        }

        private void CheckAndCreateDirectory()
        {
            if (!Directory.Exists(this.logFolder)) 
            {
                Directory.CreateDirectory(this.logFolder);
            }
        }

        private ILoggerRepository GetRepository()
        {
            bool exists = false;
            ILoggerRepository repository = null;
            ILoggerRepository[] repositories = LogManager.GetAllRepositories();

            // logFileName과 동일한 log 파일이 존재한다면, 해당 파일을 repository 값으로 지정
            if (repositories != null)
            {
                foreach (ILoggerRepository r in repositories)
                {
                    if (r.Name.Equals(logFileName))
                    {
                        repository = r;
                        exists = true;
                        break;
                    }
                }
            }

            // logFileName이 없다면, 해당 이름의 로그 파일 생성
            if (!exists)
            {
                repository = LogManager.CreateRepository(logFileName);
            }

            return repository;
        }

        private void SetRollingDateAppender(string logFolder)
        {
            CheckAndCloseRoller();

            roller = new RollingDateAppender()
            {
                Name = logFolder + "FileAppender",
                File = logFolder + @"\" + logFileName,
                LockingModel = new FileAppender.MinimalLock(),
                AppendToFile = true,
                RollingStyle = RollingFileAppender.RollingMode.Composite,
                DatePattern = "_yyyyMMdd",
                MaxAgeRollBackups = TimeSpan.FromDays(logDurationDays),
                MaxSizeRollBackups = -1,
                MaximumFileSize = "1MB",
                StaticLogFileName = true,
                Layout = new log4net.Layout.PatternLayout("[%level] %date %logger -> %message%newline") //[INFO] 2020-01-19 21:39:49,388 MyProject 
            };

            roller.ActivateOptions();
            BasicConfigurator.Configure(roller);
        }

        private void CheckAndCloseRoller()
        {
            if (this != null && this.roller != null && !runAsConsole)
            {
                roller.Close();
            }
        }

        /// <summary>
        /// 디버그 정보 쓰기
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public void WirteDebug(string message, Exception ex = null)
        {
            if (runAsConsole) return;

            if (ex != null)
            {
                log.Debug(message, ex);
            }
            else
            {
                log.Debug(message);
            }
        }

        /// <summary>
        /// 실행 정보 쓰기
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public void WirteInfo(string message, Exception ex = null)
        {
            if (runAsConsole) return;

            if (ex != null)
            {
                log.Info(message, ex);
            }
            else
            {
                log.Info(message);
            }
        }

        /// <summary>
        /// 경고 로그 쓰기
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public void WirteWarn(string message, Exception ex = null)
        {
            if (runAsConsole) return;

            if (ex != null)
            {
                log.Warn(message, ex);
            }
            else
            {
                log.Warn(message);
            }
        }

        /// <summary>
        /// 오류 로그 쓰기
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public void WirteError(string message, Exception ex = null)
        {
            if (runAsConsole) return;

            if (ex != null)
            {
                log.Error(message, ex);
            }
            else
            {
                log.Error(message);
            }
        }

        /// <summary>
        /// 치명적인 오류 쓰기
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public void WirteFatal(string message, Exception ex = null)
        {
            if (runAsConsole) return;

            if (ex != null)
            {
                log.Fatal(message, ex);
            }
            else
            {
                log.Fatal(message);
            }
        }
    }

    class RollingDateAppender : RollingFileAppender
    {
        public TimeSpan MaxAgeRollBackups { get; set; }

        public RollingDateAppender() : base()
        {
            PreserveLogFileNameExtension = true;
            StaticLogFileName = false;
        }

        protected override void AdjustFileBeforeAppend()
        {
            base.AdjustFileBeforeAppend();

            string path = Path.GetDirectoryName(File);
            var checkTime = DateTime.Now.Subtract(MaxAgeRollBackups); // DateTime.Now - MaxAgeRollBackups

            foreach (string file in Directory.GetFiles(path, "*.log"))
            {
                if (System.IO.File.GetLastWriteTime(file) < checkTime) 
                {
                    DeleteFile(file);
                }
            }
        }
    }
}
