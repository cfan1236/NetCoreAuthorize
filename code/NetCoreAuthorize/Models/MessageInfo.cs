
using NetCoreAuthorize.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace NetCoreAuthorize.Models
{
    public class MessageInfo<T>
    {
        /// <summary>
        /// 状态
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        public string ex { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public T data { get; set; }

        /// <summary>
        /// 设置状态码 
        /// </summary>
        /// <param name="returnCode"></param>
        /// <param name="data"></param>
        public MessageInfo<T> SetStatusCode(ReturnCodeType returnCode, T data)
        {
            status = (int)returnCode;
            msg = GetEnumDescription(returnCode);
            this.data = data;
            return this;
        }
        public  string GetEnumDescription(Enum enumValue)
        {
            if (enumValue == null || enumValue.ToString() == "-1") return string.Empty;
            string value = enumValue.ToString();
            FieldInfo field = enumValue.GetType().GetField(value);
            object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);    //获取描述属性
            if (objs == null || objs.Length == 0)    //当描述属性没有时，直接返回名称
                return value;
            DescriptionAttribute descriptionAttribute = (DescriptionAttribute)objs[0];
            return descriptionAttribute.Description;
        }


    }
}
