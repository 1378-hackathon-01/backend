using Thon.Web.Models.Admin;

namespace Thon.Web.Entities.Admin;

public class AdminUserPut
{
    // TODO: Пользователь может менять себе только имя и пароль.
    // TODO: Админ может менять пользователю только логин, имя и права доступа.

    public string? NewFullName { get; set; }

    public string? NewPassword { get; set; }
}
