using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Luoyi.Common
{
    /// <summary>
    /// 通过 MSIL 指令生成的字段连接器
    /// </summary>
    internal class ILFieldAccessor : IMemberAccessor
    {
        Action<object, object> _setter;
        Func<object, object> _getter;

        public ILFieldAccessor(FieldInfo fieldInfo)
        {
            FieldInfo = fieldInfo;
            _setter = GetFieldSetter(fieldInfo);
            _getter = GetFieldGetter(fieldInfo);
        }

        public void SetValue(object instance, object value)
        {
            _setter(instance, value);
        }

        public object GetValue(object instance)
        {
            return _getter(instance);
        }

        public FieldInfo FieldInfo
        {
            get;
            private set;
        }

        #region 获得字段设置器方法
        /// <summary>
        /// 获得字段设置器 
        /// </summary>
        static Action<object, object> GetFieldSetter(FieldInfo field)
        {
            var declarType = field.DeclaringType;
            var fieldType = field.FieldType;
            var dm = new DynamicMethod(string.Empty, null, new Type[] { typeof(object), typeof(object) }, true);
            var ilGen = dm.GetILGenerator();

            if (field.IsStatic) //如果是静态字段
            {
                ilGen.Emit(OpCodes.Nop);
                ilGen.Emit(OpCodes.Ldarg_1);        // 加载value参数
                if (fieldType.IsValueType)          // 拆箱/类型转换
                {
                    ilGen.Emit(OpCodes.Unbox_Any, fieldType);
                }
                else
                {
                    ilGen.Emit(OpCodes.Castclass, fieldType);
                }
                ilGen.Emit(OpCodes.Stsfld, field);
                ilGen.Emit(OpCodes.Ret);

            }
            else
            {

                ilGen.Emit(OpCodes.Nop);
                ilGen.Emit(OpCodes.Ldarg_0);
                if (declarType.IsValueType)     // 如果instance是值类型需要进行拆箱 但是此时这个方法无意义了
                {
                    ilGen.DeclareLocal(declarType);    //定义局部变量承instance载拆箱后的值
                    ilGen.Emit(OpCodes.Unbox_Any, declarType);
                    ilGen.Emit(OpCodes.Stloc_0);    //拆箱后值赋于局部变量
                    ilGen.Emit(OpCodes.Ldloca_S, 0);    //将局部变量引用推上栈
                }
                else
                {
                    ilGen.Emit(OpCodes.Castclass, declarType);
                }

                ilGen.Emit(OpCodes.Ldarg_1);
                if (fieldType.IsValueType)          // 拆箱/类型转换
                {
                    ilGen.Emit(OpCodes.Unbox_Any, fieldType);
                }
                else
                {
                    ilGen.Emit(OpCodes.Castclass, fieldType);
                }
                ilGen.Emit(OpCodes.Stfld, field);
                ilGen.Emit(OpCodes.Ret);
            }

            /*
            *  以上代码类似于生成如下方法
            *  
            *      字段为静态时：
            *      static void Set(object instance,object value)
            *      {
            *          InstanceType.StaticField = (FieldType) value;
            *      }
            *      
            *      字段为实例时
            *      static void Set(object instance,object value)
            *      {
            *          ((InstanceType)instance).InstanceField = (FieldType) value;
            *           //以上是InstanceType为引用类型时的情况
            *          
            *          InstanceType loc = (InstanceType)instance
            *          loc.InstanceField = (FieldType) value;
            *          //以上是InstanceType为值类型时的情况
            *      }
            * 
            */

            return (Action<object, object>)dm.CreateDelegate(typeof(Action<object, object>));
        }
        #endregion

        #region 获得字段获取器方法
        /// <summary>
        /// 获得字段获取器方法
        /// </summary>
        static Func<object, object> GetFieldGetter(FieldInfo field)
        {
            var declarType = field.DeclaringType;
            var fieldType = field.FieldType;
            var dm = new DynamicMethod(string.Empty, typeof(object), new Type[] { typeof(object) }, true);
            var ilGen = dm.GetILGenerator();

            if (field.IsStatic) //如果是静态字段
            {
                ilGen.Emit(OpCodes.Nop);
                if (fieldType.IsValueType)          // 获取字段 -> 装箱 返回
                {
                    ilGen.Emit(OpCodes.Ldsfld, field);
                    ilGen.Emit(OpCodes.Box, fieldType);
                }
                else
                {
                    ilGen.Emit(OpCodes.Ldsfld, field);
                }
                ilGen.Emit(OpCodes.Ret);
            }
            else
            {
                ilGen.Emit(OpCodes.Nop);
                ilGen.Emit(OpCodes.Ldarg_0);
                if (declarType.IsValueType)     // 拆箱 -> 入栈   
                {
                    ilGen.Emit(OpCodes.Unbox_Any, declarType);
                }
                else
                {
                    ilGen.Emit(OpCodes.Castclass, declarType);
                }

                if (fieldType.IsValueType)          //  获取字段 -> 装箱 返回
                {
                    ilGen.Emit(OpCodes.Ldfld, field);
                    ilGen.Emit(OpCodes.Box, fieldType);
                }
                else
                {
                    ilGen.Emit(OpCodes.Ldfld, field);
                }
                ilGen.Emit(OpCodes.Ret);
            }
            /*
             *  以上代码类似于生成如下方法
             *  
             *      字段为静态时：
             *      static object Get(object instance)
             *      {
             *          return (object)InstanceType.StaticField;
             *      }
             *      
             *      字段为实例时
             *      static object Get(object instance)
             *      {
             *          return (object)((InstanceType)instance).InstanceField;
             *      }
             * 
             */

            return (Func<object, object>)dm.CreateDelegate(typeof(Func<object, object>));
        }
        #endregion
    }
}
