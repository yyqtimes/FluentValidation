using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVFSystem.tools
{
    public class AutofacInstaceFactory
    {
        private static object flag;
        //资源mapping字典，建立ioc内所有已经注册资源key=>Type的映射字典
        private static Dictionary<string, Type> resourceMappingDic;

        static AutofacInstaceFactory()
        {
            flag = new object();
            resourceMappingDic = new Dictionary<string, Type>();
        }
        private AutofacInstaceFactory() { }
        /// <summary>
        /// 获取自定资源类型的实体
        /// 泛型方法
        /// add by ys on 2014-9-25
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetInstance<T>() where T : class
        {
            lock (flag)
            {
                return ServiceLocator.Current.GetInstance<T>();
            }
        }

        /// <summary>
        /// 获取自定资源类型的实体
        /// 非泛型方法
        /// add by ys on 2014-9-25
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static object GetInstance(Type serviceType)
        {
            if (null == serviceType) throw new ArgumentException("type can not be null");
            lock (flag)
            {
                return ServiceLocator.Current.GetInstance(serviceType);
            }
        }

        /// <summary>
        /// 根据资源名获取指定类型的实体
        /// 适用于需要根据类型名获取对应类型实体
        /// add by ys on 2014-9-26
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        public static T GetInstance<T>(string key) where T : class
        {
            if (!key.Equals(typeof(T).Name)) return null;
            lock (flag)
            {
                return ServiceLocator.Current.GetInstance<T>(key);
            }
        }
        /// <summary>
        /// 装填IOC注册资源的映射信息
        /// add by ys on 2014-9-26        
        /// </summary>
        /// <param name="key"></param>
        /// <returns>返回资源类型</returns>
        public static void SetMappingType(string key, Type type)
        {
            lock (flag)
            {
                if (null != key && null != type)
                {
                    resourceMappingDic.Add(key, type);
                }
            }
        }
    }
}
