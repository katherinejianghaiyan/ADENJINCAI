using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Resources;
using System.Web.Compilation;
using System.Web.UI;
using System.CodeDom;
using System.ComponentModel;

namespace Luoyi.Web
{
    [ExpressionPrefix("Lang")]
    [ExpressionEditor("LangEditor")]
    public class LanguageExpression : ExpressionBuilder
    {
        /// <summary>
        /// 切换多语言
        /// 参数在GetCodeExpression中的数据中
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="target"></param>
        /// <param name="entry"></param>
        /// <param name="ControlID"></param>
        /// <returns></returns>
        public static object GetEvalData(string expression, Type target, string entry, string ControlID)
        {
            //  ControlID： ID，控件的ID，每个页面唯一就行了。如果是服务器控件程序会获取其ID
            //  expression: 要显示的文本

            string id = ControlID;
            string text = expression;
            //如果未指定ID
            if (ControlID == null)
            {
                id = expression.Split(',')[0];
                text = expression.Split(',')[1];
            }
            return HtmlLang.Lang(id, text);
        }

        public override object EvaluateExpression(object target, BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context)
        {
            //    string id = entry.ControlID;
            //    string text = entry.Expression;
            return GetEvalData(entry.Expression, target.GetType(), entry.Name, entry.ControlID);
        }

        //重写GetCodeExpression 初始化时执行
        public override CodeExpression GetCodeExpression(BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context)
        {
            Type type = entry.DeclaringType;
            PropertyDescriptor descriptor = TypeDescriptor.GetProperties(type)[entry.PropertyInfo.Name];
            CodeExpression[] expressionArray = new CodeExpression[4];
            expressionArray[0] = new CodePrimitiveExpression(entry.Expression);
            expressionArray[1] = new CodeTypeOfExpression(type);
            expressionArray[2] = new CodePrimitiveExpression(entry.Name);
            expressionArray[3] = new CodePrimitiveExpression(entry.ControlID);
            return new CodeCastExpression(descriptor.PropertyType, new CodeMethodInvokeExpression(new
           CodeTypeReferenceExpression(base.GetType()), "GetEvalData", expressionArray));
        }

        public override bool SupportsEvaluate
        {
            get { return true; }
        }
    }

}