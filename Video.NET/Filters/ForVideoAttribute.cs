using System;

namespace Video.NET.Filters
{
    /// <summary>
    /// Представляет атрибут флага, указывающего что фильтр применим только к видео целиком.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ForVideoAttribute : Attribute
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ForVideoAttribute"/>
        /// </summary>
        public ForVideoAttribute() : base() { }
    }
}