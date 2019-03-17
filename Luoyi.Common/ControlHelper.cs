using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.UI.WebControls;

namespace Luoyi.Common
{
    /// <summary>
    /// 获取、绑定、校验控件值的操作方法类
    /// </summary>
    public static class ControlHelper
    {
        /// <summary>
        /// 获得文本输入框为空时的提示消息
        /// </summary>
        /// <param name="textBox">文件框控件ID</param>
        /// <param name="controlName">文本框控件名称</param>
        /// <returns></returns>
        public static string GetEmptyMessage(this TextBox textBox, string controlName)
        {
            return textBox.Text.Trim() == string.Empty ? string.Concat("<li>", controlName, "不能为空</li>") : string.Empty;
        }

        /// <summary>
        /// 获得文本输入框为空时且长度超出时的提示消息
        /// </summary>
        /// <param name="textBox">文本框控件ID</param>
        /// <param name="controlName">文本框控件名称</param>
        /// <param name="maxLength">最大长度</param>
        /// <returns></returns>
        public static string GetEmptyMessage(this TextBox textBox, string controlName, int maxLength)
        {
            string msg = GetEmptyMessage(textBox, controlName);
            if (string.IsNullOrEmpty(msg) == false)
            {
                return msg;
            }

            if (textBox.Text.Trim().Length > maxLength)
            {
                return string.Format("<li>{0}不能超过{1}个字符</li>", controlName, maxLength);
            }

            return string.Empty;
        }

        /// <summary>
        /// 返回文本框内容超出最大长度时的错误提示信息
        /// </summary>
        /// <param name="textBox">文本框控件ID</param>
        /// <param name="controlName">文本框控件名称</param>
        /// <param name="maxLength">最大长度</param>
        /// <returns></returns>
        public static string GetThanMaxLengthMessage(this TextBox textBox, string controlName, int maxLength)
        {
            if (textBox.Text.Trim().Length > maxLength) return string.Format("<li>{0}不能超过{1}个字符</li>", controlName, maxLength);
            return string.Empty;
        }

        /// <summary>
        /// 获得文本输入框值无效时的提示消息
        /// </summary>
        /// <param name="textBox">文本框控件ID</param>
        /// <param name="controlName">文本框控件名称</param>
        /// <param name="type">值类型</param>
        /// <param name="required">是否必须输入</param>
        /// <returns></returns>
        public static string GetInvalidMessage(this TextBox textBox, string controlName, TextValueType type, bool required)
        {
            string msg;

            if (required)
            {
                msg = GetEmptyMessage(textBox, controlName);
                if (!string.IsNullOrEmpty(msg))
                {
                    return msg;
                }
            }

            string value = textBox.Text.Trim();
            if (!string.IsNullOrEmpty(value))
            {
                switch (type)
                {
                    case TextValueType.Int:
                        if (value.IsInt32() == false)
                        {
                            return string.Concat("<li>", controlName, "的值必须为数字</li>");
                        }
                        break;
                    case TextValueType.Decimal:
                        if (value.IsDecimal() == false)
                        {
                            return string.Concat("<li>", controlName, "的值必须为数字或小数</li>");
                        }
                        break;
                    case TextValueType.Numeric:
                        if (value.IsInteger() == false)
                        {
                            return string.Concat("<li>", controlName, "的值必须为整数</li>");
                        }
                        break;
                }
            }

            return string.Empty;
        }


        /// <summary>
        /// 获得下拉框未选择有效项时的提示消息
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="controlName"></param>
        /// <returns></returns>
        public static string GetUnSelectMessage(this DropDownList ddl, string controlName)
        {
            if( ddl.Items.Count > 0 && ddl.SelectedIndex <= 0)
                return string.Concat("<li>请选择", controlName, "</li>");
            else
                return string.Empty;
        }

        /// <summary>
        /// 获得单选框没有选择时的提示消息
        /// </summary>
        /// <param name="rbl"></param>
        /// <param name="controlName"></param>
        /// <returns></returns>
        public static string GetUnSelectMessage(this RadioButtonList rbl, string controlName)
        {
            if(rbl.SelectedIndex == -1)
                return string.Concat("<li>请选择", controlName, "</li>");
            else
                return string.Empty;
        }

