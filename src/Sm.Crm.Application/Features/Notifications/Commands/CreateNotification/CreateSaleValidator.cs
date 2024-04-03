using FluentValidation;

namespace Sm.Crm.Application.Features.Notifications.Commands.CreateNotification;

public class CreateNotificationValidator : AbstractValidator<CreateNotificationCommand>
{
    public CreateNotificationValidator()
    {
    }
}