namespace Thon.Core.Enums;

/// <summary>
/// Уровень доступа к чему-либо.
/// </summary>
public enum AccessLevel
{
    /// <summary>
    /// Нет доступа.
    /// </summary>
    None = 0,

    /// <summary>
    /// Доступ только на чтение.
    /// </summary>
    Read = 1,

    /// <summary>
    /// Доступ на чтение и изменение.
    /// </summary>
    Write = 2,
}
