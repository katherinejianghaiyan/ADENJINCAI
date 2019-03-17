using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Luoyi.Common
{
    /// <summary>
    /// 通过 MSIL 指令生成的属性连接器
    /// </summary>
    internal class ILPropertyAccessor : IMemberAccessor
    {
        Action<object, object> _setter;
        Func<object, object> _getter;

        public ILPropertyAccessor(PropertyInfo propertyInfo)
        {
            PropertyInfo = propertyInfo;
            _setter = GetPropertySetter(propertyInfo);
            _getter = GetPropertyGetter(propertyInfo);
        }
        /// <summary>
        /// 给属性赋值
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="value"></param>
        public void SetValue(object instance, object value)
        {
            _setter(instance, value);
        }
        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public object GetValue(object instance)
        {
            return _getter(instance);
        }

        public PropertyInfo PropertyInfo
        {
            get;
            private set;
        }
        
        #region 获得属性设置器方法
        /// <summary>
        /// 获得属性设置器 
        /// </summary>
        static Action<object, object> GetPropertySetter(PropertyInfo property)
        {
            var propertySetMethod = property.GetSetMethod(true);
            var declarType = property.DeclaringType;
            var propertyType = property.PropertyType;
            var dm = new DynamicMethod(string.Empty, null, new Type[] { typeof(object), typeof(object) }, true);
            var ilGen = dm.GetILGenerator();

            if (propertySetMethod.IsStatic)
            {
                ilGen.Emit(OpCodes.Nop);
                ilGen.Emit(OpCodes.Ldarg_1);        // 加载value 参数 
                if (propertyType.IsValueType)          // 如果要赋值的属性类型为值类型将参数拆箱 否则 将其类型转换  推上栈 
                {
                    ilGen.Emit(OpCodes.Unbox_Any, propertyType);
                }
                else
                {
                    ilGen.Emit(OpCodes.Castclass, propertyType);
                }
                ilGen.Emit(OpCodes.Call, propertySetMethod);                 // 通过调用属性的Set方法将转换后的值赋给静态属性
                ilGen.Emit(OpCodes.Ret);
            }
            else
            {
                ilGen.Emit(OpCodes.Nop);
                ilGen.Emit(OpCodes.Ldarg_0);    //加载instance参数
                OpCode callMode = OpCodes.Callvirt; //属性Set方法调用模式 (call or callvirt)
                if (declarType != null && declarType.IsValueType)     // 如果instance是值类型需要进行拆箱 但是此时这个方法意义不大了
                {
                    callMode = OpCodes.Call;    //instance是值类型是使用call 指令来调用属性的Set方法
                    ilGen.DeclareLocal(declarType);
                    ilGen.Emit(OpCodes.Unbox_Any, declarType);
                    ilGen.Emit(OpCodes.Stloc_0);
                    ilGen.Emit(OpCodes.Ldloca_S, 0);
                }
                else
                {
                    if (declarType != null) ilGen.Emit(OpCodes.Castclass, declarType);
                }
                ilGen.Emit(OpCodes.Ldarg_1);

                ilGen.Emit(propertyType.IsValueType ? OpCodes.Unbox_Any : OpCodes.Castclass, propertyType);

                ilGen.Emit(callMode, propertySetMethod);
                ilGen.Emit(OpCodes.Ret);
            }
             /*
              *  以上代码类似于生成如下方法
              *  
              *      属性为静态时：
              *      static void Set(object instance,object value)
              *      {
              *          InstanceType.StaticProperty = (PropertyType) value;
              *      }
              *      
              *      属性为实例时
              *      static void Set(object instance,object value)
              *      {
              *          ((InstanceType)instance).InstanceProperty = (PropertyType) value;
              *           //以上是InstanceType为引用类型时的情况
              *          
              *          InstanceType loc = (InstanceType)instance
              *          loc.InstanceProperty = (PropertyType) value;
              *          //以上是InstanceType为值类型时的情况
              *      }
              * 
              */

            return (Action<object, object>)dm.CreateDelegate(typeof(Action<object, object>));
        }
        #endregion

        #region 获得属性访问方法
        /// <summary>
        /// 获得属性访问方法
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        private static Func<object, object> GetPropertyGetter(PropertyInfo property)
        {
            var propertyGetMethod = property.GetGetMethod(true);
            var declarType = property.DeclaringType;
            var propertyType = property.PropertyType;
            var dm = new DynamicMethod(string.Empty, typeof(object), new Type[] { typeof(object) }, true);
            var ilGen = dm.GetILGenerator();

            if (propertyGetMethod.IsStatic)
            {
                ilGen.Emit(OpCodes.Nop);
                ilGen.Emit(OpCodes.Call, propertyGetMethod);    // 通过调用属性的Set方法装载静态属性
                if (propertyType.IsValueType)   // 如果该属性为值类型 返回时装箱
                {
                    ilGen.Emit(OpCodes.Box, propertyType);
                }

                ilGen.Emit(OpCodes.Ret);
            }
            else
            {
                ilGen.Emit(OpCodes.Nop);
                ilGen.Emit(OpCodes.Ldarg_0);    //加载instance参数
                OpCode callMode = OpCodes.Callvirt; //属性Set方法调用模式 (call or callvirt)
                if (declarType != null && declarType.IsValueType)     // 如果instance是值类型需要进行拆箱
                {
                    callMode = OpCodes.Call;    //instance是值类型是使用 call 指令来调用属性的Set方法
                    ilGen.DeclareLocal(declarType);
                    ilGen.Emit(OpCodes.Unbox_Any, declarType);
                    ilGen.Emit(OpCodes.Stloc_0);
                    ilGen.Emit(OpCodes.Ldloca_S, 0);
                }
                else
                {
                    if (declarType != null) ilGen.Emit(OpCodes.Castclass, declarType);
                }
                ilGen.Emit(callMode, propertyGetMethod);

                if (propertyType.IsValueType)   // 如果该属性为值类型 返回时装箱
                {
                    ilGen.Emit(OpCodes.Box, propertyType);
                }
                ilGen.Emit(OpCodes.Ret);
            }
            /*
             *  以上代码类似于生成如下方法
             *  
             *      属性为静态时：
             *      static object Get(object instance)
             *      {
             *          return (object)InstanceType.PropertyField;
             *      }
             *      
             *      属性为实例时
             *      static object Get(object instance)
             *      {
             *          return (object)((InstanceType)instance).PropertyField;
             *      }
             * 
             */

            return (Func<object, object>)dm.CreateDelegate(typeof(Func<object, object>));
        }
        #endregion
    }
}
