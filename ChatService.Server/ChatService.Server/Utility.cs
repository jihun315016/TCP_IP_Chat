using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatService.Server
{
    class Utility
    {
        public static T GetAppSettings<T>(IConfiguration configuration, string section, string key)
        {
            IConfigurationSection serverInfo = configuration.GetSection(section);
            T value;

            try
            {
                value = serverInfo.GetValue<T>(key);
                if (EqualityComparer<T>.Default.Equals(value, default))
                {
                    // value가 T 타입의 기본값인 경우 처리할 내용
                    return default;
                }
                else
                {
                    // value가 T 타입의 기본값이 아닌 경우 처리할 내용
                    return value;
                }
            }
            catch (Exception ex)
            {
                return default;
            }
        }
    }
}
