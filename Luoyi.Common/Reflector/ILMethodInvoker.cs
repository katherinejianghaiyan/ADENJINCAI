 using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Luoyi.Common
{
    /// <summary>
    /// 通过 MSIL 指令生成的方法调用器
    /// </summary>
    internal class ILMethodInvoker : IMethodInvoker
    {
        Func<object, object[], object> _invoker;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodInfo"></param>
        public ILMethodInvoker(MethodBase methodInfo)
        {
            this.MethodInfo = methodInfo;
            this._invoker = ILMethodInvoker.GetMethodInvoker(methodInfo);
        }
        static ILMethodInvoker()
        {
            _intPtr_ToPointer = typeof(IntPtr).GetMethods().Where(
                (m) =>
                {
                    var ps = m.GetParameters();
                    return m.Name == "op_Explicit" && m.ReturnType == typeof(void*) && ps.Length == 1 && ps[0].ParameterType == typeof(IntPtr);
                }).First();
            _pointer_ToIntPtr = typeof(IntPtr).GetMethod("op_Explicit", new Type[] { typeof(void*) });
        }
        struct LocalVar
        {
            public LocalBuilder Var;
            public int Index;
        }

        private static MethodInfo _intPtr_ToPointer;
        private static MethodInfo _pointer_ToIntPtr;

        /// <summary>
        /// 处理装箱指令
        /// </summary>
        /// <param name="ilGen"></param>
        /// <param name="var"></param>
        private static void EmitBox(ILGenerator ilGen, LocalBuilder var)
        {
            if (var.LocalType.IsPointer)  //变量是指针类型将其转换为平台句柄(IntPtr) 再装箱到栈顶
            {
                ilGen.Emit(OpCodes.Call, _pointer_ToIntPtr);
                ilGen.Emit(OpCodes.Box, typeof(IntPtr));
            }
            else
            {
                if (var.LocalType.IsValueType)
                {
                    ilGen.Emit(OpCodes.Box, var.LocalType);
                }
            }
        }

        /// <summary>
        /// 处理拆箱指令
        /// </summary>
        /// <param name="ilGen"></param>
        /// <param name="toType"></param>
        private static void EmitUnbox(ILGenerator ilGen, Type toType)
        {
            if (toType.IsPointer)  //如果参数是指针类型的引用则首先将其转换为平台句柄(IntPtr) 再还原回指针
            {
                ilGen.Emit(OpCodes.Unbox_Any, typeof(IntPtr));
                ilGen.Emit(OpCodes.Call, _intPtr_ToPointer);
            }
            else
            {
                if (toType.IsValueType)
                    ilGen.Emit(OpCodes.Unbox_Any, toType);
                else
                    ilGen.Emit(OpCodes.Castclass, toType);
            }
        }

        /*
         *  实现方法调用器 基本思想：
         *  1.将方法参数作为object[]传入
         *  2.拆箱各个object[]中元素作为方法调用参数推入栈进行方法调用 如果参数是ByRef需要创建相应的本地变量来进行引用传递
         *  3.返回方法调用结果 整理ByRef的各个参数
         */
       /// <summary>
       /// 
       /// </summary>
       /// <param name="methodInfo"></param>
       /// <returns></returns>
        public static Func<object, object[], object> GetMethodInvoker(MethodBase methodInfo)
        {

            var dm = new DynamicMethod(string.Empty, typeof(object), new Type[] { typeof(object), typeof(object[]) }, true);
            var ilGen = dm.GetILGenerator();
            var isConstructor = methodInfo is ConstructorInfo;

            ilGen.Emit(OpCodes.Nop);
            if (!isConstructor && !methodInfo.IsStatic)   //如果方法非静态方法 将对象实例入栈
            {
                ilGen.Emit(OpCodes.Ldarg_0);
                EmitUnbox(ilGen, methodInfo.DeclaringType);
            }

            var methodParameters = methodInfo.GetParameters();    //获得方法参数列表
            var vars = new List<LocalVar>();
            for (int i = 0; i < methodParameters.Length; i++)
            {
                ilGen.Emit(OpCodes.Ldarg_1);    //加载参数数组
                ilGen.Emit(OpCodes.Ldc_I4, i);  //加载数组索引
                ilGen.Emit(OpCodes.Ldelem_Ref); //将数组元素推上栈顶 
                /*=========================================
                 *  现在参数元素已经在栈顶
                 *=========================================*/

                var p = methodParameters[i];
                var pType = p.ParameterType;

                if (pType.IsByRef)    //如果参数是按照引用传递定义局部变量来接收
                {
                    var elementType = pType.GetElementType();
                    var v = ilGen.DeclareLocal(elementType);
                    vars.Add(new LocalVar { Var = v, Index = i });
                    EmitUnbox(ilGen, elementType);
                    ilGen.Emit(OpCodes.Stloc, v);           //存储到局部变量
                    ilGen.Emit(OpCodes.Ldloca_S, v);  //将局部变量地址推上栈顶
                }
                else
                {
                    EmitUnbox(ilGen, pType);
                }
            }

            Type returnType = null;             //方法返回类型
            if (isConstructor)  //如果方法是构造函数
            {
                returnType = methodInfo.DeclaringType;
                ilGen.Emit(OpCodes.Newobj, (ConstructorInfo)methodInfo);
            }
            else
            {
                var callMode = methodInfo.IsStatic || methodInfo.DeclaringType.IsValueType ?
                    OpCodes.Call : OpCodes.Callvirt;

                var minfo = (MethodInfo)methodInfo;
                returnType = minfo.ReturnType;
                ilGen.Emit(callMode, minfo);
            }

            LocalBuilder returnVar = null;  //返回值变量
            if (returnType == typeof(void)) //如果是过程方法则最后返回null
            {
                ilGen.Emit(OpCodes.Nop);
                returnVar = ilGen.DeclareLocal(typeof(object));
                ilGen.Emit(OpCodes.Ldnull);
            }
            else
            {
                returnVar = ilGen.DeclareLocal(returnType);
            }
            ilGen.Emit(OpCodes.Stloc, returnVar);
            /*=========================================
             *  方法调用已经完毕，现在进行引用参数整理 
             *=========================================*/

            foreach (var item in vars)
            {
                ilGen.Emit(OpCodes.Ldarg_1);             //加载参数数组
                ilGen.Emit(OpCodes.Ldc_I4, item.Index);  //加载数组索引
                ilGen.Emit(OpCodes.Ldloc, item.Var);
                EmitBox(ilGen, item.Var);                //处理装箱
                ilGen.Emit(OpCodes.Stelem_Ref);          //将改变后的局部变量赋值到参数数组
            }
            ilGen.Emit(OpCodes.Ldloc, returnVar);
            EmitBox(ilGen, returnVar);
            ilGen.Emit(OpCodes.Ret);
            return (Func<object, object[], object>)dm.CreateDelegate(typeof(Func<object, object[], object>));
        }



        public MethodBase MethodInfo
        {
            private set;
            get;
        }

        public object Invoke(object instance, params object[] parameters)
        {
            return this._invoker(instance, parameters);
        }
    }
}
