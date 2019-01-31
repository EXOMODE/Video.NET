using System;

namespace Video.NET.Filters
{
    /// <summary>
    /// Представляет атрибут уточнения назначаемого типа потока.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class ForStreamAttribute : Attribute
    {
        /// <summary>
        /// Тип потока.
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ForStreamAttribute"/>
        /// </summary>
        /// <param name="type">Тип потока.</param>
        public ForStreamAttribute(Type type) : base() => Type = type;
    }
}