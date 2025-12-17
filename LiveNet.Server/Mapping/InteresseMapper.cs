using LiveNet.Api.ViewModels;
using LiveNet.Domain.Models;

namespace LiveNet.Api.Mapping;

public static class InteresseMapper
{
    public static InteresseModel ToInteresseModel(InteresseViewModel viewModel)
    {
        return new InteresseModel
        {
            Id = viewModel.Id,
            Interesse = viewModel.Interesse,
        };
    }
}
