using FluentValidation;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace firstProject.Services.DTO.Validators
{
    public class AddBlogDtoValidator: AbstractValidator<AddBlogDto>
    {
        public AddBlogDtoValidator()
        {
            RuleFor(x => x.Url).NotEmpty().WithMessage("Url is required");
        }
    }
}