        /// <summary>
        /// 获得多选框没有选择时的提示消息
        /// </summary>
        /// <param name="cbl"></param>
        /// <param name="controlName"></param>
        /// <returns></returns>
        public static string GetNotSelectedMessage(this CheckBoxList cbl, string controlName)
        {        
            if (cbl.SelectedIndex == -1)
                return string.Concat("<li>请选择", controlName, "</li>");
            else
                return string.Empty;
        }
        
        /// <summary>
        /// 只允许上传图片
        /// </summary>
        /// <param name="upload"></param>
        /// <param name="controlName"></param>
        /// <returns></returns>
        public static string GetNotImageFileMessage(this FileUpload upload, string controlName)
        {
            if(upload.HasFile)
            {
                if(upload.PostedFile.ContentType.ToLower().IndexOf("image") == -1)
                    return string.Format("<li>上传的{0}图片格式不正确</li>", controlName);
            }
            return string.Empty;
        }


        /// <summary>
        /// 只允许上传安全的文件
        /// </summary>
        /// <param name="upload"></param>
        /// <param name="controlName"></param>
        /// <returns></returns>
        public static string GetNotSafeFileMessage(this FileUpload upload, string controlName)
        {
            if (!upload.HasFile) return string.Empty;

            var safeExtName = new List<string>()
            {
                ".jpg",
                ".jpeg",
                ".gif",
                ".png",
                ".doc",
                ".docx",
                ".xls",
                ".xlsx",
                ".zip",
                ".rar",
                ".txt",
                ".rtf",
                ".ppt",
                ".pptx"
            };

            string fileExtName = Path.GetExtension(upload.FileName);

            return safeExtName.Contains(fileExtName) == false ? string.Format("<li>上传的{0}文件格式不正确</li>", controlName) : string.Empty;
        }

        /// <summary>
        /// 获得文本框无SQL注入的文本值
        /// </summary>
        /// <param name="textBox">文本框控件</param>
        /// <returns></returns>
        public static string GetSafeValue(this TextBox textBox)
        {
            return textBox.Text.IsUnSafeSql() ?  string.Empty : textBox.Text.Trim();
        }


        /// <summary>
        /// 获得文本框的数字值
        /// </summary>
        /// <param name="textBox">文本框控件</param>
        /// <returns></returns>
        public static int GetIntValue(this TextBox textBox)
        {
            return string.IsNullOrEmpty(textBox.Text.Trim()) ? 0 : textBox.Text.Trim().ToInt32();
        }

        /// <summary>
        /// 获得下拉控件选择的Int值
        /// </summary>
        /// <param name="ddl"></param>
        /// <returns></returns>
        public static int GetIntValue(this DropDownList ddl)
        {
            return ddl.SelectedIndex < 0 ? 0 : ddl.SelectedValue.ToInt32();
        }


        /// <summary>
        /// 获得文本框的小数值
        /// </summary>
        /// <param name="textBox">文本框控件</param> 
        /// <returns></returns>
        public static decimal GetDecimalValue(this TextBox textBox)
        {
            return string.IsNullOrEmpty(textBox.Text.Trim()) ? 0 : textBox.Text.Trim().ToDecimal();
        }

        /// <summary>
        /// 返回从CheckBoxList中选定的单个或多个值
        /// </summary>
        /// <param name="cbl">CheckBoxList控件名称</param>
        /// <returns>返回选定的值</returns>
        public static string GetValue(this CheckBoxList cbl)
        {
            string result = string.Empty;

            for(int i = 0; i < cbl.Items.Count; i++)
            {
                if (cbl.Items[i].Selected)
                {
                    result += string.Concat(cbl.Items[i].Value, ",");
                }
            }

            return result.TrimEnd(',');
        }


        /// <summary>
        /// 从CheckBoxList中选定
        /// </summary>
        /// <param name="cbl">CheckBoxList控件名称</param>
        /// <param name="value">要选中项值，多个值以,号间隔</param>
        public static void BindValue(this CheckBoxList cbl, string value)
        {
            if(string.IsNullOrEmpty(value)) return; 

            Array.ForEach(value.Split(','),
            delegate(string s)
            {
                for(int i = 0; i < cbl.Items.Count; i++)
                    if (cbl.Items[i].Value == s)
                    {
                        cbl.Items[i].Selected = true;
                    }
            });
        }

