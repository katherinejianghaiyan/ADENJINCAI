using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Luoyi.Common
{
    /// <summary>
    /// 可传递数据项的对象<br/>
    /// 可以使用在Page与Control之间进行数据传递
    /// </summary>
    /// <typeparam name="T">传递数据类型</typeparam>
    /// <remarks>
    /// <para>
    /// 用户控件：Header.ascx
    /// </para>
    /// <code lang="c#">
    /// <![CDATA[
    ///     public partial class Header : System.Web.UI.UserControl, ITransfer<int>
    ///     {
    ///         protected void Page_Load(object sender, EventArgs e)
    ///         {
    ///     
    ///         }
    ///         /// <summary>
    ///         /// 当前登录用户ID,有Page页面传递过来
    ///         /// </summary>
    ///         public int Data
    ///         {
    ///             get;
    ///             set;
    ///         }
    ///     }
    /// ]]>
    /// </code>
    /// <para>
    /// 页面：default.aspx</para>
    /// <code lang="C#">
    /// <![CDATA[
    /// string header=WebControlHelper.GetPartial("~/Controls/Header.ascx",100);    //获取Header.ascx输入字符串，并将100传递给Hedder.ascx
    /// //该方式，在对于用户控件所需参数不确定时，比较有用。
    /// ]]>
    /// </code>
    /// </remarks>
    public interface ITransfer<T>
    {
        /// <summary>
        /// 传递的数据对象
        /// </summary>
        T Data { get; set; }
    }
}
