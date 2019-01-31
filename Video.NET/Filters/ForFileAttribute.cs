using System;

namespace Video.NET.Filters
{
    /// <summary>
    /// Представляет атрибут флага, указывающего что фильтр применим только к файлу целиком.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ForFileAttribute : Attribute
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ForFileAttribute"/>
        /// </summary>
        public ForFileAttribute() : base() { }
    }
}