        /// <summary>
        /// 从单选项中选定
        /// </summary>
        /// <param name="rbl">RadioButtonList控件名称</param>
        /// <param name="value">要选定的值</param>
        public static void BindValue(this RadioButtonList rbl, string value)
        {
            if (string.IsNullOrEmpty(value)) return; 

            for(int i = 0; i < rbl.Items.Count; i++)
            {
                if (rbl.Items[i].Value == value)
                {
                    rbl.Items[i].Selected = true;
                }
            }
        }

        /// <summary>
        /// 下拉框绑定字典
        /// </summary>
        /// <param name="ddlList">下拉框</param>
        /// <param name="dictData">要绑定的字典</param>
        /// <param name="defaultText">默认选项名称</param>
        /// <param name="defaultValue">默认选项值</param>
        public static void BindDictionary(this DropDownList ddlList, Dictionary<int, string> dictData, string defaultText = "选择", string defaultValue = "0")
        {
            ddlList.DataSource = dictData;
            ddlList.DataValueField = "key";
            ddlList.DataTextField = "value";
            ddlList.DataBind();
            ddlList.Items.Insert(0, new ListItem(defaultText, defaultValue));
        }

        /// <summary>
        /// 下拉框绑定字典
        /// </summary>
        /// <param name="ddlList">下拉框</param>
        /// <param name="dictData">要绑定的字典</param>
        /// <param name="defaultText">默认选项名称</param>
        /// <param name="defaultValue">默认选项值</param>
        public static void BindDictionary(this DropDownList ddlList, Dictionary<string, string> dictData, string defaultText = "选择", string defaultValue = "0")
        {
            ddlList.DataSource = dictData;
            ddlList.DataValueField = "key";
            ddlList.DataTextField = "value";
            ddlList.DataBind();
            ddlList.Items.Insert(0, new ListItem(defaultText, defaultValue));
        }

        /// <summary>
        /// 下拉框绑定字典
        /// </summary>
        /// <param name="rblList">单选框</param>
        /// <param name="dictData">要绑定的字典</param> 
        public static void BindDictionary(this RadioButtonList rblList, Dictionary<int, string> dictData)
        {
            rblList.DataSource = dictData;
            rblList.DataValueField = "key";
            rblList.DataTextField = "value";
            rblList.DataBind(); 
        }

        /// <summary>
        /// 文本控件值校验类型
        /// </summary>
        public enum TextValueType
        {
            /// <summary>
            /// int 类型，包括负数
            /// </summary>
            Int,
            /// <summary>
            /// 小数类型
            /// </summary>
            Decimal,
            /// <summary>
            /// 整形数字类型
            /// </summary>
            Numeric
        }

        #region  根据实体绑定到页面控件
        /// <summary>
        /// 数据绑定
        /// </summary>
        /// <param name="obj">实体对象</param>
        /// <param name="container">页面(this.Page)</param>
        public static void BindObjectToControls(object obj, System.Web.UI.Control container)
        {
            if (obj == null) return;
            Type objType = obj.GetType();
            PropertyInfo[] objPropertiesArray = objType.GetProperties();

            foreach (PropertyInfo objProperty in objPropertiesArray)
            {
                //文本框
                System.Web.UI.Control control = container.FindControl("txt" + objProperty.Name);
                //下拉框  ddl
                if (control == null)
                    control = container.FindControl("ddl" + objProperty.Name);
                //单选框  rbl
                if (control == null)
                    control = container.FindControl("rbl" + objProperty.Name);
                //复选框   cb
                if (control == null)
                    control = container.FindControl("cb" + objProperty.Name);
                //标签    ltl
                if (control == null)
                    control = container.FindControl("ltl" + objProperty.Name);
                //标签    lbl
                if (control == null)
                    control = container.FindControl("lbl" + objProperty.Name);
                //链接    ltl
                if (control == null)
                    control = container.FindControl("hlk" + objProperty.Name);
                if (control != null)
                {
                    if (control is ListControl)
                    {
                        ListControl listControl = (ListControl)control;
                        if (objProperty.GetValue(obj, null) != null)
                        {
                            string propertyValue = objProperty.GetValue(obj, null).ToString();
                            string[] arr = propertyValue.Split('^');
                            foreach (string value in arr)
                            {
                                ListItem listItem = listControl.Items.FindByValue(value);
                                if (listItem != null) listItem.Selected = true;
                            }
                        }
                    }
                    else if (control is CheckBox)
                    {
                        if (objProperty.PropertyType == typeof(bool))
                            ((CheckBox)control).Checked = (bool)objProperty.GetValue(obj, null);
                    }

                    else if (control is Calendar)
                    {
                        if (objProperty.PropertyType == typeof(DateTime))
                            ((Calendar)control).SelectedDate = (DateTime)objProperty.GetValue(obj, null);
                    }
                    else if (control is TextBox)
                    {
                        //处理日期
                        if (objProperty.Name.IndexOf("Date") > 0 && (objProperty.GetValue(obj, null)).ToString().Length == 8)
                            ((TextBox)control).Text =
                                (objProperty.GetValue(obj, null)).ToString().ToInt32().ToDateTime().ToString("yyyy-MM-dd");
                        else
                            ((TextBox)control).Text = (objProperty.GetValue(obj, null)).ToString();
                    }
                    else if (control is HiddenField)
                    {
                        ((HiddenField)control).Value = (objProperty.GetValue(obj, null)).ToString();
                    }
                    else if (control is Label)
                    {
                        ((Label)control).Text = (objProperty.GetValue(obj, null)).ToString();
                    }
                    else if (control is Literal)
                    {
                        if (objProperty.Name.IndexOf("Date") > 0 && (objProperty.GetValue(obj, null)).ToString().Length == 8)
                            ((Literal)control).Text =
                                (objProperty.GetValue(obj, null)).ToString().ToInt32().ToDate().ToString("yyyy-MM-dd");
                        else
                            ((Literal)control).Text = objProperty.GetValue(obj, null) == null ? "" : objProperty.GetValue(obj, null).ToString();
                    }
                    else if (control is HyperLink)
                    {
                        ((HyperLink)control).NavigateUrl = objProperty.GetValue(obj, null) == null ? "" : objProperty.GetValue(obj, null).ToString();
                    }

                    //HyperLink
                }
            }
        }
        #endregion

