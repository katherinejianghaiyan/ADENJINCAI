
using System;

namespace Luoyi.Common.Validate
{
    /// <summary>
    /// 数据合法性验证帮助类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ValidationHelper<T>
    {


        #region 属性
        /// <summary>    
        /// 获取待验证的参数的值.    
        /// </summary>
        public T Value { get; private set; }
        /// <summary>    
        /// 获取待验证的参数的名称.    
        /// </summary>    
        public string Name { get; private set; }
        /// <summary>
        /// 是否通过验证
        /// </summary>
        public bool Passed { get; set; }
        /// <summary>
        /// 返回消息
        /// </summary>
        public string Msg { get; set; }



        #endregion


        #region 构造函数
        /// <summary>    
        /// 创建一个<see cref="ValidationHelper&lt;T&gt;"/>的对象.    
        /// </summary>    
        /// <param name="value">待验证的参数的值.</param>   
        /// /// <param name="name">待验证的参数的名称.</param>    
        public ValidationHelper(T value, string name)
        {
            Value = value;
            Name = name;

            Passed = true;
        }
        #endregion
        #region 基本方法
        /// <summary>    
        /// 验证参数不为其默认值.   
        ///  </summary>   
        ///<returns>this指针以方便链式调用.</returns>
        /// <exception cref="ArgumentException">参数为值类型且为默认值.</exception>    
        /// <exception cref="ArgumentNullException">参数为引用类型且为null.</exception>    
        public ValidationHelper<T> NotDefault()
        {

            //if (b_Passed && Value.Equals(default(T)))
            //{
            if (Value is ValueType && Passed && Value.Equals(default(T)))
            {
                Msg = String.Format("{0}不能使用默认值", Name);
                Passed = false;
            }
            else if (!(Value is ValueType) && Passed && Value.Equals(default(T)))   //null
            {
                Msg = String.Format("{0}不能为空", Name);
                Passed = false;
            }

            //}
            return this;
        }

        /// <summary>    
        /// 使用自定义方法进行验证.    
        /// </summary>    
        /// <param name="rule">用以验证的自定义方法.</param>
        /// <param name="msg">提示消息</param>
        /// <returns>this指针以方便链式调用.</returns>    
        /// <exception cref="Exception">验证失败抛出相应异常.</exception>    
        /// <remarks><paramref name="rule"/>的第一个参数为参数值,第二个参数为参数名称.</remarks>    
        public ValidationHelper<T> CustomRule(Func<T, bool> rule, string msg)
        {
            if (Passed)
            {
                Passed = rule(Value);
                Msg = msg;
            }
            return this;
        }

        /// <summary>
        /// 结束验证
        /// </summary>
        /// <param name="errMsgAction">对验证失败消息处理</param>
        /// <returns></returns>
        public bool End(Action<string> errMsgAction=null)
        {
            if (!Passed && errMsgAction != null)
            {
                errMsgAction(Msg);
            }
            return Passed;
        }
        /// <summary>
        /// 结束验证
        /// </summary>
        /// <param name="errMsg">验证失败消息</param>
        /// <returns>是否验证通过</returns>
        public bool End(out string errMsg)
        {
            errMsg = null;
            if (!Passed )
            {
                errMsg = Msg;
            }
            return Passed;
        }
        ///// <summary>
        ///// 自定义显示错误提示信息
        ///// </summary>
        ///// <param name="sme"></param>
        ///// <returns></returns>
        //public ValidationHelper<T> ShowMsg(Action<string> sme)
        //{
        //    if (sme != null && !Passed)
        //    {
        //        sme(Msg);
        //    }
        //    return this;
        //}
        ///// <summary>
        ///// 显示错误提示信息
        ///// </summary>
        //public ValidationHelper<T> ShowMsg()
        //{
        //    if (!Passed)
        //    {

        //    }
        //    return this;
        //}


        /// <summary>
        /// 转到下一组验证
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="value"></param>
        /// <param name="argName"></param>
        /// <returns></returns>
        public ValidationHelper<T1> ToNext<T1>(T1 value, string argName=null)
        {
            
            ValidationHelper<T1> v = new ValidationHelper<T1>(value, argName);
            v.Passed = Passed;
            v.Msg = Msg;
            return v;
        }
        #endregion

       
    }
}
