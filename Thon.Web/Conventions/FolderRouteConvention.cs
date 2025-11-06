using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Thon.Web.Conventions;

public class FolderRouteConvention : IControllerModelConvention
{
    public void Apply(ControllerModel controller)
    {
        var namespaceParts = controller.ControllerType.Namespace?.Split('.') ?? [];

        var controllersIndex = Array.IndexOf(namespaceParts, "Controllers");
        if (controllersIndex >= 0 && controllersIndex < namespaceParts.Length - 1)
        {
            var folderName = namespaceParts[controllersIndex + 1];

            var selector = controller.Selectors.FirstOrDefault();
            if (selector is not null)
            {
                if (selector.AttributeRouteModel is not null)
                    selector.AttributeRouteModel.Template = $"{folderName}/{selector.AttributeRouteModel.Template}";
                else
                    selector.AttributeRouteModel = new AttributeRouteModel { Template = $"{folderName}/[controller]" };
            }
        }
    }
}