        #region 根据页面获取可绑定的实体值
        /// <summary>
        /// 根据页面获取可绑定的实体值
        /// </summary>
        /// <param name="model">实体页面</param>
        /// <param name="request">页面请求</param>
        /// <param name="fromName">服务端控件前缀（ctl00$ContentPlaceHolder1$）</param>
        public static void SaveModelFromPage(object model, HttpRequest request, string fromName)
        {
            if (model == null) return;
            Type objType = model.GetType();
            PropertyInfo[] objPropertiesArray = objType.GetProperties();

            foreach (PropertyInfo objProperty in objPropertiesArray)
            {
                //文本框
                if (request.Form[fromName + "txt" + objProperty.Name] != null)
                {
                    //处理时间
                    if (objProperty.Name.IndexOf("Date") > 0 && request.Form[fromName + "txt" + objProperty.Name].Length != 8)
                        objProperty.SetValue(model,
                        Convert.ChangeType(DateTimeHelper.ToDateInt(request.Form[fromName + "txt" + objProperty.Name].ToDateTime()), objProperty.PropertyType),
                        null);
                    else
                        objProperty.SetValue(model,
                        Convert.ChangeType(request.Form[fromName + "txt" + objProperty.Name], objProperty.PropertyType),
                        null);
                }
                //下拉框
                else if (request.Form[fromName + "ddl" + objProperty.Name] != null)
                    objProperty.SetValue(model,
                        Convert.ChangeType(request.Form[fromName + "ddl" + objProperty.Name], objProperty.PropertyType),
                        null);
                //单选框
                else if (request.Form[fromName + "rbl" + objProperty.Name] != null)
                    objProperty.SetValue(model,
                        Convert.ChangeType(request.Form[fromName + "rbl" + objProperty.Name], objProperty.PropertyType),
                        null);
                //复选框
                else if (request.Form[fromName + "cb" + objProperty.Name] != null)
                    objProperty.SetValue(model,
                        Convert.ChangeType(request.Form[fromName + "cb" + objProperty.Name], objProperty.PropertyType),
                        null);
                //标签
                else if (request.Form[fromName + "ltl" + objProperty.Name] != null)
                    objProperty.SetValue(model,
                        Convert.ChangeType(request.Form[fromName + "ltl" + objProperty.Name], objProperty.PropertyType),
                        null);
                //标签
                else if (request.Form[fromName + "lbl" + objProperty.Name] != null)
                    objProperty.SetValue(model,
                        Convert.ChangeType(request.Form[fromName + "lbl" + objProperty.Name], objProperty.PropertyType),
                        null);
                else
                {
                }
            }
        }
        #endregion
    }
}