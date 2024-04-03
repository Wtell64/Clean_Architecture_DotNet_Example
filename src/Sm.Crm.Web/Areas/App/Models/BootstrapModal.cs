namespace Sm.Crm.Web.Areas.App.Models;

public class BootstrapModal
{
    public string? Id { get; set; }
    public string? AreaLabeledId { get; set; }
    public ModalSize Size { get; set; }
    public string? Message { get; set; }

    public string ModalSizeClass
    {
        get
        {
            switch (Size)
            {
                case ModalSize.Small:
                    return "modal-sm";

                case ModalSize.Large:
                    return "modal-lg";

                case ModalSize.Medium:
                default:
                    return "";
            }
        }
    }
}

public class ModalHeader
{
    public string? Heading { get; set; }
}

public class ModalFooter
{
    public string SubmitButtonText { get; set; } = "Submit";
    public string CancelButtonText { get; set; } = "Cancel";
    public string SubmitButtonId { get; set; } = "btn-submit";
    public string CancelButtonId { get; set; } = "btn-cancel";
    public bool OnlyCancelButton { get; set; }
}

public enum ModalSize
{
    Small,
    Large,
    Medium
}